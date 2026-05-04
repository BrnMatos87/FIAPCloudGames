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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly BaseLogger<UsuarioController> _logger;

        public UsuarioController(
            IUsuarioService usuarioService,
            BaseLogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet("resumo")]
        public IActionResult ListarResumo()
        {
            var usuarios = _usuarioService.ListarResumo();
            return Ok(usuarios);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _usuarioService.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id:int}/resumo")]
        public IActionResult ObterResumoPorId([FromRoute] int id)
        {
            var usuario = _usuarioService.ObterResumoPorId(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuario);
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            var usuario = _usuarioService.ObterPorId(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] UsuarioInput input)
        {
            _logger.LogInformation("Iniciando cadastro de usuário");

            _usuarioService.Criar(input);

            _logger.LogInformation("Usuário cadastrado com sucesso.");
            return Created();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] UsuarioUpdateInput input)
        {
            _logger.LogInformation("Iniciando atualização de usuário");

            var sucesso = _usuarioService.Atualizar(input);

            if (!sucesso)
                return NotFound("Usuário não encontrado.");

            _logger.LogInformation("Usuário atualizado com sucesso.");
            return NoContent();
        }

        [HttpPatch("senha")]
        public IActionResult AlterarSenha([FromBody] UsuarioSenhaUpdateInput input)
        {
            _logger.LogInformation("Iniciando alteração de senha do usuário");

            var sucesso = _usuarioService.AlterarSenha(input);

            if (!sucesso)
                return NotFound("Usuário não encontrado.");

            _logger.LogInformation("Senha do usuário alterada com sucesso.");
            return NoContent();
        }

        [HttpPatch("perfil")]
        public IActionResult AlterarPerfil([FromBody] UsuarioPerfilUpdateInput input)
        {
            _logger.LogInformation("Iniciando alteração de perfil do usuário");

            var sucesso = _usuarioService.AlterarPerfil(input);

            if (!sucesso)
                return NotFound("Usuário não encontrado.");

            _logger.LogInformation("Perfil do usuário alterado com sucesso.");
            return NoContent();
        }

        [HttpPatch("{id:int}/inativar")]
        public IActionResult Inativar([FromRoute] int id)
        {
            _logger.LogInformation("Iniciando inativação de usuário");

            var sucesso = _usuarioService.Inativar(id);

            if (!sucesso)
                return NotFound("Usuário não encontrado.");

            _logger.LogInformation("Usuário inativado com sucesso.");
            return NoContent();
        }

        [HttpPatch("{id:int}/ativar")]
        public IActionResult Ativar([FromRoute] int id)
        {
            _logger.LogInformation("Iniciando ativação de usuário");

            var sucesso = _usuarioService.Ativar(id);

            if (!sucesso)
                return NotFound("Usuário não encontrado.");

            _logger.LogInformation("Usuário ativado com sucesso.");
            return NoContent();
        }
    }
}