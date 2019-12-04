using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace MovieData.Models
{
    public class UserDataAccess :IUserDataAcess
    {
        string connectionString = "Server=FSIND-LT-11;Database=DbContext;Trusted_Connection=True";
        public bool addUser(User user)
        {
            if (CheckUserDetails(user.EmailID).EmailID != user.EmailID)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SpAddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                     cmd.Parameters.AddWithValue("@EmailID", user.EmailID);
                     cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastNAme", user.LastName);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                   

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        public User CheckUserDetails(string EmailID)
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 30).Value = EmailID;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.EmailID = rdr["EmailID"].ToString();
                    user.Password = rdr["Password"].ToString();
                }
            }
            return user;
        }


        public bool CheckUserLogin(string EmailID, string Password)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID",EmailID);
                cmd.Parameters.AddWithValue("@Password",Password);
                con.Open();
                int codereturn = (int)cmd.ExecuteScalar();
                return codereturn == 1;


            }
        }

        public bool CheckPassword(string EmailID, string old_pwd, string new_pwd)
        {
            using (SqlConnection con=new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("spChange_pass3", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@old_pwd",old_pwd);
                cmd.Parameters.AddWithValue("@new_Pwd",new_pwd);
                con.Open();
                int codereturn = (int)cmd.ExecuteScalar();

               
                return codereturn == 1;

            }
        }

        public bool NewPassword(string EmailID, string new_pwd)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("spForgetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@new_Pwd", new_pwd);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
                
            }
        }

    }
}
