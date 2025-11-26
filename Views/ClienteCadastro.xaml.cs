using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Projeto_BD.Models;
using WPF_Projeto_BD.Controllers;
using System.Text.RegularExpressions;

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para ClienteCadastro.xaml
    /// </summary>
    public partial class ClienteCadastro : Window
    {
        private Cliente clienteEditando = null;

        // CONSTRUTOR PARA CADASTRO NOVO
        public ClienteCadastro()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(txtCPF_CNPJ, OnPasteOnlyDigits);
            DataObject.AddPastingHandler(txtTelefone, OnPasteOnlyDigits);
        }

        // CONSTRUTOR PARA EDIÇÃO
        public ClienteCadastro(Cliente cliente)
        {
            InitializeComponent();
            DataObject.AddPastingHandler(txtCPF_CNPJ, OnPasteOnlyDigits);
            DataObject.AddPastingHandler(txtTelefone, OnPasteOnlyDigits);

            clienteEditando = cliente;

            // Preencher campos da tela
            txtNome.Text = cliente.Nome;
            txtCPF_CNPJ.Text = cliente.CPF_CNPJ;
            txtEndereco.Text = cliente.Endereco;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;
        }

        private bool ValidarCampos()
        {
            // Nome
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome não pode estar vazio.");
                return false;
            }

            // CPF/CNPJ
            if (string.IsNullOrWhiteSpace(txtCPF_CNPJ.Text))
            {
                MessageBox.Show("CPF/CNPJ não pode estar vazio.");
                return false;
            }

            if (txtCPF_CNPJ.Text.Length < 11) 
            {
                MessageBox.Show("CPF/CNPJ inválido.");
                return false;
            }

            // Endereço
            if (string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                MessageBox.Show("O endereço não pode estar vazio.");
                return false;
            }

            // Telefone
            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("O telefone não pode estar vazio.");
                return false;
            }

            if (txtTelefone.Text.Length < 8)
            {
                MessageBox.Show("Telefone inválido.");
                return false;
            }

            // Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("O email não pode estar vazio.");
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Email inválido.");
                return false;
            }

            // Tudo certo
            return true;
        }


        private void Salvar(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
                return;

            ClienteController controller = new ClienteController();

            // Se for um novo cliente
            if (clienteEditando == null)
            {
                string cpfLimpo = Regex.Replace(txtCPF_CNPJ.Text, "[^0-9]", "");
                string telefoneLimpo = Regex.Replace(txtTelefone.Text, "[^0-9]", "");

                Cliente c = new Cliente
                {
                    Nome = txtNome.Text.Trim(),
                    CPF_CNPJ = cpfLimpo,
                    Endereco = txtEndereco.Text.Trim(),
                    Telefone = telefoneLimpo,
                    Email = txtEmail.Text.Trim()
                };


                controller.SalvarCliente(c);
                MessageBox.Show("Cliente cadastrado com sucesso!");
            }
            else
            {
                // Atualização
                clienteEditando.Nome = txtNome.Text;
                clienteEditando.Endereco = txtEndereco.Text;
                clienteEditando.CPF_CNPJ = Regex.Replace(txtCPF_CNPJ.Text, "[^0-9]", "");
                clienteEditando.Telefone = Regex.Replace(txtTelefone.Text, "[^0-9]", "");
                clienteEditando.Email = txtEmail.Text;

                controller.EditarCliente(clienteEditando);
                MessageBox.Show("Cliente atualizado com sucesso!");
            }

            // Voltar à lista
            var clienteLista = new ClienteLista();
            clienteLista.Show();
            this.Close();
        }

        private static readonly Regex OnlyDigits = new Regex(@"[^\d]", RegexOptions.Compiled);

        private void MaskCPF_CNPJ(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // permite apenas números na digitação direta
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void FormatCPF_CNPJ(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;

            // salva posição original do cursor
            int selStart = tb.SelectionStart;

            // limpa o texto (apenas dígitos)
            string digits = OnlyDigits.Replace(tb.Text, "");

            // limita o tamanho (CPF 11, CNPJ 14) — evita entradas inválidas enormes
            if (digits.Length > 14) digits = digits.Substring(0, 14);

            string formatted;
            if (digits.Length <= 11)
            {
                // formato CPF: 000.000.000-00
                formatted = digits;
                if (formatted.Length > 3) formatted = formatted.Insert(3, ".");
                if (formatted.Length > 7) formatted = formatted.Insert(7, ".");
                if (formatted.Length > 11) formatted = formatted.Insert(11, "-");
            }
            else
            {
                // formato CNPJ: 00.000.000/0000-00
                formatted = digits;
                if (formatted.Length > 2) formatted = formatted.Insert(2, ".");
                if (formatted.Length > 6) formatted = formatted.Insert(6, ".");
                if (formatted.Length > 10) formatted = formatted.Insert(10, "/");
                if (formatted.Length > 15) formatted = formatted.Insert(15, "-");
            }

            // atualizar sem provocar recursão
            tb.TextChanged -= FormatCPF_CNPJ;
            tb.Text = formatted;
            // tenta colocar o cursor numa posição coerente
            tb.SelectionStart = Math.Min(formatted.Length, selStart + (formatted.Length - digits.Length));
            tb.TextChanged += FormatCPF_CNPJ;
        }

        private void MaskTelefone(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void FormatTelefone(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            int selStart = tb.SelectionStart;

            string digits = OnlyDigits.Replace(tb.Text, "");

            // limita razoavelmente (DDD + 9 dígitos = 11, ou 10)
            if (digits.Length > 11) digits = digits.Substring(0, 11);

            string formatted = digits;

            if (digits.Length >= 1)
            {
                // insere parênteses quando houver pelo menos 2 dígitos de DDD
                if (digits.Length >= 2)
                {
                    formatted = $"({digits.Substring(0, 2)})";
                    string rest = digits.Substring(2);
                    if (rest.Length <= 4)
                    {
                        formatted += " " + rest;
                    }
                    else if (rest.Length <= 8)
                    {
                        // caso 8 dígitos: NNNN-NNNN
                        formatted += " " + rest.Substring(0, rest.Length - 4) + "-" + rest.Substring(rest.Length - 4);
                    }
                    else
                    {
                        // 9 dígitos: NNNNN-NNNN
                        formatted += " " + rest.Substring(0, rest.Length - 4) + "-" + rest.Substring(rest.Length - 4);
                    }
                }
                else
                {
                    formatted = digits; // ainda incompleto para DDD
                }
            }

            tb.TextChanged -= FormatTelefone;
            tb.Text = formatted;
            tb.SelectionStart = Math.Min(formatted.Length, selStart + (formatted.Length - digits.Length));
            tb.TextChanged += FormatTelefone;
        }

        private void OnPasteOnlyDigits(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = e.DataObject.GetData(DataFormats.Text) as string;
                if (!Regex.IsMatch(text, @"^\d+$"))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


    }
}
