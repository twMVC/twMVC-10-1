using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Sample.Repository.DAAB_Oracle
{
    public class BaseRepository
    {
        private DatabaseProviderFactory factory = new DatabaseProviderFactory();

        private Database db;
        protected Database Db
        {
            get
            {
                if (this.db == null)
                {
                    this.db = string.IsNullOrWhiteSpace(this.ConnectionStringName)
                        ? factory.CreateDefault()
                        : factory.Create(this.ConnectionStringName);

                }
                return this.db;
            }
        }

        private string connectionStringName;
        protected string ConnectionStringName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.connectionStringName))
                {
                    this.connectionStringName = "Oracle_Northwind";
                }
                return connectionStringName;
            }
            set { connectionStringName = value; }
        }

        public BaseRepository(string connectionStringName = "")
        {
            if (!string.IsNullOrWhiteSpace(connectionStringName))
            {
                this.ConnectionStringName = connectionStringName;
            }
        }
    }
}
