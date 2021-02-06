using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario Obter(int idUsuario);
        void Inserir(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Deletar(int idUsuario);
    }
}
