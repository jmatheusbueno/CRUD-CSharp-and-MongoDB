using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CSharp_and_MongoDB.Models
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]        
        public string Name { get; set; }

        [BsonElement("mail")]
        public string Mail { get; set; }

        [BsonElement("number")]
        public string Number { get; set; }
    }
}
