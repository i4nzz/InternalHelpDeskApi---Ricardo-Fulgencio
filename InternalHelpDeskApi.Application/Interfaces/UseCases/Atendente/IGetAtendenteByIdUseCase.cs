using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetAtendenteByIdUseCase
    {
        Task<Atendente> GetById(int id);
    }
}
