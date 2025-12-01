using System.Collections.Generic; // Importa listas genéricas
using Wpf_Projeto_BD.Models; // Importa os modelos (Funcionario)
using WPF_Projeto_BD.Data.DAO; // Importa os DAOs para acesso ao banco de dados

namespace WPF_Projeto_BD.Controllers // Define o namespace da aplicação
{
    internal class FuncionarioController // Controller responsável pela lógica de negócios de Funcionários
    {
        private FuncionarioDAO funcionarioDao = new FuncionarioDAO(); // DAO para operações com funcionários

        // ==========================
        // Cadastrar novo funcionário
        // ==========================
        public string CadastrarFuncionario(string nome, string cpf, string cargo, string telefone, 
                                           string email, int idEmpresa, string departamento) // Método para cadastrar um novo funcionário
        {
            // Cria instância de funcionário (ID será gerado pelo banco)
            var funcionario = new Funcionario(
                0,          // id será gerado pelo banco
                nome, // nome do funcionário
                cpf, // CPF do funcionário
                cargo, // cargo do funcionário
                telefone, // telefone do funcionário
                email, // email do funcionário
                departamento, // departamento do funcionário
                idEmpresa // ID da empresa associada
            );

            funcionarioDao.Inserir(funcionario); // Insere o funcionário no banco
            return "ok"; // Retorna "ok" em caso de sucesso
        }

        // ==========================
        // Obter todos os funcionários de uma empresa
        // ==========================
        public List<Funcionario> ObterTodos(int idEmpresa) // Método para obter todos os funcionários de uma empresa
        {
            return funcionarioDao.ObterTodos(idEmpresa); // Retorna a lista de funcionários da empresa
        }

        // ==========================
        // Editar um funcionário existente
        // ==========================
        public string AtualizarFuncionario(Funcionario func) // Método para atualizar os dados de um funcionário
        {
            bool ok = funcionarioDao.Atualizar(func); // Atualiza os dados do funcionário
            return ok ? "ok" : "erro"; // Retorna "ok" se sucesso, "erro" caso falhe
                                       // Operador ternário: verifica a variável booleana 'ok'
                                       // Se ok == true -> retorna "ok"
                                       // Se ok == false -> retorna "erro"
        }

        // ==========================
        // Excluir um funcionário pelo ID
        // ==========================
        public bool ExcluirFuncionario(int idFuncionario)
        {
            return funcionarioDao.Excluir(idFuncionario); // Remove o funcionário do banco
        }

        // ==========================
        // Obter um funcionário por ID
        // ==========================
        public Funcionario ObterPorId(int idFuncionario)
        {
            return funcionarioDao.ObterPorId(idFuncionario); // Retorna o funcionário correspondente ao ID
        }
    }
}

/*
FuncionarioController gerencia a lógica de negócios de funcionários.
- Permite cadastrar, editar, excluir e consultar funcionários de uma empresa.
- Utiliza FuncionarioDAO para interagir com o banco de dados, garantindo persistência dos dados.
- Fornece métodos para obter todos os funcionários de uma empresa ou um funcionário específico por ID.
- Retorna mensagens ou booleanos simples para indicar sucesso ou falha nas operações.
*/
