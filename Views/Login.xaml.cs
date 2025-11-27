using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WPF_Projeto_BD.Views
{
    /// <summary>
    /// Lógica interna para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string login = txtUsuario.Text.Trim();
            string senha = txtSenha.Password.Trim(); 

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            try
            {
                  string connString = "server=localhost;database=MiniTCC;userid=root;password=@260914Zveek;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"SELECT u.id_usuario, u.id_empresa, e.nome_fantasia 
                                     FROM Usuario u
                                     INNER JOIN Empresa e ON u.id_empresa = e.id_empresa
                                     WHERE u.login=@login AND u.senha=@senha AND u.tipo_usuario='ADM'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int idEmpresa = reader.GetInt32(1);
                        string nomeFantasia = reader.GetString(2);

                        // Login OK, abrir MainWindow da empresa
                        var home = new Home();
                        home.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha inválidos.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro de conexão com o banco de dados: " + ex.Message);
            }
        }
    }
}

