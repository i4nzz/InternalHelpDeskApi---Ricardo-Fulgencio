using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Services
{
    public class ChamadoPriorityComparer : IComparer<Chamados>
    {
        public int Compare(Chamados? x, Chamados? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int pesoX = x.Prioridade?.Peso ?? 0;
            int pesoY = y.Prioridade?.Peso ?? 0;

            int comparacaoPeso = pesoY.CompareTo(pesoX);
            if (comparacaoPeso != 0)
            {
                return comparacaoPeso;
            }

            return x.AberturaEm.CompareTo(y.AberturaEm);
        }
    }
}