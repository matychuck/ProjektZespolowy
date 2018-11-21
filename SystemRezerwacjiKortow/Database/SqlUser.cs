using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlUser
    {
        // dodanie użytkownika - rejestracja
        // przy wywolywaniu funckji w rejestracji 
        // pod user.RoleID trzeba podstawić SqlDatabase.UserRoleID
        // pod user.CustomerID trzeba podstawić SqlDatabase.CustomerAtr
        public static void InsertUser(User user)
        {
            if (SqlDatabase.OpenConnection())
            {              
                var command = new SqlCommand
                    ("INSERT INTO [User] (FirstName, Surname, Email, DateOfBirth, Password, IsEmailVeryfied, RoleID, CustomerID, ActivationCode) " +
                    "VALUES (@name, @surname, @email, @birth, @password, @veryfied, @role, @customer, @code) " +
                    "SELECT @@IDENTITY as UserID"
                    , SqlDatabase.connection);
                command.Parameters.AddWithValue("@name", user.FirstName);
                command.Parameters.AddWithValue("@surname", user.Surname);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@birth", user.DateOfBirth);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@veryfied", user.IsEmailVeryfied);
                command.Parameters.AddWithValue("@role", user.RoleID);
                command.Parameters.AddWithValue("@customer", user.CustomerID);
                command.Parameters.AddWithValue("@code", user.ActivationCode);

                command.CommandTimeout = SqlDatabase.Timeout;
                var result = command.ExecuteReader();
                if (result.Read())
                {
                    var tmp = result["UserID"];
                    user.UserID = int.Parse(tmp.ToString());
                    //var tmp = reader.GetValue(0);
                   // position = int.Parse(tmp.ToString()) + 1;
                }

                SqlDatabase.CloseConnection();
            }
        }

        // sprawdzenie czy użytkownik o danym adresie email już istnieje
        public static bool CheckUserExists(string email)
        {
            bool exists = false;
            if (SqlDatabase.OpenConnection())
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE Email = @email", SqlDatabase.connection);
                command.Parameters.AddWithValue("@email", email);

                command.CommandTimeout = SqlDatabase.Timeout;

                var result = command.ExecuteReader();
                while (result.Read())
                {
                    if (result.GetInt32(0) > 0) exists = true;
                }
                SqlDatabase.CloseConnection();
            }

            return exists;
        }

        // sprawdzanie czy kod aktywacyjny istnieje - do weryfikacji użytkownika
        public static bool FindActivationCode(string code)
        {
            bool exists = false;
            if (SqlDatabase.OpenConnection())
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE ActivationCode = @code", SqlDatabase.connection);
                command.Parameters.AddWithValue("@code", code);

                command.CommandTimeout = SqlDatabase.Timeout;

                var result = command.ExecuteReader();
                while (result.Read())
                {
                    if (result.GetInt32(0) > 0) exists = true;
                }
                SqlDatabase.CloseConnection();
            }
            return exists;
        }

        
        // sprawdzenie czy użytkownik potwierdził swój adres email (wszedł w link aktywacyjny przesłany mailem)
        // do logowania użytkownika
        public static bool CheckEmailVeryfied(UserLogin login)
        {
            bool exists = false;

            if (SqlDatabase.OpenConnection())
            {
                var command = new SqlCommand("SELECT * FROM [User] WHERE Email = @email and IsEmailVeryfied = 1", SqlDatabase.connection);
                command.Parameters.AddWithValue("@email", login.Email);

                command.CommandTimeout = SqlDatabase.Timeout;

                var result = command.ExecuteReader();
                while (result.Read())
                {
                    if (result.GetInt32(0) > 0) exists = true;
                }
                SqlDatabase.CloseConnection();
            }
            return exists;
        }


        // email użytkownika został zweryfikowany - uzytkownik nacisnął link w mailu
        public static void UpdateUserEmailVeryfied(string code)
        {
            if (SqlDatabase.OpenConnection())
            {
                var command = new SqlCommand("UPDATE [User] SET IsEmailVeryfied = 1  WHERE ActivationCode = @code", SqlDatabase.connection);
                command.Parameters.AddWithValue("@code", code);

                command.CommandTimeout = SqlDatabase.Timeout;

                command.ExecuteNonQuery();
                SqlDatabase.CloseConnection();
            }
        }


        // zwraca hasło użytkownika o podanym adresie email
        public static string GetUserPassword(UserLogin login)
        {
            string password = "";
            if (SqlDatabase.OpenConnection())
            {
                var command = new SqlCommand("SELECT Password FROM [User] WHERE Email = @email", SqlDatabase.connection);
                command.Parameters.AddWithValue("@email", login.Email);

                command.CommandTimeout = SqlDatabase.Timeout;

                var result = command.ExecuteReader();
                while (result.Read())
                {
                    password = result.GetString(0);
                }
                SqlDatabase.CloseConnection();
            }
            return password;
        }

        // dodanie danych użytkownika lub ich modyfikacja
        public static void AddAddress(Customer customer, User user)
        {
            if (SqlDatabase.OpenConnection())
            {
                var command = new SqlCommand("dbo.AddAddress", SqlDatabase.connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                command.Parameters.AddWithValue("@City", customer.City);
                command.Parameters.AddWithValue("@Street", customer.Street);
                command.Parameters.AddWithValue("@ZipCode", customer.ZipCode);
                command.Parameters.AddWithValue("@DiscountValue", customer.DiscountValue);
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

                command.CommandTimeout = SqlDatabase.Timeout;

                // użyć jeżeli chcemy wykorzystać wartość return z procedury
                //command.Parameters.Add("@ReturnValue", SqlDbType.Int, 4).Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();

                customer.CustomerID = int.Parse(command.Parameters["@CustomerID"].Value.ToString());

                // użyć jeżeli chcemy wykorzystać wartość return z procedury
                //customer.CustomerID = int.Parse(command.Parameters["@ReturnValue"].Value.ToString());

                SqlDatabase.CloseConnection();
            }
        }

    }
}