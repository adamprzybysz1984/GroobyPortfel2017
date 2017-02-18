using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroobyPortfel
{
    static class Categories
    {
        public static List<Category> categories = new List<Category>();

        public static void LoadCategories()
        {
            StreamReader sr = new StreamReader("loginy\\" + Config.LoggedUser + "\\categories.txt");
            int id = 0;
            while(!sr.EndOfStream)
            {
                categories.Add(new Category() { Id = id, Name = sr.ReadLine() });
                id++;
            }
        }
    }
}
