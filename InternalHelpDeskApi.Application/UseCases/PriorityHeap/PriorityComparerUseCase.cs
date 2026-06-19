using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Services
{
    public class PriorityComparerUseCase : IComparer<Chamados>, IPriorityComparerUseCase
    {
        public int Compare(Chamados? x, Chamados? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;

            int pesoX = (x.Categoria?.Peso ?? 0) * 10 + (x.Prioridade?.Peso ?? 0);
            int pesoY = (y.Categoria?.Peso ?? 0) * 10 + (y.Prioridade?.Peso ?? 0);

            int comparacaoPeso = pesoY.CompareTo(pesoX);
            if (comparacaoPeso != 0)
            {
                return -comparacaoPeso;
            }

            return x.CriadoEm.CompareTo(y.CriadoEm);
        }
    }
}