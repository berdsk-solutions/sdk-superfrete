namespace Berdsk.Sdk.SuperFrete.Helpers
{
    /// <summary>
    ///     Serviços de entrega disponíveis na plataforma SuperFrete.
    /// </summary>
    public enum SfShippingServiceType
    {
        /// <summary>
        ///     Correios PAC (código 1).
        /// </summary>
        Pac = 1,

        /// <summary>
        ///     Correios SEDEX (código 2).
        /// </summary>
        Sedex = 2,

        /// <summary>
        ///     Jadlog Package (código 3).
        /// </summary>
        Jadlog = 3,

        /// <summary>
        ///     Correios Mini Envios (código 17).
        /// </summary>
        MiniEnvios = 17,

        /// <summary>
        ///     Loggi (código 31).
        /// </summary>
        Loggi = 31
    }

    /// <summary>
    ///     Constantes inteiras dos códigos de serviço de entrega da SuperFrete.
    ///     Use esta classe quando precisar do valor numérico bruto do serviço (ex: serialização de requests).
    /// </summary>
    public static class SfShippingServiceTypeValues
    {
        /// <summary>Correios PAC — código 1.</summary>
        public const int Pac = 1;

        /// <summary>Correios SEDEX — código 2.</summary>
        public const int Sedex = 2;

        /// <summary>Jadlog Package — código 3.</summary>
        public const int Jadlog = 3;

        /// <summary>Correios Mini Envios — código 17.</summary>
        public const int MiniEnvios = 17;

        /// <summary>Loggi — código 31.</summary>
        public const int Loggi = 31;
    }
}
