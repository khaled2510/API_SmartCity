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
    public class PresentationController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly dal.PresentationData data;

        MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<model.Presentation, dto.Presentation>();
            cfg.CreateMap<model.Presentation, dto.Presentation>().ReverseMap();
        });
        public PresentationController()
        {
            this.mapper = new Mapper(configuration);
            this.data = new dal.PresentationData(new dal.BDAgendaContext());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPresEvent(int id)
        {
            IEnumerable<model.Presentation> presentations = await data.GetPresentationEvent(id);
            if (presentations == null)
                return NotFound();

            return Ok(presentations.Select(mapper.Map<dto.Presentation>));
        }

        [HttpPost]
        [Authorize(Roles = model.Constants.Roles.Professionnel)]
        public async Task<IActionResult> Post([FromBody] dto.Presentation nouvelPresentation)
        {
            model.Presentation presentationAjout = mapper.Map<model.Presentation>(nouvelPresentation);
            presentationAjout = await data.AjoutePresentation(presentationAjout);

            return Created("api/Presentation" + presentationAjout.Id, mapper.Map<dto.Presentation>(presentationAjout));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = model.Constants.Roles.Admin + "," + model.Constants.Roles.Professionnel)]
        public async Task<ActionResult> Delete(int id)
        {
            model.Presentation presASupprimer = await data.GetPresentation(id);
            if (presASupprimer == null)
                return NotFound();
            await data.SupprimerPresentationAsync(presASupprimer);
            return Ok();
        }
    }
}