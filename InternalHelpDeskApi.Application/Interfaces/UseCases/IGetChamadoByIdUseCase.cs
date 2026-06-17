using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface IGetChamadoByIdUseCase
    {
        Task<ChamadosDtos> GetById(int id);
    }
}
