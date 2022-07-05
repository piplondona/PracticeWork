namespace lambdaMutant.Utility
{
    public static class Utility
    {
        public static string[,] ConvertArrayUnidimensionalToMatrix(string[] arrayUnidimensional)
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

        public static string[] ConvertMatrixToArrayStringReverse(string[,] matrix)
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
        
        public static string[] ConvertJSONToArray(string jsonInput)
        {
            string inputRefined = jsonInput.Substring(jsonInput.IndexOf('['));
            inputRefined = inputRefined.Replace("}",String.Empty);
            inputRefined = inputRefined.Replace("[",String.Empty);
            inputRefined = inputRefined.Replace("]",String.Empty);
            inputRefined = inputRefined.Replace('"',' ');
            inputRefined = inputRefined.Replace(" ", String.Empty);
            return inputRefined.Split(",");
        }
    }
}