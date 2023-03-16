using ShirtStoreWebsite.Models;
using System.Collections.Generic;

namespace ShirtStoreWebsite.Services
{
    public interface IShirtRepository
    {
        IEnumerable<Shirt> GetShirts();
        bool AddShirt(Shirt shirt);
        bool RemoveShirt(int id);
    }
}
