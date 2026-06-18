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

            int pesoX = (x.Categoria?.Peso ?? 0) + (x.Categoria?.Prioridade?.Peso ?? 0);
            int pesoY = (y.Categoria?.Peso ?? 0) + (y.Categoria?.Prioridade?.Peso ?? 0);

            int comparacaoPeso = pesoX.CompareTo(pesoY);
            if (comparacaoPeso != 0)
            {
                return comparacaoPeso;
            }

            return y.AberturaEm.CompareTo(x.AberturaEm);
        }
    }
}