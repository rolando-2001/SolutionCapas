using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BusinessCustomer
    {

        public List<Customer> ListCustomer() {

            DataCustomer data = new DataCustomer();
            List<Customer> customers = data.ObtenerCustomers();
            return customers; 

        }

        public List<Customer> SearchCustomer(string name) {
            DataCustomer data = new DataCustomer();
            List<Customer> customers = data.ObtenerCustomers();
            return customers
                .Where(x => x.Name != null && x.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

        }

        public void AddCustomer(Customer customer)
        {
            DataCustomer data = new DataCustomer();
            data.CrearCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            DataCustomer data = new DataCustomer();
            data.UpdateCustomer(customer);
        }
        public void DeleteCustomer(Customer customer) {
            DataCustomer data = new DataCustomer();
            data.DeleteCustomer(customer);
        }
    }
}
