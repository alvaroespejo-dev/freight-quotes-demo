using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Constants.Get;

public record GetConstantsQuery(IReadOnlyList<long> ConstantTypeIds) : IRequest<GetConstantsResponse>;
