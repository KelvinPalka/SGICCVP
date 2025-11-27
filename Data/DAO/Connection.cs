using MySql.Data.MySqlClient;

namespace WPF_Projeto_BD.Data
{
    public static class Connection
    {
        private const string CONNECTION_STRING =
            "server=localhost;user id=root;password=SENHA;database=MiniTCC;";

        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(CONNECTION_STRING);
            conn.Open();
            return conn;
        }
    }
}
