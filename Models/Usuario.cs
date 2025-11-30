public class Usuario
{
    public int IdUsuario { get; set; }
    public int IdEmpresa { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }
    public string Salt { get; set; }
    public string TipoUsuario { get; set; }

    public Usuario() { }

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
