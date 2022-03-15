using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myRemiza.Models
{
    public partial class Samochody
    {
        public Samochody()
        {
            Sprzet = new HashSet<Sprzet>();
            Wyjazdy = new HashSet<Wyjazdy>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Typ { get; set; }
        public string Oznaczenie { get; set; }
        public string Nr_rejestracyjny { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data_pozyskania { get; set; }
        [DataType(DataType.Date)]
        public DateTime Przeglad { get; set; }

        public virtual ICollection<Sprzet> Sprzet { get; set; }
        public virtual ICollection<Wyjazdy> Wyjazdy { get; set; }
    }
}
