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
    public class EmployeeController : ApiController
    {
        private const string connectionString = "EmployeeDB";

        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT [EmployeeID]
                                  ,[EmployeeName]
                                  ,[Department]
                                  ,[MailID]
                                  ,[DOJ]
                              FROM [dbo].[Employess]";

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


        public string Post(Employee employee)
        {
            try
            {
                DataTable dt = new DataTable();

                string query = @"INSERT INTO [dbo].[Employess]
                               ([EmployeeName]
                               ,[Department]
                               ,[MailID]
                               ,[DOJ])
                                VALUES
                               ('" + employee.EmployeeName + @"',
                               '" + employee.Department + @"',
                               '" + employee.MailID + @"',
                               '" + ((DateTime)employee.DOJ).ToString("dd/MM/yyyy HH:mm:ss") + @"')";

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


        public string Put(Employee employee)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"UPDATE [dbo].[Employess]
                               SET[EmployeeName] = '" + employee.EmployeeName + @"'
                                  ,[Department] = '" + employee.Department + @"'
                                  ,[MailID] = '" + employee.MailID + @"'
                                  ,[DOJ] = '" + ((DateTime)employee.DOJ).ToString("dd/MM/yyyy HH:mm:ss") + @"'
                                WHERE[EmployeeID] = " + employee.EmployeeID;

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

                return "Updated sucessfully";
            }
            catch (Exception exc)
            {
                return "Failed to update";
            }
        }

        public string Delete(int employeeId)
        {
            return string.Empty;
        }

    }
}
