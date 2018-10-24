using PagueVeloz.APIs.Saque;
using PagueVeloz.Util;
using System.Threading.Tasks;

namespace PagueVeloz.APIs.PagamentoConta
{
    /// <summary>
    /// API para saques (TED bancária).
    /// </summary>
    public class SaqueAPI : BaseAPI
    {
        protected override string Url => "api/v2/saque";

        public SaqueAPI(PagueVelozClient client) : base(client) { }

        /// <summary>
        /// Realiza a solicitação de um saque.
        /// </summary>
        /// <param name="dto">Informações para o saque.</param>
        /// <returns>O identificador do saque realizado.</returns>
        public async Task<IdDTO> SolicitarAsync(SolicitacaoDTO dto)
        {
            return await _client.PostAsync<SolicitacaoDTO, IdDTO>(Url, dto);
        }
    }
}
