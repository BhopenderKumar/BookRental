using BookRental.Infrastructure.Logging;
using BookRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Rent a book
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPost("rent")]
        public async Task<IActionResult> RentBook(int userId, int bookId)
        {
            try
            {
                if (userId <= 0 || bookId <= 0)
                {
                    return BadRequest(new { message = "Invalid userId or bookId." });
                }

                await _rentalService.RentBookAsync(userId, bookId);
                return Ok(new { message = "Book rented successfully." });
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while renting the book.", error = ex.Message });
            }
        }

        /// <summary>
        /// Return a book
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook(int rentalId)
        {
            try
            {
                if (rentalId <= 0)
                {
                    return BadRequest(new { message = "Invalid rentalId." });
                }

                await _rentalService.ReturnBookAsync(rentalId);
                return Ok(new { message = "Book returned successfully." });
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while returning the book.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get rental history for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("history")]
        public async Task<IActionResult> RentalHistory(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { message = "Invalid userId." });
                }

                var history = await _rentalService.GetUserRentalHistoryAsync(userId);
                if (history == null || !history.Any())
                {
                    return NotFound(new { message = "No rental history found for the user." });
                }

                return Ok(history);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while retrieving the rental history.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get rental statistics
        /// </summary>
        /// <returns></returns>
        [HttpGet("stats")]
        public async Task<IActionResult> Stats()
        {
            try
            {
                var stats = await _rentalService.GetRentalStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while retrieving rental statistics.", error = ex.Message });
            }
        }

        /// <summary>
        /// Mark overdue rentals
        /// </summary>
        [HttpPost("mark-overdue")]
        public async Task<IActionResult> MarkOverdueRentalsAsync()
        {
            try
            {
                await _rentalService.MarkOverdueRentalsAsync();
                return Ok(new { message = "Overdue rentals have been successfully marked." });
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while marking overdue rentals.", error = ex.Message });
            }
        }

        /// <summary>
        /// Send overdue notifications to users
        /// </summary>
        [HttpPost("send-overdue-notifications")]
        public async Task<IActionResult> SendOverdueNotificationsAsync()
        {
            try
            {
                await _rentalService.SendOverdueNotificationAsync();
                return Ok(new { message = "Overdue notifications have been successfully sent." });
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return StatusCode(500, new { message = "An error occurred while sending overdue notifications.", error = ex.Message });
            }
        }
    }
}