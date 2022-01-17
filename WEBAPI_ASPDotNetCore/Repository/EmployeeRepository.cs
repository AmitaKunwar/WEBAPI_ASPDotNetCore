using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Contracts;
using WEBAPI_ASPDotNetCore.Models;

namespace WEBAPI_ASPDotNetCore.Repository
{
    public  class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository 
    {
        public EmployeeRepository(RepositoryContext _reposContext)
            : base(_reposContext)
        {
        }

        //public string InsertEmployeeRecord(Employee emp)
        //{

        //    var con = ConnectionSQL();
        //    try
        //    {
        //        // con.Open();
        //        scmd = new SqlCommand("InsertEmployeeData", con);
        //        scmd.CommandType = CommandType.StoredProcedure;
        //        scmd.Parameters.AddWithValue("@Name", emp.Name);
        //        scmd.Parameters.AddWithValue("@Email", emp.Email);
        //        scmd.Parameters.AddWithValue("@Gender", emp.Gender);
        //        scmd.Parameters.AddWithValue("@Status", emp.Status);
        //        scmd.Parameters.AddWithValue("@Phone", emp.Phone);
        //        scmd.Parameters.AddWithValue("@Address", emp.Address);
        //        con.Open();
        //        int i = scmd.ExecuteNonQuery();
        //        con.Close();
        //        if (i >= 1)
        //        {
        //            return "New Employee Added Successfully";

        //        }
        //        else
        //        {
        //            return "Employee Not Added";

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return "";
        //}

        //private SqlConnection ConnectionSQL()
        //{
        //    string connetionString = null;
        //    SqlConnection cnn;
        //    // connetionString = "Data Source=localhost\\SQLExpress;Initial Catalog=SchoolDB;User ID=presales;Password=Ga1eway#";
        //    cnn = new SqlConnection(connetionString);

        //    return cnn;
        //}

        
    }
}
