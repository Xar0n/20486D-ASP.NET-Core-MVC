using Cupcakes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cupcakes.Repositories
{
    public interface ICupcakeRepository
    {
        Task<IEnumerable<Cupcake>> GetCupcakes();
        Task<Cupcake> GetCupcakeById(int id);
        Task CreateCupcake(Cupcake cupcake);
        Task DeleteCupcake(int id);
        Task<int> SaveChanges();
        IQueryable<Bakery> PopulateBakeriesDropDownList();
    }
}
