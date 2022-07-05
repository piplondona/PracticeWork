using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using LambdaMutantML_Nivel3.Domain.Model;

namespace LambdaMutantML_Nivel3.Infraestructure.DriverAdapters.Repository
{
    public class MutantRepository : IMutantRepository
    {
        private readonly IAmazonDynamoDB dynamoDB;

        private const string TABLE_NAME = "Mutants";

        public MutantRepository(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }

        public async Task<bool> Add(Mutant mutant)
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

        public IEnumerable<Mutant> Gets(bool isMutant)
        {
            List<Mutant> lstMutant = new List<Mutant>();

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
                    
                    lstMutant.Add(new Mutant{
                        Dna = dna?.S,
                        IsMutant = Convert.ToBoolean(mutant.S)
                    }); 
                }
            }

            return lstMutant;
        }
        
    }
}