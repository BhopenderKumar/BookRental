using BookRental.Api.Controllers;
using BookRental.Core.DTOs;
using BookRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookRental.Test
{
    public class RentalsControllerTests
    {
        private readonly Mock<IRentalService> _rentalServiceMock;
        private readonly RentalsController _controller;

        public RentalsControllerTests()
        {
            _rentalServiceMock = new Mock<IRentalService>();
            _controller = new RentalsController(_rentalServiceMock.Object);
        }

        [Fact]
        public async Task RentBook_ReturnsBadRequest_WhenInvalidUserIdOrBookId()
        {
            
            int invalidUserId = 0;
            int validBookId = 1;
                        
            var result = await _controller.RentBook(invalidUserId, validBookId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task RentBook_ReturnsOk_WhenBookRentedSuccessfully()
        {
            
            int validUserId = 1;
            int validBookId = 1;
            _rentalServiceMock.Setup(service => service.RentBookAsync(validUserId, validBookId)).Returns(Task.CompletedTask);

            var result = await _controller.RentBook(validUserId, validBookId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task RentBook_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            
            int validUserId = 1;
            int validBookId = 1;
            _rentalServiceMock.Setup(service => service.RentBookAsync(validUserId, validBookId)).ThrowsAsync(new Exception("Something went wrong"));

            var result = await _controller.RentBook(validUserId, validBookId);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBook_ReturnsBadRequest_WhenInvalidRentalId()
        {
            
            int invalidRentalId = 0;
            
            var result = await _controller.ReturnBook(invalidRentalId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBook_ReturnsOk_WhenBookReturnedSuccessfully()
        {
            
            int validRentalId = 1;
            _rentalServiceMock.Setup(service => service.ReturnBookAsync(validRentalId)).Returns(Task.CompletedTask);

            var result = await _controller.ReturnBook(validRentalId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task ReturnBook_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            
            int validRentalId = 1;
            _rentalServiceMock.Setup(service => service.ReturnBookAsync(validRentalId)).ThrowsAsync(new Exception("Something went wrong"));

            var result = await _controller.ReturnBook(validRentalId);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RentalHistory_ReturnsBadRequest_WhenInvalidUserId()
        {
            
            int invalidUserId = 0;

            var result = await _controller.RentalHistory(invalidUserId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task RentalHistory_ReturnsNotFound_WhenNoHistoryFound()
        {
            
            int validUserId = 1;
            _rentalServiceMock.Setup(service => service.GetUserRentalHistoryAsync(validUserId)).ReturnsAsync(new List<RentalDto>());

            var result = await _controller.RentalHistory(validUserId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task RentalHistory_ReturnsOk_WhenHistoryFound()
        {
            
            int validUserId = 1;
            var rentalHistory = new List<RentalDto> { new RentalDto { BookId = 1, RentalDate = DateTime.Now } };
            _rentalServiceMock.Setup(service => service.GetUserRentalHistoryAsync(validUserId)).ReturnsAsync(rentalHistory);

            var result = await _controller.RentalHistory(validUserId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(rentalHistory, okResult.Value);
        }

        [Fact]
        public async Task Stats_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            
            _rentalServiceMock.Setup(service => service.GetRentalStatsAsync()).ThrowsAsync(new Exception("Something went wrong"));

            var result = await _controller.Stats();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task MarkOverdueRentals_ReturnsOk_WhenMarkedSuccessfully()
        {
            
            _rentalServiceMock.Setup(service => service.MarkOverdueRentalsAsync()).Returns(Task.CompletedTask);

            var result = await _controller.MarkOverdueRentalsAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task MarkOverdueRentals_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            
            _rentalServiceMock.Setup(service => service.MarkOverdueRentalsAsync()).ThrowsAsync(new Exception("Something went wrong"));

            var result = await _controller.MarkOverdueRentalsAsync();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SendOverdueNotifications_ReturnsOk_WhenSentSuccessfully()
        {
            
            _rentalServiceMock.Setup(service => service.SendOverdueNotificationAsync()).Returns(Task.CompletedTask);
            
            var result = await _controller.SendOverdueNotificationsAsync();
                        
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task SendOverdueNotifications_ReturnsStatusCode500_WhenExceptionOccurs()
        {
            
            _rentalServiceMock.Setup(service => service.SendOverdueNotificationAsync()).ThrowsAsync(new Exception("Something went wrong"));

            var result = await _controller.SendOverdueNotificationsAsync();

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }

}
