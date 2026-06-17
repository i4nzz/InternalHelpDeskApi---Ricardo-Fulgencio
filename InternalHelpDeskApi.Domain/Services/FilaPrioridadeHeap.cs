namespace InternalHelpDeskApi.Infrastructure.Structures
{
    public class FilaPrioridadeHeap<T>
    {
        private readonly List<T> _heap = new();
        private readonly IComparer<T> _comparer;

        public FilaPrioridadeHeap(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public int Contagem => _heap.Count;
        public bool EstaVazia => _heap.Count == 0;

        public void Enfileirar(T item)
        {
            _heap.Add(item);
            Subir(_heap.Count - 1);
        }

        public T Desenfileirar()
        {
            if (EstaVazia) throw new InvalidOperationException("Fila vazia.");

            T topo = _heap[0];
            int ultimoIndice = _heap.Count - 1;

            _heap[0] = _heap[ultimoIndice];
            _heap.RemoveAt(ultimoIndice);

            if (_heap.Count > 0) Descer(0);

            return topo;
        }

        private void Subir(int indice)
        {
            while (indice > 0)
            {
                int pai = (indice - 1) / 2;
                if (_comparer.Compare(_heap[indice], _heap[pai]) <= 0) break;

                Trocar(indice, pai);
                indice = pai;
            }
        }

        private void Descer(int indice)
        {
            int tamanho = _heap.Count;
            while (indice < tamanho)
            {
                int esquerda = 2 * indice + 1;
                int direita = 2 * indice + 2;
                int maior = indice;

                if (esquerda < tamanho && _comparer.Compare(_heap[esquerda], _heap[maior]) > 0)
                    maior = esquerda;

                if (direita < tamanho && _comparer.Compare(_heap[direita], _heap[maior]) > 0)
                    maior = direita;

                if (maior == indice) break;

                Trocar(indice, maior);
                indice = maior;
            }
        }

        private void Trocar(int i, int j)
        {
            T temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }
    }
}