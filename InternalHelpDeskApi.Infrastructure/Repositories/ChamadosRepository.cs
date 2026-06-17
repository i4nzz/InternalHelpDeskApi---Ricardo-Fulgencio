using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InternalHelpDeskApi.Infrastructure
{
    public class ChamadosRepository : RepositoryBase<ChamadosDtos>, IChamadoRepository
    {
        private readonly DataBase _context;
        public ChamadosRepository(DataBase context) : base(context) 
        {
            _context = context;
        }

        public async Task SoftDeleteAsync(ChamadosDtos chamados)
        {
            chamados.Status = Domain.Enums.StatusChamadoEnum.Cancelado;
            _context.Set<ChamadosDtos>().Remove(chamados);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChamadosDtos>> GetByDesc(string descricao)
        {
            var chamado = await _context.Set<ChamadosDtos>().Where(c => c.Descricao.Contains(descricao)).ToListAsync();
            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }
            return chamado;
        }
        public async Task<List<ChamadosDtos>> GetByCPF(string cpf)
        {
            var chamado = await _context.Set<ChamadosDtos>()
                .Include(c => c.Solicitante)
                .Where(c => c.Solicitante.CPF == cpf)
                .ToListAsync();

            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }
            return chamado;
        }
        public async Task<List<ChamadosDtos>> GetAllOpen()
        {
            return await _context.Set<ChamadosDtos>()
                .Include(c => c.Categoria)
                .ThenInclude(cat => cat.Prioridade)
                .Where(c => c.Status == Domain.Enums.StatusChamadoEnum.Aberto)
                .ToListAsync();
        }
    }
}