using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myRemiza.Models
{
    public partial class Sprzet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Firma { get; set; }
        public string Rodzaj { get; set; }
        public string Nr_seryjny { get; set; }
        [Column("MagazynId")]
        public int MagazynId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data_pozyskania { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data_Przegladu { get; set; }

        public virtual Magazyn Magazyn { get; set; }
        public virtual Samochody Samochody { get; set; }
    }
}
