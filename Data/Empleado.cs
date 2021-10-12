using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Data
{
    public class Empleado
    {

        public int id_empleado { get; set; }

        [Required(ErrorMessage = "* Obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "* Obligatorio")]
        public string apellido { get; set; }
        [Required(ErrorMessage = "* Obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "* Mail incorreto")]
        public string email { get; set; }
        [Required(ErrorMessage = "* Obligatorio")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "* Obligatorio")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "* Obligatorio")]
        public string tipo_empleado { get; set; }
    }
        
}
