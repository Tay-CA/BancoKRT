using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Queries.List
{
    public class ListGestaoLimiteQuery : IRequest<List<GestaoLimite>>
    {
    }
}
