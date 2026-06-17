using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetChamadoByIdUseCase
    {
        Task<Chamado> GetById(int id);
    }
}
