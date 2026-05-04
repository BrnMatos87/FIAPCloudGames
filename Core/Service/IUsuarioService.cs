using Core.Dto;
using Core.Input;

namespace Core.Service
{
    public interface IUsuarioService
    {
        IList<UsuarioResumoDto> ListarResumo();
        IList<UsuarioDto> Listar();

        UsuarioResumoDto? ObterResumoPorId(int id);
        UsuarioDto? ObterPorId(int id);

        void Criar(UsuarioInput input);
        bool Atualizar(UsuarioUpdateInput input);

        bool AlterarSenha(UsuarioSenhaUpdateInput input);
        bool AlterarPerfil(UsuarioPerfilUpdateInput input);

        bool Inativar(int id);
        bool Ativar(int id);
    }
}