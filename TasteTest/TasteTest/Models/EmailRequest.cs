using System.ComponentModel.DataAnnotations;

namespace TasteTest.Models
{
    public class EmailRequest
    {
        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Formato email non valido")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Formato email non conforme")]
        public string Email { get; set; }
        public int? IDCliente { get; set; }  // opzionale, può essere null per nuovi clienti


    }
}


//sintassi della regex implementata in modello: 

//  ^[a-zA-Z0-9._%+-]+     # parte locale (prima della @)
//  @
//  [a - zA - Z0 - 9.-] +  # dominio (es. gmail, azienda)
//  \.
//  [a-zA-Z] { 2,4}$       # estensione (.com, .it, .org...) <---"Il dominio dell'email deve avere da 2 a 4 lettere"