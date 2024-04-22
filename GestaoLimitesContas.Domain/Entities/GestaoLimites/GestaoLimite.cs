using Amazon.DynamoDBv2.DataModel;
using GestaoLimitesContas.Domain.Entities.GestaoLimites.ValueObjects;

namespace GestaoLimitesContas.Domain.Entities.GestaoLimites
{
    [DynamoDBTable("GestaoLimite")]
    public class GestaoLimite
    {
        [DynamoDBHashKey]
        public string Id { get; private set; }

        [DynamoDBProperty]
        public string Cpf { get; private set; }

        [DynamoDBProperty]
        public int Agencia { get; private set; }

        [DynamoDBProperty]
        public int Conta { get; private set; }

        [DynamoDBProperty]
        public decimal LimiteMaximo { get; private set; }
        
        [DynamoDBProperty]
        public decimal LimiteAtual { get; private set; }

        [DynamoDBProperty]
        public decimal Saldo { get; private set; }

        public static GestaoLimite Create(GestaoLimiteDadosCliente dadosCliente, GestaoLimiteDadosBancarios dadosBancarios, decimal limite)
        {
            return new GestaoLimite
            {
                Id = Guid.NewGuid().ToString(),
                Cpf = dadosCliente.Cpf,
                Agencia = dadosBancarios.Agencia,
                Conta = dadosBancarios.Conta,
                LimiteMaximo = limite,
                LimiteAtual = limite,
                Saldo = 20
            };
        }

        public void UpdateLimite(decimal valor)
        { 
            LimiteAtual = valor - (LimiteMaximo - LimiteAtual);
            LimiteMaximo = valor; 
        }
        public void ReceiveTransfer(decimal valor) => Saldo += valor;

        public (bool Autorizada, bool FaltaSaldo, decimal LimiteAtual) Transfer(decimal valorATransferir)
        {
            if (valorATransferir > LimiteAtual)
                return (false, false, LimiteAtual);
            if (valorATransferir > Saldo)
                return (false, true, LimiteAtual);

            LimiteAtual -= valorATransferir;
            Saldo -= valorATransferir;

            return (true, false, LimiteAtual);
        } 
    }
}
