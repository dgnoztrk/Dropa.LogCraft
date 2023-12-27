### LogCraft

##### Use of
###### appsettings.json
```
 "MongoDbSettings": {
   "ConnectionString": "localhost:27017",
   "Database": "TestDatabase"
 }
```
###### Entity
```
 public class Audit : BaseMongoDbEntity
 {
     public string Title { get; set; }
     public string Descr { get; set; }
 }
```
###### BaseEntity inheritable
```
public abstract class BaseMongoDbEntity
{
    public BaseMongoDbEntity()
    {
        CreatedDate = DateTime.Now;
    }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    [BsonElement(Order = 0)]
    public ObjectId ID { get; } = ObjectId.GenerateNewId();

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime CreatedDate { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement(Order = 101)]
    public DateTime? ModifiedDate { get; set; }
}
```

###### program.cs
```
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddMongoDbAuditLogRepository<Audit>();
```

###### controller
```
private readonly IAuditLogRepository<Audit> _auditLogRepository;

public Controller(IAuditLogRepository<Audit> auditLogRepository)
{
    _auditLogRepository = auditLogRepository;
}
```

###### in method or logging middleware
```
  await _auditLogRepository.Add(new Audit()
  {
      Descr = "Test Descr",
      Title = "Test Title"
  });
```


#### Happy Code !
