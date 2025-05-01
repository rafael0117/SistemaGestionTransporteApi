using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;
using System.Data;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class PersonalDAO : IPersonal
    {
        private readonly string cadena;

        public PersonalDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }

        public IEnumerable<Personal> getPersonales()
        {
            List<Personal> lista = new List<Personal>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM personal", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Personal
                    {
                        IdPersonal = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Apellido = dr.GetString(2),
                        Dni = dr.GetString(3),
                        Telefono = dr.IsDBNull(4) ? null : dr.GetString(4),
                        Email = dr.IsDBNull(5) ? null : dr.GetString(5),
                        Direccion = dr.IsDBNull(6) ? null : dr.GetString(6),
                        IdRol = dr.GetInt32(7)
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public Personal getPersonal(int id)
        {
            return getPersonales().FirstOrDefault(p => p.IdPersonal == id);
        }

        public string insertPersonal(Personal personal)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_insertar_personal", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", personal.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", personal.Apellido);
                    cmd.Parameters.AddWithValue("@dni", personal.Dni);
                    cmd.Parameters.AddWithValue("@telefono", string.IsNullOrEmpty(personal.Telefono) ? DBNull.Value : personal.Telefono);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(personal.Email) ? DBNull.Value : personal.Email);
                    cmd.Parameters.AddWithValue("@direccion", string.IsNullOrEmpty(personal.Direccion) ? DBNull.Value : personal.Direccion);
                    cmd.Parameters.AddWithValue("@idrol", personal.IdRol);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha registrado {i} personal(es).";
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

        public string updatePersonal(Personal personal)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_actualizar_personal", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idpersonal", personal.IdPersonal);
                    cmd.Parameters.AddWithValue("@nombre", personal.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", personal.Apellido);
                    cmd.Parameters.AddWithValue("@dni", personal.Dni);
                    cmd.Parameters.AddWithValue("@telefono", string.IsNullOrEmpty(personal.Telefono) ? DBNull.Value : personal.Telefono);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(personal.Email) ? DBNull.Value : personal.Email);
                    cmd.Parameters.AddWithValue("@direccion", string.IsNullOrEmpty(personal.Direccion) ? DBNull.Value : personal.Direccion);
                    cmd.Parameters.AddWithValue("@idrol", personal.IdRol);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} personal(es).";
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

        public string deletePersonal(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_personal", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idpersonal", id);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} personal(es).";
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
