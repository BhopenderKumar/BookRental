namespace BookRental.Core.DTOs
{
    public class RentalStatsDto
    {
        public BookDto MostOverdueBook { get; set; }
        public BookDto MostPopularBook { get; set; }
        public BookDto LeastPopularBook { get; set; }
    }
}
