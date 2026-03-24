using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para enviar frete para a SuperFrete.
/// </summary>
public class CartRequest
{
    /// <summary>
    ///     Dados do remetente.
    /// </summary>
    [JsonPropertyName("from")]
    public ContactInfo From { get; set; } = new();

    /// <summary>
    ///     Dados do destinatário.
    /// </summary>
    [JsonPropertyName("to")]
    public ContactInfo To { get; set; } = new();

    /// <summary>
    ///     Serviço escolhido (1: PAC, 2: SEDEX, etc).
    /// </summary>
    [JsonPropertyName("service")]
    public int Service { get; set; }

    /// <summary>
    ///     Produtos para a declaração de conteúdo.
    /// </summary>
    [JsonPropertyName("products")]
    public List<DeclarationProduct>? Products { get; set; }

    /// <summary>
    ///     Dimensões e peso do pacote.
    /// </summary>
    [JsonPropertyName("volumes")]
    public PackageDimensions Volumes { get; set; } = new();

    /// <summary>
    ///     Opções adicionais.
    /// </summary>
    [JsonPropertyName("options")]
    public CartOptions? Options { get; set; }

    /// <summary>
    ///     Nome da plataforma.
    /// </summary>
    [JsonPropertyName("platform")]
    public string Platform { get; set; } = "C# Wrapper";

    /// <summary>
    ///     Identificação do pedido na plataforma.
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    /// <summary>
    ///     URL da plataforma.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

