using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public class SqlDatabase
    {
        static SqlConnection connection = Initialize();

        public static SqlConnection Initialize()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.Pooling = false;
            stringBuilder.DataSource = "pocztabmg.pl";
            stringBuilder.UserID = "01167725_0000001";
            stringBuilder.InitialCatalog = "01167725_0000001";
            stringBuilder.Password = "t59Go:SwHYyT";
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
        #region Lists
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

        // zwraca listę danych kortów
        public static List<Court> GetCourtsList()
        {
            var list = new List<Court>();
            //if (OpenConnection())
            //{
            //    var command = new SqlCommand("select * from dbo.Court", connection);
            //    //command.Parameters.AddWithValue("@number", number);
            //    var reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        list.Add(new Court()
            //        {
            //            CourtID = (int)reader["CourtID"],
            //            CourtNumber = (int)reader["CourtNumber"],
            //            SurfaceType = (string)reader["SurfaceType"],
            //            IsForDoubles = (byte)reader["IsForDoubles"],
            //            IsCovered = (byte)reader["IsCovered"],
            //            PriceH = (decimal)reader["PriceH"],
            //            Name = (string)reader["Name"],
            //        });
            //    }
            //    CloseConnection();
            //}
            return list;
        }

        public static List<ComplexCourt> GetComplexes()
        {
            var list = new List<ComplexCourt>();
            //if (OpenConnection())
            //{
            //    var command = new SqlCommand("select * from dbo.Gear", connection);
            //    //command.Parameters.AddWithValue("@number", number);
            //    var reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        list.Add(new Gear()
            //        {
            //            GearID = (int)reader["GearID"],
            //            PriceH = (decimal)reader["PriceH"],
            //            Name = (string)reader["Name"],
            //            Amount = (int)reader["Amount"],
            //        });
            //    }
            //    CloseConnection();
            //}
            return list;
        }

        //Get gear list
        public static List<Gear> GetGears()
        {
            var list = new List<Gear>();
            if (OpenConnection())
            {
                var command = new SqlCommand("select * from dbo.Gear", connection);
                //command.Parameters.AddWithValue("@number", number);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Gear()
                    {
                        GearID = (int)reader["GearID"],
                        PriceH = (decimal)reader["PriceH"],
                        Name = (string)reader["Name"],
                        Amount = (int)reader["Amount"],
                    });
                }
                CloseConnection();
            }
            return list;
        }

        // zwraca listę użytkowników
        public static List<User> GetUsers()
        {
            var list = new List<User>();
            if (OpenConnection())
            {
                var command = new SqlCommand("select * from dbo.User", connection);
                //command.Parameters.AddWithValue("@number", number);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new User()
                    {
                        UserID = (int)reader["userID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["Surname"],
                        Email = (string)reader["Email"],
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        //IsEmailVeryfied = (bool)reader["IsEmailVerified"],
                        RoleID = (int)reader["RoleID"],
                        CustomerID = (int)reader["CustomerID"],
                    });
                }
                CloseConnection();
            }
            return list;
        }
        #endregion

        #region Insert
        public static void InsertCourt(Court court)
        {
            if (OpenConnection())
            {
                //var command = new SqlCommand
                //    ("INSERT INTO Book (Author, Title, ISBN, Category) " +
                //    "VALUES (@author,@title,@isbn,@category)", connection);
                //command.Parameters.AddWithValue("@author", court.Author);
                //command.Parameters.AddWithValue("@title", court.Title);
                //command.Parameters.AddWithValue("@isbn", court.ISBN);
                //command.Parameters.AddWithValue("@category", cour.Category);

                //command.ExecuteNonQuery();

                CloseConnection();
            }
        }
        #endregion

    }
}