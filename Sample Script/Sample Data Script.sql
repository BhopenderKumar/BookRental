-- Insert data into Books table
INSERT INTO Books (Title, Author, ISBN, Genre, AvailableCopies) VALUES
    ('The Great Gatsby', 'F. Scott Fitzgerald', '9780743273565', 'Classics', 5),
    ('To Kill a Mockingbird', 'Harper Lee', '9780060935467', 'Classics', 5),
    ('1984', 'George Orwell', '9780451524935', 'Dystopian', 5),
    ('Pride and Prejudice', 'Jane Austen', '9780141199078', 'Romance', 5),
    ('The Catcher in the Rye', 'J.D. Salinger', '9780316769488', 'Classics', 5),
    ('The Hobbit', 'J.R.R. Tolkien', '9780547928227', 'Fantasy', 5),
    ('Fahrenheit 451', 'Ray Bradbury', '9781451673319', 'Science Fiction', 5),
    ('The Book Thief', 'Markus Zusak', '9780375842207', 'Historical Fiction', 5),
    ('Moby-Dick', 'Herman Melville', '9781503280786', 'Classics', 5),
    ( 'War and Peace', 'Leo Tolstoy', '9781400079988', 'Historical Fiction', 5);

-- Insert data into Users table
INSERT INTO Users (Name, Email) VALUES
    ('Bhopender Kumar', 'bhopender.kumar@example.com'),
    ('Sahil Thakur', 'sahil.thakur@example.com'),
    ('Abhay Singh', 'abhay.singh@example.com');

-- Insert data into Rentals table
-- Assuming RentalDate is based on the previous example
INSERT INTO Rentals (BookId, UserId, RentalDate, ReturnDate) VALUES
    (1, 1, '2024-11-03', NULL),  -- Book is currently rented by Alice Johnson
    (3, 2, '2024-10-29', '2024-11-12'),  -- Book 1984 rented and returned by Bob Smith
    (2, 3, '2024-10-24', NULL);  -- Book To Kill a Mockingbird currently rented by Charlie Brown
