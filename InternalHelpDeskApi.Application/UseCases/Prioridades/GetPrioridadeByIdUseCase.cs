using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Prioridades
{
    public class GetPrioridadeByIdUseCase : IGetPrioridadeByIdUseCase
    {
        private readonly IPrioridadeRepository _prioridadeRepository;

        public GetPrioridadeByIdUseCase(IPrioridadeRepository prioridadeRepository)
        {
            _prioridadeRepository = prioridadeRepository;
        }

        public async Task<Prioridade> GetById(int id)
        {
            var prioridade = await _prioridadeRepository.GetById(id);
            if (prioridade == null)
            {
                throw new Exception($"Prioridade com ID {id} não encontrada.");
            }
            return prioridade;
        }
    }
}
