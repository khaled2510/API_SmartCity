using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;


namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly dal.UtilisateurData data;

        MapperConfiguration configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<model.Utilisateur,dto.Utilisateur>();
            cfg.CreateMap<model.Utilisateur,dto.Utilisateur>().ReverseMap();
        });
        public UtilisateurController()
        {
            this.mapper = new Mapper(configuration);
            this.data = new dal.UtilisateurData(new dal.BDAgendaContext());
        }

        // GET api/utilisateur
        [HttpGet("count/{role}")]
        [Authorize(Roles = model.Constants.Roles.Admin)]
        public ActionResult<int> CountAcount(string role)
        {
            return data.AllUtilisateursWithRole(role);
        }

        // GET api/utilisateur/pseudo
        [HttpGet("{pseudo}")]
        public async Task<IActionResult> GetByPseudo(string pseudo)
        {
            model.Utilisateur utilisateur = await data.UtilisateurParPseudoAsync(pseudo);
            if(utilisateur == null)
                return NotFound();

            dto.Utilisateur utilisateurDTO = mapper.Map<dto.Utilisateur>(utilisateur);
            return Ok(utilisateurDTO);
        }

        // POST api/utilisateur
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dto.Utilisateur nouveauUtilisateur)
        {
            model.Utilisateur utilisateurAjout = mapper.Map<model.Utilisateur>(nouveauUtilisateur);
            try{
                utilisateurAjout = await data.AjouteUtilisateur(utilisateurAjout);
            }catch{
                return BadRequest(new dto.BusinessError("NameAlreadyExist"));
            }

            return Created("api/Utilisateur" + utilisateurAjout.Pseudo, mapper.Map<dto.Utilisateur>(utilisateurAjout));
        }

        // DELETE api/utilisateur/pseudo
        [HttpDelete("{pseudo}")]
        public async Task<ActionResult> Delete(string pseudo)
        {
            await data.SupprimerCompteAsync(pseudo);
            return Ok();
        }
    }
}