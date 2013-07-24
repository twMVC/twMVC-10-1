using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
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
            this.ConnectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
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
