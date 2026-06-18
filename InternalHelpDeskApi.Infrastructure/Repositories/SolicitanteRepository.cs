using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Infrastructure.Persistence;

namespace InternalHelpDeskApi.Infrastructure.Repositories
{
    public class SolicitanteRepository : RepositoryBase<Solicitante>, ISolicitanteRepository
    {
        public SolicitanteRepository(HelpDeskContext context) : base(context)
        {
        }
    }
}
