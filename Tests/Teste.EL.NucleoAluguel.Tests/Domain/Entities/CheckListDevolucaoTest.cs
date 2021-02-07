using System;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class CheckListDevolucaoTestTest
    {
        [Fact]
        public void CriarCheckListDevolucao_CheckListDevolucaoInvalido_Test()
        {
            var checkListDevolucao = new CheckListDevolucao(0, 0, DateTime.MinValue, false, false, false, false, 0);

            Assert.True(checkListDevolucao.Invalid);
            Assert.Contains(checkListDevolucao.Notifications, n => n.Property == nameof(checkListDevolucao.IdAluguel));
            Assert.Contains(checkListDevolucao.Notifications, n => n.Property == nameof(checkListDevolucao.IdVeiculo));
            Assert.Contains(checkListDevolucao.Notifications, n => n.Property == nameof(checkListDevolucao.ValorAluguel));
        }

        [Fact]
        public void CriarCheckListDevolucao_CheckListDevolucaoValido_Test()
        {
            var checkListDevolucao = new CheckListDevolucao(1, 1, DateTime.Now, false, false, false, false, 200);

            Assert.True(checkListDevolucao.Valid);
        }

        [Fact]
        public void CriarCheckListDevolucao_TotalAdicionalValido_Test()
        {
            var checkListDevolucao = new CheckListDevolucao(1, 1, new DateTime(2021, 1, 1), false, false, false, false, 200);

            Assert.True(checkListDevolucao.Valid);
            Assert.Equal(120, checkListDevolucao.TotalCobrancaAdicional);
        }
    }
}
