using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieData.Models
{
    public class MovieDataAccess
    {
        string connectionString = "Server=FSIND-LT-11;Database=DbContext;Trusted_Connection=True";

        public IEnumerable<Movie> GetAllMovie()
        {
            List<Movie> lstMovie = new List<Movie>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllMovie ", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Movie movie = new Movie();

                    movie.ID = Convert.ToInt32(rdr["ID"]);
                    movie.Title = rdr["Title"].ToString();
                    movie.Genre = rdr["Genre"].ToString();
                    movie.ReleseDate = rdr["ReleseDate"].ToString();
                    
                    lstMovie.Add(movie);
                }
                con.Close();
            }
            return lstMovie;
        }

        public void AddMovie(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SpMovie1", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title",movie.Title);
                cmd.Parameters.AddWithValue("@Genre",movie.Genre);
                cmd.Parameters.AddWithValue("@ReleseDate",movie.ReleseDate);
                

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateMovie(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", movie.ID);
                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@Genre", movie.Genre);
                cmd.Parameters.AddWithValue("@ReleseDate", movie.ReleseDate);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Movie GetMovieData(int? id)
        {

            Movie movie = new Movie();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM MvcMovieContext WHERE ID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    movie.ID = Convert.ToInt32(rdr["ID"]);
                    movie.Title = rdr["Title"].ToString();
                    movie.Genre = rdr["Genre"].ToString();
                    movie.ReleseDate = rdr["ReleseDate"].ToString();
                }
            }
            return movie;
        }

        public void DeleteMovie(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteMovie ", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }




    }
}
