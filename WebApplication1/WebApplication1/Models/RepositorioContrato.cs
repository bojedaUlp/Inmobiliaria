using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RepositorioContrato
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioContrato(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionString:DefaultConnection"];
        }

        public int Alta(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Contrato(id_Inmueble,id_Inquilino,fechaDesde,fechaHasta,importeMensual,estadoContrato)  " +
                    $"VALUES(@idInmueble,@idInquilino,@fechaD,@fechaH,@importe,@estado)" +
                    $"SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idInmueble",c.Id_Inmueble);
                    command.Parameters.AddWithValue("@idIquilino",c.Id_Inquilino);
                    command.Parameters.AddWithValue("@fechaD",c.FechaDesde);
                    command.Parameters.AddWithValue("@fechaH",c.FechaHasta);
                    command.Parameters.AddWithValue("@importe",c.ImporteMensual);
                    command.Parameters.AddWithValue("@estado",c.EstadoContrato);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    c.Id_Contrato = res;
                    connection.Close();
                }
            }
                    return res;
        }
        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Contrato WHERE id_Contrato=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Contrato SET id_Inmueble=@idInmueble, id_Inquilino=@inqui, fechaDesde=@fechaD, fechaHasta=@fechaH, importeMensual=@importe, estadoContrato=@estado" +
                        $"WHERE id_Contrato=@idC;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idInmueble",c.Id_Inmueble);
                    command.Parameters.AddWithValue("@inqui",c.Id_Inquilino);
                    command.Parameters.AddWithValue("@fechaD",c.FechaDesde);
                    command.Parameters.AddWithValue("@fechaH",c.FechaHasta);
                    command.Parameters.AddWithValue("@importe", c.ImporteMensual);
                    command.Parameters.AddWithValue("@estado",c.EstadoContrato);
                    command.Parameters.AddWithValue("@idC",c.Id_Contrato);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public IList<Contrato> ObtenerTodos()
        {
            IList<Contrato> res = new List<Contrato>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $" SELECT id_Contrato, c.id_Inquilino, c.id_Inmueble , fechaDesde, fechaHasta, importeMensual, estadoContrato, " +
                    " i.nombreI, i.apellidoI " + " inm.DirecionInm " +
                    $" FROM Contrato c INNER JOIN Inmueble inm ON c.id_Inmueble = inm.id_Inmueble " +
                    $" INNER JOIN Inquilino i ON c.id_Inquilino = i.id_Inquilino ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contrato c = new Contrato
                        {
                            Id_Contrato = reader.GetInt32(0),
                            Id_Inquilino= reader.GetInt32(1),
                            Id_Inmueble= reader.GetInt32(2),
                            FechaDesde= reader.GetDateTime(3),
                            FechaHasta= reader.GetDateTime(4),
                            ImporteMensual= reader.GetDecimal(5),
                            EstadoContrato= reader.GetInt32(6),

                            Inquilino = new Inquilino
                            {
                                Id_Inquilino=reader.GetInt32(1),
                                NombreI=reader.GetString(7),
                                ApellidoI=reader.GetString(8),
                            },

                            Inmueble=new Inmueble
                            {
                                Id_Inmueble=reader.GetInt32(2),
                                DireccionInm=reader.GetString(9)
                            }


                        }; res.Add(c);
                    }

                }connection.Close();
            }

            return res;

        }

        public Contrato ObtenerPorId(int id)
        {
            Contrato res = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $" SELECT id_Contrato, c.id_Inquilino, c.id_Inmueble , fechaDesde, fechaHasta, importeMensual, estadoContrato, " +
                    " i.nombreI, i.apellidoI " + " in.DirecionInm " +
                    $"FROM Contrato c INNER JOIN Inmueble in ON c.id_Inmueble = in.id_Inmueble " +
                    $" INNER JOIN Inquilino i ON c.id_Inquilino = i.id_Inquilino " +
                    $" WHERE c.id_Contrato = @idC ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idC",id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contrato c = new Contrato
                        {
                            Id_Contrato = reader.GetInt32(0),
                            Id_Inquilino = reader.GetInt32(1),
                            Id_Inmueble = reader.GetInt32(2),
                            FechaDesde = reader.GetDateTime(3),
                            FechaHasta = reader.GetDateTime(4),
                            ImporteMensual = reader.GetDecimal(5),
                            EstadoContrato = reader.GetInt32(6),

                            Inquilino = new Inquilino
                            {
                                Id_Inquilino = reader.GetInt32(1),
                                NombreI = reader.GetString(7),
                                ApellidoI = reader.GetString(8),
                            },

                            Inmueble = new Inmueble
                            {
                                Id_Inmueble = reader.GetInt32(2),
                                DireccionInm = reader.GetString(9)
                            }


                        };
                    }

                }
                connection.Close();
            }

            return res;

        }

    }
}
