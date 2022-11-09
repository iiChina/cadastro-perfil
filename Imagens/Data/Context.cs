using Imagens.Models;
using Microsoft.EntityFrameworkCore;

namespace Imagens.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Perfil> Perfis { get; set; }
    }
}
