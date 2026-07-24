using System.Globalization;
using AEspejo.FreightQuotes.CarrierApiClient.ApiCall;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Constants;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Response;
using AEspejo.FreightQuotes.CarrierApiClient.Extensions;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;

namespace AEspejo.FreightQuotes.CarrierApiClient.Rate
{
    /// <summary>
    /// Real UPS small-package rate client. Resolves credentials from the carrier settings, authenticates
    /// (OAuth client-credentials over HTTP Basic), builds the UPS Rating request from a
    /// <see cref="RateQuoteRequest"/> and maps the reply back to <see cref="RateQuoteResponse"/>.
    /// Mirrors <see cref="FedexRateClient"/>.
    /// </summary>
    public class UpsRateClient(IUpsService upsService)
        : RateApiCallBase, ICarrierRateClient
    {
        private readonly IUpsService _upsService = upsService;

        public async Task<IReadOnlyList<RateQuoteResponse>> GetQuoteAsync(
            RateQuoteRequest freightQuote, Carrier carrier, CancellationToken ct)
        {
            var credentials = UpsCredentials.FromCarrier(carrier);

            var validationError = CheckValidParameters(freightQuote, carrier);
            if (validationError is not null)
            {
                return [validationError];
            }

            var token = await _upsService.Token(TokenRequest(credentials), ct);
            if (string.IsNullOrWhiteSpace(token.AccessToken))
            {
                return [ErrorQuote(carrier, token.Messages)];
            }

            var rateRequest = BuildRequest(freightQuote, credentials, carrier);
            var response = await _upsService.RateAsync(rateRequest, token.AccessToken, credentials, ct);

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

        private static UpsTokenRequest TokenRequest(UpsCredentials credentials) => new()
        {
            UrlToken = credentials.TokenUrl,
            ClientId = credentials.ClientId,
            ClientSecret = credentials.ClientSecret,
            MerchantId = string.IsNullOrWhiteSpace(credentials.Account) ? null : credentials.Account,
            ApiCallTimeout = credentials.ApiCallTimeout,
        };

        private static UpsRateRoot BuildRequest(RateQuoteRequest request, UpsCredentials credentials, Carrier carrier)
        {
            var packages = Packages(request);

            return new UpsRateRoot
            {
                RateRequest = new UpsRateRequest
                {
                    Request = new UpsRequest
                    {
                        TransactionReference = new UpsTransactionReference { CustomerContext = request.RequestId },
                    },
                    Shipment = new UpsShipment
                    {
                        Shipper = new UpsShipper
                        {
                            Name = string.IsNullOrWhiteSpace(carrier.Name) ? "Shipper" : carrier.Name,
                            ShipperNumber = credentials.Account,
                            Address = Address(request.OriginAddress),
                        },
                        ShipFrom = new UpsAddressParty
                        {
                            Name = PartyName(request.OriginAddress, carrier.Name, "Shipper"),
                            Address = Address(request.OriginAddress),
                        },
                        ShipTo = new UpsAddressParty
                        {
                            Name = PartyName(request.DestinationAddress, null, "Recipient"),
                            Address = Address(request.DestinationAddress),
                        },
                        PaymentDetails = PaymentDetails(credentials),
                        Service = new UpsCodeDescription { Code = UpsApiConstants.ServiceGround, Description = "Ground" },
                        NumOfPieces = packages.Count.ToString(CultureInfo.InvariantCulture),
                        Package = packages,
                    },
                },
            };
        }

        private static string PartyName(RateQuoteAddress? address, string? fallback, string defaultName)
        {
            if (!string.IsNullOrWhiteSpace(address?.Name))
            {
                return address!.Name!;
            }

            return string.IsNullOrWhiteSpace(fallback) ? defaultName : fallback!;
        }

        private static UpsAddress Address(RateQuoteAddress? address)
        {
            var result = new UpsAddress();
            if (address is null)
            {
                return result;
            }

            if (!string.IsNullOrWhiteSpace(address.Address1))
            {
                result.AddressLine.Add(address.Address1.TruncateOrEmpty(35));
            }
            if (!string.IsNullOrWhiteSpace(address.Address2))
            {
                result.AddressLine.Add(address.Address2.TruncateOrEmpty(35));
            }

            result.City = address.City;
            result.StateProvinceCode = address.StateCode.TruncateOrEmpty(5);
            result.PostalCode = address.Zip.TruncateOrEmpty(10);
            result.CountryCode = address.CountryCode.TruncateOrEmpty(2);

            return result;
        }

        private static UpsPaymentDetails PaymentDetails(UpsCredentials credentials) => new()
        {
            ShipmentCharge =
            [
                new UpsShipmentCharge
                {
                    Type = UpsApiConstants.PaymentTypeBillShipper,
                    BillShipper = new UpsBillShipper { AccountNumber = credentials.Account },
                },
            ],
        };

        private static List<UpsPackage> Packages(RateQuoteRequest request)
        {
            if (request.LineItems is null || request.LineItems.Count == 0)
            {
                return [];
            }

            return [.. request.LineItems.Select(Package)];
        }

        private static UpsPackage Package(RateQuoteLineItem item)
        {
            var package = new UpsPackage
            {
                PackagingType = new UpsCodeDescription
                {
                    Code = UpsApiConstants.PackagingTypeCustomer,
                    Description = "Packaging",
                },
                PackageWeight = new UpsPackageWeight
                {
                    UnitOfMeasurement = new UpsCodeDescription
                    {
                        Code = UpsApiConstants.UnitPounds,
                        Description = UpsApiConstants.UnitPoundsDescription,
                    },
                    Weight = Number(item.Weight),
                },
            };

            if (item.Length > 0 && item.Width > 0 && item.Height > 0)
            {
                package.Dimensions = new UpsDimensions
                {
                    UnitOfMeasurement = new UpsCodeDescription
                    {
                        Code = UpsApiConstants.UnitInches,
                        Description = UpsApiConstants.UnitInchesDescription,
                    },
                    Length = Number(item.Length),
                    Width = Number(item.Width),
                    Height = Number(item.Height),
                };
            }

            return package;
        }

        private static string Number(decimal value)
            => value.ToString("0.##", CultureInfo.InvariantCulture);

        private List<RateQuoteResponse> HandleResponse(UpsRateResponseRoot? root, Carrier carrier)
        {
            const string noQuotesMessage = "UPS Rater: No quotes were found";
            var quotes = new List<RateQuoteResponse>();

            if (root?.ErrorEnvelope?.Errors is { Length: > 0 } errors)
            {
                var messages = errors
                    .Select(e => !string.IsNullOrWhiteSpace(e.Message) ? e.Message : e.Code)
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .ToArray();

                quotes.Add(ErrorQuote(carrier, messages.Length == 0 ? [noQuotesMessage] : messages));
                return quotes;
            }

            var ratedShipments = root?.RateResponse?.RatedShipment;
            if (ratedShipments is null || ratedShipments.Length == 0)
            {
                quotes.Add(ErrorQuote(carrier, noQuotesMessage));
                return quotes;
            }

            foreach (var rated in ratedShipments)
            {
                quotes.Add(MapQuote(carrier, rated));
            }

            return quotes;
        }

        private static RateQuoteResponse MapQuote(Carrier carrier, UpsRatedShipment rated)
        {
            var accessorials = AccessorialsResponse(rated);
            var accessorialCharge = UpsMappings.ParseMoney(rated.ServiceOptionsCharges?.MonetaryValue);

            var negotiatedTotal = rated.NegotiatedRateCharges?.TotalCharge?.MonetaryValue;
            var totalCharge = !string.IsNullOrWhiteSpace(negotiatedTotal)
                ? UpsMappings.ParseMoney(negotiatedTotal)
                : UpsMappings.ParseMoney(rated.TotalCharges?.MonetaryValue);

            var baseCharge = UpsMappings.ParseMoney(rated.TransportationCharges?.MonetaryValue);
            if (baseCharge == 0m)
            {
                baseCharge = totalCharge - accessorialCharge;
            }

            return new RateQuoteResponse
            {
                CarrierId = carrier.Id,
                CarrierName = carrier.Name,
                ServiceLevel = UpsMappings.ServiceName(rated.Service?.Code),
                TotalCharge = totalCharge,
                AccessorialCharge = accessorialCharge,
                BaseCharge = baseCharge,
                TransitDays = TransitDays(rated),
                Accessorial = accessorials,
            };
        }

        private static int TransitDays(UpsRatedShipment rated)
        {
            var guaranteed = rated.GuaranteedDelivery?.BusinessDaysInTransit;
            if (!string.IsNullOrWhiteSpace(guaranteed))
            {
                return UpsMappings.ParseDays(guaranteed);
            }

            return UpsMappings.ParseDays(rated.TimeInTransit?.ServiceSummary?.EstimatedArrival?.BusinessDaysInTransit);
        }

        private static List<RateAccessorialResponse> AccessorialsResponse(UpsRatedShipment rated)
        {
            var itemized = rated.NegotiatedRateCharges?.ItemizedCharges ?? rated.ItemizedCharges;
            if (itemized is null)
            {
                return [];
            }

            return [.. itemized
                .Where(c => !string.Equals(c.Code, UpsApiConstants.ItemizedFuelSurchargeCode, StringComparison.OrdinalIgnoreCase))
                .Select(c => new RateAccessorialResponse
                {
                    Name = string.IsNullOrWhiteSpace(c.Description) ? c.Code ?? string.Empty : c.Description,
                    Cost = UpsMappings.ParseMoney(c.MonetaryValue),
                })];
        }
    }
}
