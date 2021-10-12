using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Data;
using Farm.Interfaces;
using Farm.Repositorios;

namespace Farm.Servicios
{
    public class ServicioGalpon : IGalponServices
    {
        private IGalponRepositorio galponRepositorio;
        private SqlConfiguracion configuracion;

        public ServicioGalpon(SqlConfiguracion c)
        {
            configuracion = c;
            galponRepositorio = new RepositorioGalpon(configuracion.CadenaConexion);
        }

        public Task<bool> GuardarGalpon(Galpon galpon)
        {
            if (galpon.id_galpon == 0)
                return galponRepositorio.GuardarGalpon(galpon);
            else
                return null;
        }
        public Task<IEnumerable<Galpon>> DameTodosLosGalpones()
        {
            return galponRepositorio.DameTodosLosGalpones();
        }
    }



}
