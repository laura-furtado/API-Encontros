using API_Encontros;
using API_Encontros.DTOs;
using API_Encontros.Models;
using API_Encontros.Repositories;

namespace API_Encontros.Services
{
    public class EncontroService : IEncontroService
    {
        private readonly IEncontroRepository _repositorio;

        public EncontroService(IEncontroRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<EncontroResponse> CriarAsync(CriarEncontroRequest request)
        {
            //validação de data
            if (request.data_hora <= DateTime.UtcNow)
                throw new Exception("A data e hora do encontro devem estar no futuro.");

            //validação de local ou link
            if (string.IsNullOrWhiteSpace(request.local) && string.IsNullOrWhiteSpace(request.link_encontro))
                throw new Exception("Informe o local ou o link do encontro.");

            //validação de existencia do clube
            if (!await _repositorio.ClubeExisteAsync(request.clube_id))
                throw new Exception("O clube informado não existe.");

            //validação se o livro pertence ao clube
            if (request.livro_clube_id != null &&
                !await _repositorio.LivroClubePertenceAoClubeAsync(request.livro_clube_id.Value, request.clube_id))
            {
                throw new Exception("O livro informado não pertence ao clube.");
            }

            

            var agora = DateTime.UtcNow;

            var encontro = new Encontro
            {
                id = Guid.NewGuid(),
                clube_id = request.clube_id,
                livro_clube_id = request.livro_clube_id,
                titulo = request.titulo.Trim(),
                descricao = request.descricao,
                data_hora = request.data_hora,
                local = request.local,
                link_encontro = request.link_encontro,
                situacao = situacao.AGENDADO,
                criado_em = agora,
                atualizado_em = agora
            };

            await _repositorio.AdicionarAsync(encontro);
            await _repositorio.SalvarAlteracoesAsync();

            return MapearParaResponse(encontro);
        }

        public async Task<EncontroResponse?> ObterPorIdAsync(Guid id)
        {
            var encontro = await _repositorio.ObterPorIdAsync(id);
            return encontro == null ? null : MapearParaResponse(encontro);
        }

        public async Task<(List<EncontroResponse> Itens, int Total)> ListarAsync(
            Guid? clubeId,
            situacao? situacao,
            DateTime? dataInicio,
            DateTime? dataFim,
            int pagina,
            int tamanhoPagina)
        {
            var (itens, total) = await _repositorio.ListarAsync(
                clubeId, situacao, dataInicio, dataFim, pagina, tamanhoPagina);

            var response = itens.Select(MapearParaResponse).ToList();

            return (response, total);
        }

        public async Task<EncontroResponse?> AtualizarAsync(Guid id, AtualizarEncontroRequest request)
        {
            var encontro = await _repositorio.ObterPorIdAsync(id);
            if (encontro == null) return null;

            //so tem como editar se tiver agendado
            if (encontro.situacao != situacao.AGENDADO)
                throw new Exception("Só é possível editar encontros com situação AGENDADO.");

            if (request.data_hora <= DateTime.UtcNow)
                throw new Exception("A data e hora do encontro devem estar no futuro.");

            if (string.IsNullOrWhiteSpace(request.local) && string.IsNullOrWhiteSpace(request.link_encontro))
                throw new Exception("Informe o local ou o link do encontro.");

            if (request.livro_clube_id != null &&
                !await _repositorio.LivroClubePertenceAoClubeAsync(request.livro_clube_id.Value, encontro.clube_id))
            {
                throw new Exception("O livro informado não pertence ao clube.");
            }


            encontro.titulo = request.titulo.Trim();
            encontro.descricao = request.descricao;
            encontro.data_hora = request.data_hora;
            encontro.local = request.local;
            encontro.link_encontro = request.link_encontro;
            encontro.livro_clube_id = request.livro_clube_id;
            encontro.atualizado_em = DateTime.UtcNow;

            _repositorio.Atualizar(encontro);
            await _repositorio.SalvarAlteracoesAsync();

            return MapearParaResponse(encontro);
        }

        public async Task<EncontroResponse?> AlterarSituacaoAsync(Guid id, AlterarSituacaoEncontroRequest request)
        {
            var encontro = await _repositorio.ObterPorIdAsync(id);
            if (encontro == null) return null;

            // Máquina de estados: só permite AGENDADO -> REALIZADO ou AGENDADO -> CANCELADO
            if (encontro.situacao != situacao.AGENDADO)
                throw new Exception($"Não é possível alterar a situação de um encontro {encontro.situacao}.");

            if (request.nova_situacao != situacao.REALIZADO && request.nova_situacao != situacao.CANCELADO)
                throw new Exception("A nova situação deve ser REALIZADO ou CANCELADO.");

            

            encontro.situacao = request.nova_situacao;
            encontro.atualizado_em = DateTime.UtcNow;

            _repositorio.Atualizar(encontro);
            await _repositorio.SalvarAlteracoesAsync();

            return MapearParaResponse(encontro);
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var encontro = await _repositorio.ObterPorIdAsync(id);
            if (encontro == null) return false;

            //regra só exclui se estiver agendado
            if (encontro.situacao != situacao.AGENDADO)
                throw new Exception("Só é possível excluir encontros com situação AGENDADO.");

            

            _repositorio.Remover(encontro);
            await _repositorio.SalvarAlteracoesAsync();

            return true;
        }

        
        private static EncontroResponse MapearParaResponse(Encontro e)
        {
            return new EncontroResponse
            {
                id = e.id,
                clube_id = e.clube_id,
                livro_clube_id = e.livro_clube_id,
                titulo = e.titulo,
                descricao = e.descricao,
                data_hora = e.data_hora,
                local = e.local,
                link_encontro = e.link_encontro,
                situacao = e.situacao.ToString(),
                criado_em = e.criado_em,
                atualizado_em = e.atualizado_em
            };
        }
    }
}