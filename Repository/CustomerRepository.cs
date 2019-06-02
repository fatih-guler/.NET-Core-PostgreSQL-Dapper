using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using NetCore_Dapper.Models;
using Npgsql;

namespace NetCore_Dapper.Repository
{
    public class CustomerRepository : IRepository<Customer>
    {
        private string connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DbInfo:ConnectionString"); // Getting connection string
        }
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
        public void Add(Customer item)
        {
           using (IDbConnection dbConnection = Connection)
           {
               dbConnection.Open();
               dbConnection.Execute("INSERT INTO customer(name, phone, email, address) VALUES(@Name,@Phone,@Email,@Address)", item);
           }
        }

        public IEnumerable<Customer> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Customer>("SELECT * FROM customer");
            }
        }

        public Customer FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Customer>("SELECT * FROM customer WHERE Id=@Id", new {Id = id});
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM customer WHERE Id=@Id", new {Id = id});
            }
        }

        public void Update(Customer item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name=@Name, phone=@Phone, email=@Email, address=@Address WHERE id=@Id", item);                
            }
        }
    }
}