using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace OST_Inventory_B_2.Models
{
    public class Customer
    {
        public static DataTable LstCustomer()
        {
            DataTable dtCust = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstCustomer";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear(); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0; 
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtCust);

            cmd.Dispose();
            connection.Close();


            return dtCust;
        }
    }
}