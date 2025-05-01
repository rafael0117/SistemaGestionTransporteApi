using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Data.SqlClient;
using SistemaGestionTransporteApi.Models;
using SistemaGestionTransporteApi.Repositorio.Interfaces;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaGestionTransporteApi.Repositorio.DAO
{
    public class DestinoDAO : IDestino
    {
        private readonly string cadena;

        public DestinoDAO()
        {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("sql");
        }

        public IEnumerable<Destino> getDestinos()
        {
            List<Destino> lista = new List<Destino>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM destino", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Destino
                    {
                        IdDestino = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        imagen = dr.IsDBNull(2) ? null : dr.GetString(2)
                    });
                }
                dr.Close();
            }
            return lista;
        }

        public Destino getDestino(int id)
        {
            return getDestinos().FirstOrDefault(d => d.IdDestino == id);
        }

        public async Task<string> insertDestino(Destino destino, IFormFile imagen)
        {
            string mensaje = "";

            try
            {
                // 1. Abrir el stream de la imagen
                using Stream image = imagen.OpenReadStream();
                string urlimagen = await SubirStorage(image, imagen.FileName);
                Console.WriteLine("✅ Imagen subida correctamente. URL: " + urlimagen);

                // 2. Crear conexión SQL y ejecutar SP
                using SqlConnection cn = new SqlConnection(cadena);
                using SqlCommand cmd = new SqlCommand("usp_insertar_destino", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre_des", destino.nombre);
                cmd.Parameters.AddWithValue("@imagen", urlimagen);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                mensaje = $"✅ Se ha registrado {i} destino(s).";
                Console.WriteLine(mensaje);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("❌ Error SQL: " + sqlEx.Message);
                Console.WriteLine("Código de error SQL: " + sqlEx.ErrorCode);
                mensaje = "Error al registrar en la base de datos: " + sqlEx.Message;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("❌ Error al leer la imagen: " + ioEx.Message);
                mensaje = "Error al procesar la imagen.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error general: " + ex.Message);
                mensaje = "Ocurrió un error inesperado: " + ex.Message;
            }

            return mensaje;
        }


        public async Task<string> updateDestino(Destino destino, IFormFile imagen)
		{
			string urlimagen = destino.imagen; // Mantener imagen anterior si no se sube nueva

			if (imagen != null)
			{
				Stream image = imagen.OpenReadStream();
				urlimagen = await SubirStorage(image, imagen.FileName);
			}

			string mensaje = "";
			using (SqlConnection cn = new SqlConnection(cadena))
			{
				try
				{
					SqlCommand cmd = new SqlCommand("usp_actualizar_destino", cn);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@id_destino", destino.IdDestino);
					cmd.Parameters.AddWithValue("@nombre_des", destino.nombre);
					cmd.Parameters.AddWithValue("@imagen", urlimagen ?? (object)DBNull.Value);
					cn.Open();
					int i = cmd.ExecuteNonQuery();
					mensaje = $"Se ha actualizado {i} destino(s).";
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


		public string deleteDestino(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminar_destino", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_destino", id);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} destino(s).";
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
        public async Task<string> SubirStorage(Stream archivo, string nombre)
        {
            string email = "mamanirafael324@gmail.com";
            string clave = "core123";
            string ruta = "sistemagestiontransportecore.firebasestorage.app";
            string api_key = "AIzaSyAY4kmse206_ctVgRXD3qjdum6Bui0yDAU";

            try
            {
                // 1. Autenticación
                var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
                var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

                Console.WriteLine("✅ Autenticación exitosa.");

                // 2. Subida al storage
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    ruta,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("Fotos_Destinos")
                    .Child(nombre)
                    .PutAsync(archivo, cancellation.Token);

                var downloadUrl = await task;

                Console.WriteLine("✅ Archivo subido correctamente. URL: " + downloadUrl);
                return downloadUrl;
            }
            catch (FirebaseAuthException ex)
            {
                Console.WriteLine("❌ Error de autenticación: " + ex.Message);
            }
            catch (FirebaseStorageException ex)
            {
                Console.WriteLine("❌ Error al subir el archivo a Firebase Storage: " + ex.Message);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("❌ La operación fue cancelada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error inesperado: " + ex.Message);
            }

            // Si falla, devolvemos cadena vacía
            return string.Empty;
        }

    }
}
