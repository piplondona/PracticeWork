using MercadoLibre.Mutant.Level3.Domain.Model;

namespace MercadoLibre.Mutant.Level3.Infraestructure.DriverAdapters.Repository
{
    public interface IMutantRepository
    {
        
        // public void SetRepository(IRepository repository);
        public Task<bool> Add(Domain.Model.Mutant mutant);
        public IEnumerable<Domain.Model.Mutant> Gets(bool isMutant);
    }
}