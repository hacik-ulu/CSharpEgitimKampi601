using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();

            var document = new BsonDocument
            {
                { "CustomerName", customer.CustomerName },
                { "CustomerSurname", customer.CustomerSurname },
                { "CustomerCity", customer.CustomerCity },
                { "CustomerBalance", customer.CustomerBalance },
                { "CustomerShoppingCount", customer.CustomerShoppingCount }
            };

            customerCollection.InsertOne(document);
        }
        public List<Customer> GetAllCustomers()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = customer["_id"].ToString(),
                    CustomerName = customer["CustomerName"].ToString(),
                    CustomerSurname = customer["CustomerSurname"].ToString(),
                    CustomerCity = customer["CustomerCity"].ToString(),
                    CustomerBalance = decimal.Parse(customer["CustomerBalance"].ToString()),
                    CustomerShoppingCount = int.Parse(customer["CustomerShoppingCount"].ToString()),
                });
            }
            return customerList;
        }
        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var deletedValue = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(deletedValue);
        }
        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);
            customerCollection.UpdateOne(filter, updatedValue);
            MessageBox.Show("Müşteri Güncelleme İşlemi Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var customer = customerCollection.Find(new BsonDocument { { "_id", ObjectId.Parse(id) } }).FirstOrDefault();
            MessageBox.Show("Müşteri Bilgileri Getirme İşlemi Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return new Customer
            {
                CustomerId = customer["_id"].ToString(),
                CustomerName = customer["CustomerName"].ToString(),
                CustomerSurname = customer["CustomerSurname"].ToString(),
                CustomerCity = customer["CustomerCity"].ToString(),
                CustomerBalance = decimal.Parse(customer["CustomerBalance"].ToString()),
                CustomerShoppingCount = int.Parse(customer["CustomerShoppingCount"].ToString()),
            };


        }

    }
}
