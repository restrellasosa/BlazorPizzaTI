using BlazorAppPizza.Shared;
using BlazorAppPizza.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppPizza.Server.Controllers
{
    // [Route("api/[specials]")]
    [Route("specials")]
    [ApiController]
    public class SpecialsController : ControllerBase
    {
        private readonly PizzaStoreContex Context;
        public SpecialsController(PizzaStoreContex context)
        {
            this.Context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
        {
            return await Context.Specials
                .OrderByDescending(s => s.BasePrice).ToListAsync();
        }

    }

}
