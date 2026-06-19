namespace InternalHelpDeskApi.Application.Interfaces.UseCases.PriorityHeap
{
    public interface IFilaPrioridadeHeapUseCase<T>
    {
        int Contagem { get; }
        bool EstaVazia { get; }
        void Enfileirar(T item);
        T Desenfileirar();
    }
}
