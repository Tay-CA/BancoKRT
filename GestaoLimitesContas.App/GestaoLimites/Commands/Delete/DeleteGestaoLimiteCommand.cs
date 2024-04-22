using GestaoLimitesContas.Shared.Results;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Commands.Delete
{
    public class DeleteGestaoLimiteCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
