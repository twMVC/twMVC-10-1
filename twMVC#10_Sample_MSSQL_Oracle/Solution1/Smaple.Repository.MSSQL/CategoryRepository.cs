using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain;
using Sample.Repository.Interface;

namespace Sample.Repository.EF_MSSQL
{
    public class CategoryRepository : ICategoryRepository
    {
        private MsSQLNorthwindEntities db = new MsSQLNorthwindEntities();

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> result = new List<Category>();

            var categories = this.db.Categories.OrderBy(x => x.CategoryID);
            foreach (var item in categories)
            {
                var category = new Category()
                {
                    CategoryID = item.CategoryID,
                    CategoryName = item.CategoryName,
                    Description = item.Description
                };
                result.Add(category);
            }

            return result;
        }

        /// <summary>
        /// Gets the one.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Category GetOne(int id)
        {
            var item = this.db.Categories.FirstOrDefault(x => x.CategoryID == id);
            if (item == null)
            {
                return null;
            }
            else
            {
                var category = new Category()
                {
                    CategoryID = item.CategoryID,
                    CategoryName = item.CategoryName,
                    Description = item.Description
                };
                return category;
            }
        }
    }
}
