using System;

namespace Wpf_Projeto_BD.Models // Define o namespace da aplicação (Models)
{
    public class Usuario // Classe que representa um usuário do sistema
    {
        public int IdUsuario { get; set; } // Identificador único do usuário
        public int IdEmpresa { get; set; } // ID da empresa à qual o usuário está vinculado
        public string Nome { get; set; } // Nome completo do usuário
        public string Email { get; set; } // Email do usuário
        public string SenhaHash { get; set; } // Hash da senha do usuário
        public string Salt { get; set; } // Salt utilizado para o hash da senha
        public string TipoUsuario { get; set; } // Tipo do usuário (ex: Administrador, Operador)

        // Construtor vazio
        public Usuario() { }

        // Construtor parametrizado para criar um usuário com todos os dados
        public Usuario(int idUsuario, int idEmpresa, string nome, string email, string senhaHash, string salt, string tipoUsuario)
        {
            IdUsuario = idUsuario;
            IdEmpresa = idEmpresa;
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Salt = salt;
            TipoUsuario = tipoUsuario;
        }
    }
}

/*
Resumo técnico:
- Usuario é um Model que representa os usuários cadastrados no sistema.
- Contém informações essenciais como ID, empresa vinculada, nome, email, hash da senha, salt e tipo de usuário.
- Possui construtor vazio e parametrizado para facilitar a criação de objetos.
- Centraliza a representação de dados de usuários, separando a lógica de negócios (Controller) e persistência (DAO).
*/
