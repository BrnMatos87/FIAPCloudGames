using Core.Input;
using Core.Service;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        private readonly BaseLogger<JogoController> _logger;

        public JogoController(IJogoService jogoService, BaseLogger<JogoController> logger)
        {
            _jogoService = jogoService;
            _logger = logger;
        }

        [HttpGet("resumo")]
        public IActionResult ListarResumo()
        {
            var jogos = _jogoService.ListarResumo();
            return Ok(jogos);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var jogos = _jogoService.Listar();
            return Ok(jogos);
        }

        [HttpGet("{id:int}/resumo")]
        public IActionResult ObterResumoPorId([FromRoute] int id)
        {
            var jogo = _jogoService.ObterResumoPorId(id);

            if (jogo == null)
                return NotFound("Jogo não encontrado.");

            return Ok(jogo);
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            var jogo = _jogoService.ObterPorId(id);

            if (jogo == null)
                return NotFound("Jogo não encontrado.");

            return Ok(jogo);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] JogoInput input)
        {
            _logger.LogInformation("Iniciando cadastro de jogo.");

            _jogoService.Criar(input);

            _logger.LogInformation("Jogo cadastrado com sucesso.");
            return Created();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] JogoUpdateInput input)
        {
            _logger.LogInformation("Iniciando atualização de jogo.");

            var sucesso = _jogoService.Atualizar(input);

            if (!sucesso)
                return NotFound("Jogo não encontrado.");

            _logger.LogInformation("Jogo atualizado com sucesso.");
            return NoContent();
        }

        [HttpPatch("/promocao/aplicar")]
        public IActionResult AplicarPromocao([FromBody] JogoPromocaoInput input)
        {
            _logger.LogInformation("Iniciando aplicação de promoção do jogo.");

            var sucesso = _jogoService.AplicarPromocao(input);

            if (!sucesso)
                return NotFound("Jogo não encontrado.");

            _logger.LogInformation("Promoção do jogo aplicada com sucesso.");
            return NoContent();
        }

        [HttpPatch("{id:int}/promocao/remover")]
        public IActionResult RemoverPromocao([FromRoute] int id)
        {
            _logger.LogInformation("Iniciando remoção de promoção do jogo.");

            var sucesso = _jogoService.RemoverPromocao(id);

            if (!sucesso)
                return NotFound("Jogo não encontrado.");

            _logger.LogInformation("Promoção do jogo removida com sucesso.");
            return NoContent();
        }

        [HttpPatch("{id:int}/inativar")]
        public IActionResult Inativar([FromRoute] int id)
        {
            _logger.LogInformation("Iniciando inativação de jogo.");

            var sucesso = _jogoService.Inativar(id);

            if (!sucesso)
                return NotFound("Jogo não encontrado.");

            _logger.LogInformation("Jogo inativado com sucesso.");
            return NoContent();
        }

        [HttpPatch("{id:int}/ativar")]
        public IActionResult Ativar([FromRoute] int id)
        {
            _logger.LogInformation("Iniciando ativação de jogo.");

            var sucesso = _jogoService.Ativar(id);

            if (!sucesso)
                return NotFound("Jogo não encontrado.");

            _logger.LogInformation("Jogo ativado com sucesso.");
            return NoContent();
        }
    }
}