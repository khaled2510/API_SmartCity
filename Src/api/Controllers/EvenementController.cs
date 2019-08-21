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
    public class EvenementController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly dal.EvenementData data;

        MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<model.Evenement, dto.Evenement>();
            cfg.CreateMap<model.Evenement, dto.Evenement>().ReverseMap();
        });
        public EvenementController()
        {
            this.mapper = new Mapper(configuration);
            this.data = new dal.EvenementData(new dal.BDAgendaContext());
        }

        [HttpGet("pseudo/{pseudo}/{pageSizeEvent}/{pageIndexEvent}")]
        [Authorize(Roles = model.Constants.Roles.Professionnel)]
        public async Task<IActionResult> GetAllEventOfUser(string pseudo, int pageSizeEvent = 3, int pageIndexEvent = 0)
        {
            IEnumerable<model.Evenement> evenements = await data.EvenementsEtPrésentationsCreerPar(pseudo, pageSizeEvent, pageIndexEvent);
            if (evenements == null || !evenements.Any())
                return NotFound();

            IEnumerable<dto.Evenement> evenementsDTO = evenements.Select(mapper.Map<dto.Evenement>);
            return Ok(evenementsDTO);
        }

        [HttpGet("parCatego/{id}")]
        public async Task<IActionResult> GetAllEventOfCatego(int id)
        {
            IEnumerable<model.Evenement> evenements = await data.EvenementsEtPrésentationParCatego(id);
            if (evenements == null || !evenements.Any())
                return NotFound();

            IEnumerable<dto.Evenement> evenementsDTO = evenements.Select(mapper.Map<dto.Evenement>);
            return Ok(evenementsDTO);
        }

        [HttpGet("parDate/{dateDebut}/{dateFin}")]
        public async Task<IActionResult> GetAllEventByDate(DateTime dateDebut, DateTime dateFin)
        {
            IEnumerable<model.Evenement> evenements = await data.EvenementsEtPrésentationParDate(dateDebut, dateFin);
            if (evenements == null || !evenements.Any())
                return NotFound();

            IEnumerable<dto.Evenement> evenementsDTO = evenements.Select(mapper.Map<dto.Evenement>);
            return Ok(evenementsDTO);
        }

        [HttpGet("parParticipation/{pseudo}")]
        public async Task<IActionResult> GetAllEventByPresentation(String pseudo)
        {
            IEnumerable<model.Evenement> evenements = await data.EvenementsEtPrésentationParParticipation(pseudo);
            if (evenements == null || !evenements.Any())
                return NotFound();

            IEnumerable<dto.Evenement> evenementsDTO = evenements.Select(mapper.Map<dto.Evenement>);
            return Ok(evenementsDTO);
        }

        [HttpGet("count/{pseudo}")]
        public ActionResult<int> Get(string pseudo)
        {
            return data.CountAllEvent(pseudo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            model.Evenement evenement = await data.EvenementParId(id);
            if (evenement == null)
                return NotFound();

            dto.Evenement evenementDTO = mapper.Map<dto.Evenement>(evenement);
            return Ok(evenementDTO);
        }

        [HttpPost]
        [Authorize(Roles = model.Constants.Roles.Professionnel)]
        public async Task<IActionResult> Post([FromBody] dto.Evenement nouveauEvenemnt)
        {
            model.Evenement evenementAjout = mapper.Map<model.Evenement>(nouveauEvenemnt);
            try
            {
                evenementAjout = await data.AjouteEvenement(evenementAjout);
            }
            catch
            {
                return BadRequest(new dto.BusinessError("NameAlreadyExist"));
            }

            return Created("api/Evenement" + evenementAjout.Nom, mapper.Map<dto.Evenement>(evenementAjout));
        }

        // [HttpPut("{id}")]
        // [Authorize(Roles = model.Constants.Roles.Professionnel)]
        // public async Task<IActionResult> Put(int id, [FromBody] dto.Evenement eventModif)
        // {
        //     model.Evenement evenement = await data.EvenementParId(id);
        //     if (evenement == null)
        //         return NotFound();
                
        //     model.Evenement evenementModif = mapper.Map<model.Evenement>(eventModif);
        //     try
        //     {
        //         evenementModif = await data.ChangeEvenement(evenement, evenementModif);
        //     }
        //     catch
        //     {
        //         return BadRequest(new dto.BusinessError("NameAlreadyExist"));
        //     }

        //     return Ok(mapper.Map<dto.Evenement>(evenement));
        // }

        // Correction concurrence
        [HttpPut("{id}")]
        [Authorize(Roles = model.Constants.Roles.Professionnel)]
        public async Task<IActionResult> Put(int id, [FromBody] dto.Evenement eventModif)
        {
            model.Evenement evenement = await data.EvenementParId(id);
            if (evenement == null)
                return NotFound();

            eventModif.Id = evenement.Id;
            model.Evenement evenementModif = mapper.Map<model.Evenement>(eventModif);
            model.Evenement evenementNomExistant = await data.EvenementsEtPrésentationParNom(evenementModif);
            if (evenementNomExistant != null)
            {
                return BadRequest(new dto.BusinessError("NameAlreadyExist"));
            }
            
            evenementModif = await data.ChangeEvenement(evenement, evenementModif);

            return Ok(mapper.Map<dto.Evenement>(evenement));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = model.Constants.Roles.Admin + "," + model.Constants.Roles.Professionnel)]
        public async Task<ActionResult> Delete(int id)
        {
            model.Evenement eventASupprimer = await data.EvenementsPrésentationParticipationComNbjaimeParId(id);
            if (eventASupprimer == null)
                return NotFound();

            await data.SupprimerEventAsync(eventASupprimer);
            return Ok();
        }
    }
}
