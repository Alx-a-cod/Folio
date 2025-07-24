
using System.ComponentModel.DataAnnotations;

namespace TasteTest.Models
{
    public class ClientiViewModel
    {

        public int? IDCliente { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public ClientiViewModel() { }

    }
}
