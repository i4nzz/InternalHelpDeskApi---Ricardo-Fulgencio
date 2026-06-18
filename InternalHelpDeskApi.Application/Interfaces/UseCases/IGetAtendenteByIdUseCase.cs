using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetAtendenteByIdUseCase
    {
        Task<Atendente> GetById(int id);
    }
}
