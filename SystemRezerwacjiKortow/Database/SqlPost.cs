using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemRezerwacjiKortow.Models;

namespace SystemRezerwacjiKortow.Database
{
    public static class SqlPost
    {
        // dodanie postu przez administratora
        // data dodania uzupełniana automatycznie 
        public static void InsertPost(Post post)
        {
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand
                        ("INSERT INTO Post (TitlePL, TitleEN, TitleDE, DescriptionPL, DescriptionEN, DescriptionDE) " +
                        "VALUES (@TitlePL, @TitleEN, @TitleDE, @DescriptionPL, @DescriptionEN, @DescriptionDE) " +
                        "SELECT @@IDENTITY as PostID"
                        , connection);
                    command.Parameters.AddWithValue("@TitlePL", post.TitlePL);
                    command.Parameters.AddWithValue("@TitleEN", post.TitleEN);
                    command.Parameters.AddWithValue("@TitleDE", post.TitleDE);
                    command.Parameters.AddWithValue("@DescriptionPL", post.DescriptionPL);
                    command.Parameters.AddWithValue("@DescriptionEN", post.DescriptionEN);
                    command.Parameters.AddWithValue("@DescriptionDE", post.DescriptionDE);

                    command.CommandTimeout = SqlDatabase.Timeout;
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var tmp = reader["PostID"];
                        post.PostID = int.Parse(tmp.ToString());

                    }
                    SqlDatabase.CloseConnection(connection);
                }
            }
        }

        // edycja postu przez administratora
        // data uzupełniana automatycznie 
        // PostID -> id edytowanego postu
        // post -> post zawierający wyedytowane dane
        public static bool UpdatePost(int postID, Post post)
        {
            bool result = false;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand
                        ("UPDATE Post SET TitlePL = @TitlePL, TitleEN = @TitleEN, TitleDE = @TitleDE, " +
                            "DescriptionPL = @DescriptionPL, DescriptionEN = @DescriptionEN, DescriptionDE = @DescriptionDE" +
                            "   WHERE PostID = @PostID", connection);
                    command.Parameters.AddWithValue("@PostID", postID);
                    command.Parameters.AddWithValue("@TitlePL", post.TitlePL);
                    command.Parameters.AddWithValue("@TitleEN", post.TitleEN);
                    command.Parameters.AddWithValue("@TitleDE", post.TitleDE);
                    command.Parameters.AddWithValue("@DescriptionPL", post.DescriptionPL);
                    command.Parameters.AddWithValue("@DescriptionEN", post.DescriptionEN);
                    command.Parameters.AddWithValue("@DescriptionDE", post.DescriptionDE);

                    try
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return result;
        }

        // zwraca listę postów posortowanych malejąco (najnowsze u góry)
        public static List<Post> GetPosts()
        {
            var list = new List<Post>();
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("select * from dbo.VPost order by DateOfInsert desc", connection);
                    command.CommandTimeout = SqlDatabase.Timeout;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Post()
                        {
                            PostID = (int)reader["PostID"],
                            TitlePL = (string)reader["TitlePL"],
                            TitleEN = (string)reader["TitleEN"],
                            TitleDE = (string)reader["TitleDE"],
                            DescriptionPL = (string)reader["DescriptionPL"],
                            DescriptionEN = (string)reader["DescriptionEN"],
                            DescriptionDE = (string)reader["DescriptionDE"],
                            DateOfInsert = (DateTime)reader["DateOfInsert"]
                        });
                    }
                    SqlDatabase.CloseConnection(connection);
                }
            }
            return list;
        }

        // zwraca konkretny post
        public static Post GetPost(int postID)
        {
            Post post = null;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand("SELECT * FROM VPost WHERE PostID = @PostID", connection);
                    command.Parameters.AddWithValue("@PostID", postID);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        post = new Post()
                        {
                            PostID = (int)reader["PostID"],
                            TitlePL = (string)reader["TitlePL"],
                            TitleEN = (string)reader["TitleEN"],
                            TitleDE = (string)reader["TitleDE"],
                            DescriptionPL = (string)reader["DescriptionPL"],
                            DescriptionEN = (string)reader["DescriptionEN"],
                            DescriptionDE = (string)reader["DescriptionDE"],
                            DateOfInsert = (DateTime)reader["DateOfInsert"]
                        };
                    }
                    SqlDatabase.CloseConnection(connection);
                }
            }
            return post;
        }

        // usuwanie posta o danym id
        public static bool DeletePost(int postID)
        {
            bool result = false;
            using (SqlConnection connection = SqlDatabase.NewConnection())
            {
                if (SqlDatabase.OpenConnection(connection))
                {
                    var command = new SqlCommand
                        ("DELETE FROM Post WHERE PostID = @PostID", connection);
                    command.Parameters.AddWithValue("@PostID", postID);

                    try
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return result;
        }
    }
}