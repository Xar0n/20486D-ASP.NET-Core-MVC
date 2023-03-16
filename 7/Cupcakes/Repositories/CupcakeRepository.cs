using Cupcakes.Data;
using Cupcakes.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cupcakes.Repositories
{
    public class CupcakeRepository : ICupcakeRepository
    {
        private CupcakeContext _context;

        public CupcakeRepository(CupcakeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cupcake>> GetCupcakes()
        {
            return await _context.Cupcakes.ToListAsync();
        }

        public async Task<Cupcake> GetCupcakeById(int id)
        {
            return await _context.Cupcakes.Include(b => b.Bakery)
                .SingleOrDefaultAsync(c => c.CupcakeId == id);
        }

        public async Task CreateCupcake(Cupcake cupcake)
        {
            if (cupcake.PhotoAvatar != null && cupcake.PhotoAvatar.Length > 0)
            {
                cupcake.ImageMimeType = cupcake.PhotoAvatar.ContentType;
                cupcake.ImageName = Path.GetFileName(cupcake.PhotoAvatar.FileName);
                using (var memoryStream = new MemoryStream())
                {
                    cupcake.PhotoAvatar.CopyTo(memoryStream);
                    cupcake.PhotoFile = memoryStream.ToArray();
                }
                _context.Add(cupcake);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCupcake(int id)
        {
            var cupcake = _context.Cupcakes.SingleOrDefault(c => c.CupcakeId == id);
            _context.Cupcakes.Remove(cupcake);
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<Bakery> PopulateBakeriesDropDownList()
        {
            var bakeriesQuery = from b in _context.Bakeries
                                orderby b.BakeryName
                                select b;
            return bakeriesQuery;
        }
    }
}
