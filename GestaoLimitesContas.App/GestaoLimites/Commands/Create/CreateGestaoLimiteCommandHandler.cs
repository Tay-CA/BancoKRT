using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using GestaoLimitesContas.Domain.Entities.GestaoLimites.ValueObjects;
using GestaoLimitesContas.Infraestructure.Contexts;
using GestaoLimitesContas.Shared.Results;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Commands.Create
{
    public class CreateGestaoLimiteCommandHandler : IRequestHandler<CreateGestaoLimiteCommand, Result>
    {
        private readonly IGestaoLimiteContext context;

        public CreateGestaoLimiteCommandHandler(IGestaoLimiteContext context)
        {
            this.context = context;
        }
        public async Task<Result> Handle(CreateGestaoLimiteCommand request, CancellationToken cancellationToken)
        {

            List<ScanCondition> conditions = AddConditions(request);
            GestaoLimite? existingAccount = await ValidateUniqueRule(conditions, cancellationToken);

            if (existingAccount is not null)
                return Result.Failure("Já existe uma conta com esses dados.");

            GestaoLimite gestaoLimite = GestaoLimite.Create(
                new GestaoLimiteDadosCliente(request.Cpf),
                new GestaoLimiteDadosBancarios(request.Agencia, request.Conta),
                request.Limite);
            await context.DynamoContext.SaveAsync(gestaoLimite, cancellationToken);

            return Result.Success(gestaoLimite.Id);
        }

        private async Task<GestaoLimite?> ValidateUniqueRule(List<ScanCondition> conditions, CancellationToken cancellationToken)
        {
            var accounts = await context.DynamoContext.ScanAsync<GestaoLimite>(conditions).GetRemainingAsync(cancellationToken);
            var existingAccount = accounts.FirstOrDefault();

            return existingAccount; 
        }

        private static List<ScanCondition> AddConditions(CreateGestaoLimiteCommand request)
        {
            return [
                new ScanCondition("Cpf", ScanOperator.Equal, request.Cpf),
                new ScanCondition("Conta", ScanOperator.Equal, request.Conta),
                new ScanCondition("Agencia", ScanOperator.Equal, request.Agencia),
            ];
        }
    }
}
