using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using MercadoLibre.Mutant.Level3.Domain.Model;

namespace MercadoLibre.Mutant.Level3.Infraestructure.DriverAdapters.Repository
{
    public class MutantRepository : IMutantRepository
    {
        private readonly IAmazonDynamoDB dynamoDB;

        private const string TABLE_NAME = "Mutants";

        public MutantRepository(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }

        public async Task<bool> Add(Domain.Model.Mutant mutant)
        {
            PutItemRequest reqputitem = new PutItemRequest{
                        TableName = TABLE_NAME, 
                        Item = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>()
                        {
                            {"isMutant", new Amazon.DynamoDBv2.Model.AttributeValue { S = Convert.ToString(mutant.IsMutant)}},
                            {"dna", new Amazon.DynamoDBv2.Model.AttributeValue{S = mutant.Dna}}
                        }
                    };

            PutItemResponse response = await dynamoDB.PutItemAsync(reqputitem);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return true;
            
            return false;
        }

        public IEnumerable<Domain.Model.Mutant> Gets(bool isMutant)
        {
            List<Domain.Model.Mutant> lstMutant = new List<Domain.Model.Mutant>();

            ScanRequest request = new ScanRequest{
                TableName = TABLE_NAME,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>{
                    {":val", new AttributeValue { S = Convert.ToString(isMutant)}}
                },
                FilterExpression = "isMutant = :val"
            };

            Task<ScanResponse> response = dynamoDB.ScanAsync(request);

            if (response != null && response.Result.Items != null)
            {
                foreach (var item in response.Result.Items)
                {
                    item.TryGetValue("dna", out var dna);
                    item.TryGetValue("isMutant", out var mutant);
                    
                    lstMutant.Add(new Domain.Model.Mutant{
                        Dna = dna?.S,
                        IsMutant = Convert.ToBoolean(mutant.S)
                    }); 
                }
            }

            return lstMutant;
        }
        
    }
}