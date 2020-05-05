using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorAppPizza.Shared
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        [Required(ErrorMessage = "How do you expect to receive the pizza if we don't even know what city you're in?"), MaxLength(50)]
        public string City { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
