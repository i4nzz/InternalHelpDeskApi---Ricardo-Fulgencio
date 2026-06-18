using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Infrastructure.Persistence;

namespace InternalHelpDeskApi.Infrastructure.Repositories
{
    public class PrioridadeRepository : RepositoryBase<Prioridade>, IPrioridadeRepository
    {
        public PrioridadeRepository(HelpDeskContext context) : base(context)
        {
        }
    }
}
