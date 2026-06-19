using InternalHelpDeskApi.Application.Interfaces.UseCases.PriorityHeap;
using InternalHelpDeskApi.Application.UseCases.PriorityHeap;
using InternalHelpDeskApi.Domain.Entities;

namespace InternalHelpDeskApi.Tests.Domain.Services
{
    public class ChamadoPriorityComparerTests
    {
        [Fact]
        public void Compare_DeveSelecionarPrimeiroChamadoComMaiorPrioridade()
        {
            var chamadoMaiorPrioridade = CriarChamado(
                id: 1,
                titulo: "Servidor principal caiu",
                categoriaPeso: 3,
                prioridadePeso: 10,
                criadoEm: new DateTime(2026, 06, 18, 8, 0, 0)
            );

            var chamadoMenorPrioridade = CriarChamado(
                id: 2,
                titulo: "Troca de mouse",
                categoriaPeso: 1,
                prioridadePeso: 1,
                criadoEm: new DateTime(2026, 06, 18, 8, 0, 0)
            );

            IPriorityComparerUseCase comparer = new PriorityComparerUseCase();

            var resultado = comparer.Compare(chamadoMaiorPrioridade, chamadoMenorPrioridade);

            Assert.True(resultado > 0);
        }

        [Fact]
        public void Compare_DeveAplicarDesempatePorData_QuandoChamadosTiveremMesmaPrioridade()
        {
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

            IPriorityComparerUseCase comparer = new PriorityComparerUseCase();

            var resultado = comparer.Compare(chamadoMaisNovo, chamadoMaisAntigo);

            Assert.True(resultado > 0);
        }

        [Fact]
        public void Compare_DeveRetornarZero_QuandoChamadosTiveremMesmoPesoEMesmaData()
        {
            var dataCriacao = new DateTime(2026, 06, 18, 8, 0, 0);

            var chamado1 = CriarChamado(
                id: 1,
                titulo: "Chamado 1",
                categoriaPeso: 2,
                prioridadePeso: 5,
                criadoEm: dataCriacao
            );

            var chamado2 = CriarChamado(
                id: 2,
                titulo: "Chamado 2",
                categoriaPeso: 2,
                prioridadePeso: 5,
                criadoEm: dataCriacao
            );

            IPriorityComparerUseCase comparer = new PriorityComparerUseCase();

            var resultado = comparer.Compare(chamado1, chamado2);

            Assert.Equal(0, resultado);
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