using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetPrioridadeByIdUseCase
    {
        Task<Prioridade> GetById(int id);
    }
}
