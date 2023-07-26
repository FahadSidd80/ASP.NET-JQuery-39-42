using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

namespace Web_L42_WbSr_asmxhtmlCRUD20523
{
    /// <summary>
    /// Summary description for EmployeeServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmployeeServices : System.Web.Services.WebService
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WebSerHtmlJQuery"].ConnectionString);

        [WebMethod]
        public void Insert(string name, string address, int age)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_tblEmployee", con);
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action","INSERT");
            cmd.Parameters.AddWithValue("@name",name);
            cmd.Parameters.AddWithValue("@address",address);
            cmd.Parameters.AddWithValue("@age",age);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        [WebMethod]
        public string Display()
        {
            string data = "";
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_tblEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "GET");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return data; 
        }

        [WebMethod]
        public void Delete(int empid)
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
        public string Edit(int empid)
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
        public void Update(int empid,string name, string address, int age)
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
