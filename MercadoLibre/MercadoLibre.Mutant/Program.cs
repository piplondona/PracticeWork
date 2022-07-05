using System;
using System.Text.RegularExpressions;
using System.Text.Json;

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
                    string inputRefined = RefineString(input);
                    if (IsMutant(inputRefined.Split(',')))
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
            string[,] dnaMatrix = ConvertArrayUnidimensionalToMatrix(dna);
            string[] dnaString = ConvertMatrixToArrayStringReverse(dnaMatrix);
            return FindSequenceHorizontally(dnaString);
        }

        static int FindSequenceSide(string[] dna)
        {
           int numSequencesFinded = 0;
           string[,] dnaMatrix = ConvertArrayUnidimensionalToMatrix(dna);
           string[] dnaString = new string[1];
           numSequencesFinded = GetNumsSequenceSide(dnaMatrix, 0, dnaString, 0);
           return numSequencesFinded;
        }

        static int GetNumsSequenceSide(string[,] dnaMatrix, int filaArray, string[] arrayNew, int numSequences)
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

        static string[,] ConvertArrayUnidimensionalToMatrix(string[] arrayUnidimensional)
        {
            string[,] matrix = new string[arrayUnidimensional.Length, arrayUnidimensional.Length];
            for (int i=0; i< arrayUnidimensional.Length; i++)
            {
                char[] newRow = arrayUnidimensional[i].ToCharArray();
                for (int j = 0; j < newRow.Length; j++)
                {
                    matrix[j,i] = newRow[j].ToString();
                }
            }

            return matrix;
        }

        static string[] ConvertMatrixToArrayStringReverse(string[,] matrix)
        {
            string[] arrayString = new string[matrix.GetLongLength(1)];
            for (int i=0; i < matrix.GetLongLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLongLength(1); j++)
                {
                    arrayString[i] += matrix[i,j];
                }
            }

            return arrayString;
        }

        static string RefineString(string input)
        {
            string inputRefined = input.Replace("{", String.Empty);
            inputRefined = inputRefined.Replace("}",String.Empty);
            inputRefined = inputRefined.Replace('"',' ');
            inputRefined = inputRefined.Replace(" ", String.Empty);
            return inputRefined;
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
