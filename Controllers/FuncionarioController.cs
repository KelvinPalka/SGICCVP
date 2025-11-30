using System.Collections.Generic;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;

namespace WPF_Projeto_BD.Controllers
{
    internal class FuncionarioController
    {
        private FuncionarioDAO funcionarioDao = new FuncionarioDAO();

        // ==========================
        // Cadastrar novo funcionário
        // ==========================
        public string CadastrarFuncionario(string nome, string cpf, string cargo, string telefone,
                                           string email, int idEmpresa, string departamento)
        {
            var funcionario = new Funcionario(
                0,          // id será gerado pelo banco
                nome,
                cpf,
                cargo,
                telefone,
                email,
                departamento,
                idEmpresa
            );

            funcionarioDao.Inserir(funcionario); // CORRIGIDO
            return "ok";
        }

        // ==========================
        // Obter todos os funcionários de uma empresa
        // ==========================
        public List<Funcionario> ObterTodos(int idEmpresa)
        {
            return funcionarioDao.ObterTodos(idEmpresa); // CORRIGIDO
        }

        // ==========================
        // Editar um funcionário existente
        // ==========================
        public string AtualizarFuncionario(Funcionario func)
        {
            bool ok = funcionarioDao.Atualizar(func);
            return ok ? "ok" : "erro";
        }

        // ==========================
        // Excluir um funcionário pelo ID
        // ==========================
        public bool ExcluirFuncionario(int idFuncionario)
        {
            return funcionarioDao.Excluir(idFuncionario); // CORRIGIDO
        }

        // ==========================
        // Obter um funcionário por ID
        // ==========================
        public Funcionario ObterPorId(int idFuncionario)
        {
            return funcionarioDao.ObterPorId(idFuncionario); // CORRIGIDO
        }
    }
}
