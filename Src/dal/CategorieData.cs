using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dal
{                               
    public class CategorieData
    {
        private readonly BDAgendaContext context;

        public CategorieData(BDAgendaContext context)
        {
            this.context = context;
        }

        //Categorties
        public async Task<IEnumerable<model.Categorie>> getAllCategories()
        {
            return await context.Categorie.OrderBy(catego => catego.Id).ToListAsync();
        }
    }
}