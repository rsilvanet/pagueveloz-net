using PagueVeloz.APIs.Boleto;
using PagueVeloz.APIs.PagamentoConta;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PagueVeloz.Util
{
    /// <summary>
    /// Client para realizar requisições nas API's PagueVeloz.
    /// </summary>
    public class PagueVelozClient
    {
        private const string sandboxBaseUrl = "https://sandbox.pagueveloz.com.br/";
        private const string producaoBaseUrl = "https://www.pagueveloz.com.br/";

        private PagueVelozHttp _http;

        /// <summary>
        /// Ambiente PagueVeloz onde o Cliente fará as requisições.
        /// </summary>
        public PagueVelozEnvironment Environment { get; private set; }

        /// <summary>
        /// Credenciais geradas para o Client.
        /// </summary>
        public PagueVelozCredentials Credentials { get; private set; }

        /// <summary>
        /// URL do host PagueVeloz (baseado no 'environment' selecionado).
        /// </summary>
        public string BaseURL { get; private set; }

        public PagueVelozClient(PagueVelozEnvironment environment, PagueVelozCredentials credentials)
        {
            Environment = environment;
            Credentials = credentials;
            BaseURL = ResolveBaseURL(Environment);

            _http = new PagueVelozHttp(this);

            Boletos = new BoletoAPI(this);
            PagamentoDeContas = new PagamentoContaAPI(this);
            Saques = new SaqueAPI(this);
        }

        /// <summary>
        /// Retorna adequadamente o host PagueVeloz com base no ambiente.
        /// </summary>
        /// <param name="environment">O ambiente que será utilizado.</param>
        /// <returns>A url do host PagueVeloz.</returns>
        private string ResolveBaseURL(PagueVelozEnvironment environment)
        {
            if (environment == PagueVelozEnvironment.Sandbox)
            {
                return sandboxBaseUrl;
            }
            else if (environment == PagueVelozEnvironment.Producao)
            {
                return producaoBaseUrl;
            }

            throw new NotImplementedException($"Ambiente {environment.ToString()} não implementado.");
        }

        /// <summary>
        /// Trata o response recebido de uma requisição na API.
        /// </summary>
        /// <typeparam name="T">O tipo do objeto que deve retornar da API.</typeparam>
        /// <param name="response">O response a ser tratado.</param>
        /// <returns>Um objeto do tipo 'T' obtido através do response.</returns>
        private async Task<T> NormalizeResponse<T>(HttpResponseMessage response) => await _http.NormalizeResponse<T>(response);

        #region HTTP

        /// <summary>
        /// Realiza um GET na API.
        /// </summary>
        /// <typeparam name="TOut">O tipo do objeto que deve retornar da API.</typeparam>
        /// <param name="url">Rota da API para requisição.</param>
        /// <returns>Um objeto do tipo 'TOut'.</returns>
        public async Task<TOut> GetAsync<TOut>(string url) => await NormalizeResponse<TOut>(await _http.GetAsync(url));

        /// <summary>
        /// Realiza um POST na API.
        /// </summary>
        /// <typeparam name="TIn">O tipo do objeto que será enviado no body da requisição.</typeparam>
        /// <typeparam name="TOut">O tipo do objeto que deve retornar da API.</typeparam>
        /// <param name="url">Rota da API para requisição.</param>
        /// <param name="content">Um objeto do tipo 'TIn' que será enviado no body da requisição.</param>
        /// <returns>Um objeto do tipo 'TOut'.</returns>
        public async Task<TOut> PostAsync<TIn, TOut>(string url, TIn content) => await NormalizeResponse<TOut>(await _http.PostAsync(url, content));

        /// <summary>
        /// Realiza um PUT na API.
        /// </summary>
        /// <typeparam name="TIn">O tipo do objeto que será enviado no body da requisição.</typeparam>
        /// <typeparam name="TOut">O tipo do objeto que deve retornar da API.</typeparam>
        /// <param name="url">Rota da API para requisição.</param>
        /// <param name="content">Um objeto do tipo 'TIn' que será enviado no body da requisição.</param>
        /// <returns>Um objeto do tipo 'TOut'.</returns>
        public async Task<TOut> PutAsync<TIn, TOut>(string url, TIn content) => await NormalizeResponse<TOut>(await _http.PutAsync(url, content));

        /// <summary>
        /// Realiza um DELETE na API.
        /// </summary>
        /// <typeparam name="TOut">O tipo do objeto que deve retornar da API.</typeparam>
        /// <param name="url">Rota da API para requisição.</param>
        /// <returns>Um objeto do tipo 'TOut'.</returns>
        public async Task<TOut> DeleteAsync<TOut>(string url) => await NormalizeResponse<TOut>(await _http.DeleteAsync(url));

        /// <summary>
        /// Realiza um POST na API.
        /// </summary>
        /// <typeparam name="TIn">O tipo do objeto que será enviado no body da requisição.</typeparam>
        /// <param name="url">Rota da API para requisição.</param>
        /// <param name="content">Um objeto do tipo 'TIn' que será enviado no body da requisição.</param>
        public async Task<HttpResponseMessage> PostAsync<TIn>(string url, TIn content) => await _http.PostAsync(url, content);

        /// <summary>
        /// Realiza um PUT na API.
        /// </summary>
        /// <typeparam name="TIn">O tipo do objeto que será enviado no body da requisição.</typeparam>
        /// <param name="url">Rota da API para requisição.</param>
        /// <param name="content">Um objeto do tipo 'TIn' que será enviado no body da requisição.</param>
        public async Task<HttpResponseMessage> PutAsync<TIn>(string url, TIn content) => await _http.PutAsync(url, content);

        /// <summary>
        /// Realiza um DELETE na API.
        /// </summary>
        /// <param name="url">Rota da API para requisição.</param>
        public async Task<HttpResponseMessage> DeleteAsync(string url) => await _http.DeleteAsync(url);

        #endregion

        #region API's

        /// <summary>
        /// Referência para a API de boletos.
        /// </summary>
        public BoletoAPI Boletos { get; }

        /// <summary>
        /// Referência para a API de pagamento de contas.
        /// </summary>
        public PagamentoContaAPI PagamentoDeContas { get; }

        /// <summary>
        /// Referência para a API de saques.
        /// </summary>
        public SaqueAPI Saques { get; }

        #endregion
    }
}
