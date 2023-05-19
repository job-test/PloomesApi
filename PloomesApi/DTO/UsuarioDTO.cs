using PloomesApi.Models;

namespace PloomesApi.DTO
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public static UsuarioDTO Novo(string nome, string email)
        {
            var usuario = new UsuarioDTO();

            usuario.Nome = nome;
            usuario.Email = email;

            return usuario;
        }
    }
}
