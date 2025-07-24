using System.ComponentModel.DataAnnotations;

namespace TasteTest.Models
{
    public class Cliente
    {
        public int? IDCliente { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public Cliente() { }

        public Cliente(int idCliente, string nome, string cognome, string email)
        {
            IDCliente = idCliente;
            Nome = nome;
            Cognome = cognome;
            Email = email;
        }
    }
}