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
    public class NbJaimeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly dal.NombreJaimeData data;
        MapperConfiguration configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<model.NombreJaime, dto.NombreJaime>();
            cfg.CreateMap<model.NombreJaime, dto.NombreJaime>().ReverseMap();
        });
        public NbJaimeController()
        {
            this.mapper = new Mapper(configuration);
            this.data = new dal.NombreJaimeData(new dal.BDAgendaContext());
        }

        [HttpGet("testLike/{idEvent}/{pseudo}")]
        public async Task<IActionResult> Get(int idEvent, String pseudo)
        {
            model.NombreJaime testJaime = new model.NombreJaime();
            testJaime.EvenementId = idEvent;
            testJaime.UtilisateurId = pseudo;
            testJaime = await data.GetJaime(testJaime);
            Boolean jaimeTest = testJaime != null;
            return Ok(jaimeTest);
        }

        [HttpGet("count/{id}")]
        public ActionResult<int> Get(int id)
        {
            return data.CountAllNbJaime(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dto.NombreJaime jaime)
        {
            model.NombreJaime ajoutJaime = mapper.Map<model.NombreJaime>(jaime);
            try{
               ajoutJaime = await data.AjouteJaime(ajoutJaime);
            }catch{
                return Unauthorized();
            }
            
            return Created("api/NombreJaime" + ajoutJaime.Id, mapper.Map<dto.NombreJaime>(ajoutJaime));
        }

        [HttpDelete("{idEvent}/{pseudo}")]
        public async Task<IActionResult> Delete(int idEvent, String pseudo)
        {
            model.NombreJaime nombreJaimeFound = new model.NombreJaime();
            nombreJaimeFound.EvenementId = idEvent;
            nombreJaimeFound.UtilisateurId = pseudo;
            nombreJaimeFound = await data.GetJaime(nombreJaimeFound);
            if (nombreJaimeFound == null)
                return NotFound();

            await data.SupprimerJaimeAsync(nombreJaimeFound);
            return Ok(mapper.Map<dto.NombreJaime>(nombreJaimeFound));
        }
    }
}