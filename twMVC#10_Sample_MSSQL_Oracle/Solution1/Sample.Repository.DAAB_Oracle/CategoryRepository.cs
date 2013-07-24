using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Oracle.DataAccess.Client;
using Sample.Domain;
using Sample.Repository.Interface;

namespace Sample.Repository.DAAB_Oracle
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        //=========================================================================================
        // 進階操作 -  使用 Accessor 與 IRowMapper

        /// <summary>
        /// Gets the categories .
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            string sqlStatement = "select * from \"Categories\" order by \"CategoryID\"";

            try
            {
                DataAccessor<Category> accessor =
                    this.Db.CreateSqlStringAccessor<Category>(sqlStatement, new CategoryMapper());

                var categories = accessor.Execute().ToList();
                return categories;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Category GetOne(int id)
        {
            string sqlStatement = "select * from \"Categories\" where \"CategoryID\" = :CategoryID";

            try
            {
                DataAccessor<Category> accessor =
                    this.Db.CreateSqlStringAccessor<Category>(
                        sqlStatement,
                        new CategoryIDParameterMapper(),
                        new CategoryMapper());

                var data = accessor.Execute(new object[] { id }).FirstOrDefault();
                return data;
            }
            catch
            {
                throw;
            }
        }
    }

    public class CategoryIDParameterMapper:IParameterMapper
    {
        public void AssignParameters(DbCommand command, object[] parameterValues)
        {
            var param = command.CreateParameter();
            param.ParameterName = "CategoryID";
            param.Value = parameterValues[0];
            command.Parameters.Add(param);
        }
    }

    public class CategoryMapper : IRowMapper<Category>
    {
        public Category MapRow(IDataRecord reader)
        {
            Category item = new Category();

            item.CategoryID = int.Parse(reader["CategoryID"].ToString());
            item.CategoryName = reader["CategoryName"].ToString();
            item.Description = reader["Description"].ToString();

            return item;
        }
    }
}
