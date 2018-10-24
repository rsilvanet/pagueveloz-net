namespace PagueVeloz.APIs.Saque
{
    /// <summary>
    /// DTO utilizado para solicitação de saques.
    /// </summary>
    public class SolicitacaoDTO
    {
        /// <summary>
        /// [Obrigatório] A conta bancária de destino do valor.
        /// </summary>
        public IdDTO ContaBancaria { get; set; }

        /// <summary>
        /// [Obrigatório] O valor do saque a ser realizado.
        /// </summary>
        public decimal Valor { get; set; }
    }
}
