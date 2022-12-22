using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace AwarieNoweZnowu.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Imię użytkownika:")]
        [MaxLength(20)]
        public string? FirstName { get; set; }

        [Display(Name = "Nazwisko użytkownika:")]
        [MaxLength(50)]
        public string? LastName { get; set; }

        #region dodatkowe pole nieodwzorowywane w bazie
        [NotMapped]
        [Display(Name = "Pan/Pani:")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        #endregion

        [Display(Name = "Informacja o użytkowniku:")]
        [MaxLength(255, ErrorMessage = "Zbyt długi opis - maksymalnie 255 znaków")]
        public string? Information { get; set; }


        public virtual List<Zgloszenia>? Zgloszenias { get; set; }

        
    }
}
