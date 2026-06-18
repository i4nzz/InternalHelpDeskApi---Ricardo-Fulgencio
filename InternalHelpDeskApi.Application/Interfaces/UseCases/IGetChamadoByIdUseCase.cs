using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetChamadoByIdUseCase
    {
        Task<Chamados> GetById(int id);
    }
}
