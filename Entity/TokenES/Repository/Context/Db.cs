using Microsoft.EntityFrameworkCore;
using ReyBanPac.ModeloCanonico.Model;

namespace ReyBanPac.TokenES.Repository.Context
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {

        }
        public DbSet<TokenModel> Models => Set<TokenModel>();
    }
}
