namespace GestaoLimitesContas.Infraestructure.Options
{
    public class DynamoOptions
    {
        public const string DynamoDb = "DynamoDb";

        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string ServiceURL { get; set; }
    }
}
