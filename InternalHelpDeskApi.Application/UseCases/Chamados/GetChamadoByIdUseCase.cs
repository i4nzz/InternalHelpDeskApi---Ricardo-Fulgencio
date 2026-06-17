using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Chamados
{
    public class GetChamadoByIdUseCase : IGetChamadoByIdUseCase
    {
        private readonly IChamadoRepository _chamadoRepository;
        public GetChamadoByIdUseCase(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }
        public async Task<Chamado> GetById(int id)
        {
            var chamado = await _chamadoRepository.GetById(id);
            if (chamado == null)
            {
                throw new Exception($"Chamado com ID {id} não encontrado.");
            }
            return chamado;
        }
    }
}
