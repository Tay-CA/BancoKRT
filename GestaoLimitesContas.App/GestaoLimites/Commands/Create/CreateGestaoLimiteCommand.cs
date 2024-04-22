using GestaoLimitesContas.Shared.Results;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Commands.Create
{
    public class CreateGestaoLimiteCommand : IRequest<Result>
    {
        public string Cpf { get; set; }
        public int Agencia { get; set; }
        public int Conta { get; set; }
        public decimal Limite { get; set; }
    }
}
