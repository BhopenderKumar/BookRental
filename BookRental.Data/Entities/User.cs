﻿namespace BookRental.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
