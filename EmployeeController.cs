using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        [Route("api/employee/getEmployees")]
        [HttpGet]

        public HttpResponseMessage Get()
        {
            string query = @"select employeeId,employeeName ,department,
                         convert(varchar(10),dateOfJoining,120) as dateOfJoining ,photoFileName from dbo.employee";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["MasterApp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("api/employee/createEmployees")]
        [HttpPost]

        public string Post(Employee emp)
        {
            try
            {
                string query = @"insert into dbo.employee values(
                     
                 '" + emp.employeeName + @"'
                 ,'" + emp.department + @"'
                 ,'" + emp.dateOfJoining + @"'
                 ,'" + emp.photoFileName + @"'

                  )";
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
            catch (Exception)
            {

                return "Failed To Add";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                string query = @"update  dbo.employee set 
                               employeeName='" + emp.employeeName+@"'
                              , department = '" + emp.department+@"'
                               ,dateOfJoining = '" + emp.dateOfJoining+ @"'
                               ,photoFileName = '" + emp.photoFileName + @"' where employeeId=" + emp.employeeId + @"";

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
        public string Delete(int id)
        {
            try
            {
                string query = @"delete  dbo.employee where employeeId=" + id + @"";

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

        [Route("api/employee/getAllDepartments")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"select departmentName from dbo.department";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["MasterApp"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }


            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/employee/saveFile")]
       
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var filepath = HttpContext.Current.Server.MapPath("~/Photos/"+ fileName);

                postedFile.SaveAs(filepath);
                return fileName;
            }
            catch(Exception e)
            {
                return "anonymous.png";

            }

        }
    }
}
