using Microsoft.EntityFrameworkCore;
using PEPRN231_SU24_009909_Repo.Models;


namespace PEPRN231_SU24_009909_Repo.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly EnglishPremierLeague2024DBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository()
        {
            _context = new EnglishPremierLeague2024DBContext();
            _dbSet = _context.Set<T>();
        }

		public IQueryable<T> Entities => _context.Set<T>();

		public async Task<IList<T>> Get(Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetById(string id, string pk, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, pk) == id);
        }

        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<PremierLeagueAccount?> Login(string email, string password)
        {
            return await _context.PremierLeagueAccounts.SingleOrDefaultAsync(s => s.EmailAddress == email && s.Password == password);
        }

    }
}
