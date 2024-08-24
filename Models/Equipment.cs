using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web; 

namespace OST_Inventory_B_2.Models
{
    [Serializable]
    public class Equipment 
    {
         
        public int EquipmentID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public DateTime EntryDate { get; set; }
        public Member Member { get; set; }
        
        public int Stock { get; set; }

        public Equipment() {
            Member=new Member();
        }

        public static int SaveEquipment(string Name,int count)
        {
            int result = 0;
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "dbo.spOST_InsEquipment";
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Name", Name));
            cmd.Parameters.Add(new SqlParameter("@EcCount", count));
            cmd.CommandTimeout = 0;

            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { 
                string sds= ex.Message;
            }

            cmd.Dispose();
            sqlConnection.Close();

            return result;
        }
        public static List<Equipment> LstEquipment()
        {
            List<Equipment> plstData = new List<Equipment>(); 
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstEquipment";
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
                    Equipment obj = new Equipment();
                    obj.EquipmentID = Convert.ToInt32(mrd["EquipmentID"].ToString());
                    obj.Name = mrd["EquipmentName"].ToString();
                    obj.Count = Convert.ToInt16(mrd["Quantity"].ToString());
                    obj.Stock = Convert.ToInt16(mrd["Stock"].ToString());
                    obj.EntryDate = Convert.ToDateTime(mrd["EntryDate"].ToString());
                    plstData.Add(obj);
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
            return plstData;
        }
        public static DataTable dtEquipment()
        {
            //List<Equipment> plstData = new List<Equipment>();
            DataTable dataTable = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOST_LstEquipment";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            cmd.Dispose();
            connection.Close();
            return dataTable;
            //return list;
        }
        public static DataTable LstAssignedEquipment()
        {
            //List<Equipment> plstData = new List<Equipment>();
            DataTable dataTable = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.spOsp_LstCustomerEquiipment";
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            cmd.Dispose();
            connection.Close();
            return dataTable;
        }

        public static int AssignEquipment(int custID,int EquiID, int count,int IsRel)
        {
            int result = 0;
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "dbo.spOST_InsEquiAssignment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.Add(new SqlParameter("@CustomerID", custID));
            cmd.Parameters.Add(new SqlParameter("@EquipmentID", EquiID));
            cmd.Parameters.Add(new SqlParameter("@EquiCount", count));
            cmd.Parameters.Add(new SqlParameter("@IsRelease", IsRel));
            cmd.CommandTimeout = 0;

            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string sds = ex.Message;
            }

            cmd.Dispose();
            sqlConnection.Close();

            return result;
        }
    }
}