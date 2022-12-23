using static System.Net.Mime.MediaTypeNames;

namespace SystemZglaszaniaAwariiGlowny.Models.ModelView
{
    public class MMViewModel
    {
        public IEnumerable<Maszyna>? Maszynas { get; set; }
        public MMView? MMView { get; set; }
    }
}
