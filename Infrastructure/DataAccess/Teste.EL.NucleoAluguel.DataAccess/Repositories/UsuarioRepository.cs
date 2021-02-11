using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        List<Usuario> _usuariosMock;
        public UsuarioRepository()
        {
            _usuariosMock = new List<Usuario>();
            _usuariosMock.Add(new Usuario(1, PerfilUsuario.Cliente, "12542471657", "123456"));
            _usuariosMock.Add(new Usuario(2, PerfilUsuario.Operador, "137108", "456789"));
        }

        public void Atualizar(Usuario usuario)
        {
            if (_usuariosMock != null)
            {
                _usuariosMock.RemoveAll(_usuariosMock => _usuariosMock.IdUsuario == usuario.IdUsuario);
                _usuariosMock.Add(usuario);
            }
        }

        public void Deletar(int idUsuario)
        {
            if (_usuariosMock != null)
                _usuariosMock.RemoveAll(usuarioMock => usuarioMock.IdUsuario == idUsuario);
        }

        public int Inserir(Usuario usuario)
        {
            int proximoId = 0;

            if (usuario != null)
            {
                proximoId = _usuariosMock.Max(al => al.IdUsuario);
                proximoId++;

                usuario.IdUsuario = proximoId;
                _usuariosMock.Add(usuario);
            }
            return proximoId;

            if (usuario != null)
                _usuariosMock.Add(usuario);
        }

        public Usuario Obter(int idUsuario)
        {
            if (_usuariosMock != null)
                return _usuariosMock.Where(usuarioMock => usuarioMock.IdUsuario == idUsuario).FirstOrDefault();
            else
                return null;
        }

        public Usuario ObterPorLogin(string login)
        {
            if (_usuariosMock != null)
                return _usuariosMock.Where(usuarioMock => usuarioMock.Login.ToLower().Trim() == login.ToLower().Trim()).FirstOrDefault();
            else
                return null;
        }

        public Usuario ObterPorLoginESenha(string login, string senha)
        {
            if (_usuariosMock != null)
                return _usuariosMock.Where(usuarioMock => usuarioMock.Login == login && usuarioMock.Senha == senha).FirstOrDefault();
            else
                return null;
        }
    }
}
