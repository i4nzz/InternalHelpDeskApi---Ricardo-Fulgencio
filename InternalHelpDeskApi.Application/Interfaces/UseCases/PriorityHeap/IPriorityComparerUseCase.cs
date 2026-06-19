using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Application.Interfaces
{
    public interface IPriorityComparerUseCase
    {
        public int Compare(Chamados? x, Chamados? y);
    }
}
