﻿#nullable enable
namespace BookRental.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public int AvailableCopies { get; set; }
    }
}