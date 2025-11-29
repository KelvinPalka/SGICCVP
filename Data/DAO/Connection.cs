using MySql.Data.MySqlClient; // Importa a biblioteca do MySQL Connector, necessária para conectar no banco MySQL.

namespace WPF_Projeto_BD.Data
{
    public static class Connection
    {
        // String de conexão usada para acessar o banco de dados.
        // Ela contém: servidor, usuário, senha e nome do banco.
        private const string CONNECTION_STRING =
             "server=localhost;user id=alunos;password=etec;database=MiniTCC_PNTJ;";

        // Método público e estático que retorna uma conexão aberta.
        // Pode ser chamado de qualquer lugar sem precisar instanciar a classe.
        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(CONNECTION_STRING);
            // Cria uma nova conexão usando a string acima.

            conn.Open();
            // Abre a conexão antes de devolver.

            return conn;
            // Retorna a conexão aberta pronta para uso.
        }
    }
}
