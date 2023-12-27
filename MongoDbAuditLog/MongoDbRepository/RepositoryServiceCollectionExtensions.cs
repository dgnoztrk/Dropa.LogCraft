using Microsoft.Extensions.DependencyInjection;
using MongoDbAuditLog.AuditLogRepository.Abstract;
using MongoDbAuditLog.AuditLogRepository.Concrete;
using MongoDbAuditLog.MongoDbRepository.Abstract;
using MongoDbAuditLog.MongoDbRepository.Concrete;

namespace MongoDbAuditLog.MongoDbRepository
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static void AddMongoDbAuditLogRepository<T>(this IServiceCollection services) where T : class
        {
            services.AddSingleton<IRepository<T>, Repository<T>>();
            services.AddSingleton<IAuditLogRepository<T>, AuditLogRepository<T>>();
        }
    }
}