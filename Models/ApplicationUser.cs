using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace myRemiza.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Nazwa_jednostki { get; set; }
        public int UsernameChangeLimit { get; set; } = 5;
    }
}
