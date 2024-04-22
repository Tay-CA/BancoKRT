using Amazon.DynamoDBv2.DataModel;

namespace GestaoLimitesContas.Infraestructure.Contexts
{
    public interface IGestaoLimiteContext
    {
        void Seed();

        public DynamoDBContext DynamoContext { get; }
    }
}
