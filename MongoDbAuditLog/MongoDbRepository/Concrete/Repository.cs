using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbAuditLog.MongoDbRepository.Abstract;
using System.Linq.Expressions;
using MongoDbAuditLog.MongoDbRepository.Models;

namespace MongoDbAuditLog.MongoDbRepository.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var db = client.GetDatabase(options.Value.Database);
            _collection = db.GetCollection<T>($"{typeof(T).Name.ToLowerInvariant()}Logs");
        }

        public async ValueTask<List<T>> GetAllAsync()
        {
            var result = await _collection.Find(_ => true).ToListAsync();
            return result;
        }

        public async ValueTask<T> GetByIdAsync(string id)
        {
            var result = await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
            return result;
        }

        public async ValueTask InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async ValueTask DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
        }

        public async ValueTask<List<T>> GetAllAsync(int page = 1, int pageSize = 10, Sort sort = Sort.Descending, string sortBy = "CreatedDate", Expression<Func<T, bool>> filterExpression = null)
        {
            var query = _collection.Find(filterExpression ?? Builders<T>.Filter.Empty);
            SortDefinition<T> sortDefinition = sort == Sort.Ascending ? Builders<T>.Sort.Ascending(sortBy) : Builders<T>.Sort.Descending(sortBy);
            query = query.Sort(sortDefinition);
            return await query
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async ValueTask<List<T>> GetAllAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            var query = _collection.Find(filterExpression ?? Builders<T>.Filter.Empty);
            SortDefinition<T> sortDefinition = Builders<T>.Sort.Ascending("CreatedDate");
            query = query.Sort(sortDefinition);
            return await query.ToListAsync();
        }
    }
}