using BookRental.Api.Controllers;
using BookRental.Core.DTOs;
using BookRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using Moq;

namespace BookRental.Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller = new BooksController(_bookServiceMock.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ReturnsOkResult_WhenBooksExist()
        {
            
            var books = new List<BookDto> { new BookDto { Id = 1, Title = "Book 1" } };
            _bookServiceMock.Setup(service => service.GetAllBooksAsync()).ReturnsAsync(books);

            
            var result = await _controller.GetAllBooksAsync();

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BookDto>>(okResult.Value);
            Assert.Equal(1, returnValue.Count);
        }

        [Fact]
        public async Task GetAllBooksAsync_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            
            _bookServiceMock.Setup(service => service.GetAllBooksAsync()).ThrowsAsync(new Exception("Something went wrong"));

            
            var result = await _controller.GetAllBooksAsync();

            
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Search_ReturnsBadRequest_WhenNoSearchCriteriaProvided()
        {
            
            var controller = new BooksController(_bookServiceMock.Object);

            
            var result = await controller.Search(null, null);

            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Search_ReturnsNotFound_WhenNoBooksMatchCriteria()
        {
            
            _bookServiceMock.Setup(service => service.SearchBooksAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new List<BookDto>());

            
            var result = await _controller.Search("NonExistentBook", "Fiction");

            
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            
            _bookServiceMock.Setup(service => service.GetBookByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Something went wrong"));

            
            var result = await _controller.GetById(1);

            
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task Search_ReturnsOk_WhenBooksMatchSearchCriteria()
        {
            
            var books = new List<BookDto> { new BookDto { Id = 1, Title = "Fiction Book" } };
            _bookServiceMock.Setup(service => service.SearchBooksAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(books);

            
            var result = await _controller.Search("Fiction Book", "Fiction");

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BookDto>>(okResult.Value);
            Assert.Equal(1, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsBadRequest_WhenInvalidBookId()
        {
            
            var controller = new BooksController(_bookServiceMock.Object);

            
            var result = await controller.GetById(0);

            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenBookNotFound()
        {
            
            _bookServiceMock.Setup(service => service.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync((BookDto)null);

            
            var result = await _controller.GetById(1);

            
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenBookFound()
        {
            
            var book = new BookDto { Id = 1, Title = "Book 1" };
            _bookServiceMock.Setup(service => service.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync(book);

            
            var result = await _controller.GetById(1);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BookDto>(okResult.Value);
            Assert.Equal("Book 1", returnValue.Title);
        }
    }
}
