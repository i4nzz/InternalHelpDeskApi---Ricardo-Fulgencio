namespace InternalHelpDeskApi.Application.Interfaces;

public interface IResult
{
    bool Sucesso { get; }
    string? MensagemErro { get; }
}

public class Result : IResult
{
    public bool Sucesso { get; protected set; }
    public string? MensagemErro { get; protected set; }

    public static Result Ok()
    {
        return new Result { Sucesso = true };
    }

    public static Result Falha(string mensagem)
    {
        return new Result
        {
            Sucesso = false,
            MensagemErro = mensagem
        };
    }

    public static Result<T> Ok<T>(T dados)
    {
        return new Result<T>
        {
            Sucesso = true,
            Dados = dados
        };
    }

    public static Result<T> Falha<T>(string mensagem)
    {
        return new Result<T>
        {
            Sucesso = false,
            MensagemErro = mensagem
        };
    }
}

public class Result<T> : Result
{
    public T? Dados { get; set; }
}
