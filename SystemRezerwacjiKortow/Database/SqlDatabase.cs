using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlDatabase
    {
        public static int UserRoleId;   // numer roli użytkownik
        public static int CustomerAtr;  // atrapa customera
        public static int Timeout = 120;

        public static string connectionString;

        #region Konfiguracja bazy
        
        public static void BuildConnectionString()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            //stringBuilder.ConnectRetryCount = 1;
            stringBuilder.ConnectTimeout = Timeout;
            stringBuilder.Pooling = false;
            //stringBuilder.IntegratedSecurity = false;
            //stringBuilder.MultipleActiveResultSets = true; 

            stringBuilder.DataSource = ConfigurationManager.AppSettings["BazaDataSource"];
            stringBuilder.UserID = ConfigurationManager.AppSettings["BazaUserID"];
            stringBuilder.InitialCatalog = ConfigurationManager.AppSettings["BazaInitialCatalog"];
            stringBuilder.Password = ConfigurationManager.AppSettings["BazaPassword"];
            connectionString = stringBuilder.ToString();
        }

        public static bool OpenConnection(SqlConnection connection)
        {
            bool ret = false;

            try
            {
                connection.Open();
                ret = true;
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
                    default:
                        Console.WriteLine(ex.Message);
                        break;
                }
            }
            if (!ret) throw new Exception("Problem z połączeniem się z serverem SQL");
            return ret;
        }

        public static bool CloseConnection(SqlConnection connection)
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

        public static SqlConnection NewConnection()
        {
            return new SqlConnection(connectionString);
        } 
        #endregion

        #region Inicjalizacja zmiennych początkowych
        public static void init()
        {
            BuildConnectionString();
            SqlDatabase.UserRoleId = SqlDatabase.GetUserRoleId();
            SqlDatabase.CustomerAtr = SqlDatabase.GetCustomerAtr();
        }
        public static int GetUserRoleId()
        {
            SqlConnection connection = NewConnection();
            int result = -1;
            if (OpenConnection(connection))
            {
                var command = new SqlCommand("select RoleID from dbo.Role where RoleName='user'", connection);

                command.CommandTimeout = Timeout;

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["RoleID"];
                }
                else
                {
                    throw new System.ArgumentException("W tabeli dbo.Role brakuje roli o nazwie user");
                }

                CloseConnection(connection);                
            }
            return result;
        }

        public static int GetCustomerAtr()
        {
            SqlConnection connection = NewConnection();
            int result = -1;
            if (OpenConnection(connection))
            {
                var command = new SqlCommand("select CustomerID from dbo.Customer where CompanyName='Atrapa' and City='Atrapa'", connection);

                command.CommandTimeout = Timeout;

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["CustomerID"];
                }
                else
                {
                    throw new System.ArgumentException("W tabeli dbo.Customer brakuje klienta o nazwie firmy Atrapa i mieście Atrapa");
                }

                CloseConnection(connection);
            }
            return result;
        }
        #endregion

        // zwraca listę danych klientów
        public static List<Customer> GetCustomers()
        {
            SqlConnection connection = NewConnection();
            var list = new List<Customer>();
            if (OpenConnection(connection))
            {
                var command = new SqlCommand("select * from dbo.Customer", connection);
                command.CommandTimeout = Timeout;

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
                CloseConnection(connection);
            }
            return list;
        }
    }
}
