using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MVC_ADONET.Models.Repository
{
    public abstract class BaseRepository
    {
        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public BaseRepository()
        {
            this.ConnectionString = WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
        }

        public BaseRepository(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                this._connectionString = connectionString;
            }
        }
    }

}