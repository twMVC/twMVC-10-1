using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MultiProject.Repository.Interface;
using MultiProjects.Domain;

namespace MultiProjects.Repository.EF
{
    public class CategoryRepository : ICategoryRepository
    {
        private NorthwindEntities db = new NorthwindEntities();

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public void Create(Category instance)
        {
            Mapper.CreateMap<Category, Categories>();
            var category = Mapper.Map<Categories>(instance);
            db.Categories.Add(category);
            db.SaveChanges();
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(Category instance)
        {
            Mapper.CreateMap<Category, Categories>();
            var category = Mapper.Map<Categories>(instance);
            db.Categories.Add(category);
            db.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            var category = this.db.Categories.FirstOrDefault(x => x.CategoryID == id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> result = new List<Category>();

            var categories = this.db.Categories.OrderByDescending(x => x.CategoryID);
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
