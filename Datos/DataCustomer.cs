using Entidades;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DataCustomer
    {
        public readonly string connectionString = "Data Source=LAB1502-17;Initial Catalog=Semana7;Integrated Security=True;;TrustServerCertificate=True";

        public List<Customer> ObtenerCustomers()
        {
            var lista = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("USP_ListarCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Customer
                    {
                        CustomerId = Convert.ToInt32(reader["customer_id"]),
                        Name = reader["name"].ToString()!,
                        Address = reader["address"]?.ToString(),
                        Phone = reader["phone"]?.ToString(),
                        Active = Convert.ToBoolean(reader["active"])
                    });
                }

                reader.Close();
            }

            return lista;
        }

        public void CrearCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("USP_CreateCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Address", (object?)customer.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@Phone", (object?)customer.Phone ?? DBNull.Value);
                

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


    }
}
