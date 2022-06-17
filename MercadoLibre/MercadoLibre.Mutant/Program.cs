using System;
using System.Text.RegularExpressions;

namespace MercadoLibre.Mutant
{
    class Program
    {
        
        static void Main(){

            Console.WriteLine("Please, type your dna, input next format: {\"valor1\",\"valor2\",..}:");
            string? input = Console.ReadLine();

            try
            {
                if (ValidateInputString(input))
                {
                    if (IsMutant(input.Split(',')))
                        ShowMessage("Information: Dna is positive for be a Mutant");
                    else
                        ShowMessage("Information: Dna is negative for be a Mutant");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(String.Format("Error: description: {0}", ex.ToString()));       
                throw;
            }
        }

        static bool ValidateInputString(string? inputString)
        {
            if (!String.IsNullOrEmpty(inputString))
            {
                string patternInput = @"^\{\""[ACGT]*\""([\,]?(\""[ACGT]*\""))*\}$";
                Match matchRegExpr = Regex.Match(inputString, patternInput);
                if (matchRegExpr.Success)
                {
                    return true;
                }
                else
                {
                    ShowMessage("Error: The input format is wrong");
                }
            }
            else
            {
                ShowMessage("Error: Input empty, could you input someone value");
            }

            return false;
        }

        static bool IsMutant(string[] dna)
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

        static int FindSequenceHorizontally(string[] dna)
        {
            return dna.Count(p => p.ToString().Contains(((Sequences)Sequences.AAAA).ToString()) || p.ToString().Contains(((Sequences)Sequences.TTTT).ToString()) 
            || p.ToString().Contains(((Sequences)Sequences.CCCC).ToString()) || p.ToString().Contains(((Sequences)Sequences.GGGG).ToString()));
        }

        static int FindSequenceVertically(string[] dna)
        {
            string[,] dnaMatrix = new string[dna.Length, dna.Length];
            for (int i=0; i< dna.Length; i++)
            {
                string[] newRow = dna[i].Split();
            }
            return 0;
        }

        static int FindSequenceSide(string[] dna)
        {
            return 0;
        }

        static void ShowMessage(string message)
        {
            Console.WriteLine(message);
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
        };
    }
}
