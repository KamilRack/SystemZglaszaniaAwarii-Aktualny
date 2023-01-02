using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SystemZglaszaniaAwariiGlowny.Models
{
    public class Mechanik
    {
        [Key]
        [Display(Name = "Identyfikator mechanika")]
        public int MechanikId { get; set; }
        [Required]
        [Display(Name = "Imie Mechanika")]
        public string MechanikName { get; set; }
        [Required]
        [Display(Name = "Nazwisko Mechanika")]
        public string MechanikNazwisko { get; set; }

        [NotMapped]
        [Display(Name = "Pan/Pani:")]
        public string MechanikFullname
        {
            get { return MechanikName + " " + MechanikNazwisko; }
        }
        public virtual List<Zgloszenia>? Zgloszenias { get; set; }
    }
}
