using System.Windows; // Importa classes para exibir mensagens (MessageBox)
using MySql.Data.MySqlClient; // Importa classes para conectar e executar comandos no MySQL

public class ConnectionTest // Classe para testar a conexão com o banco
{
    // Método estático para testar a conexão
    public static void Test()
    {
        // String de conexão com servidor, usuário, senha e banco
        string connStr = "server=localhost; user id=alunos;password=etec;database=MiniTCC_PNTJ;";

        // Cria e abre a conexão dentro de um bloco using para garantir fechamento automático
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open(); // Abre a conexão com o banco

            MessageBox.Show("Conexão OK!"); // Exibe mensagem de sucesso se a conexão abrir corretamente
        } // A conexão é automaticamente fechada ao sair do bloco using
    }
}

/*
ConnectionTest é uma classe de utilitário para verificar se a conexão com o banco MySQL está funcionando.
- Cria uma conexão usando MySqlConnection e string de conexão.
- Abre a conexão e exibe uma mensagem de confirmação.
- O uso do bloco using garante que a conexão seja fechada automaticamente.
- Útil para testes iniciais de configuração do banco antes de rodar a aplicação completa.
*/
