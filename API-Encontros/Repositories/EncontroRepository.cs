using API_Encontros.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Encontros.Repositories
{
    public class EncontroRepository : IEncontroRepository
    {
        private readonly AppDbContext _context;

        public EncontroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Encontro?> ObterPorIdAsync(Guid id)
        {
            return await _context.Encontros
                .FirstOrDefaultAsync(e => e.id == id);
        }

        public async Task<(List<Encontro> Itens, int Total)> ListarAsync(
            Guid? clubeId,
            situacao? situacao,
            DateTime? dataInicio,
            DateTime? dataFim,
            int pagina,
            int tamanhoPagina)
        {
            var consulta = _context.Encontros.AsNoTracking().AsQueryable();

            if (clubeId != null)
                consulta = consulta.Where(e => e.clube_id == clubeId);

            if (situacao != null)
                consulta = consulta.Where(e => e.situacao == situacao);

            if (dataInicio != null)
                consulta = consulta.Where(e => e.data_hora >= dataInicio);

            if (dataFim != null)
                consulta = consulta.Where(e => e.data_hora <= dataFim);

            var total = await consulta.CountAsync();

            var itens = await consulta
                .OrderBy(e => e.data_hora)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return (itens, total);
        }

        public async Task AdicionarAsync(Encontro encontro)
        {
            await _context.Encontros.AddAsync(encontro);
        }

        public void Atualizar(Encontro encontro)
        {
            _context.Encontros.Update(encontro);
        }

        public void Remover(Encontro encontro)
        {
            _context.Encontros.Remove(encontro);
        }

        public async Task<bool> ClubeExisteAsync(Guid clubeId)
        {
            return await _context.Clubes
                .AnyAsync(c => c.id == clubeId);
        }

        public async Task<bool> LivroClubePertenceAoClubeAsync(Guid livroClubeId, Guid clubeId)
        {
            return await _context.LivrosClubes
                .AnyAsync(lc => lc.id == livroClubeId && lc.clube_id == clubeId);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}