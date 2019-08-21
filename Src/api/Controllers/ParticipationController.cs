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
    public class ParticipationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly dal.ParticipationData data;
        MapperConfiguration configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<model.Participation, dto.Participation>();
            cfg.CreateMap<model.Participation, dto.Participation>().ReverseMap();
        });
        public ParticipationController()
        {
            this.mapper = new Mapper(configuration);
            this.data = new dal.ParticipationData(new dal.BDAgendaContext());
        }

        [HttpGet("testParticipation/{idEvent}/{pseudo}")]
        public async Task<IActionResult> Get(int idEvent, String pseudo)
        {
            model.Participation testParticipation = new model.Participation();
            testParticipation.EvenementId = idEvent;
            testParticipation.ParticipantId = pseudo;
            testParticipation = await data.GetParticipation(testParticipation);
            Boolean partTest = testParticipation != null;
            return Ok(partTest);
        }

        [HttpGet("count/{id}")]
        public ActionResult<int> Get(int id)
        {
            return data.CountAllPart(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dto.Participation nouvelParticipation)
        {
            model.Participation ajoutParticipation = mapper.Map<model.Participation>(nouvelParticipation);
            try{
               ajoutParticipation = await data.AjouteParticipation(ajoutParticipation);
            }catch{
                return Unauthorized();
            }
            
            return Created("api/Participation" + ajoutParticipation.Id, mapper.Map<dto.Participation>(ajoutParticipation));
        }

        [HttpDelete("{idEvent}/{pseudo}")]
        public async Task<IActionResult> Delete(int idEvent, String pseudo)
        {
            model.Participation participationFound = new model.Participation();
            participationFound.EvenementId = idEvent;
            participationFound.ParticipantId = pseudo;
            participationFound = await data.GetParticipation(participationFound);
            if (participationFound == null)
                return NotFound();

            await data.SupprimerParticipAsync(participationFound);
            return Ok(mapper.Map<dto.Participation>(participationFound));
        }
    }
}