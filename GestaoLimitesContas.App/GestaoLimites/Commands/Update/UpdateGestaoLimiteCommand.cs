using GestaoLimitesContas.Shared.Results;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Commands.Update
{
    public class UpdateGestaoLimiteCommand : IRequest<Result>
    {
        public string Id  { get; set; }

        public decimal NovoLimite { get; set; }
    }
}
