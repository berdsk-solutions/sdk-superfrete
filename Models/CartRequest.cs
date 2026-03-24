using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para enviar frete para a SuperFrete.
/// </summary>
public class CartRequest
{
    /// <summary>
    ///     Dados do remetente.
    /// </summary>
    [JsonProperty("from")]
    public ContactInfo From { get; set; } = new();

    /// <summary>
    ///     Dados do destinatário.
    /// </summary>
    [JsonProperty("to")]
    public ContactInfo To { get; set; } = new();

    /// <summary>
    ///     Serviço escolhido (1: PAC, 2: SEDEX, etc).
    /// </summary>
    [JsonProperty("service")]
    public int Service { get; set; }

    /// <summary>
    ///     Produtos para a declaração de conteúdo.
    /// </summary>
    [JsonProperty("products")]
    public List<DeclarationProduct>? Products { get; set; }

    /// <summary>
    ///     Dimensões e peso do pacote.
    /// </summary>
    [JsonProperty("volumes")]
    public PackageDimensions Volumes { get; set; } = new();

    /// <summary>
    ///     Opções adicionais.
    /// </summary>
    [JsonProperty("options")]
    public CartOptions? Options { get; set; }

    /// <summary>
    ///     Nome da plataforma.
    /// </summary>
    [JsonProperty("platform")]
    public string Platform { get; set; } = "C# Wrapper";

    /// <summary>
    ///     Identificação do pedido na plataforma.
    /// </summary>
    [JsonProperty("tag")]
    public string? Tag { get; set; }

    /// <summary>
    ///     URL da plataforma.
    /// </summary>
    [JsonProperty("url")]
    public string? Url { get; set; }
}
