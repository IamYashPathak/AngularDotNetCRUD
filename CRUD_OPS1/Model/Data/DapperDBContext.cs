﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUD_OPS1.Model.Data
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public DapperDBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connectionString = _configuration.GetConnectionString("connection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
