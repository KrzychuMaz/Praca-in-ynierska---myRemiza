using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myRemiza.Models
{
    public partial class Rodzaj_zdarzenia
    {
        public Rodzaj_zdarzenia()
        {
            Wyjazdy = new HashSet<Wyjazdy>();
        }
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Wyjazdy> Wyjazdy { get; set; }
    }
}
