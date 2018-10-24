using PagueVeloz.APIs;
using PagueVeloz.APIs.Saque;
using Xunit;

namespace PagueVeloz.Tests
{
    public class Saque : Base
    {
        [Fact]
        public void Saque_ConsigoSolicitarUmSaque()
        {
            var dto = new SolicitacaoDTO
            {
                ContaBancaria = new IdDTO { Id = 4140 },
                Valor = 10m
            };

            var retorno = GetClient().Saques.SolicitarAsync(dto).Result;

            Assert.NotNull(retorno);
            Assert.True(retorno.Id > 0);
        }
    }
}
