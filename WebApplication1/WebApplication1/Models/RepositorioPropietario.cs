﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RepositorioPropietario
    {
		private readonly string connectionString;
		private readonly IConfiguration configuration;

		public RepositorioPropietario(IConfiguration configuration) 
		{
			this.configuration = configuration;
			connectionString = configuration["ConnectionString:DefaultConnection"];
		}

		public int Alta(Propietario p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Propietario (nombreP,apellidoP,domicilioP,dniP,telefonoP,emailP,claveP) " +
					$"VALUES (@nombre, @apellido, @domicilio, @dni, @telefono, @email, @claveP);" +
					$"SELECT SCOPE_IDENTITY();";//devuelve el id insertado
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", p.NombreP);
					command.Parameters.AddWithValue("@apellido", p.ApellidoP);
					command.Parameters.AddWithValue("@domicilio", p.DomicilioP);
					command.Parameters.AddWithValue("@dni", p.DniP);
					command.Parameters.AddWithValue("@telefono", p.TelefonoP);
					command.Parameters.AddWithValue("@email", p.EmailP);
					command.Parameters.AddWithValue("@claveP", p.ClaveP);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					p.Id_Propietario = res;
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
				string sql = $"DELETE FROM Propietario WHERE id_Propietario = @id";
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
		public int Modificacion(Propietario p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Propietario SET nombreP=@nombre, apellidoP=@apellido,dniP=@dni, domicilioP=@domicilio, telefonoP=@telefono, emailP=@email, claveP=@claveP " +
					$" WHERE id_Propietario = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", p.NombreP);
					command.Parameters.AddWithValue("@apellido", p.ApellidoP);
					command.Parameters.AddWithValue("@domicilio", p.DomicilioP);
					command.Parameters.AddWithValue("@dni", p.DniP);
					command.Parameters.AddWithValue("@telefono", p.TelefonoP);
					command.Parameters.AddWithValue("@email", p.EmailP);
					command.Parameters.AddWithValue("@claveP", p.ClaveP);
					command.Parameters.AddWithValue("@id", p.Id_Propietario);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Propietario> ObtenerTodos()
		{
			IList<Propietario> res = new List<Propietario>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT id_Propietario, nombreP, apellidoP, domicilioP, dniP,telefonoP,emailP, claveP " +
					$" FROM Propietario";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Propietario p = new Propietario
						{
							Id_Propietario = reader.GetInt32(0),
							NombreP = reader.GetString(1),
							ApellidoP = reader.GetString(2),
							DomicilioP = reader.GetString(3),
							DniP = reader.GetString(4),
							TelefonoP = reader.GetString(5),
							EmailP = reader.GetString(6),
							ClaveP=reader.GetString(7),
							
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Propietario ObtenerPorId(int id)
		{
			Propietario p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT id_Propietario,nombreP,apellidoP,domicilioP,dniP,telefonoP,emailP, claveP FROM Propietario" +
					$" WHERE id_Propietario=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Propietario
						{
							Id_Propietario = reader.GetInt32(0),
							NombreP = reader.GetString(1),
							ApellidoP = reader.GetString(2),
							DomicilioP = reader.GetString(3),
							DniP = reader.GetString(4),
							TelefonoP = reader.GetString(5),
							EmailP = reader.GetString(6),
							ClaveP = reader.GetString(7),
						};
					}
					connection.Close();
				}
			}
			return p;
		}

		public Propietario ObtenerPorEmail(string email)
		{
			Propietario p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT id_Propietario,nombreP,apellidoP,domicilioP,dniP,telefonoP,emailP, claveP  FROM Propietario" +
					$" WHERE emailP=@email";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Propietario
						{
							Id_Propietario = reader.GetInt32(0),
							NombreP = reader.GetString(1),
							ApellidoP = reader.GetString(2),
							DomicilioP = reader.GetString(3),
							DniP = reader.GetString(4),
							TelefonoP = reader.GetString(5),
							EmailP = reader.GetString(6),
							ClaveP = reader.GetString(7),
						};
					}
					connection.Close();
				}
			}
			return p;
		}

		public IList<Propietario> BuscarPorNombre(string nombre)
		{
			List<Propietario> res = new List<Propietario>();
			Propietario p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT id_Propietario, nombreP, apellidoP,domicilioP, dniP, telefonoP, emailP, claveP  FROM Propietario" +
					$" WHERE nombreP LIKE %@nombre% OR apellidoP LIKE %@nombre";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						p = new Propietario
						{
							Id_Propietario = reader.GetInt32(0),
							NombreP = reader.GetString(1),
							ApellidoP = reader.GetString(2),
							DomicilioP = reader.GetString(3),
							DniP = reader.GetString(4),
							TelefonoP = reader.GetString(5),
							EmailP = reader.GetString(6),
							ClaveP = reader.GetString(7),

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
