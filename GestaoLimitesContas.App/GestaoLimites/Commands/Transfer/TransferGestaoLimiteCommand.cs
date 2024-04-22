using GestaoLimitesContas.Shared.Results;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Commands.Transfer
{
    public class TransferGestaoLimiteCommand : IRequest<Result>
    {
        public string IdOrigem { get; set; }

        public string IdDestino { get; set; } 

        public decimal Valor { get; set; }
    }
}
