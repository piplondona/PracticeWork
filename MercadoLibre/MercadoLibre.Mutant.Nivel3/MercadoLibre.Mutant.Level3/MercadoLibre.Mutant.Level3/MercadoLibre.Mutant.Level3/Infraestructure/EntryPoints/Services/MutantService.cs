using MercadoLibre.Mutant.Level3.Domain.UseCases;
using MercadoLibre.Mutant.Level3.Helpers;

namespace MercadoLibre.Mutant.Level3
{
    public class MutantService
    {
        private readonly MutantUseCase mutantUseCase;

        public MutantService()
        {
            this.mutantUseCase = new MutantUseCase();
        }

        public Task<bool> IsMutant(string dna)
        {
            string[] dnaArray = this.ConvertDna(dna);
            return this.mutantUseCase.IsMutant(dnaArray);
        }

        public decimal GetStats()
        {
            return this.mutantUseCase.GetStats();
        }

        public int GetNumMutants(bool isMutant)
        {
            return this.mutantUseCase.GetNumMutants(isMutant);
        }

        private string[] ConvertDna(string dna)
        {
            return Helper.ConvertJSONToArray(dna);
        }
    }
}