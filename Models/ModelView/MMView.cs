namespace SystemZglaszaniaAwariiGlowny.Models.ModelView
{
    public class MMView
    {
        public MMView(int pageSize = 5)
        {
            PageSize = pageSize;
        }

        public int MMCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? NazwaMagazynu { get; set; }
        public string? Fraza { get; set; }

        public string? NazwaMaszyny { get; set; }
        public string? FrazaM { get; set; }


        public int PageCount => (int)Math.Ceiling((decimal)MMCount / PageSize);
    }
}
