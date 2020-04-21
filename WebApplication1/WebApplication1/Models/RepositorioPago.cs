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
                string sql = $"INSERT INTO Pago(importe,fecha)" +
                    $"VALUES (@importe,@fec);" +
                    $"SELECT Scope_Identit();";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@importe",p.Importe);
                    command.Parameters.AddWithValue("@fec",p.FechaPago);
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
                string sql = $"UPDATE Pago SET importe=@importe,fehaPago=@fec" +
                            $"WHERE id_Pago=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@importe",p.Importe);
                    command.Parameters.AddWithValue("@fec",p.FechaPago);
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
                string sql = $"SELECT id_Pago,importe,fechaPago" +
                    $"FROM Pago";

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
                            Importe = reader.GetFloat(1),
                            FechaPago = reader.GetDateTime(2),
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }

            return res;
        }
    }
}
