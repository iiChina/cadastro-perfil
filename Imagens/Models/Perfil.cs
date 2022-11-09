namespace Imagens.Models
{
    public class Perfil
    {
        public Guid Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public Byte[]? Foto { get; set; }
    }
}
