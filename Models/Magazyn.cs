using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace myRemiza.Models
{
    public partial class Magazyn
    {
        public Magazyn()
        {
           Sprzet  = new HashSet<Sprzet>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Sprzet> Sprzet { get; set; }
    }
}
