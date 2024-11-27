
using Microsoft.EntityFrameworkCore;

using IPStackApi.Models;
using IPStackApi.Context;

namespace IPStackApi.Repositories
{
    public class IPDetailsRepository
    {
        private readonly AppDBContext _context;
        public IPDetailsRepository(AppDBContext context) 
        {   
            _context = context;
        }

        public async Task<IPDetailsEntity?> FindByIp(string Ip)
        {
            return await _context.IPDetailsEntity.FindAsync(Ip);
        }

        public async Task Create(IPDetailsEntity entity) {
            await _context.IPDetailsEntity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}