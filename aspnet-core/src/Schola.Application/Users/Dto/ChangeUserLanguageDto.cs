using System.ComponentModel.DataAnnotations;

namespace Schola.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}