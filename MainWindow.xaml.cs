using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using static Mader_Control_System.Login.LoginWindow;

namespace Mader_Control_System
{
    public partial class MainWindow : Window
    {
        private List<Cliente> clientes;
        private int clienteAtual = -1; // Índice do cliente atual

        public MainWindow()
        {
            InitializeComponent();

            clientesButton.Checked += ToggleButton_Checked;
            parceirosButton.Checked += ToggleButton_Checked;
            vendedoresButton.Checked += ToggleButton_Checked;

            clientesButton.Unchecked += ToggleButton_Unchecked;
            parceirosButton.Unchecked += ToggleButton_Unchecked;
            vendedoresButton.Unchecked += ToggleButton_Unchecked;

            CarregarClientes();
            ExibirCliente();

        }

        private void Close_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Minimized_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FileAdd_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ToggleVisibility(NewFileArea);
        }

        private void UserPage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ToggleVisibility(UserArea);
        }

        private void HomeButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NewFileArea.Visibility = Visibility.Collapsed;
            UserArea.Visibility = Visibility.Collapsed;
        }

        private void ToggleVisibility(UIElement element)
        {
            element.Visibility = element.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == clientesButton)
            {
                parceirosButton.IsChecked = false;
                vendedoresButton.IsChecked = false;
            }
            else if (sender == parceirosButton)
            {
                clientesButton.IsChecked = false;
                vendedoresButton.IsChecked = false;
            }
            else if (sender == vendedoresButton)
            {
                clientesButton.IsChecked = false;
                parceirosButton.IsChecked = false;
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // Comportamento padrão
        }

        private void ChangeButtonColor(Button button, string colorHex, string originalColorHex, double seconds = 0.1)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(seconds) };
            timer.Tick += (s, args) =>
            {
                button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(originalColorHex));
                timer.Stop();
            };
            timer.Start();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeButtonColor(saveButton, "#757A8B", "#8A90A6");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeButtonColor(cancelButton, "#D9D9D9", "#F2F2F2");
        }

        private void CarregarClientes()
        {
            clientes = DatabaseHelper.ObterClientes();
        }


        private void ExibirCliente()
        {
            if (clienteAtual >= 0 && clienteAtual < clientes.Count)
            {
                var cliente = clientes[clienteAtual];
                IdTextBox.Text = cliente.Id.ToString();
                NomeTextBox.Text = cliente.Nome;
                DataCadastroTextBox.Text = cliente.DataCadastro;
                NomeSocialTextBox.Text = cliente.NomeSocial;
                DataNascTextBox.Text = cliente.DataNasc;
                CPFTextBox.Text = cliente.CPF;
                RGTextBox.Text = cliente.RG;
                CNPJTextBox.Text = cliente.CNPJ;
                IETextBox.Text = cliente.IE;
                Celular01TextBox.Text = cliente.Celular01;
                Celular02TextBox.Text = cliente.Celular02;
                Email01TextBox.Text = cliente.Email01;
                Email02TextBox.Text = cliente.Email02;
                EnderecoTextBox.Text = cliente.Endereco;
                BairroTextBox.Text = cliente.Bairro;
                CidadeTextBox.Text = cliente.Cidade;
                EstadoTextBox.Text = cliente.Estado;
                CEPTextBox.Text = cliente.CEP;
                ComplementoTextBox.Text = cliente.Complemento;
                ReferenciasParaEntregaTextBox.Text = cliente.ReferenciasParaEntrega;
                ObservacoesGeraisTextBox.Text = cliente.ObservacoesGerais;
            }
            else
            {
                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            IdTextBox.Clear();
            NomeTextBox.Clear();
            DataCadastroTextBox.Clear();
            NomeSocialTextBox.Clear();
            DataNascTextBox.Clear();
            CPFTextBox.Clear();
            RGTextBox.Clear();
            CNPJTextBox.Clear();
            IETextBox.Clear();
            Celular01TextBox.Clear();
            Celular02TextBox.Clear();
            Email01TextBox.Clear();
            Email02TextBox.Clear();
            EnderecoTextBox.Clear();
            BairroTextBox.Clear();
            CidadeTextBox.Clear();
            EstadoTextBox.Clear();
            CEPTextBox.Clear();
            ComplementoTextBox.Clear();
            ReferenciasParaEntregaTextBox.Clear();
            ObservacoesGeraisTextBox.Clear();
        }

        private void PrimeiroCadastro_Click(object sender, RoutedEventArgs e)
        {
            clienteAtual = 0;
            ExibirCliente();
        }


        private void CadastroAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (clienteAtual > 0)
            {
                clienteAtual--;
                ExibirCliente();
            }
        }


        private void ProximoCadastro_Click(object sender, RoutedEventArgs e)
        {
            if (clienteAtual < clientes.Count - 1)
            {
                clienteAtual++;
                ExibirCliente();
            }
        }


        private void UltimoCadastro_Click(object sender, RoutedEventArgs e)
        {
            clienteAtual = clientes.Count - 1;
            ExibirCliente();
        }


        private void NovoCadastro_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
            clienteAtual = -1; // Indica novo cadastro
        }


        private void SalvarCadastro_Click(object sender, RoutedEventArgs e)
        {
            var cliente = new Cliente
            {
                Nome = NomeTextBox.Text,
                DataCadastro = DataCadastroTextBox.Text,
                NomeSocial = NomeSocialTextBox.Text,
                DataNasc = DataNascTextBox.Text,
                CPF = CPFTextBox.Text,
                RG = RGTextBox.Text,
                CNPJ = CNPJTextBox.Text,
                IE = IETextBox.Text,
                Celular01 = Celular01TextBox.Text,
                Celular02 = Celular02TextBox.Text,
                Email01 = Email01TextBox.Text,
                Email02 = Email02TextBox.Text,
                Endereco = EnderecoTextBox.Text,
                Bairro = BairroTextBox.Text,
                Cidade = CidadeTextBox.Text,
                Estado = EstadoTextBox.Text,
                CEP = CEPTextBox.Text,
                Complemento = ComplementoTextBox.Text,
                ReferenciasParaEntrega = ReferenciasParaEntregaTextBox.Text,
                ObservacoesGerais = ObservacoesGeraisTextBox.Text
            };

            if (clienteAtual == -1) // Novo cadastro
            {
                DatabaseHelper.AdicionarCliente(cliente);
            }
            else // Atualizar cadastro existente
            {
                cliente.Id = int.Parse(IdTextBox.Text);
                DatabaseHelper.AtualizarCliente(cliente);
            }

            CarregarClientes(); // Recarrega a lista de clientes após salvar
            clienteAtual = clientes.Count - 1; // Define o cliente atual para o último da lista
            ExibirCliente(); // Exibe o cliente atual
        }

        private void EditarCadastro_Click(object sender, RoutedEventArgs e)
        {
            if (clienteAtual >= 0)
            {
                // Permitir edição em todos os campos
                NomeTextBox.IsReadOnly = false;
                DataCadastroTextBox.IsReadOnly = false;
                NomeSocialTextBox.IsReadOnly = false;
                DataNascTextBox.IsReadOnly = false;
                CPFTextBox.IsReadOnly = false;
                RGTextBox.IsReadOnly = false;
                CNPJTextBox.IsReadOnly = false;
                IETextBox.IsReadOnly = false;
                Celular01TextBox.IsReadOnly = false;
                Celular02TextBox.IsReadOnly = false;
                Email01TextBox.IsReadOnly = false;
                Email02TextBox.IsReadOnly = false;
                EnderecoTextBox.IsReadOnly = false;
                BairroTextBox.IsReadOnly = false;
                CidadeTextBox.IsReadOnly = false;
                EstadoTextBox.IsReadOnly = false;
                CEPTextBox.IsReadOnly = false;
                ComplementoTextBox.IsReadOnly = false;
                ReferenciasParaEntregaTextBox.IsReadOnly = false;
                ObservacoesGeraisTextBox.IsReadOnly = false;

                // Habilitar botão de "Salvar" (se houver)
                SalvarButton.IsEnabled = true;
            }
        }


        private void DeletarCadastro_Click(object sender, RoutedEventArgs e)
        {
            if (clienteAtual >= 0)
            {
                // Confirmação antes de deletar
                var resultado = MessageBox.Show("Tem certeza que deseja deletar este cadastro?", "Confirmar Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    var clienteId = int.Parse(IdTextBox.Text);
                    DatabaseHelper.DeletarCliente(clienteId);
                    CarregarClientes(); // Recarrega a lista de clientes

                    // Atualiza o cliente atual
                    clienteAtual = clientes.Count > 0 ? 0 : -1; // Se houver clientes, mostra o primeiro; caso contrário, -1
                    ExibirCliente(); // Exibe o próximo cliente ou limpa a tela
                }
            }
        }

        private void CancelarCadastro_Click(object sender, RoutedEventArgs e)
        {
            ExibirCliente();
        }


    }
}
