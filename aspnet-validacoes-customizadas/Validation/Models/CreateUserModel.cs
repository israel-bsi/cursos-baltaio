using System.ComponentModel.DataAnnotations;

namespace Validation.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "O usu�rio � obrigat�rio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O nome de usu�rio deve contar entre 3 e 10 caracteres")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Senha obrigat�rio")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Sal�rio obrigat�rio")]
        [Range(0, 999.99, ErrorMessage = "Voce ganha muito")]
        public decimal Salary { get; set; }
        [BlockDomain("gmail.com", ErrorMessage = "Dominio invalido")]
        [EmailInUseAtrribute]
        [Required(ErrorMessage = "Email obrigat�rio")]
        [EmailAddress(ErrorMessage = "Email inv�lido")]
        public string Email { get; set; }
    }

    public class EmailInUseAtrribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (string)value == "israel@israel.com"
                ? new ValidationResult("Esse email j� est� em uso")
                : ValidationResult.Success;
        }
    }

    public class BlockDomainAttribute : ValidationAttribute
    {
        public string Domain { get; set; }

        public BlockDomainAttribute(string domain)
        {
            Domain = domain;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            return ((string)value).Contains(Domain)
                ? new ValidationResult("Dominio invalido")
                : ValidationResult.Success;
        }
    }
}