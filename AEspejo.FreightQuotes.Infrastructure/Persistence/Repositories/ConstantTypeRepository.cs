using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Repositories;

public class ConstantTypeRepository(FreightQuotesDbContext db) : GenericRepository<ConstantType>(db), IConstantTypeRepository
{
}