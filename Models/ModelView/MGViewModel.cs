using static System.Net.Mime.MediaTypeNames;

namespace SystemZglaszaniaAwariiGlowny.Models.ModelView
{
    public class MGViewModel
    {
        public IEnumerable<Magazyn>? Magazyns{ get; set; }


        public MMView? MMView { get; set; }
    }
}
