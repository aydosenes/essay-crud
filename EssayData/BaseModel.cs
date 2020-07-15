using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Essay.MongoDb
{
    public abstract class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    } 
}
