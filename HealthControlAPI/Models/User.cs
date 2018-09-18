using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthControlAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }

    }
}