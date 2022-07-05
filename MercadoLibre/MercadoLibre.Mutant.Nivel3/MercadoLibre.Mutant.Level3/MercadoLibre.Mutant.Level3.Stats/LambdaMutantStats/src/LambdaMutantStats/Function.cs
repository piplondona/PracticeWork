using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using MercadoLibre.Mutant.Level3;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaMutantStats;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        APIGatewayProxyResponse response = new APIGatewayProxyResponse();
         try
        {
            MutantService mutantService = new MutantService();
            int numMutants = mutantService.GetNumMutants(true);
            int numHumans = mutantService.GetNumMutants(false);
            decimal ratio = mutantService.GetStats();
            
            string msgresponse = @"{ ""count_mutant_dna"": " + numMutants.ToString() + ",";
            msgresponse += @"""count_human_dna"": " + numHumans.ToString() + ",";
            msgresponse += @"""ratio"": " + ratio.ToString() + "}";
            response = ReturnResponse(200, msgresponse);
        }
        catch (System.Exception ex)
        {
            ReturnResponse(500, String.Format(@"{ ""Result"": ""Error, description: {0} ""}", ex.ToString())); 
            context.Logger.Log(String.Format("Description: {0}", ex.ToString()));     
            throw;
        }
        return response;
    }

    private APIGatewayProxyResponse ReturnResponse(int statusCode, string body)
    {
        return new APIGatewayProxyResponse{
            StatusCode = statusCode,
            Body = body,
            Headers = new Dictionary<string,string>{
                { "Content-type", "application/json"}
            }
        };
    }
}
