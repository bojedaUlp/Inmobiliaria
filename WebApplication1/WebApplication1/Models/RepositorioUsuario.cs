using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RepositorioUsuario
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RepositorioUsuario(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration["ConnectionString:DefaultConnection"];
        }

        public int Alta(Usuario u)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Usuario(nombre,apellido,rol,email,password) " +
                    $"VALUES($nom,$ape,$rol,$email,$password); "  +
                    $"SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql,connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nom",u.Nombre);
                    command.Parameters.AddWithValue("@ape",u.Apellido);
                    command.Parameters.AddWithValue("@rol",u.Rol);
                    command.Parameters.AddWithValue("@email",u.Email);
                    command.Parameters.AddWithValue("@password",u.Password);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    u.Id_Usuario = res;
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
                string sql = "DELETE FROM Usuario WHERE id_Usuario=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
                return res;
            }
        }

        public int Modificacion(Usuario u)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Usuario SET nombre=@nom, apellido=@ape, rol=@rol, email=@email, password=@pass " +
                    $" WHERE id_Usuario = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@nom",u.Nombre);
                    command.Parameters.AddWithValue("@ape",u.Apellido);
                    command.Parameters.AddWithValue("@rol",u.Rol);
                    command.Parameters.AddWithValue("@email",u.Email);
                    command.Parameters.AddWithValue("@pass",u.Password);
                    command.Parameters.AddWithValue("@id",u.Id_Usuario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();

                }
            }

            return res;
        }

        public Usuario ObtenerPorId(int id)
        {
            Usuario res = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT id_Usuario,nombre,apellido,rol,email,password " +
                    $"FROM Usuario WHERE id_Usuario=@id ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Usuario
                        {
                            Id_Usuario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Rol = reader.GetInt32(3),
                            Email = reader.GetString(4),
                            Password = reader.GetString(5),
                        };
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Usuario> ObtenerTodos(int id)
        {
            IList<Usuario> res = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT id_Usuario,nombre,apellido,rol,email,password " +
                    $" FROM Usuario WHERE id_Usuario=@id ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                      Usuario  aux = new Usuario
                        {
                            Id_Usuario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Rol = reader.GetInt32(3),
                            Email = reader.GetString(4),
                            Password = reader.GetString(5),
                        };res.Add(aux);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Usuario ObtenerPorEmail(string  email)
        {
            Usuario res = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT id_Usuario,nombre,apellido,rol,email,password " +
                    $"FROM Usuario WHERE email=@email ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new Usuario
                        {
                            Id_Usuario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Rol = reader.GetInt32(3),
                            Email = reader.GetString(4),
                            Password = reader.GetString(5),
                        };
                    }
                    connection.Close();
                }
            }
            return res;
        }

    }
}
