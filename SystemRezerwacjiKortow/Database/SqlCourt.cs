using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlCourt
    {
        // dodanie kortu lub jego modyfikacja
        // CourtID = 0 to dodawanie nowego kortu
        // CourtID > 0 modyfikowanie kortu o tym ID
        public static bool AddModifyCourt(Court court)
        {
            bool result = false;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("dbo.AddCourt", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourtNumber", court.CourtNumber);
                    command.Parameters.AddWithValue("@SurfaceType", court.SurfaceType);
                    command.Parameters.AddWithValue("@IsForDoubles", court.IsForDoubles);
                    command.Parameters.AddWithValue("@IsCovered", court.IsCovered);
                    command.Parameters.AddWithValue("@PriceH", court.PriceH);
                    command.Parameters.AddWithValue("@Name", court.Name);
                    command.Parameters.AddWithValue("@CourtID", court.CourtID);
                    command.Parameters["@CourtID"].Direction = ParameterDirection.Output;  // żeby móc wyciągać dane

                    command.CommandTimeout = SqlDatabase.Timeout;

                    // użyć jeżeli chcemy wykorzystać wartość return z procedury
                    //command.Parameters.Add("@ReturnValue", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;
                    try
                    {
                        command.ExecuteNonQuery();
                        court.CourtID = int.Parse(command.Parameters["@CourtID"].Value.ToString());
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

        // usuwanie kortu
        // sprawdzenie czy nie ma powiązań w innych tabelach jest po stronie bazy
        // przy wywołaniu dać w if'ie, żeby sprawdzić czy na pewno udało się usunąć
        public static bool DeleteCourt(Court court)
        {
            bool result = false;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("dbo.DeleteCourt", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourtID", court.CourtID);

                    command.CommandTimeout = SqlDatabase.Timeout;

                    try
                    {
                        command.ExecuteNonQuery();
                        court.CourtID = 0;
                        result = true;
                    }
                    catch (Exception ex)
                    {
                    }

                    SqlDatabase.CloseConnection(connection);
                }
            }                
            return result;
        }

        // zwraca listę kortów
        public static List<Court> GetCourts()
        {
            var list = new List<Court>();
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("select * from dbo.VCourt", connection);
                    command.CommandTimeout = SqlDatabase.Timeout;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Court()
                        {
                            CourtID = (int)reader["CourtID"],
                            CourtNumber = (int)reader["CourtNumber"],
                            SurfaceType = (string)reader["SurfaceType"],
                            IsForDoubles = (bool)reader["IsForDoubles"],
                            IsCovered = (bool)reader["IsCovered"],
                            PriceH = (decimal)reader["PriceH"],
                            Name = (string)reader["Name"]
                        });
                    }
                    SqlDatabase.CloseConnection(connection);
                }
            }       
            return list;
        }
    }
}