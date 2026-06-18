using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetSolicitanteByIdUseCase
    {
        Task<Solicitante> GetById(int id);
    }
}
