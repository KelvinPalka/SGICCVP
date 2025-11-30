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

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para EmpresaDados.xaml
    /// </summary>
    public partial class EmpresaDados : Window
    {
        private Usuario usuarioLogado;
        private int idEmpresa; // Campo da classe
        private EmpresaController controller;

        public EmpresaDados(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogado = usuario;
            idEmpresa = usuarioLogado.IdEmpresa;
            controller = new EmpresaController(usuarioLogado);
            CarregarEmpresa();
        }

        private void CarregarEmpresa()
        {
            Empresa empresa = controller.ObterEmpresa(usuarioLogado.IdEmpresa);
            int idEmpresa = usuarioLogado.IdEmpresa;

            if (empresa == null)
            {
                MessageBox.Show("Nenhuma empresa encontrada.");
                return;
            }

            lblCNPJ.Text = empresa.CNPJ;
            lblNomeFantasia.Text = empresa.Nome_fantasia;
            lblRazaoSocial.Text = empresa.Razao_social;
            lblEmail.Text = empresa.Email;
            lblTelefone.Text = empresa.Telefone;
            lblEndereco.Text = empresa.Endereco;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            // Passa usuário logado e id da empresa
            EmpresaEditar editarWindow = new EmpresaEditar(usuarioLogado, idEmpresa);
            editarWindow.Show();
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Config configWindow = new Config(usuarioLogado);
            configWindow.Show();
            this.Close();
        }
    }
}
