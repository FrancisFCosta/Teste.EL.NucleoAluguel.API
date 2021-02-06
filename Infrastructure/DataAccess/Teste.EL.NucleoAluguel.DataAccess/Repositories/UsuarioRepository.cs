﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        List<Usuario> _usuariosMock;
        public UsuarioRepository()
        {
            _usuariosMock = new List<Usuario>();
            _usuariosMock.Add(new Usuario(1,PerfilUsuario.Cliente, "12542471657", "Raposa123"));
        }

        public void Atualizar(Usuario usuario)
        {
            if (_usuariosMock != null)
            {
                _usuariosMock.ForEach(usuarioMock =>
                {
                    if (usuarioMock.IdUsuario == usuario.IdUsuario)
                    {
                        usuarioMock = usuario;
                    }
                });
            }
        }

        public void Deletar(int idUsuario)
        {
            if (_usuariosMock != null)
                _usuariosMock.RemoveAll(usuarioMock => usuarioMock.IdUsuario == idUsuario);
        }

        public void Inserir(Usuario usuario)
        {
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
    }
}
