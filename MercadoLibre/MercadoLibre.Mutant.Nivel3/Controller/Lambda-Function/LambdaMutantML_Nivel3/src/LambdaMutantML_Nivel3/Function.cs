using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using LambdaMutantML_Nivel3.EntryPoints.Services;
using LambdaMutantML_Nivel3.Infraestructure.DriverAdapters.Repository;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaMutantML_Nivel3;

public class Function
{
    /// <summary>
    /// This is function validates DNAs for humans or mutants
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
                Helpers.Helper.Dna = request.Body;
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

    /* private bool IsMutant(string[] dna)
    {
        int numSequences = FindSequenceHorizontally(dna);
        if  (numSequences > 1)
            return true;
        
        numSequences += FindSequenceVertically(dna);
        if  (numSequences > 1)
                return true;

        numSequences += FindSequenceSide(dna);
        if  (numSequences > 1)
                return true;

        return false;
    }

    private int FindSequenceHorizontally(string[] dna)
    {
        return dna.Count(p => p.ToString().Contains(((Sequences)Sequences.AAAA).ToString()) || p.ToString().Contains(((Sequences)Sequences.TTTT).ToString()) 
        || p.ToString().Contains(((Sequences)Sequences.CCCC).ToString()) || p.ToString().Contains(((Sequences)Sequences.GGGG).ToString()));
    }

    private int FindSequenceVertically(string[] dna)
    {
        string[,] dnaMatrix = Helper.ConvertArrayUnidimensionalToMatrix(dna);
        string[] dnaString = Helper.ConvertMatrixToArrayStringReverse(dnaMatrix);
        return FindSequenceHorizontally(dnaString);
    }

    private int FindSequenceSide(string[] dna)
    {
        int numSequencesFinded = 0;
        string[,] dnaMatrix = Helper.ConvertArrayUnidimensionalToMatrix(dna);
        string[] dnaString = new string[1];
        numSequencesFinded = GetNumsSequenceSide(dnaMatrix, 0, dnaString, 0);
        return numSequencesFinded;
    }

    private int GetNumsSequenceSide(string[,] dnaMatrix, int filaArray, string[] arrayNew, int numSequences)
    {
        string[,] matrixNew = new string[dnaMatrix.GetLength(1)-1, dnaMatrix.GetLength(1)-1];
        int rowArrayNew = 0;
        int colArrayNew = 0;

        for (int i = 0; i < dnaMatrix.GetLength(1); i++)
        {
            if ((i + 1).Equals(dnaMatrix.GetLength(1)))
            {
                rowArrayNew -= 1;
            } 

            colArrayNew = 0;
            for (int j = 0; j < dnaMatrix.GetLength(1); j++)
            {
                if (i==j)
                {
                    arrayNew[filaArray] += dnaMatrix[i,j];
                }
                else
                {
                    matrixNew[rowArrayNew,colArrayNew] = dnaMatrix[i,j];
                    colArrayNew++;
                }
            }

            rowArrayNew++;
        }

        numSequences += FindSequenceHorizontally(arrayNew);

        if (numSequences > 1)
            return numSequences;
        else
        {
            if (matrixNew.GetLength(1) > 3)
            {
                arrayNew[0] = String.Empty;
                GetNumsSequenceSide(matrixNew, filaArray, arrayNew, numSequences);  
            }
        }
            
        return numSequences;
    }

    enum NitrogenBase{
        C,
        A,
        T,
        G
    };

    enum Sequences{
        AAAA,
        CCCC,
        TTTT,
        GGGG
    }; */
}