using MercadoLibre.Mutant.Level3.Helpers;
using MercadoLibre.Mutant.Level3.Infraestructure.DriverAdapters.Repository;
using MercadoLibre.Mutant.Level3.Domain.Model;
using Amazon.DynamoDBv2;

namespace MercadoLibre.Mutant.Level3.Domain.UseCases
{
    public class MutantUseCase
    {
        private readonly MutantRepository mutantRepository;

        public MutantUseCase()
        {
            this.mutantRepository = new MutantRepository(new AmazonDynamoDBClient());
        }

        public async Task<bool> IsMutant(string[] dna)
        {
            bool isMutant = false;
            Domain.Model.Mutant mutant = new Domain.Model.Mutant();
            mutant.Dna = Helper.Dna;
            int numSequences = FindSequenceHorizontally(dna);
            if  (numSequences > 1)
            {
                isMutant = true;
            }
            else
            {
                numSequences += FindSequenceVertically(dna);
                if  (numSequences > 1)
                {
                    isMutant = true;
                }
                else
                {
                    numSequences += FindSequenceSide(dna);
                    if  (numSequences > 1)
                    {
                        isMutant = true;
                    }
                }
            }

            mutant.IsMutant = isMutant;            
            await this.SaveDNA(mutant);
            return isMutant;
        }

        public int GetNumMutants(bool isMutant)
        {
            return mutantRepository.Gets(isMutant).Count();
        }

        public decimal GetStats()
        {
            IEnumerable<Domain.Model.Mutant> mutants = mutantRepository.Gets(true);
            IEnumerable<Domain.Model.Mutant> humans = mutantRepository.Gets(false);
            return mutants.Count() / humans.Count();
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

        private async Task<bool> SaveDNA(Domain.Model.Mutant mutant)
        {
            return await this.mutantRepository.Add(mutant);
        }

        enum Sequences{
            AAAA,
            CCCC,
            TTTT,
            GGGG
        };
    }
}