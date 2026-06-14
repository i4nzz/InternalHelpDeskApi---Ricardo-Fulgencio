using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Domain.PriorityRules;

public class FilaPrioridadeHeap
{
    private readonly List<Chamado> _heap = new();

    public int Count => _heap.Count;
    public bool IsEmpty => _heap.Count == 0;

    /// <summary>
    /// Insere um chamado na fila de prioridade. O(log n).
    /// Após inserir no final, sobe (HeapifyUp) até posição correta.
    /// </summary>
    public void Inserir(Chamado chamado)
    {
        _heap.Add(chamado);
        HeapifyUp(_heap.Count - 1);
    }

    /// <summary>
    /// Retorna e remove o chamado de maior prioridade (raiz do heap). O(log n).
    /// Move o último elemento para a raiz e desce (HeapifyDown).
    /// </summary>
    public Chamado RemoverMaiorPrioridade()
    {
        if (IsEmpty)
            throw new InvalidOperationException("A fila de prioridade está vazia.");

        var max = _heap[0];

        var ultimo = _heap[^1];
        _heap[0] = ultimo;
        _heap.RemoveAt(_heap.Count - 1);

        if (!IsEmpty)
            HeapifyDown(0);

        return max;
    }

    /// <summary>
    /// Retorna (sem remover) o chamado de maior prioridade. O(1).
    /// </summary>
    public Chamado PegarMaiorPrioridade()
    {
        if (IsEmpty)
            throw new InvalidOperationException("A fila de prioridade está vazia.");

        return _heap[0];
    }

    /// <summary>
    /// Constrói um heap a partir de uma lista existente. O(n).
    /// Útil para reconstruir a fila após reinicialização do sistema.
    /// </summary>
    public void ConstruirDeListaExistente(IEnumerable<Chamado> chamados)
    {
        _heap.Clear();
        _heap.AddRange(chamados);

        // Heapify de baixo para cima a partir do último nó interno
        for (int i = (_heap.Count / 2) - 1; i >= 0; i--)
            HeapifyDown(i);
    }

    /// <summary>
    /// Retorna todos os chamados em ordem de prioridade (maior para menor).
    /// Útil para exibição da fila sem consumir os chamados.
    /// </summary>
    public IReadOnlyList<Chamado> VisualizarEmOrdem()
    {
        // Cria cópia do heap, extrai em ordem
        var copia = new FilaPrioridadeHeap();
        copia.ConstruirDeListaExistente(_heap);

        var resultado = new List<Chamado>();
        while (!copia.IsEmpty)
            resultado.Add(copia.RemoverMaiorPrioridade());

        return resultado;
    }

    /// <summary>
    /// Sobe o elemento na posição index até a posição correta no heap.
    /// Compara com o pai: se maior, troca de lugar.
    /// </summary>
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int pai = (index - 1) / 2;

            if (_heap[index].ScorePrioridade > _heap[pai].ScorePrioridade)
            {
                Trocar(index, pai);
                index = pai;
            }
            else break;
        }
    }

    /// <summary>
    /// Desce o elemento na posição index até a posição correta no heap.
    /// Compara com os filhos: troca pelo maior filho se ele for maior.
    /// </summary>
    private void HeapifyDown(int index)
    {
        int tamanho = _heap.Count;

        while (true)
        {
            int maior = index;
            int filhoEsq = 2 * index + 1;
            int filhoDir = 2 * index + 2;

            if (filhoEsq < tamanho && _heap[filhoEsq].ScorePrioridade > _heap[maior].ScorePrioridade)
                maior = filhoEsq;

            if (filhoDir < tamanho && _heap[filhoDir].ScorePrioridade > _heap[maior].ScorePrioridade)
                maior = filhoDir;

            if (maior == index)
                break;

            Trocar(index, maior);
            index = maior;
        }
    }

    private void Trocar(int i, int j)
    {
        (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
    }
}
