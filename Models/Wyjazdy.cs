using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myRemiza.Models
{
    public partial class Wyjazdy
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Miejsce { get; set; }
        [Column("Rodzaj_zdarzeniaId")]
        public int Rodzaj_zdarzeniaId { get; set; }
        public string Strazacy { get; set; }
        [Column("SamochodyId")]
        public int SamochodyId { get; set; }
        [Column("SamochodyId2")]
        public int SamochodyId2 { get; set; }
        public string Opis { get; set; }

        public virtual Samochody Samochody { get; set; }
        public virtual Rodzaj_zdarzenia Rodzaj_zdarzenia { get; set; }
    }
}
