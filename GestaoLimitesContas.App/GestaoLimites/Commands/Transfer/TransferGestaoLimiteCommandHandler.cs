using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using GestaoLimitesContas.Infraestructure.Contexts;
using GestaoLimitesContas.Shared.Results;
using MediatR;

namespace GestaoLimitesContas.App.GestaoLimites.Commands.Transfer
{
    public class TransferGestaoLimiteCommandHandler : IRequestHandler<TransferGestaoLimiteCommand, Result>
    {
        private readonly IGestaoLimiteContext context;
        public TransferGestaoLimiteCommandHandler(IGestaoLimiteContext context)
        {
            this.context = context;
        }

        public async Task<Result> Handle(TransferGestaoLimiteCommand request, CancellationToken cancellationToken)
        {
            List<ScanCondition> conditionsSenderAccount = AddConditionsSenderAccount(request);
            GestaoLimite? existingSenderAccount = await ValidateSenderUniqueRule(conditionsSenderAccount, cancellationToken);

            if (existingSenderAccount is null)
                return Result.Failure("Não existe uma conta com esses dados");

            List<ScanCondition> conditionsReceiverAccount = AddConditionsReceiverAccount(request);
            GestaoLimite? existingReceiverAccount = await ValidateReceiverUniqueRule(conditionsReceiverAccount, cancellationToken);

            if (existingReceiverAccount is null)
                return Result.Failure("Não existe uma conta com esses dados");

            var retorno = existingSenderAccount.Transfer(request.Valor);
            if (retorno.Autorizada)
            {
                existingReceiverAccount.ReceiveTransfer(request.Valor);
                var batchWrite = context.DynamoContext.CreateBatchWrite<GestaoLimite>();
                batchWrite.AddPutItem(existingSenderAccount);
                batchWrite.AddPutItem(existingReceiverAccount);
                await batchWrite.ExecuteAsync();

                return Result.Success($"Transação realizada com sucesso. Limite atual de transações PIX: {existingSenderAccount.LimiteAtual}. Saldo atual: {existingSenderAccount.Saldo}");
            }

            if (retorno.FaltaSaldo)
            {
                return Result.Failure($"Transação não aprovada por insuficiência de saldo. Saldo: {existingSenderAccount.Saldo}");
            }

            return Result.Failure($"Transação não aprovada por insuficiência de limite para transaçoes PIX. Limite atual: {existingSenderAccount.LimiteAtual}");
        }

        private async Task<GestaoLimite?> ValidateReceiverUniqueRule(List<ScanCondition> conditionsReceiverAccount, CancellationToken cancellationToken)
        {
            var receiverAccount = await context.DynamoContext.ScanAsync<GestaoLimite>(conditionsReceiverAccount).GetRemainingAsync(cancellationToken);
            var existingReceiverAccount = receiverAccount.FirstOrDefault();

            return existingReceiverAccount;
        }

        private async Task<GestaoLimite?> ValidateSenderUniqueRule(List<ScanCondition> conditionsSenderAccount, CancellationToken cancellationToken)
        {
            var senderAccount = await context.DynamoContext.ScanAsync<GestaoLimite>(conditionsSenderAccount).GetRemainingAsync(cancellationToken);
            var existingSenderAccount = senderAccount.FirstOrDefault();

            return existingSenderAccount;
        }

        private static List<ScanCondition> AddConditionsReceiverAccount(TransferGestaoLimiteCommand request)
        {
            List<ScanCondition> conditionsReceiverAccount = [new ScanCondition("Id", ScanOperator.Equal, request.IdDestino)];
            return conditionsReceiverAccount;
        }

        private static List<ScanCondition> AddConditionsSenderAccount(TransferGestaoLimiteCommand request)
        {
            List<ScanCondition> conditionsSenderAccount = [new ScanCondition("Id", ScanOperator.Equal, request.IdOrigem)];
            return conditionsSenderAccount;
        }
    }
}
