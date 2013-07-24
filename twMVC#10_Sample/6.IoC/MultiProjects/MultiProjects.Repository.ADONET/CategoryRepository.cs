using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiProjects.Domain;
using MultiProjects.Repository.Interface;

namespace MultiProjects.Repository.ADONET
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository()
            : base()
        {
        }

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public void Create(Category instance)
        {
            string sqlStatement = "INSERT [dbo].[Categories]([CategoryName],[Description])";
            sqlStatement += "VALUES(@CategoryName,@Description);";

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand(sqlStatement, conn))
            {
                command.Parameters.Add(new SqlParameter("CategoryName", instance.CategoryName));
                command.Parameters.Add(new SqlParameter("Description", instance.Description));

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Update(Category instance)
        {
            string sqlStatement = "UPDATE [dbo].[Categories] ";
            sqlStatement += "SET ";
            sqlStatement += "[CategoryName] = @CategoryName, ";
            sqlStatement += "[Description] = @Description ";
            sqlStatement += "WHERE [dbo].[Categories].[CategoryID] = @CategoryID;";

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand(sqlStatement, conn))
            {
                command.Parameters.Add(new SqlParameter("CategoryName", instance.CategoryName));
                command.Parameters.Add(new SqlParameter("Description", instance.Description));
                command.Parameters.Add(new SqlParameter("CategoryID", instance.CategoryID));

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            string sqlStatement = "DELETE FROM [dbo].[Categories] ";
            sqlStatement += "WHERE [dbo].[Categories].[CategoryID] = @CategoryID;";

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand(sqlStatement, conn))
            {
                command.Parameters.Add(new SqlParameter("CategoryID", id));

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            string sqlStatement = "select * from Categories order by CategoryID desc";

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand(sqlStatement, conn))
            {
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
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

            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            using (SqlCommand comm = new SqlCommand(sqlStatement, conn))
            {
                comm.Parameters.Add(new SqlParameter("CategoryID", id));

                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = 180;

                if (conn.State != ConnectionState.Open) conn.Open();

                using (IDataReader reader = comm.ExecuteReader())
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
