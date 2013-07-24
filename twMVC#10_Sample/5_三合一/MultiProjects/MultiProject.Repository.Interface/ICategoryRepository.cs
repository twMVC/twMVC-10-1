using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiProjects.Domain;

namespace MultiProject.Repository.Interface
{
    public interface ICategoryRepository
    {
        void Create(Category instance);

        void Update(Category instance);

        void Delete(int id);

        List<Category> GetCategories();

        Category GetOne(int id);

    }
}
