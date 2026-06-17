using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Infrastructure.Persistence;

namespace InternalHelpDeskApi.Infrastructure
{
    public class ChamadosRepository
    {
        private readonly DataBase _context;
        public ChamadosRepository(DataBase context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Chamado chamados)
        {
            chamados.Status = Domain.Enums.StatusChamadoEnum.Cancelado;
            _context.Set<Chamado>().Remove(chamados);
            await _context.SaveChangesAsync();
        }
    }
}
