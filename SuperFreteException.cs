namespace Berdsk.Sdk.SuperFrete;

/// <summary>
///     Exceção lançada quando ocorre um erro na API do SuperFrete.
/// </summary>
public class SuperFreteException : Exception
{
    /// <summary>
    ///     Inicializa uma nova instância da classe <see cref="SuperFreteException" />.
    /// </summary>
    /// <param name="message">Mensagem de erro.</param>
    /// <param name="errorCode">Código do erro.</param>
    public SuperFreteException(string message, string? errorCode = null) : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    ///     Código do erro retornado pela API.
    /// </summary>
    public string? ErrorCode { get; }
}
