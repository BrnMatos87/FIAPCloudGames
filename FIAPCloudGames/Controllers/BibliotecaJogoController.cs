using Core.Input;
using Core.Service;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Usuario")]
    public class BibliotecaJogoController : ControllerBase
    {
        private readonly IBibliotecaJogoService _bibliotecaJogoService;
        private readonly BaseLogger<BibliotecaJogoController> _logger;

        public BibliotecaJogoController(
            IBibliotecaJogoService bibliotecaJogoService,
            BaseLogger<BibliotecaJogoController> logger)
        {
            _bibliotecaJogoService = bibliotecaJogoService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var jogosUsuario = _bibliotecaJogoService.Listar();
            return Ok(jogosUsuario);
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            var biblioteca = _bibliotecaJogoService.ObterPorId(id);

            if (biblioteca == null)
                return NotFound("Registro não encontrado.");

            return Ok(biblioteca);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] BibliotecaJogoInput input)
        {
            _logger.LogInformation("Iniciando cadastro de jogo do usuário.");

            _bibliotecaJogoService.Criar(input);

            _logger.LogInformation("Cadastro de jogo do usuário realizado com sucesso.");
            return Created();
        }

        [HttpPatch("{id:int}/inativar")]
        public IActionResult Inativar([FromRoute] int id)
        {
            _logger.LogInformation("Iniciando inativação de jogo do usuário.");

            var sucesso = _bibliotecaJogoService.Inativar(id);

            if (!sucesso)
                return NotFound("Registro não encontrado.");

            _logger.LogInformation("Jogo do usuário inativado com sucesso.");
            return NoContent();
        }

        [HttpPatch("{id:int}/ativar")]
        public IActionResult Ativar([FromRoute] int id)
        {
            _logger.LogInformation("Iniciando ativação de jogo do usuário.");

            var sucesso = _bibliotecaJogoService.Ativar(id);

            if (!sucesso)
                return NotFound("Registro não encontrado.");

            _logger.LogInformation("Jogo do usuário ativado com sucesso.");
            return NoContent();
        }
    }
}