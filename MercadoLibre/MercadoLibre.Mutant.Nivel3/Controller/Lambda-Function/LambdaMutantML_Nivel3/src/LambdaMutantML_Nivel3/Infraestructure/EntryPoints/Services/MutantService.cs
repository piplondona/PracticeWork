using LambdaMutantML_Nivel3.Domain.UseCases;
using LambdaMutantML_Nivel3.Helpers;

namespace LambdaMutantML_Nivel3.EntryPoints.Services
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

        private string[] ConvertDna(string dna)
        {
            return Helper.ConvertJSONToArray(dna);
        }
    }
}