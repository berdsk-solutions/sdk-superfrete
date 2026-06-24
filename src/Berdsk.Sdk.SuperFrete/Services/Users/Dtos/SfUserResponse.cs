using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Users.Dtos
{
    /// <summary>
    ///     Informações da conta do usuário autenticado na SuperFrete.
    /// </summary>
    public class SfUserResponse
    {
        /// <summary>
        ///     Identificador único do usuário na plataforma SuperFrete.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Primeiro nome do usuário.
        /// </summary>
        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        /// <summary>
        ///     Sobrenome do usuário.
        /// </summary>
        [JsonPropertyName("lastname")]
        public string? Lastname { get; set; }

        /// <summary>
        ///     Telefone de contato do usuário, incluindo código do país (ex: <c>+5511999999999</c>).
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     Endereço de e-mail cadastrado na conta.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     CPF do usuário (somente dígitos).
        /// </summary>
        [JsonPropertyName("document")]
        public string? Document { get; set; }

        /// <summary>
        ///     Limites de uso da conta (etiquetas em espera e disponibilidade).
        /// </summary>
        [JsonPropertyName("limits")]
        public SfUserLimitsResponse? Limits { get; set; }

        /// <summary>
        ///     Saldo disponível na carteira SuperFrete, em reais.
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
