using LambdaMutantML_Nivel3.Domain.Model;

namespace LambdaMutantML_Nivel3.Infraestructure.DriverAdapters.Repository
{
    public interface IMutantRepository
    {
        
        // public void SetRepository(IRepository repository);
        public Task<bool> Add(Mutant mutant);
        public IEnumerable<Mutant> Gets(bool isMutant);
    }
}