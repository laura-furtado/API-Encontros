using API_Encontros.DTOs;
using API_Encontros.Models;

namespace API_Encontros.Services
{
    public interface IEncontroService
    {
        Task<EncontroResponse> CriarAsync(CriarEncontroRequest request);

        Task<EncontroResponse?> ObterPorIdAsync(Guid id);

        Task<(List<EncontroResponse> Itens, int Total)> ListarAsync(
            Guid? clubeId,
            situacao? situacao,
            DateTime? dataInicio,
            DateTime? dataFim,
            int pagina,
            int tamanhoPagina);

        Task<EncontroResponse?> AtualizarAsync(Guid id, AtualizarEncontroRequest request);

        Task<EncontroResponse?> AlterarSituacaoAsync(Guid id, AlterarSituacaoEncontroRequest request);

        Task<bool> RemoverAsync(Guid id);
    }
}