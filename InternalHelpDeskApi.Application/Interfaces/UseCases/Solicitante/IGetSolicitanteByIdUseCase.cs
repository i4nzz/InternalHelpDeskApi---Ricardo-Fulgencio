using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetSolicitanteByIdUseCase
    {
        Task<Solicitante> GetById(int id);
    }
}
