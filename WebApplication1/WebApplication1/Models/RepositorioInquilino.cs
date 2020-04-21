using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RepositorioInquilino
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioInquilino(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionString:DefaultConnection"];
        }

        public int Alta(Inquilino i)
        {
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Inquilino (nombreI,apellidoI,dniI,domicilioI,telefonoI,oficioI,nombreG,apellidoG,dniG,telefonoG,domicilioG) " +
					$"VALUES (@nombreI, @apellidoI, @dniI, @domicilioI, @telefonoI, @oficioI, @nombreG, @apellidoG, @dniG, @telefonoG, domicilioG);" +
					$"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombreI", i.NombreI);
					command.Parameters.AddWithValue("@apellidoI", i.ApellidoI);
					command.Parameters.AddWithValue("@domicilioI", i.DomicilioI);
					command.Parameters.AddWithValue("@dniI", i.DniI);
					command.Parameters.AddWithValue("@telefonoI", i.TelefonoI);
					command.Parameters.AddWithValue("@oficioI", i.OficioI);
					command.Parameters.AddWithValue("@nombreG", i.NombreG);
					command.Parameters.AddWithValue("@apellidoG", i.ApellidoG);
					command.Parameters.AddWithValue("@dniG", i.DniG);
					command.Parameters.AddWithValue("@telefonoG", i.TelefonoG);
					command.Parameters.AddWithValue("@domicilioG", i.DomicilioG);

					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					i.Id_Inquilino = res;
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
				string sql = $"DELETE FROM Inquilino WHERE id_Inquilino = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
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

		public int Modificacion(Inquilino i) 
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Inquilino SET nombreI=@nombreI, apellidoI=@apellidoI, dniI=@dniI, domicilioI=@domicilioI, telefonoI=@telefonoI,oficioI=@oficioI,nombreG=@nombreG,apellidoG=@apellidoG,dniG=@dniG,telefonoG=@telefonoG,domicilioG=@domicilioG" +
				$"WHERE id_Inquilino=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombreI", i.NombreI);
					command.Parameters.AddWithValue("@apellidoI", i.ApellidoI);
					command.Parameters.AddWithValue("@domicilioI", i.DomicilioI);
					command.Parameters.AddWithValue("@dniI", i.DniI);
					command.Parameters.AddWithValue("@telefonoI", i.TelefonoI);
					command.Parameters.AddWithValue("@oficioI", i.OficioI);
					command.Parameters.AddWithValue("@nombreG", i.NombreG);
					command.Parameters.AddWithValue("@apellidoG", i.ApellidoG);
					command.Parameters.AddWithValue("@dniG", i.DniG);
					command.Parameters.AddWithValue("@telefonoG", i.TelefonoG);
					command.Parameters.AddWithValue("@domicilioG", i.DomicilioG);
					command.Parameters.AddWithValue("@id", i.Id_Inquilino);

					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					connection.Close();
				}
			}

			return res;
		}

		public IList<Inquilino> ObtenerTodos()
		{
			IList<Inquilino> res = new List<Inquilino>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT nombreI,apellidoI,dniI,domicilioI,telefonoI,oficioI,nombreG,apellidoG,dniG,telefonoG,domicilioG" +
					$" FROM Inquilino";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inquilino i = new Inquilino()
						{
							Id_Inquilino= reader.GetInt32(0),
							NombreI = reader.GetString(1),
							ApellidoI = reader.GetString(2),
							DniI = reader.GetString(3),
							DomicilioI = reader.GetString(4),
							TelefonoI = reader.GetString(5),
							OficioI = reader.GetString(6),
							NombreG=reader.GetString(7),
							ApellidoG=reader.GetString(8),
							DniG=reader.GetString(9),
							TelefonoG=reader.GetString(10),
							DomicilioG=reader.GetString(11),

						};
						res.Add(i);
					}
					connection.Close();
				}
			}
			return res;
		}
	}
}
