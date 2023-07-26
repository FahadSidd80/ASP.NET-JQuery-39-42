using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using Newtonsoft.Json; // isko use kar rahe hai data format ke data ko json me convert karne ke liye

namespace WebApp_L39_Insert_JQuery__51923
{
    public partial class EmployeeForm : System.Web.UI.Page // class
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JQueryDBCRUD"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]// koi bhi C# method ko directly hum ajax pe call nahi kar sakte usko as a web mehtod hi call krna hoga
        public  static void Insert(String name, String address, int age)// method/function
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_tblEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "INSERT");
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        [WebMethod]
        public static string GetData()// method/function
        {
            string data = "";
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_tblEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "GETDATA");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return data;
        }

        [WebMethod]
        public static void Delete(int empid)// method/function
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_tblEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "DELETE");
            cmd.Parameters.AddWithValue("@empid", empid);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        [WebMethod]
        public static string Edit(int empid)// method/function
        {
            string data = "";
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_tblEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "EDIT");
            cmd.Parameters.AddWithValue("@empid", empid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return data;
        }

        [WebMethod]
        public static void Update(int empid, String name, String address, int age)// method/function
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_tblEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "UPDATE");
            cmd.Parameters.AddWithValue("@empid", empid);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        


    }
}