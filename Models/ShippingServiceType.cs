namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Códigos de serviços suportados pela SuperFrete.
/// </summary>
public enum ShippingServiceType
{
    /// <summary>
    ///     PAC (Correios)
    /// </summary>
    PAC = 1,

    /// <summary>
    ///     SEDEX (Correios)
    /// </summary>
    SEDEX = 2,

    /// <summary>
    ///     Jadlog.Package
    /// </summary>
    Jadlog = 3,

    /// <summary>
    ///     Mini Envios (Correios)
    /// </summary>
    MiniEnvios = 17,

    /// <summary>
    ///     Loggi Econômico
    /// </summary>
    Loggi = 31
}
