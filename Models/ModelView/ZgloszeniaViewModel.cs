using static System.Net.Mime.MediaTypeNames;

namespace SystemZglaszaniaAwariiGlowny.Models.ModelView
{
    public class ZgloszeniaViewModel
    {
        public IEnumerable<Zgloszenia>? Zgloszenias { get; set; }
        public MMView? MMView { get; set; }

    }
}
