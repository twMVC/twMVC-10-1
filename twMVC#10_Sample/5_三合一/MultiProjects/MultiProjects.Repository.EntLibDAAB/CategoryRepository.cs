using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiProject.Repository.Interface;
using MultiProjects.Domain;

namespace MultiProjects.Repository.EntLibDAAB
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository()
            : base()
        {
        }

        public CategoryRepository(string connectionStringName)
            : base(connectionStringName)
        {
        }

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Create(Category instance)
        {
            string sqlStatement = "INSERT [dbo].[Categories]([CategoryName],[Description])";
            sqlStatement += "VALUES(@CategoryName,@Description);";

            DbCommand comm = Db.GetSqlStringCommand(sqlStatement);
            comm.Parameters.Add(new SqlParameter("CategoryName", instance.CategoryName));
            comm.Parameters.Add(new SqlParameter("Description", instance.Description));
            Db.ExecuteNonQuery(comm);
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public void Update(Category instance)
        {
            string sqlStatement = "UPDATE [dbo].[Categories] ";
            sqlStatement += "SET ";
            sqlStatement += "[CategoryName] = @CategoryName, ";
            sqlStatement += "[Description] = @Description ";
            sqlStatement += "WHERE [dbo].[Categories].[CategoryID] = @CategoryID;";

            DbCommand comm = Db.GetSqlStringCommand(sqlStatement);
            comm.Parameters.Add(new SqlParameter("CategoryName", instance.CategoryName));
            comm.Parameters.Add(new SqlParameter("Description", instance.Description));
            comm.Parameters.Add(new SqlParameter("CategoryID", instance.CategoryID));
            Db.ExecuteNonQuery(comm);
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            string sqlStatement = "DELETE FROM [dbo].[Categories] ";
            sqlStatement += "WHERE [dbo].[Categories].[CategoryID] = @CategoryID;";

            DbCommand comm = Db.GetSqlStringCommand(sqlStatement);
            comm.Parameters.Add(new SqlParameter("CategoryID", id));
            Db.ExecuteNonQuery(comm);
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            string sqlStatement = "select * from Categories order by CategoryID Desc";

            using (DbCommand comm = Db.GetSqlStringCommand(sqlStatement))
            using (IDataReader reader = this.Db.ExecuteReader(comm))
            {
                while (reader.Read())
                {
                    Category item = new Category();
                    item.CategoryID = int.Parse(reader["CategoryID"].ToString());
                    item.CategoryName = reader["CategoryName"].ToString();
                    item.Description = reader["Description"].ToString();

                    categories.Add(item);
                }
            }
            return categories;
        }

        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Category GetOne(int id)
        {
            string sqlStatement = "select * from Categories where CategoryID = @CategoryID";

            Category item = new Category();

            using (DbCommand comm = Db.GetSqlStringCommand(sqlStatement))
            {
                comm.Parameters.Add(new SqlParameter("CategoryID", id));

                using (IDataReader reader = this.Db.ExecuteReader(comm))
                {
                    if (reader.Read())
                    {
                        item.CategoryID = int.Parse(reader["CategoryID"].ToString());
                        item.CategoryName = reader["CategoryName"].ToString();
                        item.Description = reader["Description"].ToString();
                    }
                }
            }
            return item;
        }
    }
}
