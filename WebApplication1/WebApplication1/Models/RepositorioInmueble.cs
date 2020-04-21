using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RepositorioInmueble
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioInmueble(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionString:DefaultConnection"];
        }

        public int Alta(Inmueble i) 
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string sql = "$INSERT INTO Inmueble(id_Propeario,direccionInm,uso,tipo,cantAmbientes,precioInm,estadoInm)" +
                    $"VALUES (@idP,@dire,@uso,@tipo,@cantA,@precio,@estado)" +
                    $"SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idP", i.Id_Propietario);
                    command.Parameters.AddWithValue("dire",i.DireccionInm);
                    command.Parameters.AddWithValue("@uso",i.Uso);
                    command.Parameters.AddWithValue("@tipo",i.Tipo);
                    command.Parameters.AddWithValue("@cantA",i.CantAmbientes);
                    command.Parameters.AddWithValue("@precio",i.PrecioInm);
                    command.Parameters.AddWithValue("@estado",i.EstadoInm);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    i.Id_Inmueble = res;
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Inmueble WHERE id_Inmueble=@idI ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idI",id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            return res;
        }

        public int Modificacion(Inmueble i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inmueble SET direccionInm=@dire, uso=@uso, tipo=@tipo, cantAmbientes=@cantA,precioInm=@precio,estadoInm=@estado" +
                    $"WHERE id_Inmueble=@idI;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@dire",i.DireccionInm);
                    command.Parameters.AddWithValue("@uso",i.Uso);
                    command.Parameters.AddWithValue("@tipo",i.Tipo);
                    command.Parameters.AddWithValue("@cantA",i.CantAmbientes);
                    command.Parameters.AddWithValue("@precio",i.PrecioInm);
                    command.Parameters.AddWithValue("@estado",i.EstadoInm);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }


        public IList<Inmueble> ObtenerTodos()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT direccionInm, uso, tipo cantAmbientes,precioInm,estadoIm,id_Propietario" + "p.nombreP,p.apellidoP" +
                    "FROM Inmueble i INNER JOIN Propietario p ON i.id_Propietario = p.id_Propietario";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble i = new Inmueble
                        {
                            Id_Inmueble = reader.GetInt32(0),
                            Id_Propietario = reader.GetInt32(1),
                            DireccionInm = reader.GetString(2),
                            Uso = reader.GetString(3),
                            Tipo = reader.GetString(4),
                            CantAmbientes = reader.GetInt32(5),
                            PrecioInm = reader.GetFloat(6),
                            EstadoInm = reader.GetInt32(7),

                            /*  Propietario p = new Propietario
                              {
                                  NombreP= reader.GetString(8),
                                  ApellidoP=reader.GetString(9),

                              }*/

                        }; res.Add(i);

                     }

                    connection.Close();
                }
            }
            return res;
        }
    }
}
