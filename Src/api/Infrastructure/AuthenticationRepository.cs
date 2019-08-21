using System.Collections.Generic;
using dal;

namespace api.Infrastructure
{
    public class AuthenticationRepositiry
    {
        private IEnumerable<model.Utilisateur> _users;
        private UtilisateurData data;
        
        public AuthenticationRepositiry()
        {
            this.data = new UtilisateurData(new BDAgendaContext());
            _users = data.AllUtilisateurs().Result;
        }
        public IEnumerable<model.Utilisateur> GetUsers()
        {
            return _users;
        }
    }
}
