using FluentAssertions;
using GestaoLimitesContas.Domain.Entities.GestaoLimites;

namespace GestaoLimitesContas.Tests
{
    public class GestaoLimiteTest
    {
        [Fact]
        public void GestaoLimite_Create()
        {
            var gestaoLimite = GestaoLimite.Create(new("86174164080"), new(3594, 11877855), 1000);
            gestaoLimite.Should().NotBeNull();
        }

        [Fact]
        public void GestaoLimite_Should_UpdateLimit_When_UpdateLimite_IsCalled()
        {
            var gestaoLimite = GestaoLimite.Create(new("86174164080"), new(3594, 11877855), 1000);
            gestaoLimite.UpdateLimite(3000);
            gestaoLimite.LimiteMaximo.Should().Be(3000);
        }

        [Fact]
        public void GestaoLimite_Should_UpdateSaldo_When_ReceiveTransfer_IsCalled()
        {
            var gestaoLimite = GestaoLimite.Create(new("86174164080"), new(3594, 11877855), 0);
            gestaoLimite.ReceiveTransfer(2000);
            gestaoLimite.Saldo.Should().Be(2000);
        }

        [Fact]
        public void GestaoLimite_Should_Validate_And_RemoveLimite_When_Transfer_Autorizada()
        {
            var gestaoLimitePagador = GestaoLimite.Create(new("86174164080"), new(3594, 11877855), 1000);
            var gestaoLimiteRecebedor = GestaoLimite.Create(new("89591892004"), new(1139, 16242063), 0);
            gestaoLimitePagador.ReceiveTransfer(500);
            gestaoLimitePagador.Saldo.Should().Be(500);
            var result = gestaoLimitePagador.Transfer(250);
            result.Autorizada.Should().BeTrue();
            gestaoLimitePagador.LimiteAtual.Should().Be(750);
            gestaoLimitePagador.Saldo.Should().Be(250);
            gestaoLimiteRecebedor.ReceiveTransfer(250);
            gestaoLimiteRecebedor.Saldo.Should().Be(250);
        }

        [Fact]
        public void GestaoLimite_Should_Validate_And_Not_RemoveLimite_When_Transfer_IsCalled_But_Limite_Not_Enough()
        {
            var gestaoLimitePagador = GestaoLimite.Create(new("86174164080"), new(3594, 11877855), 100);
            var gestaoLimiteRecebedor = GestaoLimite.Create(new("89591892004"), new(1139, 16242063), 0);
            gestaoLimitePagador.ReceiveTransfer(500);
            gestaoLimitePagador.Saldo.Should().Be(500);
            var result = gestaoLimitePagador.Transfer(250);
            result.Autorizada.Should().BeFalse();
            result.FaltaSaldo.Should().BeFalse();
        }
    }
}