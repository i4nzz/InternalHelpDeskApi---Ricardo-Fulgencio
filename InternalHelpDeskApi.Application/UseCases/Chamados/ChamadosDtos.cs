using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Application.UseCases.Chamados;

public class ChamadosDtos
{
    #region Inputs
    public record AbrirChamadoInput(
        string Titulo
        , string Descricao
        , CategoriaChamadoEnum Categoria
        , NivelUrgenciaEnum Urgencia
        , NivelImpactoEnum Impacto
        , Guid SolicitanteId
    );

    public record IniciarAtendimentoInput(
        Guid ChamadoId
        , Guid AtendenteId
    );

    public record ResolverChamadoInput(
        Guid ChamadoId
        , string Resolucao
    );
    #endregion

    #region Outputs
    public record ChamadoResumoOutput(
        Guid Id
        , string Titulo
        , CategoriaChamadoEnum Categoria
        , NivelUrgenciaEnum Urgencia
        , NivelImpactoEnum Impacto
        , StatusChamadoEnum Status
        , int ScorePrioridade
        , string NomeSolicitante
        , string SetorSolicitante
        , CargoFuncionarioEnum CargoSolicitante
        , DateTime AberturaEm
        , double TempoEsperaMinutos
    );

    public record ChamadoDetalheOutput(
        Guid Id
        , string Titulo
        , string Descricao
        , CategoriaChamadoEnum Categoria
        , NivelUrgenciaEnum Urgencia
        , NivelImpactoEnum Impacto
        , StatusChamadoEnum Status
        , int ScorePrioridade
        , string NomeSolicitante
        , string SetorSolicitante
        , CargoFuncionarioEnum CargoSolicitante
        , string? NomeAtendente
        , DateTime AberturaEm
        , DateTime? InicioAtendimentoEm
        , DateTime? EncerramentoEm
        , string? Resolucao
        , double TempoEsperaMinutos
    );

    public record ProximoChamadoOutput(
        Guid Id
        , string Titulo
        , int ScorePrioridade
        , NivelUrgenciaEnum Urgencia
        , NivelImpactoEnum Impacto
        , string NomeSolicitante
        , string SetorSolicitante
        , int TotalNaFila
    );
    #endregion
}
