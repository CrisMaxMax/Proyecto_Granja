using Farm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Interfaces
{
    interface IEmpleadosServices
    {
        Task<bool> GuardarEmpleado(Empleado empleado);

        Task<IEnumerable<Empleado>> DameTodosLosEmpleados();
       
    }
}
