using MongoDbAuditLog.MongoDbRepository.Models;

namespace Example.Entity
{
    public class Audit : BaseMongoDbEntity
    {
        public string Title { get; set; }
        public string Descr { get; set; }
    }
}
