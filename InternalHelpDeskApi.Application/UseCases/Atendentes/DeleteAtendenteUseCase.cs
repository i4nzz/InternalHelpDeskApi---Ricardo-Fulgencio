using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Domain.Interfaces;

namespace InternalHelpDeskApi.Application.UseCases.Atendentes
{
    public class DeleteAtendenteUseCase : IDeleteAtendenteUseCase
    {
        private readonly IAtendenteRepository _atendenteRepository;

        public DeleteAtendenteUseCase(IAtendenteRepository atendenteRepository)
        {
            _atendenteRepository = atendenteRepository;
        }

        public async Task DeleteAtendente(int id)
        {
            var atendente = await _atendenteRepository.GetById(id);
            if (atendente == null)
            {
                throw new Exception($"Atendente com ID {id} não encontrado.");
            }

            // Implementar soft delete ou hard delete conforme necessário
            throw new NotImplementedException("Deletar atendente requer implementação adicional.");
        }
    }
}
