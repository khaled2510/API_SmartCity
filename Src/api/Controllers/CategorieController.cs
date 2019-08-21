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
    public class CategorieController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly dal.CategorieData data;
        MapperConfiguration configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<model.Categorie, dto.Categorie>();
            cfg.CreateMap<model.Categorie, dto.Categorie>().ReverseMap();
        });
        public CategorieController()
        {
            this.mapper = new Mapper(configuration);
            this.data = new dal.CategorieData(new dal.BDAgendaContext());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<model.Categorie> categories = await data.getAllCategories();
            if(categories == null || !categories.Any())
                return NotFound();
            return Ok(categories.Select(mapper.Map<dto.Categorie>));
        }
    }
}
