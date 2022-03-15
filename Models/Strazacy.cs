using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myRemiza.Models
{
    public partial class Strazacy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data_urodzenia { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data_wstapienia { get; set; }
        public string Kursy { get; set; }
        public string Odznaczenia { get; set; }

    }
}
