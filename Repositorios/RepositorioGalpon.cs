using Farm.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Repositorios
{
    public class RepositorioGalpon : IGalponRepositorio
    {
        private string CadenaConexion;

        public RepositorioGalpon(String cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task<bool> GuardarGalpon(Galpon galpon)
        {
            Boolean galponCreado = false;
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.GalponProcedure";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@altitud", SqlDbType.Float).Value = galpon.altitud;
                Comm.Parameters.Add("@latitud", SqlDbType.Float).Value = galpon.latitud;
               
                await Comm.ExecuteNonQueryAsync();
                galponCreado = true;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error guardando datos galpon" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            return galponCreado;
        }

        public async Task<IEnumerable<Galpon>> DameTodosLosGalpones()
        {
            List<Galpon> lista = new List<Galpon>();
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.GalponLista";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Galpon c = new Galpon();
                    c.id_galpon = Convert.ToInt32(reader["id_galpon"]);
                    c.altitud = (float)Convert.ToDecimal(reader["altitud"]);
                    c.latitud = (float)Convert.ToDecimal(reader["latitud"]);
                    lista.Add(c);
                }
                reader.Close();

            }
            catch(SqlException ex)
            {
                throw new Exception("Error cargando datos galpon" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            return lista;
        }
    }   

}
