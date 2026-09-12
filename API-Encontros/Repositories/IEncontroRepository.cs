using API_Encontros.Models;

namespace API_Encontros.Repositories
{
    public interface IEncontroRepository
    {
        //busca um encontro peol id
        Task<Encontro?> ObterPorIdAsync(Guid id);

        //lista encontros com filtros opcionais e paginação
        Task<(List<Encontro> Itens, int Total)> ListarAsync(
            Guid? clubeId,
            situacao? situacao,
            DateTime? dataInicio,
            DateTime? dataFim,
            int pagina,
            int tamanhoPagina);

        //adiciona um encontro
        Task AdicionarAsync(Encontro encontro);

        //atualiza um encontro existente
        void Atualizar(Encontro encontro);

        //remove um encontro
        void Remover(Encontro encontro);

        //verifica se o clube existe
        Task<bool> ClubeExisteAsync(Guid clubeId);

        //verifica se o livro_clube pertence ao clube informado
        Task<bool> LivroClubePertenceAoClubeAsync(Guid livroClubeId, Guid clubeId);

        //salva todas as alterações no banco
        Task SalvarAlteracoesAsync();
    }
}