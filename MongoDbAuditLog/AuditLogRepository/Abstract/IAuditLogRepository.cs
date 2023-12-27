using System.Linq.Expressions;

namespace MongoDbAuditLog.AuditLogRepository.Abstract
{
    public interface IAuditLogRepository<T> where T : class
    {
        /// <summary>
        /// Asynchronously retrieves all data with pagination, sorting, and optional filtering.
        /// </summary>
        /// <param name="page">Page number for pagination.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="sortBy">Field name for sorting.</param>
        /// <param name="sortOrder">Sorting order ("asc" or "desc").</param>
        /// <param name="filterExpression">Optional filter expression for data retrieval.</param>
        /// <returns>Collection of objects from the database.</returns>

        ValueTask<List<T>> GetAllAsync();
        /// <summary>
        /// Asynchronously retrieves all data with pagination, sorting, and optional filtering.
        /// </summary>
        /// <param name="page">Page number for pagination.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="sortBy">Field name for sorting.</param>
        /// <param name="sort">Sorting order ("asc" or "desc").</param>
        /// <param name="filterExpression">Optional filter expression for data retrieval.</param>
        /// <returns>Collection of objects from the database.</returns>
        ValueTask<List<T>> GetAllAsync(int page = 1, int pageSize = 10, Sort sort = Sort.Descending, string sortBy = "CreatedDate", Expression<Func<T, bool>> filterExpression = null);
        /// <summary>
        /// Asynchronously retrieves all data with optional filtering.
        /// </summary>
        /// <param name="filterExpression">Optional filter expression for data retrieval.</param>
        /// <returns>Collection of objects from the database.</returns>
        ValueTask<List<T>> GetAllAsync(Expression<Func<T, bool>> filterExpression = null);
        /// <summary>
        /// Asynchronously retrieves an object by its ID.
        /// </summary>
        /// <param name="id">ID of the object to retrieve.</param>
        /// <returns>The object with the specified ID or null.</returns>
        ValueTask<T> GetByIdAsync(string id);
        /// <summary>
        /// Asynchronously inserts a new object.
        /// </summary>
        /// <param name="entity">The object to be inserted.</param>
        ValueTask Add(T entity);
        /// <summary>
        /// Asynchronously deletes an object with a specified ID.
        /// </summary>
        /// <param name="id">ID of the object to be deleted.</param>
        ValueTask DeleteAsync(string id);
    }
}