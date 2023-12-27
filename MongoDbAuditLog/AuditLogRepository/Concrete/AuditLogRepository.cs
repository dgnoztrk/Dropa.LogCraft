using MongoDbAuditLog.AuditLogRepository.Abstract;
using MongoDbAuditLog.MongoDbRepository.Abstract;
using System.Linq.Expressions;

namespace MongoDbAuditLog.AuditLogRepository.Concrete
{
    public class AuditLogRepository<T> : IAuditLogRepository<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public AuditLogRepository(IRepository<T> repo)
        {
            _repository = repo;
        }

        public async ValueTask DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async ValueTask<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async ValueTask<List<T>> GetAllAsync(int page = 1, int pageSize = 10, Sort sort = Sort.Descending, string sortBy = "CreatedDate", Expression<Func<T, bool>> filterExpression = null)
        {
            return await _repository.GetAllAsync(page, pageSize, sort, sortBy, filterExpression);
        }

        public async ValueTask<List<T>> GetAllAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            return await _repository.GetAllAsync(filterExpression);
        }

        public async ValueTask<T> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async ValueTask Add(T entity)
        {
            await _repository.InsertAsync(entity);
        }
    }
}
