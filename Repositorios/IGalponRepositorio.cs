using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Data;

namespace Farm.Repositorios
{
    interface IGalponRepositorio
    {
        Task<bool> GuardarGalpon(Galpon galpon);

        Task<IEnumerable<Galpon>> DameTodosLosGalpones();
    }
}
