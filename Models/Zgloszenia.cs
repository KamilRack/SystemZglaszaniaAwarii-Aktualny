using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.ComponentModel;

namespace SystemZglaszaniaAwariiGlowny.Models
{
    public class Zgloszenia
    {
        [Key]
        public int ZgloszeniaId { get; set; }
        [Required]
        [Display(Name = "Opis awarii")]
        public string AwariaOpis { get; set; }
        [Required]
        [Display(Name = "Czy awaria nadal trwa?")]
        [DefaultValue(true)]

        public bool Active { get; set; }

        
        [Display(Name = "Data dodania")]
        [DataType(DataType.Date, ErrorMessage = "Niepoprawny format daty")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm")]
        public System.DateTime AddedDate { get; set; }

        [Display(Name = " Nazwa magazynu ")]
        public int MagazynId { get; set; }

        [ForeignKey("MagazynId")]
        public virtual Magazyn? Magazyn { get; set; }


        [Display(Name = " Nazwa maszyny ")]
        public int MaszynaId { get; set; }

        [ForeignKey("MaszynaId")]
        public virtual Maszyna? Maszyna { get; set; }

        [Display(Name = " Osoba zgłaszająca:")]
        public string? Id { get; set; }

        [ForeignKey("Id")]
        public virtual AppUser? User { get; set; }

        [Display(Name = " Osoba realizaująca:")]
        [DefaultValue("Brak")]
        public int MechanikId { get; set; }

        [ForeignKey("MechanikId")]
        public virtual Mechanik? Mechanik { get; set; }

    }
}
