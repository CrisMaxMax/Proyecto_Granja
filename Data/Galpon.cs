using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Data
{
    public class Galpon
    {
        public int id_galpon { get; set; }
        public int id_veterinario { get; set; }
        public int id_operario { get; set; }

        [Required(ErrorMessage = "* Obligatorio")]
        public float latitud { get; set; }
        [Required(ErrorMessage = "* Obligatorio")]
        public float altitud { get; set; }
        
    }
}
