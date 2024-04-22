using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using GestaoLimitesContas.Domain.Entities.GestaoLimites;
using GestaoLimitesContas.Infraestructure.Options;
using Microsoft.Extensions.Options;

namespace GestaoLimitesContas.Infraestructure.Contexts
{
    public class GestaoLimiteContext : IGestaoLimiteContext
    {
        public DynamoDBContext DynamoContext { get; }

        private AmazonDynamoDBClient Client { get; set; }

        public GestaoLimiteContext(IOptions<DynamoOptions> options)
        {
            var dOptions = options.Value;
            var credentials = new BasicAWSCredentials(dOptions.AccessKey, dOptions.SecretKey) ;
            var config = new AmazonDynamoDBConfig { ServiceURL = dOptions.ServiceURL };
            this.Client = new AmazonDynamoDBClient(credentials, config);
            this.DynamoContext = new DynamoDBContext(Client);
        }

        public async void Seed()
        {
            var tableResponse = await Client.ListTablesAsync();
            if (!tableResponse.TableNames.Contains(nameof(GestaoLimite)))
            {
                await Client.CreateTableAsync(CreateGestaoLimiteTableRequest());

                bool isTableAvailable = false;
                while (!isTableAvailable)
                {
                    Thread.Sleep(5000);
                    var tableStatus = await Client.DescribeTableAsync(nameof(GestaoLimite));
                    isTableAvailable = tableStatus.Table.TableStatus == "ACTIVE";
                }
            }
        }

        private static CreateTableRequest CreateGestaoLimiteTableRequest()
        {
            return new CreateTableRequest
            {
                TableName = nameof(GestaoLimite),
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 3,
                    WriteCapacityUnits = 1
                },
                KeySchema =
                [
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = KeyType.HASH
                    }
                ],
                AttributeDefinitions =
                [
                    new AttributeDefinition {
                        AttributeName = "Id",
                        AttributeType = ScalarAttributeType.S
                    }
                ]
            };
        }
    }

}