using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlAdvertisement
    {
        // dodanie reklamy lub jej modyfikacja
        public static bool AddModifyAdvertisement(Advertisement advertisement)
        {
            bool result = false;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("dbo.AddAdvertisement", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", advertisement.Name);
                    command.Parameters.AddWithValue("@DateFrom", advertisement.DateFrom);
                    command.Parameters.AddWithValue("@DateTo", advertisement.DateTo);
                    command.Parameters.AddWithValue("@CourtID", advertisement.CourtID);
                    command.Parameters.AddWithValue("@Payment", advertisement.Payment);
                    command.Parameters.AddWithValue("@UserID", advertisement.UserID);

                    command.CommandTimeout = SqlDatabase.Timeout;

                    // użyć jeżeli chcemy wykorzystać wartość return z procedury
                    //command.Parameters.Add("@ReturnValue", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;
                    try
                    {
                        command.ExecuteNonQuery();   
                        result = true;
                    }
                    catch (Exception ex)
                    {
                    }

                    // użyć jeżeli chcemy wykorzystać wartość return z procedury
                    //customer.CustomerID = int.Parse(command.Parameters["@ReturnValue"].Value.ToString());

                    SqlDatabase.CloseConnection(connection);
                }
            }
            return result;
        }

        // usuwanie reklamy
        public static bool DeleteAdvertisement(Advertisement advertisement, int userID)
        {
            bool result = false;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("dbo.DeleteAdvertisement", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DateFrom", advertisement.DateFrom);
                    command.Parameters.AddWithValue("@DateTo", advertisement.DateTo);
                    command.Parameters.AddWithValue("@CourtID", advertisement.CourtID);
                    command.Parameters.AddWithValue("@UserIDDeleting", userID);

                    command.CommandTimeout = SqlDatabase.Timeout;

                    // użyć jeżeli chcemy wykorzystać wartość return z procedury
                    //command.Parameters.Add("@ReturnValue", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;
                    try
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                    }

                    // użyć jeżeli chcemy wykorzystać wartość return z procedury
                    //customer.CustomerID = int.Parse(command.Parameters["@ReturnValue"].Value.ToString());

                    SqlDatabase.CloseConnection(connection);
                }
            }
            return result;
        }

        // zwraca listę reklam
        public static List<Advertisement> GetAdvertisements(int userID)
        {
            var list = new List<Advertisement>();
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("dbo.GetAdvertisements", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = SqlDatabase.Timeout;
                    command.Parameters.AddWithValue("@UserID", userID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Advertisement()
                        {
                            Name = (string)reader["Name"],
                            DateFrom = (DateTime)reader["DateFrom"],
                            DateTo = (DateTime)reader["DateTo"],
                            CourtID = (int)reader["CourtID"],
                            Payment = (decimal)reader["Payment"],
                            UserID = (int)reader["UserID"],
                            CourtNumber = (int)reader["CourtNumber"],
                            CourtName = (string)reader["CourtName"],
                            UserName = (string)reader["UserName"],
                            Email = (string)reader["Email"]
                        });
                    }
                    SqlDatabase.CloseConnection(connection);
                }
            }
            return list;
        }

        public static Advertisement GetAdvertisement(DateTime dateFrom, DateTime dateTo, int courtID)
        {
            Advertisement advertisement = null;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("SELECT * FROM VAdvertisement WHERE DateFrom = @dateFrom and DateTo = @dateTo and CourtID = @courtID", connection);
                    command.Parameters.AddWithValue("@dateFrom", dateFrom);
                    command.Parameters.AddWithValue("@dateTo", dateTo);
                    command.Parameters.AddWithValue("@courtID", courtID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        advertisement = new Advertisement()
                        {
                            Name = (string)reader["Name"],
                            DateFrom = (DateTime)reader["DateFrom"],
                            DateTo = (DateTime)reader["DateTo"],
                            CourtID = (int)reader["CourtID"],
                            Payment = (decimal)reader["Payment"],
                            UserID = (int)reader["UserID"],
                            CourtNumber = (int)reader["CourtNumber"],
                            CourtName = (string)reader["CourtName"],
                            UserName = (string)reader["UserName"],
                            Email = (string)reader["Email"]
                        };
                    }
                    SqlDatabase.CloseConnection(connection);
                }
            }
            return advertisement;
        }
    }
}