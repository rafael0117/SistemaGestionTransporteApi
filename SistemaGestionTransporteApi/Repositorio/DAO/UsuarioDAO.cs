using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;
using System.Data;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class UsuarioDAO : IUsuario
    {
        private readonly string cadena;

        public UsuarioDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }
       
        public IEnumerable<Usuario> getUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM usuario", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        IdUsuario = dr.GetInt64(0),
                        Nombres = dr.GetString(1),
                        Apellidos = dr.GetString(2),
                        Username = dr.GetString(3),
                        Clave = dr.GetString(4),
                        IdRol = dr.GetInt32(5),
                        Correo = dr.GetString(6),
                        Direccion = dr.IsDBNull(7) ? null : dr.GetString(7)                   
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public Usuario getUsuario(long id)
        {
            return getUsuarios().FirstOrDefault(r => r.IdUsuario == id);
        }

        public Usuario login(Usuario usuario)
        {
            Usuario user = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("usp_login_usuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", usuario.Username);
                cmd.Parameters.AddWithValue("@clave", usuario.Clave);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    user = new Usuario
                    {
                        IdUsuario = dr.GetInt64(0),
                        Nombres = dr.GetString(1),
                        Apellidos = dr.GetString(2),
                        Username = dr.GetString(3),
                        Clave = dr.GetString(4),
                        IdRol = dr.GetInt32(5),
                        Correo = dr.GetString(6),
                        Direccion = dr.IsDBNull(7) ? null : dr.GetString(7)
                    };
                }
                dr.Close();
            }
            return user;
        }

        public string insertUsuarioCliente(Usuario usuario)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_usuario_cliente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@usuario", usuario.Username);
                    cmd.Parameters.AddWithValue("@clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@idrol", usuario.IdRol);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@direccion", string.IsNullOrEmpty(usuario.Direccion) ? DBNull.Value : usuario.Direccion);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha registrado {i} usuario(s).";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }
    }
}
