CENTRO PAULA SOUZA
ETEC DE HORTOLÂNDIA
Ensino Médio Integrado ao Técnico em Desenvolvimento de Sistemas

Sistema de Gerenciamento Integrado para Confecção e Comércio de Vestuário Personalizado (SIGCCVP)

---

DESCRIÇÃO DO TEMA DO PROJETO

O projeto SIGCCVP tem como tema o desenvolvimento de um sistema de gerenciamento integrado para empresas de confecção e comércio de vestuário personalizado. O objetivo é organizar e otimizar processos administrativos e produtivos, incluindo controle de estoque, produção, vendas, cadastro de fornecedores e clientes, e acompanhamento de entregas. O tema aborda a modernização da gestão empresarial por meio da tecnologia, promovendo maior eficiência operacional, redução de desperdícios, integração digital e suporte à tomada de decisão estratégica.

---

COMO EXECUTAR A APLICAÇÃO

1. **Abrir a solução no Visual Studio**

   * Localize o arquivo da solução (`.sln`) do projeto SIGCCVP no diretório onde ele foi salvo. Ele deve se chamar 'WPF_Projeto_BD.sln'
   * Abra o Visual Studio e escolha **File > Open > Project/Solution**, selecionando este arquivo.

2. **Restaurar pacotes NuGet**

   * Antes de compilar, certifique-se de que todos os pacotes NuGet estão restaurados.
   * No Visual Studio: **Tools > NuGet Package Manager > Manage NuGet Packages for Solution…** e clique em **Restore** ou, no menu, clique com o botão direito no projeto e escolha **Restore NuGet Packages**.

3. **Configurar o banco de dados**

   * Certifique-se de que o banco de dados esteja criado e com as tabelas correspondentes ao modelo.
   * O arquivo de criação está dentro da pasta Database e chama-se Database. Ele cria o banco e o user necessário para aplicação

4. **Compilar a solução**

   * No Visual Studio, pressione **F6** ou **Build > Build Solution**.
   * Verifique se não há erros de compilação.

5. **Executar a aplicação**

   * Pressione **F5** (ou **Ctrl+F5** para executar sem depuração) para iniciar a aplicação.
   * A janela principal do SIGCCVP será aberta, permitindo o uso das funcionalidades.

---

FUNCIONALIDADES IMPLEMENTADAS (CRUD COMPLETO)

O SIGCCVP conta com funcionalidades que permitem a gestão parcial (pois está em desenvolvimento) dos módulos do sistema através de operações CRUD (Create, Read, Update, Delete), garantindo total controle sobre os dados e processos da empresa:

Clientes – Cadastro, visualização, edição e exclusão de informações dos clientes (são recuperados pela tela de pedidos para serem vinculados)
Empresa - Cadastro, visualização, edição e exclusão de informações da Empresa (abre o sistema para que tudo possa funcionar)
Usuário - Cadastro, visualização, edição e exclusão de informações dos Usuários (Se for admin, tem acesso as configurações, se for User, pode apenas seguir com os processos configurados pelo admin)
Funcionário - Cadastro, visualização, edição e exclusão de informações dos Funcionários (Serve para o gerenciamento de tarefas)

Todas essas funcionalidades garantem que o SIGCCVP funcione como uma plataforma integrada, eficiente e confiável para a gestão completa da empresa.

---

Recurso Adicional : Hash + Salt com SHA256Managed no WPF .NET Framework 4.8
=====================================================================

1. Introdução
-------------
Hashing é um processo unidirecional que transforma um texto (como uma senha)
em uma cadeia de caracteres irreversível. Senhas jamais devem ser armazenadas
em texto puro para evitar que, em caso de vazamento, usuários sejam expostos.
O uso de algoritmos seguros como SHA256 ajuda a garantir integridade e dificulta
ataques de força bruta. Contudo, hashing sem salt não é suficiente, pois permite
ataques por rainbow tables.

2. Hash + Salt no .NET Framework 4.8
------------------------------------
O SHA256Managed é uma implementação do algoritmo SHA256 disponível no .NET
Framework 4.8. Ele permite gerar hashes de forma simples e segura.

- Gerar salt aleatório: normalmente com RNGCryptoServiceProvider.
- Concatenar senha + salt: senha em UTF8 + salt em bytes.
- Armazenamento: guarda-se o hash em Base64 e o salt também em Base64.

