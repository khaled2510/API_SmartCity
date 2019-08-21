using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dal
{                               
    public class ParticipationData
    {
        private readonly BDAgendaContext context;

        public ParticipationData(BDAgendaContext context)
        {
            this.context = context;
        }

        // compte les participations d'un event 
        public int CountAllPart(int idEvent){
            return context.Participation
            .Where(participation => participation.EvenementId == idEvent)
            .Count();            
        }

        // Récupére une participation d'événement sur base du pseudo du participant et l'id de event
        public async Task<model.Participation> GetParticipation(model.Participation nouvelParticipation)
        {
            return (await context.Participation.ToListAsync()).FirstOrDefault(participation => participation.ParticipantId.Equals(nouvelParticipation.ParticipantId) && participation.EvenementId == nouvelParticipation.EvenementId);
        }

        // Réjoute un participant si il ne participe pas déjâ
        public async Task<model.Participation> AjouteParticipation(model.Participation nouvelParticipation)
        {
            model.Participation participation = await GetParticipation(nouvelParticipation);

            if(participation != null)
            {
                throw new Exception("Participe déja"); 
            }

            context.Participation.Add(nouvelParticipation);
            await context.SaveChangesAsync();
            return nouvelParticipation;
        }

        public async Task SupprimerParticipAsync(model.Participation participFound)
        {
            context.Participation.Remove(participFound);
            await context.SaveChangesAsync();
        }
    }
}