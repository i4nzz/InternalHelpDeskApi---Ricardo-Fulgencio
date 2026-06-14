using InternalHelpDeskApi.Domain.Enums;

namespace InternalHelpDeskApi.Domain.Entities;

/// <summary>
/// Entidade principal do sistema. Representa um chamado aberto por um funcionário.
/// O score calculado é usado pela estrutura de Max-Heap para ordenar o atendimento.
/// </summary>
public class Chamado
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public CategoriaChamadoEnum Categoria { get; private set; }
    public NivelUrgenciaEnum Urgencia { get; private set; }
    public NivelImpactoEnum Impacto { get; private set; }
    public StatusChamadoEnum Status { get; private set; }
    public int ScorePrioridade { get; private set; }
    public DateTime AberturaEm { get; private set; }
    public DateTime? InicioAtendimentoEm { get; private set; }
    public DateTime? EncerramentoEm { get; private set; }
    public string? Resolucao { get; private set; }

    // Relacionmentos
    public Guid SolicitanteId { get; private set; }
    public Funcionario Solicitante { get; private set; } = null!;
    public Guid? AtendenteId { get; private set; }
    public Atendente? Atendente { get; private set; }

    // EF Core constructor
    protected Chamado() { }

    public Chamado(
        string titulo,
        string descricao,
        CategoriaChamadoEnum categoria,
        NivelUrgenciaEnum urgencia,
        NivelImpactoEnum impacto,
        Funcionario solicitante
       )
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Categoria = categoria;
        Urgencia = urgencia;
        Impacto = impacto;
        Status = StatusChamadoEnum.Aberto;
        AberturaEm = DateTime.UtcNow;
        SolicitanteId = solicitante.Id;
        Solicitante = solicitante;

        // Score calculado no momento da criação — usado pelo Max-Heap
        ScorePrioridade = CalcularScore(urgencia, impacto, solicitante);
    }

    /// <summary>
    /// Calcula o score de prioridade do chamado.
    /// Fórmula: (Urgência * 3) + (Impacto * 4) + PesoCargo
    /// 
    /// Justificativa: um chamado com Impacto = Empresa (4) recebe peso 16,
    /// enquanto um chamado individual com Urgência Baixa recebe score ≤ 5.
    /// Isso garante que quedas de servidor (Impacto=Empresa, Urgência=Crítica)
    /// sempre superem chamados individuais como troca de mouse.
    /// </summary>
    public static int CalcularScore(NivelUrgenciaEnum urgencia, NivelImpactoEnum impacto, Funcionario solicitante)
    {
        int pesoUrgencia = (int)urgencia * 3;
        int pesoImpacto = (int)impacto * 4;
        int pesoCargo = solicitante.PesoCargo();
        return pesoUrgencia + pesoImpacto + pesoCargo;
    }

    public void IniciarAtendimento(Atendente atendente)
    {
        if (Status != StatusChamadoEnum.Aberto && Status != StatusChamadoEnum.Aguardando)
        {
            throw new InvalidOperationException("Chamado não pode ser iniciado no status atual.");
        }

        Status = StatusChamadoEnum.EmAtendimento;
        AtendenteId = atendente.Id;
        Atendente = atendente;
        InicioAtendimentoEm = DateTime.UtcNow;
    }

    public void Resolver(string resolucao)
    {
        if (Status != StatusChamadoEnum.EmAtendimento)
        {
            throw new InvalidOperationException("Somente chamados em atendimento podem ser resolvidos.");
        }

        Status = StatusChamadoEnum.Resolvido;
        Resolucao = resolucao;
        EncerramentoEm = DateTime.UtcNow;
    }

    public void Fechar()
    {
        if (Status != StatusChamadoEnum.Resolvido)
        {
            throw new InvalidOperationException("Somente chamados resolvidos podem ser fechados.");
        }

        Status = StatusChamadoEnum.Fechado;
    }

    public void Cancelar()
    {
        if (Status == StatusChamadoEnum.Fechado || Status == StatusChamadoEnum.Resolvido)
        {
            throw new InvalidOperationException("Chamado já finalizado não pode ser cancelado.");
        }

        Status = StatusChamadoEnum.Cancelado;
        EncerramentoEm = DateTime.UtcNow;
    }

    public void ColocarEmEspera()
    {
        if (Status != StatusChamadoEnum.EmAtendimento)
        {
            throw new InvalidOperationException("Somente chamados em atendimento podem ser colocados em espera.");
        }

        Status = StatusChamadoEnum.Aguardando;
    }

    /// <summary>
    /// Tempo de espera em minutos desde a abertura.
    /// Usado para aging: chamados muito antigos podem ter score reforçado.
    /// </summary>
    public double TempoEsperaMinutos()
    {
        return (DateTime.UtcNow - AberturaEm).TotalMinutes;
    }
}

