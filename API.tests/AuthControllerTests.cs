using Moq;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Services;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;

namespace API.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockStudentService = new Mock<IStudentService>();
            _authController = new AuthController(_mockAuthService.Object, _mockStudentService.Object);
            
            // Mock HttpContext for cookie operations
            var httpContext = new DefaultHttpContext();
            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };        
        }

        [Fact]
        public async Task RegisterAsync_ReturnsOkResult_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerModel = new RegisterModel
            {
                FirstName = "admin",
                LastName = "user",
                Username = "ADMIN",
                Email = "admin@gmail.com",
                Password = "Pa$$w0rd"
            };
            var authModel = new AuthModel {
                IsAuthenticated = true, 
                User = new AppUser(),
                RefreshToken = "dummy-token",
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };
            _mockAuthService.Setup(service => service.RegisterUserAsync(registerModel)).ReturnsAsync(authModel);
            _mockStudentService.Setup(service => service.CreateStudentAsync(It.IsAny<Student>())).ReturnsAsync(new Student());

            // Act
            var result = await _authController.RegisterAsync(registerModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<LoginResponseDto>(okResult.Value);
            Assert.True(returnValue.IsAuthenticated);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _authController.ModelState.AddModelError("Error", "Model state is invalid");

            // Act
            var result = await _authController.RegisterAsync(new RegisterModel());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}