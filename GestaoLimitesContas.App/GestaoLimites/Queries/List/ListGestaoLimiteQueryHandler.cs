using Amazon.DynamoDBv2.DataModel;
using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using GestaoLimitesContas.Infraestructure.Contexts;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Queries.List
{
    public class ListGestaoLimiteQueryHandler : IRequestHandler<ListGestaoLimiteQuery, List<GestaoLimite>>
    {
        private readonly IGestaoLimiteContext context;

        public ListGestaoLimiteQueryHandler(IGestaoLimiteContext context)
        {
            this.context = context;
        }

        public async Task<List<GestaoLimite>> Handle(ListGestaoLimiteQuery request, CancellationToken cancellationToken)
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            var accounts = await context.DynamoContext.ScanAsync<GestaoLimite>(conditions).GetRemainingAsync(cancellationToken);
            var existingAccounts = accounts.ToList();

            return existingAccounts;
        }
    }
}
