using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dal
{
    public class PresentationData
    {
        private readonly BDAgendaContext context;

        public PresentationData(BDAgendaContext context)
        {
            this.context = context;
        }

        // Présentation
        public async Task<model.Presentation> AjoutePresentation(model.Presentation nouvelPresentation)
        {
            context.Presentation.Add(nouvelPresentation);
            await context.SaveChangesAsync();
            return nouvelPresentation;
        }

        // utilisé lors de la suppression d'une presentation
        public async Task<model.Presentation> GetPresentation(int id)
        {
            return (await context.Presentation.ToListAsync()).FirstOrDefault(pres => pres.Id == id);
        }

        public async Task SupprimerPresentationAsync(model.Presentation presASupprimer)
        {
            context.Presentation.Remove(presASupprimer);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<model.Presentation>> GetPresentationEvent(int idEvent)
        {
            return (await context.Presentation.ToListAsync()).Where(pres => pres.EvenementId == idEvent);
        }
    }
}