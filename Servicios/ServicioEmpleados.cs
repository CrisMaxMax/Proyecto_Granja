using Farm.Data;
using Farm.Interfaces;
using Farm.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Servicios
{
    public class ServicioEmpleados: IEmpleadosServices
    {
        private IEmpleadosRepositorio empleadosRepositorio;
        private SqlConfiguracion configuracion;

        public ServicioEmpleados(SqlConfiguracion c)
        {
            configuracion = c;
            empleadosRepositorio = new RepositorioEmpleados(configuracion.CadenaConexion);
        }
        public Task<bool> GuardarEmpleado(Empleado empleado)
        {
            if (empleado.id_empleado == 0)
                return empleadosRepositorio.GuardarEmpleado(empleado);
            else
                return null;
        }

        public Task<IEnumerable<Empleado>> DameTodosLosEmpleados()
        {
            return empleadosRepositorio.DameTodosLosEmpleados();
        }
       
    }
}
