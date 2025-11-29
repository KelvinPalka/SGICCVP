using MySql.Data.MySqlClient;

namespace WPF_Projeto_BD.Data
{
    public static class Connection
    {
        private const string CONNECTION_STRING =
             "server=localhost;user id=alunos;password=etec;database=MiniTCC_PNTJ;";

        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(CONNECTION_STRING);
            conn.Open();
            return conn;
        }
    }
}
