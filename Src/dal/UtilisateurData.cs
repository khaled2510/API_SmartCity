using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dal
{
    public class UtilisateurData
    {
        private readonly BDAgendaContext context;
        private Hash hash;

        public UtilisateurData(BDAgendaContext context)
        {
            this.context = context;
            this.hash = new Hash();
        }

        // Récupére tous les utilisateurs
        public async Task<IEnumerable<model.Utilisateur>> AllUtilisateurs()
        {
            return (await context.Utilisateur.ToListAsync());
        }

        // Récupére tous les utilisateurs
        public int AllUtilisateursWithRole(string role)
        {
            return context.Utilisateur
                .Where(utilisateur => utilisateur.Role.Equals(role))
                .Count();
        }

        // Récupére sur base d'un pseudo
        public async Task<model.Utilisateur> UtilisateurParPseudoAsync(string pseudo)
        {
            return (await AllUtilisateurs()).FirstOrDefault(utilisateur => utilisateur.Pseudo == pseudo);
        }

        public async Task<model.Utilisateur> AjouteUtilisateur(model.Utilisateur nouveauUtilisateur)
        {
            model.Utilisateur utilisateur = await UtilisateurParPseudoAsync(nouveauUtilisateur.Pseudo);

            if (utilisateur != null)
            {
                throw new Exception("utilisateur déja existant");
            }

            string passwordHash = hash.ComputeSha256Hash(nouveauUtilisateur.MotDePasse);

            nouveauUtilisateur.MotDePasse = passwordHash;
            context.Utilisateur.Add(nouveauUtilisateur);
            await context.SaveChangesAsync();
            return nouveauUtilisateur;
        }

        public async Task SupprimerCompteAsync(string pseudo)
        {
            model.Utilisateur compteASupprimer = await UtilisateurParPseudoAsync(pseudo);
            if (compteASupprimer != null)
            {
                context.Utilisateur.Remove(compteASupprimer);
                await context.SaveChangesAsync();
            }
        }
    }
}