
using Microsoft.EntityFrameworkCore;

using IPStackApi.Models;

namespace IPStackApi.Context {

    public class AppDBContext : DbContext 
    {
        public DbSet<IPDetailsEntity> IPDetailsEntity {get; set;}

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }
    }
}