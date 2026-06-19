using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IGetChamadoByIdUseCase
    {
        Task<Chamados> GetById(int id);
    }
}
