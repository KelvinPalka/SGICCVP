using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace WPF_Projeto_BD.Utils
{
    public static class HashService
    {
        public static (string hash, string salt) GerarHashComSalt(string senha)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(saltBytes);

            string salt = BytesParaHex(saltBytes);
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha + salt);

            byte[] hashBytes;
            using (SHA256Managed sha = new SHA256Managed())
                hashBytes = sha.ComputeHash(senhaBytes);

            string hash = BytesParaHex(hashBytes);

            // DEBUG
            Debug.WriteLine("====== HASH SERVICE: GERAR ======");
            Debug.WriteLine("Senha digitada: " + senha);
            Debug.WriteLine("Salt gerado: " + salt);
            Debug.WriteLine("Hash gerado: " + hash);
            Debug.WriteLine("=================================");
            // FIM DEBUG

            return (hash, salt);
        }

        public static bool VerificarHashComSalt(string senhaDigitada, string hashArmazenado, string saltArmazenado)
        {
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senhaDigitada + saltArmazenado);

            byte[] hashBytes;
            using (SHA256Managed sha = new SHA256Managed())
                hashBytes = sha.ComputeHash(senhaBytes);

            string hashDigitada = BytesParaHex(hashBytes);

            // 🔍 DEBUG
            Debug.WriteLine("====== HASH SERVICE: VERIFICAR ======");
            Debug.WriteLine("Senha digitada: " + senhaDigitada);
            Debug.WriteLine("Salt armazenado: " + saltArmazenado);
            Debug.WriteLine("Hash recalculado: " + hashDigitada);
            Debug.WriteLine("Hash armazenado: " + hashArmazenado);
            Debug.WriteLine("Resultado: " + (hashDigitada == hashArmazenado));
            Debug.WriteLine("=====================================");
            // 🔍 FIM DEBUG

            return hashDigitada.Equals(hashArmazenado, StringComparison.OrdinalIgnoreCase);
        }

        private static string BytesParaHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
