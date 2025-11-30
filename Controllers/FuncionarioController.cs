using System.Collections.Generic;
using Wpf_Projeto_BD.Models;
using WPF_Projeto_BD.Data.DAO;

namespace WPF_Projeto_BD.Controllers
{
    internal class FuncionarioController
    {
        private FuncionarioDAO dao = new FuncionarioDAO();

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
                idEmpresa   // passa o IdEmpresa da View
            );

            dao.Inserir(funcionario);
            return "ok";
        }


        // ==========================
        // Obter todos os funcionários de uma empresa
        // ==========================
        public List<Funcionario> ObterTodos(int idEmpresa)
        {
            return dao.ObterTodos(idEmpresa);
        }

        // ==========================
        // Editar um funcionário existente
        // ==========================
        public bool EditarFuncionario(Funcionario funcionario)
        {
            return dao.Editar(funcionario);
        }

        // ==========================
        // Excluir um funcionário pelo ID
        // ==========================
        public bool ExcluirFuncionario(int idFuncionario)
        {
            return dao.Excluir(idFuncionario);
        }

        // ==========================
        // Obter um funcionário por ID
        // ==========================
        public Funcionario ObterPorId(int idFuncionario)
        {
            return dao.ObterPorId(idFuncionario);
        }
    }
}
