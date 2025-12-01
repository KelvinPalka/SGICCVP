using MySql.Data.MySqlClient; // Importa a biblioteca do MySQL Connector, necessária para conectar no banco MySQL.

namespace WPF_Projeto_BD.Data // Namespace da camada de dados
{
    public static class Connection // Classe estática para gerenciar conexões com o banco
    {
        // String de conexão usada para acessar o banco de dados.
        // Contém servidor, usuário, senha e nome do banco.
        private const string CONNECTION_STRING =
             "server=localhost;user id=alunos;password=etec;database=MiniTCC_PNTJ;";

        // ==========================
        // Método para obter conexão
        // ==========================
        public static MySqlConnection GetConnection() // Retorna uma conexão aberta com o banco
        {
            var conn = new MySqlConnection(CONNECTION_STRING); // Cria uma nova conexão usando a string de conexão

            conn.Open(); // Abre a conexão antes de devolvê-la

            return conn; // Retorna a conexão aberta pronta para uso
        }
    }
}

/*
Connection é uma classe utilitária responsável por gerenciar a conexão com o banco MySQL.
- Armazena a string de conexão de forma centralizada.
- Permite obter conexões abertas de forma simples através do método estático GetConnection().
- Facilita o uso de DAOs, garantindo que todos usem a mesma configuração de conexão.
- Centraliza o gerenciamento de conexões, evitando duplicação de código em toda a aplicação.
*/
