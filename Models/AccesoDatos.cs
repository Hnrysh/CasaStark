using System.Data;
using System.Data.SqlClient;

namespace CasaStark.Models
{
    public class AccesoDatos
    {
        private readonly string _conexion;

        public AccesoDatos(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("Conexion");
        }

        public List<usuario> ObtenerUsuario()
        {
            List<usuario> lista = new List<usuario>();
             

            using (SqlConnection con = new SqlConnection(_conexion))
            {
                SqlCommand cmd = new SqlCommand("spObtenerUsuarios", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                //quite{}despues de la linea de arriba que incluye el while
                {
                    while (reader.Read())
                    {
                        lista.Add(new usuario
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Correo= reader["Correo"].ToString(),
                            Contrasena= reader["Contrasena"].ToString(),
                            Rol = reader["Rol"].ToString()

                        });
                    }
                }
            }
            return lista;
        }

        public void AgregarUsuario(usuario usuarioNuevo) 
        { using (SqlConnection con = new SqlConnection(_conexion)) 
            { try { string query = "Exec spCrearUsuarios @pNombre, @pCorreo, @pContrasena, @pRol"; 
                    using (SqlCommand cmd = new SqlCommand(query, con)) 
                    { cmd.Parameters.AddWithValue("@pNombre", usuarioNuevo.Nombre); 
                        cmd.Parameters.AddWithValue("@pCorreo", usuarioNuevo.Correo); 
                        cmd.Parameters.AddWithValue("@pContrasena", usuarioNuevo.Contrasena);
                        cmd.Parameters.AddWithValue("@pRol", usuarioNuevo.Rol);
                        con.Open(); 
                        cmd.ExecuteNonQuery(); } } catch (Exception ex) { throw new Exception("Error al registrar el usuario: " + ex.Message); 
                } 
            } 
        }

        public void ModificarUsuario(usuario usuarioModificar) 
        { using (SqlConnection con = new SqlConnection(_conexion)) 
            { try 
                { string query = "Exec spModificarUsuario @pId, @pNombre, @pCorreo, @pContrasena,@pRol"; 
                    using (SqlCommand cmd = new SqlCommand(query, con)) 
                    { cmd.Parameters.AddWithValue("@pId", usuarioModificar.Id); 
                        cmd.Parameters.AddWithValue("@pNombre", usuarioModificar.Nombre); 
                        cmd.Parameters.AddWithValue("@pCorreo", usuarioModificar.Correo); 
                        cmd.Parameters.AddWithValue("@pContrasena", usuarioModificar.Contrasena);
                        cmd.Parameters.AddWithValue("@pRol", usuarioModificar.Rol);
                        con.Open(); cmd.ExecuteNonQuery(); 
                    }
                } catch (Exception ex) 
                { 
                    throw new Exception("Error al modificar el usuario: " + ex.Message); 
                } 
            } 
        }

        public bool EliminarUsuario(int Id)
        {
            bool eliminado = false;

            using (SqlConnection con = new SqlConnection(_conexion))
            {
                try
                {
                    string query = "Exec spEliminarUsuarios @pId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@pId", Id); // Usamos el ID del usuario
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar el usuario: " + ex.Message);
                }
            }
            return eliminado;
        }
    }
}
