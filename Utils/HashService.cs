using System; // Importa namespaces essenciais do C#
using System.Diagnostics; // Necessário para usar Debug.WriteLine
using System.Security.Cryptography; // Necessário para geração de hash e salt
using System.Text; // Necessário para codificação de strings em bytes

namespace WPF_Projeto_BD.Utils // Define o namespace da aplicação (Utils)
{
    public static class HashService // Classe estática para geração e verificação de hashes com salt
    {
        // Gera um hash SHA-256 a partir de uma senha, usando um salt aleatório
        // Retorna uma tupla contendo o hash e o salt gerado
        public static (string hash, string salt) GerarHashComSalt(string senha)
        {
            byte[] saltBytes = new byte[16]; // Cria um array de 16 bytes para o salt

            using (var rng = new RNGCryptoServiceProvider()) // Cria um gerador de números aleatórios seguro
                rng.GetBytes(saltBytes); // Preenche o array saltBytes com valores aleatórios

            string salt = BytesParaHex(saltBytes); // Converte o array de bytes do salt em uma string hexadecimal
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha + salt); // Concatena a senha com o salt e converte para bytes

            byte[] hashBytes; // Array de bytes que armazenará o hash calculado
            using (SHA256Managed sha = new SHA256Managed()) // Cria uma instância do SHA-256
                hashBytes = sha.ComputeHash(senhaBytes); // Calcula o hash SHA-256 da senha + salt

            string hash = BytesParaHex(hashBytes); // Converte o hash em string hexadecimal

            // DEBUG: exibe informações detalhadas no Output do Visual Studio
            Debug.WriteLine("====== HASH SERVICE: GERAR ======"); // Separador visual
            Debug.WriteLine("Senha digitada: " + senha); // Mostra a senha digitada
            Debug.WriteLine("Salt gerado: " + salt); // Mostra o salt gerado
            Debug.WriteLine("Hash gerado: " + hash); // Mostra o hash gerado
            Debug.WriteLine("================================="); // Separador visual
            // FIM DEBUG

            return (hash, salt); // Retorna a tupla contendo hash e salt
        }

        // Verifica se a senha digitada corresponde ao hash armazenado, usando o salt armazenado
        public static bool VerificarHashComSalt(string senhaDigitada, string hashArmazenado, string saltArmazenado)
        {
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senhaDigitada + saltArmazenado);
            // Concatena a senha digitada com o salt armazenado e converte para bytes

            byte[] hashBytes; // Array de bytes para armazenar o hash recalculado
            using (SHA256Managed sha = new SHA256Managed()) // Cria instância do SHA-256
                hashBytes = sha.ComputeHash(senhaBytes); // Calcula o hash SHA-256 da senha digitada + salt

            string hashDigitada = BytesParaHex(hashBytes); // Converte o hash recalculado para string hexadecimal

            // DEBUG: exibe informações detalhadas no Output do Visual Studio
            Debug.WriteLine("====== HASH SERVICE: VERIFICAR ======"); // Separador visual
            Debug.WriteLine("Senha digitada: " + senhaDigitada); // Mostra a senha digitada
            Debug.WriteLine("Salt armazenado: " + saltArmazenado); // Mostra o salt armazenado
            Debug.WriteLine("Hash recalculado: " + hashDigitada); // Mostra o hash recalculado
            Debug.WriteLine("Hash armazenado: " + hashArmazenado); // Mostra o hash armazenado
            Debug.WriteLine("Resultado: " + (hashDigitada == hashArmazenado)); // Mostra se os hashes coincidem
            Debug.WriteLine("====================================="); // Separador visual
            // FIM DEBUG

            return hashDigitada.Equals(hashArmazenado, StringComparison.OrdinalIgnoreCase);
            // Retorna true se o hash recalculado for igual ao hash armazenado (ignorando maiúsculas/minúsculas)
        }

        // Converte um array de bytes em string hexadecimal
        private static string BytesParaHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(); // Cria um StringBuilder para montar a string hexadecimal
            foreach (byte b in bytes) // Percorre cada byte do array
                sb.Append(b.ToString("X2")); // Converte o byte em hexadecimal com 2 dígitos e adiciona ao StringBuilder
            return sb.ToString(); // Retorna a string hexadecimal completa
        }
    }
}

/*
Resumo técnico:
- HashService é uma classe utilitária para gerar e verificar hashes SHA-256 de senhas com salt.
- Garante maior segurança no armazenamento de senhas, prevenindo ataques de rainbow table.
- Possui métodos estáticos: GerarHashComSalt (gera hash e salt) e VerificarHashComSalt (valida senha).
- Inclui método auxiliar BytesParaHex para conversão de bytes para hexadecimal.
- Implementa DEBUG detalhado para auxiliar no desenvolvimento e validação do algoritmo.
*/
