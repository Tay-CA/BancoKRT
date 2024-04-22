using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Queries.Get
{
    public class GetGestaoLimiteQuery : IRequest<GestaoLimite>
    {
        public string Id { get; set; }
    }
}
