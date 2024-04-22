using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using GestaoLimitesContas.Infraestructure.Contexts;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Queries.Get
{
    public class GetGestaoLimiteQueryHandler : IRequestHandler<GetGestaoLimiteQuery, GestaoLimite>
    {
        private readonly IGestaoLimiteContext context;

        public GetGestaoLimiteQueryHandler(IGestaoLimiteContext context)
        {
            this.context = context;
        }

        public async Task<GestaoLimite> Handle(GetGestaoLimiteQuery request, CancellationToken cancellationToken)
        {
            List<ScanCondition> conditions = AddConditions(request);
            var accounts = await context.DynamoContext.ScanAsync<GestaoLimite>(conditions).GetRemainingAsync(cancellationToken);
            var existingAccount = accounts.FirstOrDefault();

            return existingAccount;
        }

        private static List<ScanCondition> AddConditions(GetGestaoLimiteQuery request)
        {
            List<ScanCondition> conditions = [new ScanCondition("Id", ScanOperator.Equal, request.Id)];
            return conditions;
        }
    }
}
