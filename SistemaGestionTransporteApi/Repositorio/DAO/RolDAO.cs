using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class RolDAO : IRol
    {
        private readonly string cadena;

        public RolDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }

        public IEnumerable<Rol> getRoles()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM rol", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Rol
                    {
                        IdRol = dr.GetInt32(0),
                        Descripcion = dr.GetString(1)
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public Rol getRol(int id)
        {
            return getRoles().FirstOrDefault(r => r.IdRol == id);
        } 
    }
}
