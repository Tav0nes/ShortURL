using Microsoft.EntityFrameworkCore;
using ShortURL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortURL.Services
{
    public class UrlListService
    {
        private readonly AppDbContext _context;

        public UrlListService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<URL>> OnListAsync()
        {
            return await _context.URLs.ToListAsync();
        }
    }
}
