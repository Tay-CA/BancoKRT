namespace GestaoLimitesContas.Domain.Entities.GestaoLimites.ValueObjects
{
    public sealed record GestaoLimiteDadosBancarios
    {
        public GestaoLimiteDadosBancarios() {}

        public GestaoLimiteDadosBancarios(
        int agencia,
        int conta)
        {
            this.Agencia = agencia;
            this.Conta = conta;
        }

        public int Agencia { get; private set;}
        public int Conta { get; private set;}
    }

}
