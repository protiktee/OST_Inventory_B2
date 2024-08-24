using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace OST_Inventory_B_2.Models
{
    public class Member
    {
        public string MemberName { get; set; }
        public int MemberId { get; set; }
        public string MemberDesignation { get; set; }
        public string MobileNumber { get; set; }

        public static List<Member> LstMember()
        {
            List<Member> plstData = new List<Member>();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOst_LstMember";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            //cmd.ExecuteNonQuery

            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    Member obj = new Member();
                    obj.MemberName = mrd["Name"].ToString(); 
                    obj.MemberId = Convert.ToInt32(mrd["id"].ToString());
                    plstData.Add(obj);
                }
            }

            cmd.Dispose();
            connection.Close();

             
            return plstData;
        }
    }
}