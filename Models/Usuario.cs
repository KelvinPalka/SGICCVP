// Classe que representa um usuário do sistema
public class Usuario
{
    public int IdUsuario { get; set; } // Identificador único do usuário
    public int IdEmpresa { get; set; } // Identificador da empresa a que o usuário pertence
    public string Nome { get; set; } // Nome completo do usuário
    public string Email { get; set; } // E-mail do usuário, usado para login e comunicação
    public string SenhaHash { get; set; } // Hash da senha do usuário (armazenamento seguro)
    public string Salt { get; set; } // Salt utilizado no hash da senha, aumenta segurança
    public string TipoUsuario { get; set; } // Tipo de usuário (ex.: Administrador, Funcionário, Cliente)

    // Construtor padrão sem parâmetros
    public Usuario() { }

    // Construtor completo para inicializar todas as propriedades
    public Usuario(int idUsuario, int idEmpresa, string nome, string email, string senhaHash, string salt, string tipoUsuario)
    {
        IdUsuario = idUsuario; // Atribui Id do usuário
        IdEmpresa = idEmpresa; // Atribui Id da empresa
        Nome = nome; // Atribui nome
        Email = email; // Atribui e-mail
        SenhaHash = senhaHash; // Atribui hash da senha
        Salt = salt; // Atribui salt utilizado no hash
        TipoUsuario = tipoUsuario; // Atribui tipo de usuário
    }
}

/*
Resumo técnico:
- Usuario é a classe modelo que representa um usuário no sistema.
- Contém informações essenciais para autenticação (Email, SenhaHash, Salt) e controle de permissões (TipoUsuario).
- IdEmpresa vincula o usuário a uma empresa específica, permitindo multiempresa.
- Construtor padrão permite criar instâncias vazias; construtor completo inicializa todas as propriedades.
- Segue boas práticas de armazenamento seguro de senha (hash + salt), sem armazenar a senha em texto plano.
*/
