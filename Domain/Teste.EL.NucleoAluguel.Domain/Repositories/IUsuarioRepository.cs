using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario Obter(int idUsuario);
        Usuario ObterPorLogin(string login);
        Usuario ObterPorLoginESenha(string login, string senha);
        void Inserir(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Deletar(int idUsuario);
    }
}
