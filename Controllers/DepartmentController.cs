using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiEmployees.Models;

namespace WebApiEmployees.Controllers
{
    public class DepartmentController : ApiController
    {
        private const string connectionString = "EmployeeDB";

        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT [DepartmentID], [DepartmentName] FROM [dbo].[Departments]";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        command.CommandType = CommandType.Text;
                        adapter.Fill(dt);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }


        public string Post(Department department)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"INSERT INTO [dbo].[Departments]([DepartmentName])
                                    VALUES ('" + department.DepartmentName + "')";

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            command.CommandType = CommandType.Text;
                            adapter.Fill(dt);
                        }
                    }
                }

                return "Added Successfully";
            }
            catch (Exception exc)
            {
                return "Failed to add";
            }
        }


        public string Put(Department department)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"UPDATE [dbo].[Departments]
                               SET[DepartmentName] = '" + department.DepartmentName + @"'
                               WHERE DepartmentID = " + department.DepartmentID;

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            command.CommandType = CommandType.Text;
                            adapter.Fill(dt);
                        }
                    }
                }

                return "Updated Successfully";
            }
            catch (Exception exc)
            {
                return "Failed to update";
            }
        }


    }
}
