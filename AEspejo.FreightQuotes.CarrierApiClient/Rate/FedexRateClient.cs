using AEspejo.FreightQuotes.CarrierApiClient.ApiCall;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Constants;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Response;
using AEspejo.FreightQuotes.CarrierApiClient.Extensions;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Constants;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;
using FedexAddress = AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request.Address;

namespace AEspejo.FreightQuotes.CarrierApiClient.Rate
{
    /// <summary>
    /// Real FedEx LTL freight rate client. Resolves credentials from the carrier settings, authenticates,
    /// builds the FedEx rate request from a <see cref="RateQuoteRequest"/> and maps the reply back to
    /// <see cref="RateQuoteResponse"/>. Adapted from the PTMS <c>FedexRateCall</c>.
    /// </summary>
    public class FedexRateClient(IFedexService fedexService)
        : RateApiCallBase, ICarrierRateClient
    {
        private readonly IFedexService _fedexService = fedexService;

        private static readonly HashSet<string> _priorityScac = new(StringComparer.OrdinalIgnoreCase)
        {
            CarrierScacConstant.FXFE,
        };

        public async Task<IReadOnlyList<RateQuoteResponse>> GetQuoteAsync(
            RateQuoteRequest freightQuote, Carrier carrier, CancellationToken ct)
        {
            var credentials = FedexCredentials.FromCarrier(carrier);

            var validationError = CheckValidParameters(freightQuote, carrier);
            if (validationError is not null)
            {
                return [validationError];
            }

            var token = await _fedexService.Token(TokenRequest(credentials), ct);
            if (string.IsNullOrWhiteSpace(token.AccessToken))
            {
                return [ErrorQuote(carrier, token.Messages)];
            }

            var rateRequest = BuildRequest(freightQuote, credentials, carrier.Scac);
            var response = await _fedexService.RateAsync(rateRequest, token.AccessToken, credentials, ct);

            return HandleResponse(response.Data, carrier);
        }

        private RateQuoteResponse? CheckValidParameters(RateQuoteRequest request, Carrier carrier)
        {
            var errors = new List<string>();

            if (request.OriginAddress is null)
            {
                errors.Add("Origin is required");
            }
            if (request.DestinationAddress is null)
            {
                errors.Add("Destination is required");
            }
            if (request.LineItems is null || request.LineItems.Count == 0)
            {
                errors.Add("At least one item is required");
            }

            return errors.Count > 0 ? ErrorQuote(carrier, errors) : null;
        }

        private static FedexTokenRequest TokenRequest(FedexCredentials credentials) => new()
        {
            UrlToken = credentials.TokenUrl,
            ClientId = credentials.ClientId,
            ClientSecret = credentials.ClientSecret,
            ApiCallTimeout = credentials.ApiCallTimeout,
        };

        private FedexRateRequest BuildRequest(RateQuoteRequest request, FedexCredentials credentials, string scac) => new()
        {
            AccountNumber = AccountNumber(credentials.Account),
            RateRequestControlParameters = new RateRequestControlParameters
            {
                ReturnTransitTimes = true,
                ServicesNeededOnRateFailure = true,
            },
            FreightRequestedShipment = FreightRequestedShipment(request, credentials, scac),
        };

        private FreightRequestedShipment FreightRequestedShipment(RateQuoteRequest request, FedexCredentials credentials, string scac)
        {
            var lineItems = LineItems(request);

            return new FreightRequestedShipment
            {
                ServiceType = ServiceType(scac),
                Shipper = Location(request.OriginAddress),
                Recipient = Location(request.DestinationAddress),
                ShippingChargesPayment = ShippingChargesPayment(request, credentials),
                FreightShipmentDetail = FreightShipmentDetail(request, credentials, lineItems),
                RateRequestType = [FedexApiConstants.RateRequestTypeAccount],
                RequestedPackageLineItems = RequestedPackageLineItems(lineItems),
                ShipDateStamp = request.ShipDate.ToString("yyyy-MM-dd"),
                TotalWeight = (long)(request.LineItems?.Sum(i => i.Weight) ?? 0),
                FreightShipmentSpecialServices = SpecialServices(request),
            };
        }

        private static string ServiceType(string scac)
            => _priorityScac.Contains(scac)
                ? FedexApiConstants.ServiceTypePriority
                : FedexApiConstants.ServiceTypeEconomy;

        private static FedexLocation Location(RateQuoteAddress? address)
        {
            var location = new FedexLocation();
            if (address is not null)
            {
                location.Address = Address(address);
            }

            return location;
        }

        private static FedexAddress Address(RateQuoteAddress address) => new()
        {
            StreetLines = StreetLines(address),
            City = address.City ?? string.Empty,
            StateOrProvinceCode = address.StateCode.TruncateOrEmpty(2),
            PostalCode = address.Zip.TruncateOrEmpty(10),
            CountryCode = address.CountryCode.TruncateOrEmpty(2),
            Residential = HasAccessorial(address.Accessorials, AccessorialCodeConstant.Residential),
        };

        private static BillingAddress BillingAddress(RateQuoteAddress address) => new()
        {
            StreetLines = StreetLines(address),
            City = address.City ?? string.Empty,
            StateOrProvinceCode = address.StateCode.TruncateOrEmpty(2),
            PostalCode = address.Zip.TruncateOrEmpty(10),
            CountryCode = address.CountryCode.TruncateOrEmpty(2),
        };

        private static string[] StreetLines(RateQuoteAddress address)
        {
            var lines = new List<string>();
            if (!string.IsNullOrWhiteSpace(address.Address1))
            {
                lines.Add(address.Address1.TruncateOrEmpty(35));
            }
            if (!string.IsNullOrWhiteSpace(address.Address2))
            {
                lines.Add(address.Address2.TruncateOrEmpty(35));
            }

            return [.. lines];
        }

        private static ShippingChargesPayment ShippingChargesPayment(RateQuoteRequest request, FedexCredentials credentials) => new()
        {
            PaymentType = PaymentTypeByTerms(request.TermsId),
            Payor = new Payor
            {
                ResponsibleParty = new ResponsibleParty
                {
                    AccountNumber = AccountNumber(credentials.AccountSecundary),
                },
            },
        };

        private static string PaymentTypeByTerms(long? termsId) => termsId switch
        {
            TermsConstant.Prepaid or TermsConstant.ThirdParty => FedexApiConstants.PaymentTypeSender,
            TermsConstant.Collect => FedexApiConstants.PaymentTypeRecipient,
            _ => string.Empty,
        };

        private static FreightShipmentDetail FreightShipmentDetail(RateQuoteRequest request, FedexCredentials credentials, LineItem[] lineItems) => new()
        {
            Role = Role(request.RoleId),
            AlternateBillingParty = AlternateBillingParty(request, credentials),
            LineItem = lineItems,
        };

        private static string Role(long? roleId) => roleId switch
        {
            RoleConstant.Shipper or RoleConstant.ThirdParty => FedexApiConstants.RoleShipper,
            RoleConstant.Consignee => FedexApiConstants.RoleConsignee,
            _ => string.Empty,
        };

        private static AlternateBillingParty AlternateBillingParty(RateQuoteRequest request, FedexCredentials credentials) => new()
        {
            AccountNumber = AccountNumber(credentials.AccountSecundary),
            Address = SelectAlternativeBillingAddress(request),
        };

        private static BillingAddress? SelectAlternativeBillingAddress(RateQuoteRequest request)
        {
            var address = request.RoleId switch
            {
                RoleConstant.Shipper => request.DestinationAddress,
                RoleConstant.Consignee => request.OriginAddress,
                _ => request.BillingAddress ?? request.OriginAddress,
            };

            return address is null ? null : BillingAddress(address);
        }

        private static FreightShipmentSpecialServices? SpecialServices(RateQuoteRequest request)
        {
            // Only forward accessorials whose code matches a known FedEx special-service type.
            var types = request.Accessorials
                .Select(a => a.Code)
                .Where(code => Enum.TryParse<SpecialServiceTypes>(code, ignoreCase: true, out _))
                .Select(code => code.ToUpperInvariant())
                .Distinct()
                .ToArray();

            return types.Length == 0 ? null : new FreightShipmentSpecialServices { SpecialServiceTypes = types };
        }

        private static LineItem[] LineItems(RateQuoteRequest request)
        {
            if (request.LineItems is null || request.LineItems.Count == 0)
            {
                return [];
            }

            var items = new List<LineItem>();
            var itemCount = 1;
            foreach (var item in request.LineItems)
            {
                items.Add(LineItem(item, itemCount));
                itemCount++;
            }

            return [.. items];
        }

        private static LineItem LineItem(RateQuoteLineItem item, int itemCount)
        {
            var lineItem = new LineItem
            {
                FreightClass = FedexMappings.FreightClass(item.FreightClass),
                HandlingUnits = item.ShipQty,
                Pieces = item.Qty,
                NmfcCode = FormatNmfcCode(item.Nmfc, item.SubClassId?.ToString()),
                SubPackagingType = FedexMappings.SubPackagingType(item.ShippingUnitCode),
                Description = item.Description,
                Weight = new RateWeight { Units = FedexApiConstants.WeightUnitsLb, Value = item.Weight },
                Id = itemCount.ToString(),
                HazardousMaterials = item.IsHazMat ? FedexApiConstants.HazardousMaterials : null,
            };

            if (item.Length > 0 && item.Width > 0 && item.Height > 0)
            {
                lineItem.Dimensions = new RateDimensions
                {
                    Length = (long)item.Length,
                    Width = (long)item.Width,
                    Height = (long)item.Height,
                    Units = FedexApiConstants.DimensionUnitsIn,
                };
            }

            return lineItem;
        }

        private static RequestedPackageLineItem[] RequestedPackageLineItems(LineItem[] lineItems)
            => [.. lineItems.Select(li => new RequestedPackageLineItem
            {
                AssociatedFreightLineItems = [new AssociatedFreightLineItem { Id = li.Id }],
                Weight = new RateWeight { Units = li.Weight.Units, Value = li.Weight.Value },
                SubPackagingType = li.SubPackagingType,
                Dimensions = li.Dimensions is null ? null : new RateDimensions
                {
                    Length = li.Dimensions.Length,
                    Width = li.Dimensions.Width,
                    Height = li.Dimensions.Height,
                    Units = li.Dimensions.Units,
                },
            })];

        private static AccountNumber AccountNumber(string? accountNumber) => new()
        {
            Value = (accountNumber ?? string.Empty).TruncateOrEmpty(9),
        };

        private static string? FormatNmfcCode(string? nmfcNumber, string? subClassDescription)
        {
            if (string.IsNullOrWhiteSpace(nmfcNumber))
            {
                return null;
            }

            var mainNmfcNumber = nmfcNumber;
            var mainSubClass = subClassDescription;
            var parts = nmfcNumber.Split('-');

            if (parts.Length > 0)
            {
                mainNmfcNumber = parts[0];
            }
            if (string.IsNullOrWhiteSpace(mainSubClass) && parts.Length > 1)
            {
                mainSubClass = parts[1];
            }

            var nmfcDigits = ExtractDigits(mainNmfcNumber);
            if (string.IsNullOrWhiteSpace(nmfcDigits))
            {
                return null;
            }

            nmfcDigits = nmfcDigits.PadLeft(6, '0');
            var subClassDigits = ExtractDigits(mainSubClass).PadLeft(2, '0');

            return $"{nmfcDigits}-{subClassDigits}";
        }

        private static string ExtractDigits(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return new string([.. value.Where(char.IsDigit)]);
        }

        private List<RateQuoteResponse> HandleResponse(FedexRateResponse? response, Carrier carrier)
        {
            const string noQuotesMessage = "Fedex Rater: No quotes were found";
            var quotes = new List<RateQuoteResponse>();

            if (response is null)
            {
                quotes.Add(ErrorQuote(carrier, noQuotesMessage));
                return quotes;
            }

            if (response.Errors is { Length: > 0 })
            {
                var messages = response.Errors
                    .Select(e => !string.IsNullOrWhiteSpace(e.Message) ? e.Message : e.Code)
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .ToArray();

                quotes.Add(ErrorQuote(carrier, messages.Length == 0 ? [noQuotesMessage] : messages));
                return quotes;
            }

            if (response.Output?.RateReplyDetails is null)
            {
                quotes.Add(ErrorQuote(carrier, noQuotesMessage));
                return quotes;
            }

            foreach (var detail in response.Output.RateReplyDetails)
            {
                if (detail.RatedShipmentDetails is null)
                {
                    continue;
                }

                var accountRates = detail.RatedShipmentDetails
                    .Where(r => string.Equals(r.RateType, FedexApiConstants.RateRequestTypeAccount, StringComparison.OrdinalIgnoreCase));

                foreach (var rated in accountRates)
                {
                    quotes.Add(MapQuote(carrier, detail, rated));
                }
            }

            if (quotes.Count == 0)
            {
                quotes.Add(ErrorQuote(carrier, noQuotesMessage));
            }

            return quotes;
        }

        private static RateQuoteResponse MapQuote(Carrier carrier, RateReplyDetail detail, RatedShipmentDetail rated)
        {
            var accessorials = AccessorialsResponse(rated);
            var accessorialCharge = accessorials.Sum(a => a.Cost);
            var totalCharge = rated.TotalNetCharge.GetValueOrDefault();

            return new RateQuoteResponse
            {
                CarrierId = carrier.Id,
                CarrierName = carrier.Name,
                QuoteNumber = rated.QuoteNumber ?? string.Empty,
                ServiceLevel = string.IsNullOrWhiteSpace(detail.ServiceName) ? detail.ServiceType : detail.ServiceName,
                TotalCharge = totalCharge,
                AccessorialCharge = accessorialCharge,
                BaseCharge = totalCharge - accessorialCharge,
                TransitDays = FedexMappings.TransitDaysFromLiteral(detail.Commit?.TransitDays?.MinimumTransitTime),
                Accessorial = accessorials,
            };
        }

        private static List<RateAccessorialResponse> AccessorialsResponse(RatedShipmentDetail rated)
        {
            var surcharges = rated.ShipmentRateDetail?.SurCharges;
            if (surcharges is null)
            {
                return [];
            }

            return [.. surcharges
                .Where(s => !string.Equals(s.Type, FedexApiConstants.SurchargeTypeFuel, StringComparison.OrdinalIgnoreCase))
                .Select(s => new RateAccessorialResponse
                {
                    Name = string.IsNullOrWhiteSpace(s.Description) ? s.Type : s.Description,
                    Cost = s.Amount.GetValueOrDefault(),
                })];
        }
    }
}
