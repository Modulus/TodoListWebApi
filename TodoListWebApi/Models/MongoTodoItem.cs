using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListWebApi.Models
{
    [BsonDiscriminator("todo")]
    public class MongoTodoItem
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("text")]
        public string Description { get; set; }

        [BsonElement("done")]
        public bool Done { get; set; }
    }
}