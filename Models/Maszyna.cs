using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SystemZglaszaniaAwariiGlowny.Models
{
    public class Maszyna
    {
        [Key]
        [Display(Name = "Identyfikator maszyny")]
        public int MaszynaId { get; set; }
        [Required]
        [Display(Name = "Nazwa maszyny")]
        public string MaszynaName { get; set; }
        [Required]
        [Display(Name = "Opis Maszyny")]
        public string MaszynaOpis { get; set; }

        [Display(Name = "Zdjęcie magazynu:")]
        [MaxLength(128)]
        [FileExtensions(Extensions = " . jpg,. png, . gif", ErrorMessage = "Niepoprawne rozszerzenie pliku")]

        public string? Graphic { get; set; }

        [Required]
        [Display(Name = "Czy maszyna jest do użytku?")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Czy wyświetlać maszynę?")]
        public bool Display { get; set; }

        [Display(Name = "Osoba dodająca:")]

        public string? Id { get; set; }

        [ForeignKey("Id")]
        public virtual AppUser? User { get; set; }

        public virtual List<Zgloszenia>? Zgloszenias { get; set; }
        public virtual List<Magazyn>? Magazyns { get; set; }
    }
}
