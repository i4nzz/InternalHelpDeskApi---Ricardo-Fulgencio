using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetPrioridadeByIdUseCase
    {
        Task<Prioridade> GetById(int id);
    }
}
