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

        public bool Delete(string id)
        {
            var collectionContacts = Database.GetCollection<Contact>("contacts");

            try
            {           
                //Checks total contacts in the database
                var lstContacts = new List<Contact>();
                lstContacts = collectionContacts.Find(c => true).ToList();
                var totalContacts = lstContacts.Count;

                collectionContacts.DeleteOne(c => c.Id == id);

                //Checks if the total number of contacts in the database has become smaller after the delete command
                lstContacts.Clear();
                lstContacts = collectionContacts.Find(c => true).ToList();

                if (lstContacts.Count >= totalContacts)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(string id, Contact contact)
        {
            var collectionContacts = Database.GetCollection<Contact>("contacts");
            try
            {
                collectionContacts.ReplaceOne(c => c.Id == id, contact);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
