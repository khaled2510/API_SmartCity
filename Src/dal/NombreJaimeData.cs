using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dal
{                               
    public class NombreJaimeData
    {
        private readonly BDAgendaContext context;

        public NombreJaimeData(BDAgendaContext context)
        {
            this.context = context;
        }

        // compte le nombres de j'aimes à un event
        public int CountAllNbJaime(int idEvent){
            return context.NombreJaime
            .Where(nbjaime => nbjaime.EvenementId == idEvent)
            .Count();            
        }

        // NombreJaime
        public async Task<model.NombreJaime> GetJaime(model.NombreJaime nouveauJaime)
        {
            return (await context.NombreJaime.ToListAsync()).FirstOrDefault(jaime => jaime.UtilisateurId.Equals(nouveauJaime.UtilisateurId) && jaime.EvenementId == nouveauJaime.EvenementId);
        }
        public async Task<model.NombreJaime> AjouteJaime(model.NombreJaime nouveauJaime)
        {
            model.NombreJaime jaime = await GetJaime(nouveauJaime);

            if(jaime != null)
            {
                throw new Exception("Déja aimer"); 
            }

            context.NombreJaime.Add(nouveauJaime);
            await context.SaveChangesAsync();
            return nouveauJaime;
        } 

        public async Task SupprimerJaimeAsync(model.NombreJaime jaimeFind)
        {
            context.NombreJaime.Remove(jaimeFind);
            await context.SaveChangesAsync();
        }
    }
}