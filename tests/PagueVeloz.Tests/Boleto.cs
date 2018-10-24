using PagueVeloz.APIs.Boleto;
using System;
using Xunit;

namespace PagueVeloz.Tests
{
    public class Boleto : Base
    {
        private RetornoEmissaoDTO Emitir()
        {
            var emissao = new EmissaoDTO()
            {
                Sacado = "Joãozinho",
                CPFCNPJSacado = "460.844.654-12",
                Valor = 100m,
                Vencimento = DateTime.Today.AddDays(10)
            };

            return GetClient().Boletos.EmitirAsync(emissao).Result;
        }

        [Fact]
        public void Boleto_ConsigoEmitirComOsCamposMinimos()
        {
            var retorno = Emitir();

            Assert.NotNull(retorno);
            Assert.True(retorno.Id > 0);
            Assert.False(string.IsNullOrWhiteSpace(retorno.Url));
        }

        [Fact]
        public void Boleto_ConsigoConsultarPeloId()
        {
            var retorno = Emitir();

            Assert.NotNull(retorno);
            Assert.True(retorno.Id > 0);

            var boleto = GetClient().Boletos.ConsultarPorIdAsync(retorno.Id).Result;

            Assert.NotNull(boleto);
            Assert.Equal(retorno.Id, boleto.Id);
            Assert.Equal(retorno.Url, boleto.Url);
            Assert.Equal("Joãozinho", boleto.Sacado);
            Assert.Equal("460.844.654-12", boleto.Documento);
            Assert.Equal(100m, boleto.Valor);
        }

        [Fact]
        public void Boleto_ConsigoCancelar()
        {
            var retorno = Emitir();
            
            Assert.NotNull(retorno);
            Assert.True(retorno.Id > 0);

            var boleto = GetClient().Boletos.ConsultarPorIdAsync(retorno.Id).Result;

            Assert.NotNull(boleto);
            Assert.False(boleto.Cancelado);

            GetClient().Boletos.CancelarAsync(boleto.Id).Wait();

            boleto = GetClient().Boletos.ConsultarPorIdAsync(retorno.Id).Result;

            Assert.NotNull(boleto);
            Assert.True(boleto.Cancelado);
        }
    }
}
