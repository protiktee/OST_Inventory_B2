using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace OST_Inventory_B_2.Models
{
    public class Account
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public bool CheckUser()
        {
            
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOst_LstMember"; 
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            //cmd.ExecuteNonQuery

            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    return true;
                }
            }

            cmd.Dispose();
            connection.Close();


            //for (int i = 0; i < 30; i++)
            //{
            //    Equipment equipment = new Equipment();
            //    equipment.Name = "Laptop "+i.ToString();
            //    equipment.Count = i*5;
            //    equipment.EntryDate = DateTime.Now.Date;
            //    list.Add(equipment);
            //} 
            //return dataTable;
            return false;
        }
    }
}