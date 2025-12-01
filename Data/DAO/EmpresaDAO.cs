using System; // Importa classes básicas do .NET
using MySql.Data.MySqlClient; // Importa classes para conectar e executar comandos no MySQL
using WPF_Projeto_BD.Models; // Importa os modelos (Empresa)

namespace WPF_Projeto_BD.Data.DAO // Namespace da camada de acesso a dados
{
    public class EmpresaDAO // DAO para gerenciar persistência de dados da entidade Empresa
    {
        // ==========================
        // Inserir uma nova empresa
        // ==========================
        public int Inserir(Empresa empresa) // Método para inserir uma empresa e retornar o ID gerado
        {
            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                // Comando SQL para inserir empresa e retornar o último ID gerado
                var cmd = new MySqlCommand(@"
                    INSERT INTO empresa 
                    (cnpj, nome_fantasia, email, telefone, razao_social, endereco)
                    VALUES
                    (@CNPJ, @NomeFantasia, @Email, @Telefone, @Razao, @Endereco);

                    SELECT LAST_INSERT_ID();
                ", conn);

                // Adiciona os parâmetros ao comando SQL
                cmd.Parameters.AddWithValue("@CNPJ", empresa.CNPJ); // CNPJ da empresa
                cmd.Parameters.AddWithValue("@NomeFantasia", empresa.Nome_fantasia); // Nome fantasia
                cmd.Parameters.AddWithValue("@Email", empresa.Email); // E-mail da empresa
                cmd.Parameters.AddWithValue("@Telefone", empresa.Telefone); // Telefone
                cmd.Parameters.AddWithValue("@Razao", empresa.Razao_social); // Razão social
                cmd.Parameters.AddWithValue("@Endereco", empresa.Endereco); // Endereço

                return Convert.ToInt32(cmd.ExecuteScalar()); // Executa INSERT e retorna ID da empresa inserida
            }
        }

        // ==========================
        // Obter empresa pelo ID
        // ==========================
        public Empresa ObterEmpresaPorId(int idEmpresa) // Método para consultar empresa pelo ID
        {
            Empresa emp = null; // Inicializa variável de retorno como null

            using (var conn = Connection.GetConnection()) // Abre conexão com o banco
            {
                // Comando SQL para selecionar a empresa pelo ID
                string sql = @"
                    SELECT * 
                    FROM empresa
                    WHERE id_empresa = @id";

                var cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@id", idEmpresa); // Adiciona parâmetro ID

                using (var dr = cmd.ExecuteReader()) // Executa consulta e obtém leitor de dados
                {
                    if (dr.Read()) // Se encontrou algum registro
                    {
                        // Cria objeto Empresa e preenche com dados do banco
                        emp = new Empresa
                        {
                            Id = dr.GetInt32("id_empresa"), // ID da empresa
                            CNPJ = dr.GetString("cnpj"), // CNPJ
                            Nome_fantasia = dr.GetString("nome_fantasia"), // Nome fantasia
                            Razao_social = dr.GetString("razao_social"), // Razão social
                            Email = dr.GetString("email"), // E-mail
                            Telefone = dr.GetString("telefone"), // Telefone
                            Endereco = dr.GetString("endereco") // Endereço
                        };
                    }
                }
            }

            return emp; // Retorna o objeto Empresa ou null se não encontrado
        }

        // ==========================
        // Editar empresa existente
        // ==========================
        public bool Editar(Empresa empresa) // Método para atualizar dados de uma empresa
        {
            try
            {
                using (var conn = Connection.GetConnection()) // Abre conexão
                {
                    // Comando SQL para atualizar empresa
                    var cmd = new MySqlCommand(@"
                        UPDATE empresa
                        SET cnpj = @CNPJ,
                            nome_fantasia = @NomeFantasia,
                            email = @Email,
                            telefone = @Telefone,
                            razao_social = @Razao,
                            endereco = @Endereco
                        WHERE id_empresa = @Id;
                    ", conn);

                    // Adiciona parâmetros ao comando
                    cmd.Parameters.AddWithValue("@CNPJ", empresa.CNPJ); // CNPJ
                    cmd.Parameters.AddWithValue("@NomeFantasia", empresa.Nome_fantasia); // Nome fantasia
                    cmd.Parameters.AddWithValue("@Email", empresa.Email); // E-mail
                    cmd.Parameters.AddWithValue("@Telefone", empresa.Telefone); // Telefone
                    cmd.Parameters.AddWithValue("@Razao", empresa.Razao_social); // Razão social
                    cmd.Parameters.AddWithValue("@Endereco", empresa.Endereco); // Endereço
                    cmd.Parameters.AddWithValue("@Id", empresa.Id); // ID da empresa a atualizar

                    cmd.ExecuteNonQuery(); // Executa UPDATE
                }

                return true; // Retorna true se sucesso
            }
            catch
            {
                return false; // Retorna false em caso de erro
            }
        }

        // ==========================
        // Verifica se CNPJ já existe
        // ==========================
        public bool ExisteCNPJ(string cnpj) // Método para verificar duplicidade de CNPJ
        {
            using (var conn = Connection.GetConnection()) // Abre conexão
            {
                string sql = "SELECT COUNT(*) FROM empresa WHERE cnpj = @cnpj"; // SQL para contar registros com mesmo CNPJ
                var cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@cnpj", cnpj); // Adiciona parâmetro CNPJ

                long count = (long)cmd.ExecuteScalar(); // Executa consulta e obtém contagem
                return count > 0; // Retorna true se já existir
            }
        }

        // ==========================
        // Verifica se e-mail já existe
        // ==========================
        public bool ExisteEmail(string email) // Método para verificar duplicidade de e-mail
        {
            using (var conn = Connection.GetConnection()) // Abre conexão
            {
                string sql = "SELECT COUNT(*) FROM empresa WHERE email = @email"; // SQL para contar registros com mesmo e-mail
                var cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@email", email); // Adiciona parâmetro e-mail

                long count = (long)cmd.ExecuteScalar(); // Executa consulta e obtém contagem
                return count > 0; // Retorna true se já existir
            }
        }

        // ==========================
        // Verifica se Razão Social já existe
        // ==========================
        public bool ExisteRazaoSocial(string razaoSocial) // Método para verificar duplicidade de razão social
        {
            using (var conn = Connection.GetConnection()) // Abre conexão
            {
                string sql = "SELECT COUNT(*) FROM empresa WHERE razao_social = @razaoSocial"; // SQL para contar registros com mesma razão social
                var cmd = new MySqlCommand(sql, conn); // Cria comando SQL
                cmd.Parameters.AddWithValue("@razaoSocial", razaoSocial); // Adiciona parâmetro razão social

                long count = (long)cmd.ExecuteScalar(); // Executa consulta e obtém contagem
                return count > 0; // Retorna true se já existir
            }
        }
    }
}

/*
EmpresaDAO gerencia a persistência de dados da entidade Empresa no banco MySQL.
- Permite inserir, consultar, editar empresas.
- Fornece métodos especializados para verificar duplicidade de CNPJ, e-mail ou razão social.
- Utiliza parâmetros SQL para evitar injeção de dados e garantir segurança.
- Centraliza toda a lógica de acesso a dados da entidade Empresa, separando a camada de negócios (Controller) da persistência.
*/
