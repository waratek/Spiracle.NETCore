namespace Spiracle.NETCore.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Spiracle.NETCore.Models;
    using System.Data.SqlClient;
    using System.Data.Common;
    using System;
    using System.Collections.Generic;
    using Oracle.ManagedDataAccess.Client;

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
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_string()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "SELECT * FROM users WHERE name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_Implicit_Join()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users, address WHERE users.id = " + id + " AND users.id = address.id";
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_Union()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT name, surname, CONVERT(varchar(500),dob,3)  FROM users WHERE id = " + id + " UNION SELECT address_1, address_2, address_3 FROM address WHERE id = " + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_int_inline()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = id;
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_int_no_quote()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "select * from users where id=" + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_string_no_quote()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "select * from users where name=" + name;
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Get_string_param_question_mark()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "SELECT top 5 id, name, surname FROM users where name <> '?' and name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult MsSql_Implicit_Join_Namespace()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM dbo.users, dbo.address WHERE dbo.users.id = " + id + " AND dbo.users.id = dbo.address.id";
            return View("SpiracleUsersView", new SqlModel(ExecuteMsSqlQuery(sqlQueryString), sqlQueryString));
        }


        public IActionResult Oracle_Get_int()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "select * from users where id = '" + id + "'";
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_Full_Outer_Join()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users FULL OUTER JOIN address ON users.id = address.id AND users.id = " + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_Implicit_Join()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users, address WHERE users.id = " + id + " AND users.id = address.id";
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_Union()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT name, surname, TO_CHAR(dob) FROM users WHERE id = " + id + " UNION SELECT address_1, address_2, address_3 FROM address WHERE id = " + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_groupby()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT count(name), name FROM users GROUP BY " + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_having()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT MIN(name) from users GROUP BY id HAVING id = " + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_inline()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = id;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_no_quote()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "select * from users where id = " + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_int_orderby()
        {
            var id = HttpContext.Request.Query["id"];
            string sqlQueryString = "SELECT * FROM users ORDER BY " + id;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "select * from users where name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string_no_quote()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "select * from users where name = " + name;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }
        
        public IActionResult Oracle_Get_string_no_quote_sanitised()
        {
            var name = HttpContext.Request.Query["name"];
            var newName = ((string)name).Replace("'", "''");
            string sqlQueryString = "select * from users where name = " + newName;
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string_param_question_mark()
        {
            var name = HttpContext.Request.Query["name"];
            string sqlQueryString = "SELECT * FROM users where name <> '?' and name = '" + name + "'";
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        public IActionResult Oracle_Get_string_sanitised()
        {
            var name = HttpContext.Request.Query["name"];
            var newName = ((string)name).Replace("'", "''");
            string sqlQueryString = "select * from users where name = '" + newName + "'";
            return View("SpiracleUsersView", new SqlModel(ExecuteOracleQuery(sqlQueryString), sqlQueryString));
        }

        private List<SpiracleUser> ExecuteMsSqlQuery(string sqlQueryString)
        {
            string connectionString = _configuration.GetConnectionString("mssql");
            var conn = new SqlConnection(connectionString);
            conn.Open();
            var command = new SqlCommand(sqlQueryString, conn);

            using SqlDataReader reader = command.ExecuteReader();
            var spiracleUsers = CreateListOfSpiracleUsers(reader);
            conn.Close();
            return spiracleUsers;
        }

        private List<SpiracleUser> ExecuteOracleQuery(string sqlQueryString)
        {
            var connectionString = _configuration.GetConnectionString("oracle"); 
            var conn = new OracleConnection(connectionString);
            conn.Open();
            var command = new OracleCommand(sqlQueryString, conn);

            using OracleDataReader reader = command.ExecuteReader();
            var spiracleUsers = CreateListOfSpiracleUsers(reader);
            conn.Close();
            return spiracleUsers;
        }

        private static List<SpiracleUser> CreateListOfSpiracleUsers(DbDataReader reader)
        {
            var spiracleUsers = new List<SpiracleUser>(); 
            
            while (reader.Read())
            {
                var spiracleUser = new SpiracleUser();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var colName = reader.GetName(i).ToLower();
                    switch (colName)
                    {
                        case "id":
                            spiracleUser.Id = CastToInt(reader.GetValue(i));
                            break;

                        case "name":
                            spiracleUser.Name = CastToString(reader.GetValue(i));
                            break;

                        case "surname":
                            spiracleUser.Surname = CastToString(reader.GetValue(i));
                            break;

                        case "dob":
                            spiracleUser.Dob = CastToDateTime(reader, i);
                            break;

                        case "credit_card":
                            spiracleUser.CreditCard = CastToString(reader.GetValue(i));
                            break;

                        case "cvv":
                            spiracleUser.Cvv = CastToInt(reader.GetValue(i));
                            break;
                    }
                }

                spiracleUsers.Add(spiracleUser);
            }
            
            return spiracleUsers;
        }

        private static int CastToInt(object val)
        {
            return val switch
            {
                int intVal => intVal,
                byte byteVal => byteVal,
                decimal decimalVal => (int) decimalVal,
                short shortVal => shortVal,
                long longVal => (int) longVal,
                _ => 0
            };
        }

        private static string CastToString(object val)
        {
            if (val is DBNull) return null;
            return (string) val;
        }

        private static DateTime CastToDateTime(DbDataReader reader, int fieldIndex)
        {
            if (reader.IsDBNull(fieldIndex)) return default;
            return (DateTime)reader.GetValue(fieldIndex);
        }
    }
}