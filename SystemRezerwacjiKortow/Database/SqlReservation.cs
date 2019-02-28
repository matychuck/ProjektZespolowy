using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public class SqlReservation
    {
        // sprawdzanie zajętości kortu w danym dniu lub godzinie
        // courtID - id kortu, którego zajętość ma być sprawdzona
        // testDate - data, której zajętość ma być sprawdzona
        // testHour - godzina, której zajętość ma być sprawdzona
        // jeśli testHour zosatnie podane 0, to będzie sprawdzana zajętość całego dnia, a nie konkretnej godziny
       
        public static int GetReservationStateCourt(int courtID, DateTime testDate, int testHour)
        {
            int result = -1;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("dbo.GetStateReservationCourt", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CourtID", courtID);
                    command.Parameters.AddWithValue("@Date", testDate);
                    command.Parameters.AddWithValue("@Hour", testHour);
                    command.CommandTimeout = SqlDatabase.Timeout;

                    // użyć jeżeli chcemy wykorzystać wartość return z procedury
                    command.Parameters.Add("@ReturnValue", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }

                    // użyć jeżeli chcemy wykorzystać wartość return z procedury
                    result = int.Parse(command.Parameters["@ReturnValue"].Value.ToString());

                    SqlDatabase.CloseConnection(connection);
                }
            }
            return result;
        }
    }
}