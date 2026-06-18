using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Infrastructure.Persistence;

namespace InternalHelpDeskApi.Infrastructure.Repositories
{
    public class AtendenteRepository : RepositoryBase<Atendente>, IAtendenteRepository
    {
        private readonly HelpDeskContext _context;
        public AtendenteRepository(HelpDeskContext context) : base(context)
        {
            _context = context;
        }
    }
}
