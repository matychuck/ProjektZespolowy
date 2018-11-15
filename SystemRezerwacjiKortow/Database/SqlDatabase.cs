using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlDatabase
    {
        static SqlConnection connection = Initialize();
        
        public static SqlConnection Initialize()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.Pooling = false;
            stringBuilder.DataSource = ConfigurationManager.AppSettings["BazaDataSource"];
            stringBuilder.UserID = ConfigurationManager.AppSettings["BazaUserID"];
            stringBuilder.InitialCatalog = ConfigurationManager.AppSettings["BazaInitialCatalog"];
            stringBuilder.Password = ConfigurationManager.AppSettings["BazaPassword"];
            Console.WriteLine(stringBuilder.ToString());
            return new SqlConnection(stringBuilder.ToString());
        }

        public static bool OpenConnection()
        {
            if (connection == null)
                Initialize();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Number);
                Console.WriteLine(ex.Message);
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        public static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Number);
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /*
        public static void InsertTest()
        {
            if (OpenConnection())
            {
                var command = new SqlCommand
                    ("INSERT INTO Test (Nazwa) VALUES ('aga')", connection);

                command.ExecuteNonQuery();

                CloseConnection();
            }
        }*/

        // zwraca listę danych klientów
        public static List<Customer> GetCustomers()
        {     
            var list = new List<Customer>();
            if (OpenConnection())
            {
                var command = new SqlCommand("select * from dbo.Customer", connection);
                //command.Parameters.AddWithValue("@number", number);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Customer()
                    {
                        CustomerID = (int)reader["CustomerID"],
                        CompanyName = (string)reader["CompanyName"],
                        City = (string)reader["City"],
                        Street = (string)reader["Street"],
                        ZipCode = (string)reader["ZipCode"],
                        DiscountValue = (decimal)reader["DiscountValue"],
                    });
                }
                CloseConnection();
            }
            return list;
        }
    }
}