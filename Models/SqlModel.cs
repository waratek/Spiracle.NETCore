using System;
using System.Collections.Generic;

namespace Spiracle.NETCore.Models
{
    public class SqlModel
    {
        public List<SpiracleUser> SpiracleUsers { get; set; }
        public string SqlQueryString { get; set; }

        public SqlModel(List<SpiracleUser> spiracleUsers, string sqlQueryString)
        {
            SpiracleUsers = spiracleUsers;
            SqlQueryString = sqlQueryString;
        }
    }
}