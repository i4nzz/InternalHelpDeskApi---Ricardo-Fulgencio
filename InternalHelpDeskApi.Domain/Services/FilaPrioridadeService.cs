using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.PriorityRules;

namespace InternalHelpDeskApi.Domain.Services;

public class FilaPrioridadeService
{
    private readonly FilaPrioridadeHeap _heap = new();

    public int TotalNaFila => _heap.Count;

    public void Enqueue(Chamado chamado) => _heap.Inserir(chamado);

    public Chamado Dequeue() => _heap.RemoverMaiorPrioridade();

    public Chamado Peek() => _heap.PegarMaiorPrioridade();

    public bool FilaVazia() => _heap.IsEmpty;

    public IReadOnlyList<Chamado> ObterFilaOrdenada() => _heap.VisualizarEmOrdem();

    /// <summary>
    /// Reconstrói o heap a partir dos chamados abertos persistidos no banco.
    /// Deve ser chamado no startup da aplicação.
    /// </summary>
    public void ReconstruirDeListaExistente(IEnumerable<Chamado> chamados)
    {
        _heap.ConstruirDeListaExistente(chamados);
    }
}
