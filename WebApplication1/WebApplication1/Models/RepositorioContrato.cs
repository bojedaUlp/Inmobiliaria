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
                string sql = $"INSERT INTO Contrato(id_Inmueble,id_Inquilino,id_Pago,fechaDesde,fechaHsta,importeMensual,estadoContrato) " +
                    $"VALUES(@idInmueble,@idInquilino,@idP,@fechaD,@fechaH,@importe,@estado)" +
                    $"SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@idInmueble",c.Id_Inmueble);
                    command.Parameters.AddWithValue("@idIquilino",c.Id_Inquilino);
                    command.Parameters.AddWithValue("@idP",c.Id_Pago);
                    command.Parameters.AddWithValue("@fechaD",c.FechaDesde);
                    command.Parameters.AddWithValue("@fechaH",c.FechaHasta);
                    command.Parameters.AddWithValue("@importe",c.ImporteMensual);
                    command.Parameters.AddWithValue("@estado",c.estadoContrato);
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
                string sql = "";
            }
        }

    }
}
