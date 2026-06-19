using InternalHelpDeskApi.Domain.Entities;
using InternalHelpDeskApi.Domain.Services;
using InternalHelpDeskApi.Infrastructure.Structures;

namespace InternalHelpDeskApi.Tests.Domain.Services
{
    public class FilaPrioridadeHeapTests
    {
        [Fact]
        public void Enfileirar_DeveAdicionarChamadoNaFila()
        {
            var fila = new FilaPrioridadeHeapUseCase<Chamados>(new PriorityComparerUseCase());

            var chamado = CriarChamado(
                id: 1,
                titulo: "Servidor caiu",
                categoriaPeso: 3,
                prioridadePeso: 10,
                criadoEm: new DateTime(2026, 06, 18, 8, 0, 0)
            );

            fila.Enfileirar(chamado);

            Assert.False(fila.EstaVazia);
            Assert.Equal(1, fila.Contagem);
        }

        [Fact]
        public void Desenfileirar_DeveRetornarChamadoComMaiorPrioridadePrimeiro()
        {
            var fila = new FilaPrioridadeHeapUseCase<Chamados>(new PriorityComparerUseCase());

            var chamadoBaixaPrioridade = CriarChamado(
                id: 1,
                titulo: "Troca de mouse",
                categoriaPeso: 1,
                prioridadePeso: 1,
                criadoEm: new DateTime(2026, 06, 18, 8, 0, 0)
            );

            var chamadoAltaPrioridade = CriarChamado(
                id: 2,
                titulo: "Servidor principal caiu",
                categoriaPeso: 3,
                prioridadePeso: 10,
                criadoEm: new DateTime(2026, 06, 18, 9, 0, 0)
            );

            fila.Enfileirar(chamadoBaixaPrioridade);
            fila.Enfileirar(chamadoAltaPrioridade);

            var primeiroChamado = fila.Desenfileirar();

            Assert.Equal(chamadoAltaPrioridade.Id, primeiroChamado.Id);
            Assert.Equal("Servidor principal caiu", primeiroChamado.Titulo);
            Assert.Equal(1, fila.Contagem);
        }

        [Fact]
        public void Desenfileirar_DeveAplicarDesempatePorData_QuandoChamadosTiveremMesmaPrioridade()
        {
            var fila = new FilaPrioridadeHeapUseCase<Chamados>(new PriorityComparerUseCase());

            var chamadoMaisAntigo = CriarChamado(
                id: 1,
                titulo: "Chamado mais antigo",
                categoriaPeso: 2,
                prioridadePeso: 5,
                criadoEm: new DateTime(2026, 06, 18, 8, 0, 0)
            );

            var chamadoMaisNovo = CriarChamado(
                id: 2,
                titulo: "Chamado mais novo",
                categoriaPeso: 2,
                prioridadePeso: 5,
                criadoEm: new DateTime(2026, 06, 18, 10, 0, 0)
            );

            fila.Enfileirar(chamadoMaisAntigo);
            fila.Enfileirar(chamadoMaisNovo);

            var primeiroChamado = fila.Desenfileirar();

            Assert.Equal(chamadoMaisNovo.Id, primeiroChamado.Id);
            Assert.Equal("Chamado mais novo", primeiroChamado.Titulo);
        }

        [Fact]
        public void Desenfileirar_DeveLancarExcecao_QuandoFilaEstiverVazia()
        {
            var fila = new FilaPrioridadeHeapUseCase<Chamados>(new PriorityComparerUseCase());

            var exception = Assert.Throws<InvalidOperationException>(() => fila.Desenfileirar());

            Assert.Equal("Fila vazia.", exception.Message);
        }

        [Fact]
        public void EstaVazia_DeveRetornarTrue_QuandoNaoExistiremChamados()
        {
            var fila = new FilaPrioridadeHeapUseCase<Chamados>(new PriorityComparerUseCase());

            Assert.True(fila.EstaVazia);
            Assert.Equal(0, fila.Contagem);
        }

        private static Chamados CriarChamado(
            int id,
            string titulo,
            int categoriaPeso,
            int prioridadePeso,
            DateTime criadoEm)
        {
            return new Chamados
            {
                Id = id,
                Titulo = titulo,
                Descricao = $"Descrição do chamado {id}",
                CategoriaId = id,
                Categoria = new Categoria
                {
                    Id = id,
                    Nome = $"Categoria {id}",
                    Peso = categoriaPeso
                },
                PrioridadeId = id,
                Prioridade = new Prioridade
                {
                    Id = id,
                    Descricao = $"Prioridade {id}",
                    Peso = prioridadePeso
                },
                CriadoEm = criadoEm,
                AtualizadoEm = criadoEm
            };
        }
    }
}