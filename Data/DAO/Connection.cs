using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

public class Connection
{
    private static string connectionString =
        "server=localhost;database=MiniTCC;userid=root;password=@260914Zveek;";

    public static MySqlConnection GetConnection()
    {
        var conn = new MySqlConnection(connectionString);
        conn.Open();
        return conn;
    }
}
