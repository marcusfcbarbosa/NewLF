using System;
using MongoDB.Bson.Serialization.Attributes;

namespace LFV2.Infra.NoSQLContext.Documents
{
    public class BaseDocument 
    {
        [BsonElement]
        public Guid id { get; set; }
    }
}