3. Processo de Registro
-----------------------
Passo a passo:
1. Usuário digita a senha.
2. Sistema gera um salt aleatório (16–32 bytes).
3. SHA256 é aplicado em (senha + salt).
4. Armazena-se no banco:
   - Hash
   - Salt
   - Demais dados do usuário

Exemplo (C#):

public static string GenerateSalt(int size = 32)
{
    using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
    {
        var saltBytes = new byte[size];
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}

public static string HashPassword(string password, string salt)
{
    using (var sha = new System.Security.Cryptography.SHA256Managed())
    {
        var combined = System.Text.Encoding.UTF8.GetBytes(password + salt);
        var hash = sha.ComputeHash(combined);
        return Convert.ToBase64String(hash);
    }
}

4. Processo de Login
--------------------
1. Busca o usuário pelo e-mail/login.
2. Recupera hash e salt armazenados.
3. Gera novo hash com a senha digitada + mesmo salt.
4. Compara hashes.
5. Se forem idênticos → login válido.

Exemplo (C#):

public static bool ValidateLogin(string passwordInput, string storedSalt, string storedHash)
{
    var newHash = HashPassword(passwordInput, storedSalt);
    return newHash == storedHash;
}

5. Estrutura no projeto WPF
---------------------------

UsuarioModel.cs
- Propriedades:
  - public string SenhaHash { get; set; }
  - public string Salt { get; set; }

UsuarioController.cs (Registro)
- Recebe a senha.
- Gera salt.
- Gera hash.
- Salva no banco.

LoginController.cs ou Login.xaml.cs
- Recebe login e senha.
- Recupera hash e salt do banco.
- Recalcula hash.
- Valida.

6. Códigos completos
--------------------

6.1 Gerar Salt

public static string GenerateSalt(int size = 32)
{
    using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
    {
        var saltBytes = new byte[size];
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}

6.2 Gerar Hash SHA256 com Salt

public static string HashPassword(string password, string salt)
{
    using (var sha = new System.Security.Cryptography.SHA256Managed())
    {
        var combined = System.Text.Encoding.UTF8.GetBytes(password + salt);
        var hash = sha.ComputeHash(combined);
        return Convert.ToBase64String(hash);
    }
}

6.3 Registrar Usuário

public void RegistrarUsuario(string email, string senha)
{
    string salt = GenerateSalt();
    string hash = HashPassword(senha, salt);

    var usuario = new UsuarioModel
    {
        Email = email,
        SenhaHash = hash,
        Salt = salt
    };

    banco.Usuarios.Add(usuario);
    banco.SaveChanges();
}

6.4 Validar Login

public bool Login(string email, string senha)
{
    var usuario = banco.Usuarios.FirstOrDefault(u => u.Email == email);
    if (usuario == null) return false;

    var hash = HashPassword(senha, usuario.Salt);
    return hash == usuario.SenhaHash;
}

7. Observações de Segurança
---------------------------
- SHA256 sozinho não é ideal porque é rápido → facilita ataques de força bruta.
- Salt é fundamental para evitar rainbow tables.
- Em produção, o ideal é usar:
  - BCrypt
  - PBKDF2
  - Argon2
- Sempre limitar tentativas de login e usar bloqueios temporários.

8. Conclusão
------------
Foi explicada a implementação de Hash + Salt com SHA256Managed no .NET Framework 4.8,
bem como o processo de login, registro e boas práticas de segurança. Para o futuro,
recomenda-se migrar para algoritmos mais resistentes como BCrypt ou PBKDF2.

9. Referências (oficiais e úteis)
---------------------------------
- SHA256Managed — Microsoft Learn (doc .NET Framework 4.8). URL:
  https://learn.microsoft.com/pt-br/dotnet/api/system.security.cryptography.sha256managed?view=netframework-4.8.1

- Assegurando a integridade dos dados com códigos hash — Microsoft Learn:
  https://learn.microsoft.com/pt-br/dotnet/standard/security/ensuring-data-integrity-with-hash-codes

- SHA256Managed — Microsoft Learn (doc .NET 8.0): 
  https://learn.microsoft.com/pt-br/dotnet/api/system.security.cryptography.sha256managed?view=net-8.0

- Criptografia na plataforma .NET — iMasters:
  https://imasters.com.br/dotnet/criptografia-na-plataforma-net


