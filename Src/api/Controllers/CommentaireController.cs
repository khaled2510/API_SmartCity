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
    public class CommentaireController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly dal.CommentaireData data;
        MapperConfiguration configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<model.Commentaire, dto.Commentaire>();
            cfg.CreateMap<model.Commentaire, dto.Commentaire>().ReverseMap();
        });
        public CommentaireController()
        {
            this.mapper = new Mapper(configuration);
            this.data = new dal.CommentaireData(new dal.BDAgendaContext());
        }

        [HttpGet("report/{pageSize}/{pageIndex}")]
        [Authorize(Roles = model.Constants.Roles.Admin)]
        public async Task<IActionResult> GetAllCommentReport(int pageSize = 5, int pageIndex = 0)
        {
            IEnumerable<model.Commentaire> commentairesSignaler = await data.GetCommentaireSignaler(pageSize, pageIndex);
            if(commentairesSignaler == null )
                return NotFound();
            return Ok(commentairesSignaler.Select(mapper.Map<dto.Commentaire>));
        }

        [HttpGet("commentEvent/{idEvent}/{pageSize}/{pageIndex}")]
        public async Task<IActionResult> GetAllCommentEvent(int idEvent, int pageSize = 5, int pageIndex = 0)
        {
            IEnumerable<model.Commentaire> commentaires = await data.GetCommentaireEvent(idEvent, pageSize, pageIndex);
            if(commentaires == null || !commentaires.Any())
                return NotFound();

            return Ok(commentaires.Select(mapper.Map<dto.Commentaire>));
        }

        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            return data.CountAll(id);
        }

        [HttpGet("report")]
        [Authorize(Roles = model.Constants.Roles.Admin)]
        public ActionResult<int> GetCountReport()
        {
            return data.CountReport();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dto.Commentaire nouveauCommentaire)
        {
            model.Commentaire commentaireAjout = mapper.Map<model.Commentaire>(nouveauCommentaire);
            commentaireAjout = await data.AjouteCommentaire(commentaireAjout);

            return Created("api/Commentaire" + commentaireAjout.Id, mapper.Map<dto.Commentaire>(commentaireAjout));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = model.Constants.Roles.Professionnel)]
        public async Task<IActionResult> Put(int id, [FromBody] dto.Commentaire commentaireSignaler)
        {
            model.Commentaire commentaireFind = await data.GetCommentaire(id);
            if (commentaireFind == null)
                return NotFound();

            commentaireFind = data.SignalerCommentaire(commentaireFind, commentaireSignaler);

            return Ok(mapper.Map<dto.Commentaire>(commentaireFind));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = model.Constants.Roles.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            model.Commentaire commentaireFind = await data.GetCommentaire(id);
            if (commentaireFind == null)
                return NotFound();

            await data.SupprimerCommentaireAsync(commentaireFind);
            return Ok();
        }
    }
}