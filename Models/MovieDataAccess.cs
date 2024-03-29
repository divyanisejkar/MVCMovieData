﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace MovieData.Models
{
    public class MovieDataAccess : IMovieDataAccess
    {
        string connectionString = "Server=FSIND-LT-11;Database=DbContext;Trusted_Connection=True";

        public List<MvcMovieContext> GetAllMovies()
        {
            
            List<MvcMovieContext> lstMovie = new List<MvcMovieContext>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllMovie ", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    MvcMovieContext movie = new MvcMovieContext();

                    movie.Id = Convert.ToInt32(rdr["ID"]);
                    movie.Title = rdr["Title"].ToString();
                    movie.Genre = rdr["Genre"].ToString();
                    movie.ReleseDate = rdr["ReleseDate"].ToString();
                    
                    lstMovie.Add(movie);
                }
                con.Close();
            }
            return lstMovie;
        }

        public bool AddMovie(MvcMovieContext movie)
        {
            if (string.IsNullOrEmpty(movie.Title))
            {
                throw new ArgumentNullException(movie.Title);

            }
            else if(string.IsNullOrEmpty(movie.Genre))
            {
                throw new ArgumentNullException(movie.Genre);
            }
            else if(Convert.ToInt32(movie.ReleseDate) < 1990 ||Convert.ToInt32(movie.ReleseDate)>2060)
            {
                throw new ArgumentOutOfRangeException(movie.ReleseDate);
            }
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
                return true;
            }
        }
        public void UpdateMovie(MvcMovieContext movie)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", movie.Id);
                cmd.Parameters.AddWithValue("@Title", movie.Title);
                cmd.Parameters.AddWithValue("@Genre", movie.Genre);
                cmd.Parameters.AddWithValue("@ReleseDate", movie.ReleseDate);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public MvcMovieContext GetMovieData(int? id)
        {

            MvcMovieContext movie = new MvcMovieContext();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM MvcMovieContext WHERE ID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    movie.Id = Convert.ToInt32(rdr["ID"]);
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
                cmd.Dispose();
                con.Close();
            }
        }

    }
}
