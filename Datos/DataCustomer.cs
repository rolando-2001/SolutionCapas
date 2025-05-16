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
        public readonly string connectionString = "Data Source=LAB1502-11;Initial Catalog=db_factura;Integrated Security=True;;TrustServerCertificate=True";

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

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("USP_UpdateCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.Parameters.AddWithValue("@Address", (object?)customer.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@Phone", (object?)customer.Phone ?? DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("USP_DeleteCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;

                
                command.Parameters.AddWithValue("@Id", customer.CustomerId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }





    }
}
