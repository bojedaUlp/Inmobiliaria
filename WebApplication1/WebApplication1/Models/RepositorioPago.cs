using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RepositorioPago
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioPago(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionString:DefaultConnection"];
        }

        public int Alta(Pago p)
        {
            int res = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Pago(id_Contrato,importe,fechaPago) " +
                    $" VALUES (@id_C,@importe,@fec); " +
                    $" SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@importe",p.Importe);
                    command.Parameters.AddWithValue("@fec",p.FechaPago);
                    command.Parameters.AddWithValue("@id_C",p.Id_Contrato);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.Id_Pago = res;
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
                string sql = $"DELETE FROM Pago WHERE id_Pago=@id";

                using (SqlCommand command = new SqlCommand(sql,connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id",id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public int Modificacion(Pago p)
        {
            int res = -1;
            using (SqlConnection connection= new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Pago SET id_Contrato=@id_C, importe=@importe,fechaPago=@fec " +
                            $" WHERE id_Pago=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@importe",p.Importe);
                    command.Parameters.AddWithValue("@fec",p.FechaPago);
                    command.Parameters.AddWithValue("@id_C",p.Id_Contrato);
                    command.Parameters.AddWithValue("@id",p.Id_Pago);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        public IList<Pago>ObtenerTodos()
        {
            IList<Pago> res = new List<Pago>();

            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                string sql = $"SELECT id_Pago,p.id_Contrato,importe,fechaPago, " +
                    $" inqui.nombreI, inqui.apellidoI, i.direccionInm " +
                    $" FROM Pago p INNER JOIN Contrato c ON p.id_Contrato=c.id_Contrato " +
                    $" INNER JOIN Inmueble i ON c.id_Inmueble= i.id_Inmueble " +
                    $" INNER JOIN Inquilino inqui ON c.id_Inquilino= inqui.id_Inquilino ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pago p = new Pago
                        {
                            Id_Pago = reader.GetInt32(0),
                            Id_Contrato = reader.GetInt32(1),
                            Importe = reader.GetDecimal(2),
                            FechaPago = reader.GetDateTime(3),

                            Contrato = new Contrato
                            {
                                Id_Contrato = reader.GetInt32(1),
                                Inquilino = new Inquilino {

                                    NombreI = reader.GetString(4),
                                    ApellidoI = reader.GetString(5), },
                                Inmueble = new Inmueble
                                {
                                    
                                    DireccionInm = reader.GetString(6),
                                }
                               
                            }

                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }

            return res;
        }


        public Pago ObtenerPorId(int id)
        {
            Pago res = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT id_Pago,p.id_Contrato,importe,fechaPago, " +
                    $" inqui.nombreI, inqui.apellidoI, i.direccionInm " +
                    $" FROM Pago p INNER JOIN Contrato c ON p.id_Contrato=c.id_Contrato " +
                    $" INNER JOIN Inmueble i ON c.id_Inmueble= i.id_Inmueble " +
                    $" INNER JOIN Inquilino inqui ON c.id_Inquilino= inqui.id_Inquilino " +
                    $" WHERE p.id_Pago=@idP ";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idP",id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res = new Pago
                        {
                            Id_Pago = reader.GetInt32(0),
                            Id_Contrato = reader.GetInt32(1),
                            Importe = reader.GetDecimal(2),
                            FechaPago = reader.GetDateTime(3),

                            Contrato = new Contrato
                            {
                                Id_Contrato = reader.GetInt32(1),
                                Inquilino = new Inquilino
                                {

                                    NombreI = reader.GetString(4),
                                    ApellidoI = reader.GetString(5),
                                },
                                Inmueble = new Inmueble
                                {

                                    DireccionInm = reader.GetString(6),
                                }

                            }

                        };
                     
                    }
                    connection.Close();
                }
            }

            return res;
        }

    }
}
