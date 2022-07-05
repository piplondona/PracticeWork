using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace lambdaMutant.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new lambdaMutant.Tests.FunctionTest();
        var request = new lambdaMutant
        var isMutant = function.FunctionHandler("hello world", context);

        Assert.Equal("HELLO WORLD", upperCase);
    }
}
