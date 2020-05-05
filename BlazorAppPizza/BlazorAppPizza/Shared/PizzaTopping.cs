using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAppPizza.Shared
{
    public class PizzaTopping
    {
        public Topping Topping { get; set; }

        public int ToppingId { get; set; }

        public int PizzaId { get; set; }
    }
}
