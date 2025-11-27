using System.Windows;
using MySql.Data.MySqlClient;

public class ConnectionTest
{
    public static void Test()
    {
        string connStr = "server=localhost;user id=root;password=SENHA;database=MiniTCC;";
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            MessageBox.Show("Conexão OK!");
        }
    }
}