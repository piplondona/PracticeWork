
Console.WriteLine(String.Format("Please, type your dna, en el siguiente formato: {0}", "valor1, valor2"));
string? dna = Console.ReadLine();
try
{
    //if (dna)
    string[] dnaArray = dna.Split(',');
    isMutant(dnaArray);
}
catch (System.Exception ex)
{
    Console.WriteLine(String.Format("Error: description: {0}", ex.ToString()));       
    throw;
}

static bool isMutant(string[] dna)
{
    return true;
}
