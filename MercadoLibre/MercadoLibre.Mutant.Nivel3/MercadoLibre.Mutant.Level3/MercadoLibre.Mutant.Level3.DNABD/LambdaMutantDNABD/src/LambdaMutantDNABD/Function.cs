using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using MercadoLibre.Mutant.Level3;
using MercadoLibre.Mutant.Level3.Helpers;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaMutantDNABD;

public class Function
{
    
    /// <summary>
    /// This function validates DNAs for humans or mutants
    /// </summary>
    /// <param name="request">Request send from API Getaway</param>
    /// <param name="context">Values about petition</param>
    /// <returns></returns>
    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        APIGatewayProxyResponse response = new APIGatewayProxyResponse();

        try
        {
            if (request.Body != null && request.Body.Contains("dna"))
            {
                context.Logger.Log("Valor Body: "+ request.Body);
                Helper.Dna = request.Body;
                MutantService mutantService = new MutantService();
                if (await mutantService.IsMutant(request.Body))
                {
                    response = ReturnResponse(200, @"{ ""Result"": ""DNS is positive for be a Mutant"" }");
                }
                else
                    response = ReturnResponse(403, @"{ ""Result"": ""DNS is negative for be a Mutant"" }");
            }
            else
                 context.Logger.Log("Body is empty or null");
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
