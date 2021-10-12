using Farm.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Farm.Repositorios
{
    public class RepositorioEmpleados : IEmpleadosRepositorio
    {
        private string CadenaConexion;

        public RepositorioEmpleados(String cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task<bool> GuardarEmpleado(Empleado empleado)
        {
            Boolean empleadoCreado = false;
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.EmpleadoProcedure";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = empleado.nombre;
                Comm.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = empleado.email;
                Comm.Parameters.Add("@telefono", SqlDbType.VarChar, 10).Value = empleado.telefono;
                Comm.Parameters.Add("@apellido", SqlDbType.VarChar ,50).Value = empleado.apellido;
                Comm.Parameters.Add("@direccion", SqlDbType.VarChar,50).Value = empleado.direccion;
                Comm.Parameters.Add("@tipo_empleado", SqlDbType.VarChar,50).Value = empleado.tipo_empleado;
                await Comm.ExecuteNonQueryAsync();
                empleadoCreado = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error guardando los datos de nuestro cliente " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return empleadoCreado;
        }

        public async Task<IEnumerable<Empleado>> DameTodosLosEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.EmpleadosLista";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Empleado c = new Empleado();
                    c.id_empleado = Convert.ToInt32(reader["id_empleado"]);
                    c.nombre = reader["nombre"].ToString();
                    c.apellido = reader["apellido"].ToString();
                    c.email = reader["email"].ToString();
                    c.direccion = reader["direccion"].ToString();
                    c.telefono = reader["telefono"].ToString();
                    c.tipo_empleado = reader["tipo_empleado"].ToString();
                    lista.Add(c);
                }
                reader.Close();

            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de empleados " + ex.Message);
            }
            finally
            {

                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return lista;
        }
        /*
        public async Task<Empleado> DameDatosEmpleado(int id_empleado)
        {
            Empleado c = new Empleado();
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.EmpleadosLista";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@id_empleado", SqlDbType.Int).Value = id_empleado;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                if (reader.Read())
                {
                    c.id_empleado = Convert.ToInt32(reader["id_empleado"]);
                    c.nombre = reader["Nombre"].ToString();
                    c.apellido = reader["apellido"].ToString();
                    c.email = reader["email"].ToString();
                    c.direccion = reader["direccion"].ToString();
                    c.telefono = reader["telefono"].ToString();
                    c.tipo_empleado = reader["tipo_empleado"].ToString();

                }
                reader.Close();

            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos del empleado " + ex.Message);
            }
            finally
            {

                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return c;
        }
        
        public async Task<bool> ModificarEmpleado(Empleado empleado)
        {
            Boolean empleadoModificado = false;
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.EmpleadoProcedure";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = empleado.nombre;
                Comm.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = empleado.email;
                Comm.Parameters.Add("@telefono", SqlDbType.VarChar, 10).Value = empleado.telefono;
                Comm.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = empleado.apellido;
                Comm.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = empleado.direccion;
                Comm.Parameters.Add("@tipo_empleado", SqlDbType.VarChar, 50).Value = empleado.tipo_empleado;


                if (empleado.nombre != null && empleado.apellido != null && empleado.email != null && empleado.telefono != null && empleado.direccion != null &&
                    empleado.tipo_empleado != null)
                    await Comm.ExecuteNonQueryAsync();

                empleadoModificado = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error guardando los datos del empleado " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return empleadoModificado;
        }
        /*
        public async Task<bool> BorrarCliente(int id)
        {
            Boolean clienteBorrado = false;
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuariosBaja";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@id", SqlDbType.Int).Value = id;

                if (id>0)
                    await Comm.ExecuteNonQueryAsync();

                clienteBorrado = true;

            }
            catch (SqlException ex)
            {
                throw new Exception("Error borrando nuestro cleinte " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return clienteBorrado;
        }

        public async Task<IEnumerable<Empleado>> DameTodosLosCLientes(string busqueda)
        {
            List<Empleado> lista = new List<Empleado>();
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuariosLista";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@busqueda", SqlDbType.VarChar, 500).Value = busqueda;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Empleado c = new Empleado();
                    c.Id = Convert.ToInt32(reader["Id"]);
                    c.Nombre = reader["Nombre"].ToString();
                    c.Email = reader["Email"].ToString();
                    c.Telefono = reader["Telefono"].ToString();
                    lista.Add(c);
                }
                reader.Close();

            }
            catch (SqlException ex)
            {
                throw new Exception("Error cargando los datos de nuestros cliente " + ex.Message);
            }
            finally
            {

                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return lista;
        }*/



    }
}
