using DoaiApi.Models;
using Microsoft.EntityFrameworkCore;


namespace DoaiApi.Data
{
    public class InstituicaoContext : DbContext
    {
        public InstituicaoContext(DbContextOptions<InstituicaoContext> opt) : base(opt)
        {

        }

        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<NotaFiscal> NotaFiscal { get; set; }

    }
}
