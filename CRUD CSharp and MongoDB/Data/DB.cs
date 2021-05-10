using CRUD_CSharp_and_MongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CSharp_and_MongoDB.Data
{
    public class DB
    {
        private static MongoClient Client = new MongoClient("mongodb://localhost:27017");
        private IMongoDatabase Database = Client.GetDatabase("contact_list");

        public bool Save(Contact contact)
        {
            var collectionContacts = Database.GetCollection<Contact>("contacts");

            try
            {
                collectionContacts.InsertOne(contact);
                return true;
            }
            catch
            {
                return false;
            }              
        }

        public List<Contact> GetContacts()
         {
            var collectionContacts = Database.GetCollection<Contact>("contacts");
            var lstContacts = new List<Contact>();

            lstContacts = collectionContacts.Find(c => true).ToList();
            return lstContacts;
        }

        public IMongoCollection<Contact> GetCollection()
        {
            return Database.GetCollection<Contact>("contacts");
        }
    }
}
