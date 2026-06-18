using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Tests.Helpers
{
    public static class ChamadoFactory
    {
        public static Chamados CriarChamado(
            int id = 1,
            string titulo = "Servidor principal caiu",
            string descricao = "Problema crítico afetando toda a empresa.",
            int categoriaId = 1,
            int prioridadeId = 1,
            int solicitanteId = 1,
            StatusEnum status = StatusEnum.Aberto)
        {
            return new Chamados
            {
                Id = id,
                Titulo = titulo,
                Descricao = descricao,
                CategoriaId = categoriaId,
                Categoria = new Categoria
                {
                    Id = categoriaId,
                    Nome = "Infraestrutura",
                    Peso = 3
                },
                PrioridadeId = prioridadeId,
                Prioridade = new Prioridade
                {
                    Id = prioridadeId,
                    Descricao = "Alta",
                    Peso = 10
                },
                SolicitanteId = solicitanteId,
                Status = status,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };
        }

        public static ChamadosDto CriarChamadoDto(
            string titulo = "Servidor principal caiu",
            string descricao = "Problema crítico afetando toda a empresa.",
            int categoriaId = 1,
            int prioridadeId = 1,
            int solicitanteId = 1,
            StatusEnum status = StatusEnum.Aberto)
        {
            return new ChamadosDto
            {
                Titulo = titulo,
                Descricao = descricao,
                CategoriaID = categoriaId,
                PrioridadeId = prioridadeId,
                SolicitanteID = solicitanteId,
                Status = status
            };
        }
    }
}