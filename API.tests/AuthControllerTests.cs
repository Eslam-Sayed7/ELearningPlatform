using Moq;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Models;
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
        public async Task GetTokenAsync_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var tokenRequest = new TokenRequestModel { Email = "user@gmail.com", Password = "pass" };
            var user = new AppUser { Token = "token", RefreshTokenExpiryTime = System.DateTime.UtcNow.AddDays(1) };
            var authModel = new AuthModel
            {
                IsAuthenticated = true,
                User = user,
                Username = "user",
                Email = "user@mail.com",
                Roles = new List<string> { "Student" },
                Message = "Success"
            };
            _mockAuthService.Setup(s => s.LoginAsync(tokenRequest)).ReturnsAsync(authModel);

            // Act
            var result = await _authController.GetTokenAsync(tokenRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginResponseDto>(okResult.Value);
            Assert.True(response.IsAuthenticated);
            Assert.Equal("user", response.Username);
        }

        [Fact]
        public async Task GetTokenAsync_ReturnsUnauthorized_WhenLoginFails()
        {
            // Arrange
            var tokenRequest = new TokenRequestModel();
            var authModel = new AuthModel { IsAuthenticated = false, Message = "Invalid" };
            _mockAuthService.Setup(s => s.LoginAsync(tokenRequest)).ReturnsAsync(authModel);

            // Act
            var result = await _authController.GetTokenAsync(tokenRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid", unauthorizedResult.Value);
        } 
        
        
        [Fact]
        public async Task AddRoleAsync_ReturnsOk_WhenRoleAddedSuccessfully()
        {
            // Arrange
            var addRoleModel = new AddRoleModel { UserId = new Guid(), Role = "Admin" };
            _mockAuthService.Setup(s => s.AddRoleAsync(addRoleModel)).ReturnsAsync(
                new AddRoleResult
                {
                    IsSuccess = true ,
                    Message = "Role added successfully",
                    Role = addRoleModel.Role,
                    UserId = addRoleModel.UserId,
                    CreatedAt = DateTime.UtcNow
                });
            // Act
            var result = await _authController.AddRoleAsync(addRoleModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(addRoleModel, okResult.Value);
        }

        [Fact]
        public async Task AddRoleAsync_ReturnsBadRequest_WhenInvalidParameterModel()
        {
            // Arrange
            var addRoleModel = new AddRoleModel();
            var errorResult = new AddRoleResult
            {
                IsSuccess = false,
                Message = "Error",
                Role = addRoleModel.Role,
                UserId = addRoleModel.UserId
            };
            _mockAuthService.Setup(s => s.AddRoleAsync(addRoleModel)).ReturnsAsync(errorResult);
            
            // Act
            var result = await _authController.AddRoleAsync(addRoleModel);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = Assert.IsType<AddRoleResult>(badRequest.Value);
            Assert.False(value.IsSuccess);
            Assert.Equal(errorResult.Message, value.Message);
        }

        [Fact]
        public async Task GetRoleAsync_ReturnsOk_WhenRolesExist()
        {
            // Arrange
            var getRoleModel = new GetRoleModel { UserId = "user" };
            var userRoleDto = new UserRoleDto { Roles = new List<string> { "Admin" } };
            _mockAuthService.Setup(s => s.GetRoleAsync(getRoleModel)).ReturnsAsync(userRoleDto);

            // Act
            var result = await _authController.GetRoleAsync(getRoleModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<UserRoleDto>(okResult.Value);
            Assert.Contains("Admin", dto.Roles);
        }

        [Fact]
        public async Task GetRoleAsync_ReturnsNotFound_WhenNoRoles()
        {
            // Arrange
            var getRoleModel = new GetRoleModel();
            var userRoleDto = new UserRoleDto { Roles = new List<string>() };
            _mockAuthService.Setup(s => s.GetRoleAsync(getRoleModel)).ReturnsAsync(userRoleDto);

            // Act
            var result = await _authController.GetRoleAsync(getRoleModel);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}

