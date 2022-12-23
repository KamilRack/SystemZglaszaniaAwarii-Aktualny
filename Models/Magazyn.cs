using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SystemZglaszaniaAwariiGlowny.Models
{
    public class Magazyn
    {
        [Key]
        [Display(Name = "Identyfikator magazynu")]
        public int MagazynId { get; set; }
        [Required]
        [Display(Name = "Nazwa Magazynu")]
        public string MagazynName { get; set; }
        [Required]
        [Display(Name = "Opis Magazynu")]
        public string MagazynOpis { get; set; }


        [Display(Name = "Zdjęcie magazynu:")]
        [MaxLength(128)]
        [FileExtensions(Extensions = " . jpg,. png, . gif", ErrorMessage = "Niepoprawne rozszerzenie pliku")]

        public string? Graphic { get; set; }

        [Required]
        [Display(Name = "Czy magazyn jest do użytku?")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Czy wyświetlać magazyn?")]
        public bool Display { get; set; }

        [Display(Name = "Osoba dodająca:")]

        public string? Id { get; set; }

        [ForeignKey("Id")]
        public virtual AppUser? User { get; set; }

        public virtual List<Zgloszenia>? Zgloszenias { get; set; }
        public virtual List<Maszyna>? Maszynas { get; set; }
    }
}
