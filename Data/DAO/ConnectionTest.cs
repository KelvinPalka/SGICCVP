using System.Windows;
using MySql.Data.MySqlClient;

public class ConnectionTest
{
    public static void Test()
    {
        string connStr = "server=localhost;database=MiniTCC;uid=root;pwd=@260914Zveek;";
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            MessageBox.Show("Conexão OK!");
        }
    }
}