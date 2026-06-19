namespace InternalHelpDeskApi.Application.Interfaces.UseCases.PriorityHeap
{
    public interface IFilaPrioridadeHeapUseCase<T>
    {
        public void Enfileirar(T item);
        public T Desenfileirar();
    }
}
