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
    public class CommentaireData
    {
        private readonly BDAgendaContext context;

        public CommentaireData(BDAgendaContext context)
        {
            this.context = context;
        }

        // Commentaire lier à un evenement
        public async Task<IEnumerable<model.Commentaire>> GetCommentaireEvent(int idEvent, int pageSize, int pageIndex)
        {
            return (await context.Commentaire.ToListAsync()).Where(commentaire => commentaire.EvenementId == idEvent)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        public int CountAll(int idEvent)
        {
            return context.Commentaire
            .Where(commentaire => commentaire.EvenementId == idEvent)
            .Count();
        }
        public int CountReport()
        {
            return context.Commentaire
            .Where(commentaire => commentaire.Signaler == 1)
            .Count();
        }

        public async Task<IEnumerable<model.Commentaire>> GetCommentaireSignaler(int pageSize, int pageIndex)
        {
            return (await context.Commentaire.ToListAsync()).Where(commentaire => commentaire.Signaler == 1)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);
        }

        // utilisé lors de la suppression d'un commentaire
        public async Task<model.Commentaire> GetCommentaire(int id)
        {
            return (await context.Commentaire.ToListAsync()).FirstOrDefault(commentaire => commentaire.Id == id);
        }
        public async Task<model.Commentaire> AjouteCommentaire(model.Commentaire nouveauCom)
        {
            context.Commentaire.Add(nouveauCom);
            await context.SaveChangesAsync();
            return nouveauCom;
        }
        public model.Commentaire SignalerCommentaire(model.Commentaire commentaireFind, dto.Commentaire commentaireSignaler)
        {
            commentaireFind.Signaler = commentaireSignaler.Signaler;
            context.Entry(commentaireFind).OriginalValues["RowVersion"] = commentaireSignaler.RowVersion;
            context.SaveChanges();

            return commentaireFind;
        }
        public async Task SupprimerCommentaireAsync(Commentaire commentaireFind)
        {
            context.Commentaire.Remove(commentaireFind);
            await context.SaveChangesAsync();
            
        }
    }
}