using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class DepartmentController : ApiController
    {
        [Route("api/department/getDepartments")]
        [HttpGet]

        public HttpResponseMessage Get()
        {
            string query = @"select departmentId,departmentName from dbo.department";
            DataTable table = new DataTable();
            using(var con=new SqlConnection(ConfigurationManager.
                ConnectionStrings["MasterApp"].ConnectionString))
                using(var cmd=new SqlCommand(query,con))
                using (var da=new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Department dept)
        {
            try
            {
                string query = @"insert into dbo.department values('" + dept.departmentName + @"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["MasterApp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "inserted Successfully";
            }
            catch(Exception) {

                return "Failed To Add";
            }
        }
        public string Put(Department dept)
        {
            try
            {
                string query = @"update  dbo.department set departmentName='" + dept.departmentName 
                                       + @"' where departmentId="+dept.departmentId+@"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["MasterApp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "updated Successfully";
            }
            catch (Exception)
            {

                return "Failed To update";
            }
        }
        [Route("api/department/deleteDepart")]
        [HttpGet]
        public string Delete(int id)
        {
            try
            {
                string query = @"delete  dbo.department where departmentId=" + id + @"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["MasterApp"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "deleted Successfully";
            }
            catch (Exception)
            {

                return "Failed To delete";
            }
        }
    }

}
