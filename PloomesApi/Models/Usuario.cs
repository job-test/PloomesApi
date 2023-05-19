namespace PloomesApi.Models
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }


        public static Usuario Novo(string nome, string email)
        {
            var usuario = new Usuario();

            usuario.Id = Guid.NewGuid();

            usuario.Nome = nome;
            usuario.Email = email;

            return usuario;
        }

    }
}
