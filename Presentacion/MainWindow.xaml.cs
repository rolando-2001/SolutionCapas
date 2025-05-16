using Entidades;
using Negocio;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Customer clienteSeleccionado;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {

            BusinessCustomer businessCustomer = new BusinessCustomer();
            List<Customer> customers = businessCustomer.ListCustomer();
            dgCustomer.ItemsSource = customers;


        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombre = txtSearch.Text.Trim();
            BusinessCustomer businessCustomer = new BusinessCustomer();

            if (!string.IsNullOrEmpty(nombre))
            {
                var resultado = businessCustomer.SearchCustomer(nombre);
                dgCustomer.ItemsSource = resultado;
            }
            else
            {
                // Si está vacío, mostramos todos los clientes
                var todos = businessCustomer.ListCustomer();
                dgCustomer.ItemsSource = todos;
            }
        }


        private bool RegisterNewCliente()
        {
            string nombre = txtNombre.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del cliente es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            BusinessCustomer businessCustomer = new BusinessCustomer();
            

            Customer nuevo = new Customer
            {
                Name = nombre,
                Address = direccion,
                Phone = telefono,
                
            };

            try
            {
                
                businessCustomer.AddCustomer(nuevo);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar cliente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomer.SelectedItem is Customer seleccionado)
            {
                clienteSeleccionado = seleccionado;
                txtNombre.Text = seleccionado.Name;
                txtDireccion.Text = seleccionado.Address;
                txtTelefono.Text = seleccionado.Phone;
            }
        }



        private bool UpdateC()
        {
            string nombre = txtNombre.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string telefono = txtTelefono.Text.Trim();


            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del cliente es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            clienteSeleccionado.Name= nombre;
            clienteSeleccionado.Address= direccion;
            clienteSeleccionado.Phone = telefono;

            try
            {
                BusinessCustomer businessCustomer = new BusinessCustomer();
                businessCustomer.UpdateCustomer(clienteSeleccionado);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar cliente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        private void createCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (RegisterNewCliente())
            {
                MessageBox.Show("Cliente registrado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtTelefono.Text = "";
                

                BusinessCustomer businessCustomer = new BusinessCustomer();
                dgCustomer.ItemsSource = businessCustomer.ListCustomer();
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateC())
            {
                MessageBox.Show("Cliente Actulizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtTelefono.Text = "";


                BusinessCustomer businessCustomer = new BusinessCustomer();
                dgCustomer.ItemsSource = businessCustomer.ListCustomer();
            }
        }



        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show("Por favor, selecciona un cliente para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            var result = MessageBox.Show($"¿Seguro que quieres eliminar al cliente {clienteSeleccionado.Name}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    BusinessCustomer businessCustomer = new BusinessCustomer();
                    businessCustomer.DeleteCustomer(clienteSeleccionado);

                    MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                   
                    txtNombre.Text = "";
                    txtDireccion.Text = "";
                    txtTelefono.Text = "";
                    clienteSeleccionado = null;

                    
                    dgCustomer.ItemsSource = businessCustomer.ListCustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar cliente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}