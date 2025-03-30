using PEPRN231_SU24_009909_Repo.Models;

namespace PEPRN231_SU24_009909_Repo.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
		IQueryable<T> Entities { get; }
		Task<T?> GetById(string id, string pk, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<IList<T>> Get(Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<PremierLeagueAccount> Login(string email, string password);

    }
}
