using Moq;
using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Services;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;

namespace ProjectManager.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _sut = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void GetUserByUsername_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var expectedUser = User.Create("maksim", "makson", "123456", DateTime.UtcNow);
            _userRepositoryMock.Setup(r => r.GetByUserName("makson")).Returns(expectedUser);

            // Act
            var user = _sut.GetByUsername("makson");

            // Assert
            Assert.True(expectedUser.IsSuccess);
            Assert.NotNull(user);
            Assert.Equal(expectedUser.Data, user);
        }

        [Fact]
        public void CreateUser_ShouldReturnTrue_WhenUserDontExist()
        {
            // Arange
            var userRequest = new CreateUserRequest
            {
                Name = "maksim",
                UserName = "makson",
                Password = "123456"
            };

            var resultCreateUserForMock = User.Create(name: userRequest.Name, userName: userRequest.UserName
                , password: userRequest.Password, createdAt: DateTime.UtcNow);

            _userRepositoryMock.Setup(r => r.GetByUserName("makson"))
                .Returns(Result<User>.Fail("User not found"));
            _userRepositoryMock.Setup(r => r.Create(It.IsAny<User>()))
                .Returns(Result<User>.Ok(resultCreateUserForMock.Data!));
                

            // Act
            var result = _sut.Create(userRequest);

            // Assert
            Assert.True(resultCreateUserForMock.IsSuccess);
            Assert.True(result.IsSuccess);
            Assert.Equal(resultCreateUserForMock.Data, result.Data);
            
        }

        [Fact]
        public void GetUserByUsername_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetByUserName("nonexistent")).Returns(Result<User>.Fail("User not found."));
            // Act
            var user = _sut.GetByUsername("nonexistent");
            // Assert
            Assert.Null(user);
        }
    }
}
