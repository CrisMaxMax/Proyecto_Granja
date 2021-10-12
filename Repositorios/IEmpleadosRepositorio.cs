using Farm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Repositorios
{
    interface IEmpleadosRepositorio
    {

        Task<bool> GuardarEmpleado(Empleado empleado);
        
        Task<IEnumerable<Empleado>> DameTodosLosEmpleados();
        
    }
}
