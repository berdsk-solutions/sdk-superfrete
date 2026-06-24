using System.Collections.Generic;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator
{
    /// <summary>
    ///     Define as operações do serviço de cotação de fretes da SuperFrete.
    /// </summary>
    public interface ISfCalculatorService
    {
        /// <summary>
        ///     Calcula o valor do frete para os serviços solicitados.
        /// </summary>
        /// <remarks>
        ///     Forneça as dimensões via <see cref="SfCalculateShippingRequest.Package" /> ou
        ///     <see cref="SfCalculateShippingRequest.Products" />.
        ///     Ao usar Products, a API retorna a caixa ideal — use essas dimensões ao criar a etiqueta.
        /// </remarks>
        /// <param name="request">Dados para o cálculo do frete.</param>
        /// <returns>Lista de resultados por serviço de entrega disponível.</returns>
        Task<List<SfCalculateShippingResponse>?> CalculateShippingAsync(SfCalculateShippingRequest request);
    }
}
