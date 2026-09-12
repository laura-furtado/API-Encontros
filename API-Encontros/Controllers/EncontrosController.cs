using API_Encontros.DTOs;
using API_Encontros.Models;
using API_Encontros.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Encontros.Controllers
{
    [ApiController]
    [Route("api/v1/encontros")]
    public class EncontrosController : ControllerBase
    {
        private readonly IEncontroService _service;

        public EncontrosController(IEncontroService service)
        {
            _service = service;
        }

        //GET api/v1/encontros
        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] Guid? clubeId,
            [FromQuery] situacao? situacao,
            [FromQuery] DateTime? dataInicio,
            [FromQuery] DateTime? dataFim,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 20)
        {
            var (itens, total) = await _service.ListarAsync(
                clubeId, situacao, dataInicio, dataFim, pagina, tamanhoPagina);

            return Ok(new { itens, pagina, tamanhoPagina, total });
        }

        // GET api/v1/encontros/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var encontro = await _service.ObterPorIdAsync(id);
            if (encontro == null) return NotFound();
            return Ok(encontro);
        }

        //POST api/v1/encontros
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarEncontroRequest request)
        {
            try
            {
                var criado = await _service.CriarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = criado.id }, criado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        //PUT api/v1/encontros/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarEncontroRequest request)
        {
            try
            {
                var atualizado = await _service.AtualizarAsync(id, request);
                if (atualizado == null) return NotFound();
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        //PATCH api/v1/encontros/{id}/situacao
        [HttpPatch("{id}/situacao")]
        public async Task<IActionResult> AlterarSituacao(Guid id, [FromBody] AlterarSituacaoEncontroRequest request)
        {
            try
            {
                var atualizado = await _service.AlterarSituacaoAsync(id, request);
                if (atualizado == null) return NotFound();
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        //DELETE api/v1/encontros/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            try
            {
                var removido = await _service.RemoverAsync(id);
                if (!removido) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}