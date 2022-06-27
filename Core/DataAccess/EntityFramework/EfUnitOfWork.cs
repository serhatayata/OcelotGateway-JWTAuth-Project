using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext, new()
    {
        private readonly TContext _db;

        public EfUnitOfWork()
        {
            _db = new TContext();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public DbContext Context()
        {
            return _db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
