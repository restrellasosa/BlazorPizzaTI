using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppPizza.Server.Models;
using BlazorAppPizza.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppPizza.Server.Controllers
{
    [Route("toppings")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly PizzaStoreContex Context;
        public ToppingsController(PizzaStoreContex context)
        {
            this.Context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Topping>>> GetToppings()
        {
            return await Context.Toppings
                .OrderBy(t => t.Name).ToListAsync();
        }

    }
}
