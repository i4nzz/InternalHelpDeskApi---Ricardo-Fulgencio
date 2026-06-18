using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.Services
{
    public class ChamadoPriorityComparer : IComparer<Chamados>
    {
        public int Compare(Chamados? x, Chamados? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;

            // Peso total: categoria vale 10x mais que prioridade
            int pesoX = (x.Categoria?.Peso ?? 0) * 10 + (x.Prioridade?.Peso ?? 0);
            int pesoY = (y.Categoria?.Peso ?? 0) * 10 + (y.Prioridade?.Peso ?? 0);

            // Max Heap: maior peso primeiro
            // Se pesoX > pesoY, retorna -1 (X vem antes)
            // Se pesoX < pesoY, retorna 1 (Y vem antes)
            int comparacaoPeso = pesoY.CompareTo(pesoX);
            if (comparacaoPeso != 0)
            {
                return -comparacaoPeso;
            }

            // Se mesmo peso, mais antigo vem primeiro (FIFO)
            return x.AberturaEm.CompareTo(y.AberturaEm);
        }
    }
}