using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Spiracle.NETCore.Models;
using System.Text.Encodings.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace Spiracle.NETCore.Controllers
{
    public class sqlController : Controller
    {
        private readonly IConfiguration _configuration;

        public sqlController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MsSql_Get_int()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users WHERE id = '" + id + "'";
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_string()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "SELECT * FROM users WHERE name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_Implicit_Join()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users, address WHERE users.id = " + id + " AND users.id = address.id";
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_Union()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT name, surname, CONVERT(varchar(500),dob,3)  FROM users WHERE id = " + id + " UNION SELECT address_1, address_2, address_3 FROM address WHERE id = " + id;
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_int_inline()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = id;
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_int_no_quote()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "select * from users where id=" + id;
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_string_no_quote()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "select * from users where name=" + name;
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_string_param_question_mark()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "SELECT top 5 id, name, surname FROM users where name <> '?' and name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Implicit_Join_Namespace()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM dbo.users, dbo.address WHERE dbo.users.id = " + id + " AND dbo.users.id = dbo.address.id";
            return View("SpiracleUsersView", new SqlModel(executeMssqlQuery(sqlQueryString), sqlQueryString));
        }


        public IActionResult Oracle_Get_int()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "select * from users where id = '" + id + "'";
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_Full_Outer_Join()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users FULL OUTER JOIN address ON users.id = address.id AND users.id = " + id;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_Implicit_Join()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users, address WHERE users.id = " + id + " AND users.id = address.id";
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_Union()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT name, surname, TO_CHAR(dob) FROM users WHERE id = " + id + " UNION SELECT address_1, address_2, address_3 FROM address WHERE id = " + id;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_groupby()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT count(name), name FROM users GROUP BY " + id;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_having()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT MIN(name) from users GROUP BY id HAVING id = " + id;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_inline()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = id;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_no_quote()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "select * from users where id = " + id;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_orderby()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users ORDER BY " + id;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "select * from users where name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string_no_quote()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "select * from users where name = " + name;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }
        
        public IActionResult Oracle_Get_string_no_quote_sanitised()
        {
            var name = HttpContext.Request.Query["name"];
            var newName = ((string)name).Replace("'", "''");
            string sqlQueryString = "select * from users where name = " + newName;
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string_param_question_mark()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "SELECT * FROM users where name <> '?' and name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string_sanitised()
        {
            var name = HttpContext.Request.Query["name"];
            var newName = ((string)name).Replace("'", "''");
            string sqlQueryString = "select * from users where name = '" + newName + "'";
            return View("SpiracleUsersView", new SqlModel(executeOracleQuery(sqlQueryString), sqlQueryString));
        }

        private List<SpiracleUser> executeMssqlQuery(string sqlQueryString)
        {
            String connectionString = _configuration.GetConnectionString("mssql");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(sqlQueryString, conn);
            List<SpiracleUser> spiracleUsers = new List<SpiracleUser>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                spiracleUsers = createListOfSpiracleUsers(reader);
            }

            conn.Close();
            return spiracleUsers;
        }

        private List<SpiracleUser> executeOracleQuery(string sqlQueryString)
        {
            String connectionString = _configuration.GetConnectionString("oracle"); 
            OracleConnection conn = new OracleConnection(connectionString);
            conn.Open();
            OracleCommand command = new OracleCommand(sqlQueryString, conn);
            List<SpiracleUser> spiracleUsers = new List<SpiracleUser>();

            using (OracleDataReader reader = command.ExecuteReader())
            {
                spiracleUsers = createListOfSpiracleUsers(reader);
            }

            conn.Close();
            return spiracleUsers;
        }

        private List<SpiracleUser> createListOfSpiracleUsers(DbDataReader reader)
        {
            List<SpiracleUser> spiracleUsers = new List<SpiracleUser>(); 
            
            while (reader.Read())
            {
                SpiracleUser spiracleUser = new SpiracleUser();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string colName = reader.GetName(i).ToLower();
                    switch (colName)
                    {
                        case "id":
                            var idVal = reader.GetValue(i);
                            // Id data type may be Decimal (Oracle XE) or int32 (ms sql server)
                            if (idVal is Decimal)
                            {
                                spiracleUser.Id = (Decimal)reader.GetValue(i);
                            }
                            else if (idVal is int)
                            {
                                spiracleUser.Id = (int)reader.GetValue(i);
                            }
                            break;
                        case "name":
                            spiracleUser.Name = (reader.GetValue(i) is DBNull ? null : (string)reader.GetValue(i));
                            break;
                        case "surname":
                            spiracleUser.Surname = (reader.GetValue(i) is DBNull ? null : (string)reader.GetValue(i));
                            break;
                        case "dob":
                            if (!reader.IsDBNull(i))
                            {
                                spiracleUser.Dob = (DateTime)reader.GetValue(i); ;
                            }
                            break;
                        case "credit_card":
                            spiracleUser.CreditCard = (reader.GetValue(i) is DBNull ? null : (string)reader.GetValue(i));
                            break;
                        case "cvv":
                            var cvvVal = reader.GetValue(i);
                            // Cvv data type may be Decimal (Oracle XE) or int32 (ms sql server)
                            if (cvvVal is Decimal)
                            {
                                spiracleUser.Cvv = (Decimal)reader.GetValue(i);
                            }
                            else if (cvvVal is int)
                            {
                                spiracleUser.Cvv = (int)reader.GetValue(i);
                            }
                            break;
                    }
                }

                spiracleUsers.Add(spiracleUser);
            }
            
            return spiracleUsers;
        }
    }
}