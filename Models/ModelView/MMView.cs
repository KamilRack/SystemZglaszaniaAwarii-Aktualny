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

        public int PageCount => (int)Math.Ceiling((decimal)MMCount / PageSize);
    }
}
