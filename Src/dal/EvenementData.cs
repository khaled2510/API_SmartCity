using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using model;

namespace dal
{
    public class EvenementData
    {
        private readonly BDAgendaContext context;

        public EvenementData(BDAgendaContext context)
        {
            this.context = context;
        }

        // Tous les evenements et leurs présentations creer par un utilisateur pro
        public async Task<IEnumerable<model.Evenement>> EvenementsEtPrésentationsCreerPar(string pseudo, int pageSize, int pageIndex)
        {
            return (await context.Evenement.Include(evenement => evenement.Presentation).ToListAsync()).Where(evenement => evenement.CreateurId == pseudo)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public int CountAllEvent(string pseudo)
        {
            return context.Evenement.Where(evenement => evenement.CreateurId.Equals(pseudo)).Count();
        }

        // Tous les evenements et leurs présentations appartenant à une catégorie
        public async Task<IEnumerable<model.Evenement>> EvenementsEtPrésentationParCatego(int idCatego)
        {
            return (await context.Evenement.Include(evenement => evenement.Presentation).ToListAsync()).Where(evenement => evenement.CategorieId == idCatego);
        }

        // Tous les evenements et leurs présentations par date
        public async Task<IEnumerable<model.Evenement>> EvenementsEtPrésentationParDate(DateTime debut, DateTime fin)
        {
            IEnumerable<model.Presentation> presentations = context.Presentation.ToList().Where(p => p.DateHeureDebut >= debut && p.DateHeureFin <= fin);
            List<model.Evenement> evenements = new List<model.Evenement>();
            foreach(model.Presentation presentation in presentations){
                model.Evenement evenement = (await context.Evenement.Include(e => e.Presentation).ToListAsync())
                                            .FirstOrDefault(e => e.Id == presentation.EvenementId);
                evenements.Add(evenement);
            }
            return evenements.Select(evenement => evenement).Distinct();
        }

        // Tous les evenements auquels un utilisateur participe
        public async Task<IEnumerable<model.Evenement>> EvenementsEtPrésentationParParticipation(String pseudo)
        {
            IEnumerable<model.Participation> participations = context.Participation.ToList().Where(p => p.ParticipantId.Equals(pseudo));
            List<model.Evenement> evenements = new List<model.Evenement>();
            foreach(model.Participation participation in participations){
                model.Evenement evenement = (await context.Evenement.Include(e => e.Presentation).ToListAsync())
                                            .FirstOrDefault(e => e.Id == participation.EvenementId);
                evenements.Add(evenement);
            }
            return evenements;
        }

        // L'evenement et ses présentations rechercher sur base de son nom
        public async Task<model.Evenement> EvenementsEtPrésentationParNom(model.Evenement nouveauEvenement)
        {
            return (await context.Evenement.Include(evenement => evenement.Presentation).ToListAsync()).FirstOrDefault(evenement => evenement.Nom.Equals(nouveauEvenement.Nom) && evenement.Id != nouveauEvenement.Id);
        }

        // L'evenement et ses présentations rechercher sur base de son id
        public async Task<model.Evenement> EvenementParId(int id)
        {
            return (await context.Evenement.Include(evenement => evenement.Presentation).ToListAsync()).FirstOrDefault(evenement => evenement.Id == id);
        }

        // Récupére tous ce qui est associé à un evenement (utile a le suppréssion)
        public async Task<model.Evenement> EvenementsPrésentationParticipationComNbjaimeParId(int id)
        {
            return (await context.Evenement.Include(evenement => evenement.Presentation)
                            .Include(evenement => evenement.Participation)
                            .Include(evenement => evenement.Commentaire)
                            .Include(evenement => evenement.NombreJaime)
                            .ToListAsync()).FirstOrDefault(evenement => evenement.Id == id);
        }

        // public async Task<IEnumerable<model.Evenement>> AllEvenementsPrésentationParticipationComNbjaime()
        // {
        //     return await context.Evenement.Include(evenement => evenement.Presentation)
        //                     .Include(evenement => evenement.Participation)
        //                     .Include(evenement => evenement.Commentaire)
        //                     .Include(evenement => evenement.NombreJaime)
        //                     .ToListAsync();
        // } 

        public async Task<model.Evenement> AjouteEvenement(model.Evenement nouveauEvenement)
        {
            model.Evenement evenement = await EvenementsEtPrésentationParNom(nouveauEvenement);

            if (evenement != null)
            {
                throw new Exception("événement déjâ existant");
            }
            nouveauEvenement.DateCreationEvenement = DateTime.Now;
            context.Evenement.Add(nouveauEvenement);
            await context.SaveChangesAsync();
            return nouveauEvenement;
        }
        public async Task SupprimerEventAsync(model.Evenement eventASupprimer)
        {
            context.Evenement.Remove(eventASupprimer);
            await context.SaveChangesAsync();
        }
        // public async Task<model.Evenement> ChangeEvenement(model.Evenement evenementAChanger, model.Evenement evenementChanger)
        // {
        //     evenementChanger.Id = evenementAChanger.Id;
        //     model.Evenement evenement = await EvenementsEtPrésentationParNom(evenementChanger);

        //     if (evenement != null)
        //     {
        //         throw new BusinessException("événement déjâ existant");
        //     }
        //     evenementAChanger.Nom = evenementChanger.Nom;
        //     evenementAChanger.Description = evenementChanger.Description;
        //     evenementAChanger.Rue = evenementChanger.Rue;
        //     evenementAChanger.Numero = evenementChanger.Numero;
        //     evenementAChanger.Localite = evenementChanger.Localite;
        //     evenementAChanger.DateCreationEvenement = DateTime.Now;
        //     evenementAChanger.CategorieId = evenementChanger.CategorieId;
        //     evenementAChanger.CreateurId = evenementChanger.CreateurId;
        //     evenementAChanger.ImageUrl = evenementChanger.ImageUrl;
        //     context.Entry(evenementAChanger).OriginalValues["RowVersion"] = evenementChanger.RowVersion;

        //     await context.SaveChangesAsync();

        //     return evenementAChanger;
        // }

        public async Task<model.Evenement> ChangeEvenement(model.Evenement evenementAChanger, model.Evenement evenementChanger)
        {
            evenementAChanger.Nom = evenementChanger.Nom;
            evenementAChanger.Description = evenementChanger.Description;
            evenementAChanger.Rue = evenementChanger.Rue;
            evenementAChanger.Numero = evenementChanger.Numero;
            evenementAChanger.Localite = evenementChanger.Localite;
            evenementAChanger.DateCreationEvenement = DateTime.Now;
            evenementAChanger.CategorieId = evenementChanger.CategorieId;
            evenementAChanger.CreateurId = evenementChanger.CreateurId;
            evenementAChanger.ImageUrl = evenementChanger.ImageUrl;
            context.Entry(evenementAChanger).OriginalValues["RowVersion"] = evenementChanger.RowVersion;

            await context.SaveChangesAsync();

            return evenementAChanger;
        }
    }
}