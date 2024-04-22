namespace GestaoLimitesContas.Domain.Entities.GestaoLimites.ValueObjects
{
    public sealed record GestaoLimiteDadosCliente
    {
        public GestaoLimiteDadosCliente() {}

        public GestaoLimiteDadosCliente(string cpf)
        {
            this.Cpf = cpf;
        }

        public string Cpf { get; private set; }
    }
}
