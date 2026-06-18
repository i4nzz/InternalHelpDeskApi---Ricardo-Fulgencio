using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;
using InternalHelpDeskApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InternalHelpDeskApi.Infrastructure
{
    public class ChamadosRepository : RepositoryBase<Chamados>, IChamadoRepository
    {
        private readonly DataBase _context;
        public ChamadosRepository(DataBase context) : base(context) 
        {
            _context = context;
        }

        public async Task SoftDeleteAsync(Chamados chamados)
        {
            chamados.Status = Domain.Enums.StatusChamadoEnum.Cancelado;
            _context.Set<Chamados>().Remove(chamados);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Chamados>> GetByDesc(string descricao)
        {
            var chamado = await _context.Set<Chamados>().Where(c => c.Descricao.Contains(descricao)).ToListAsync();
            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }
            return chamado;
        }
        public async Task<List<Chamados>> GetByCPF(string cpf)
        {
            var chamado = await _context.Set<Chamados>()
                .Include(c => c.Solicitante)
                .Where(c => c.Solicitante.CPF == cpf)
                .ToListAsync();

            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }
            return chamado;
        }
        public async Task<List<Chamados>> GetAllOpen()
        {
            return await _context.Set<Chamados>()
                .Include(c => c.Categoria)
                .ThenInclude(cat => cat.Prioridade)
                .Where(c => c.Status == Domain.Enums.StatusChamadoEnum.Aberto)
                .ToListAsync();
        }
    }
}