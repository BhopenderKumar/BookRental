namespace BookRental.Data.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsOverdue => ReturnDate == null && DateTime.UtcNow > RentalDate.AddDays(14);
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
