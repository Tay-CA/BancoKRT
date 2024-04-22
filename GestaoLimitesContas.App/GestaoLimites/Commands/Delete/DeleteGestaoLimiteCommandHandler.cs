using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using GestaoLimitesContas.Infraestructure.Contexts;
using GestaoLimitesContas.Shared.Results;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Commands.Delete
{
    public class DeleteGestaoLimiteCommandHandler : IRequestHandler<DeleteGestaoLimiteCommand, Result>
    {
        private readonly IGestaoLimiteContext context;

        public DeleteGestaoLimiteCommandHandler(IGestaoLimiteContext context)
        {
            this.context = context;
        }

        public async Task<Result> Handle(DeleteGestaoLimiteCommand request, CancellationToken cancellationToken)
        {

            List<ScanCondition> conditions = AddConditions(request);
            GestaoLimite? existingAccount = await ValidateUniqueRule(conditions, cancellationToken);

            if (existingAccount is null)
                return Result.Failure("Não existe uma conta com esses dados");

            await context.DynamoContext.DeleteAsync(existingAccount, cancellationToken);

            return Result.Success("Conta deletada com sucesso");
        }

        private async Task<GestaoLimite?> ValidateUniqueRule(List<ScanCondition> conditions, CancellationToken cancellationToken)
        {
            var accounts = await context.DynamoContext.ScanAsync<GestaoLimite>(conditions).GetRemainingAsync(cancellationToken);
            var existingAccount = accounts.FirstOrDefault();

            return existingAccount;
        }

        private static List<ScanCondition> AddConditions(DeleteGestaoLimiteCommand request)
        {
            List<ScanCondition> conditions = [new ScanCondition("Id", ScanOperator.Equal, request.Id)];

            return conditions;
        }
    }
}
