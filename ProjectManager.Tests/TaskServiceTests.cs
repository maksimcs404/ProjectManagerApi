using Moq;
using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Services;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
        private readonly TaskService _sut;

        public TaskServiceTests()
        {
            _sut = new TaskService(_taskRepositoryMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnProjectTask_WhenExist()
        {
            // Arrange
            var projectTask = ProjectTask.Create(DateTime.Now.AddDays(1), "title", "desc", 1, 1,
                ProjectManager.Core.Models.Common.Enums.TaskStatus.InProgress, Core.Models.Common.Enums.TaskPriority.Low);

            _taskRepositoryMock.Setup(m => m.Get(It.IsAny<int>()))
                .Returns(projectTask.Data);

            // Act
            var result = _sut.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProjectTask>(result);
        }

        [Fact]
        public void Get_ShouldReturnNull_WhenTaskNotExist()
        {
            // Arrange
            _taskRepositoryMock.Setup(m => m.Get(It.IsAny<int>()))
                .Returns((ProjectTask?)null);

            // Act
            var result = _sut.Get(100);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddComment_ShouldReturnComment_WhenRepositoryOk()
        {
            // Arrange
            var request = new CreateCommentRequest { Data = "text", Title = "title" };
            var commentResult = Comment.Create("text", "title", 1, 10);

            _taskRepositoryMock
                .Setup(r => r.AddComment(1, 10, request.Data, request.Title))
                .Returns(Result<Comment>.Ok(commentResult.Data!));

            // Act
            var result = _sut.AddComment(1, 10, request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            _taskRepositoryMock.Verify(r => r.AddComment(1, 10, request.Data, request.Title), Times.Once);
        }

        [Fact]
        public void AddComment_ShouldReturnFail_WhenRepositoryFail()
        {
            // Arrange
            var request = new CreateCommentRequest { Data = "text", Title = "title" };

            _taskRepositoryMock
                .Setup(r => r.AddComment(1, 10, request.Data, request.Title))
                .Returns(Result<Comment>.Fail("err"));

            // Act
            var result = _sut.AddComment(1, 10, request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("err", result.Error);
            _taskRepositoryMock.Verify(r => r.AddComment(1, 10, request.Data, request.Title), Times.Once);
        }

        [Fact]
        public void GetTaskComments_ShouldReturnList_WhenRepositoryOk()
        {
            // Arrange
            var list = new List<Comment>
            {
                Comment.Create("a", "t", 1, 10).Data!,
                Comment.Create("b", "t", 1, 11).Data!
            };

            _taskRepositoryMock
                .Setup(r => r.GetTaskComments(1))
                .Returns(Result<List<Comment>>.Ok(list));

            // Act
            var result = _sut.GetTaskComments(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data!.Count);
            _taskRepositoryMock.Verify(r => r.GetTaskComments(1), Times.Once);
        }

        [Fact]
        public void GetTaskComments_ShouldReturnFail_WhenRepositoryFail()
        {
            // Arrange
            _taskRepositoryMock
                .Setup(r => r.GetTaskComments(1))
                .Returns(Result<List<Comment>>.Fail("err"));

            // Act
            var result = _sut.GetTaskComments(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("err", result.Error);
            _taskRepositoryMock.Verify(r => r.GetTaskComments(1), Times.Once);
        }

        [Fact]
        public void AddLikeToComment_ShouldReturnLike_WhenRepositoryOk()
        {
            // Arrange
            var like = CommentLike.Create(5, 10);

            _taskRepositoryMock
                .Setup(r => r.AddLikeToComment(5, 10))
                .Returns(Result<CommentLike>.Ok(like.Data!));

            // Act
            var result = _sut.AddLikeToComment(5, 10);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            _taskRepositoryMock.Verify(r => r.AddLikeToComment(5, 10), Times.Once);
        }

        [Fact]
        public void AddLikeToComment_ShouldReturnFail_WhenRepositoryFail()
        {
            // Arrange
            _taskRepositoryMock
                .Setup(r => r.AddLikeToComment(5, 10))
                .Returns(Result<CommentLike>.Fail("err"));

            // Act
            var result = _sut.AddLikeToComment(5, 10);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("err", result.Error);
            _taskRepositoryMock.Verify(r => r.AddLikeToComment(5, 10), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnFail_WhenOwnerTasksFail()
        {
            // Arrange
            _taskRepositoryMock
                .Setup(r => r.GetAllOwnTasksByUserId(10))
                .Returns(Result<List<ProjectTask>>.Fail("err"));

            _taskRepositoryMock
                .Setup(r => r.GetAllOtherTasksByUserId(10))
                .Returns(Result<List<ProjectTask>>.Ok(new List<ProjectTask>()));

            // Act
            var result = _sut.GetAll(10);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("err", result.Error);
            _taskRepositoryMock.Verify(r => r.GetAllOwnTasksByUserId(10), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnFail_WhenOtherTasksFail()
        {
            // Arrange
            _taskRepositoryMock
                .Setup(r => r.GetAllOwnTasksByUserId(10))
                .Returns(Result<List<ProjectTask>>.Ok(new List<ProjectTask>()));

            _taskRepositoryMock
                .Setup(r => r.GetAllOtherTasksByUserId(10))
                .Returns(Result<List<ProjectTask>>.Fail("err"));

            // Act
            var result = _sut.GetAll(10);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("err", result.Error);
            _taskRepositoryMock.Verify(r => r.GetAllOtherTasksByUserId(10), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnConcat_WhenBothResultsOk()
        {
            // Arrange
            var t1 = ProjectTask.Create(DateTime.Now.AddDays(1), "title1", "desc", 1, 10,
                ProjectManager.Core.Models.Common.Enums.TaskStatus.InProgress, Core.Models.Common.Enums.TaskPriority.Low).Data!;

            var t2 = ProjectTask.Create(DateTime.Now.AddDays(1), "title2", "desc", 1, 11,
                ProjectManager.Core.Models.Common.Enums.TaskStatus.InProgress, Core.Models.Common.Enums.TaskPriority.Low).Data!;

            _taskRepositoryMock
                .Setup(r => r.GetAllOwnTasksByUserId(10))
                .Returns(Result<List<ProjectTask>>.Ok(new List<ProjectTask> { t1 }));

            _taskRepositoryMock
                .Setup(r => r.GetAllOtherTasksByUserId(10))
                .Returns(Result<List<ProjectTask>>.Ok(new List<ProjectTask> { t2 }));

            // Act
            var result = _sut.GetAll(10);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data!.Count);
        }

        [Fact]
        public void Create_ShouldReturnFail_WhenTitleInvalid()
        {
            // Arrange
            var request = new CreateTaskRequest
            {
                Title = "",
                Description = "desc",
                DeadLine = DateTime.Now.AddDays(1),
                Status = ProjectManager.Core.Models.Common.Enums.TaskStatus.ToDo,
                Priority = ProjectManager.Core.Models.Common.Enums.TaskPriority.Low
            };

            // Act
            var result = _sut.Create(request, 10, 1);

            // Assert
            Assert.False(result.IsSuccess);
            _taskRepositoryMock.Verify(r => r.Create(It.IsAny<ProjectTask>()), Times.Never);
        }

        [Fact]
        public void Create_ShouldCallRepositoryCreate_WhenRequestValid()
        {
            // Arrange
            var request = new CreateTaskRequest
            {
                Title = "title",
                Description = "desc",
                DeadLine = DateTime.Now.AddDays(1),
                Status = ProjectManager.Core.Models.Common.Enums.TaskStatus.ToDo,
                Priority = ProjectManager.Core.Models.Common.Enums.TaskPriority.Low
            };

            _taskRepositoryMock
                .Setup(r => r.Create(It.IsAny<ProjectTask>()))
                .Returns((ProjectTask t) => Result<ProjectTask>.Ok(t));

            // Act
            var result = _sut.Create(request, 10, 1);

            // Assert
            Assert.True(result.IsSuccess);
            _taskRepositoryMock.Verify(r => r.Create(It.IsAny<ProjectTask>()), Times.Once);
        }

        [Fact]
        public void GetCommentById_ShouldReturnComment_WhenRepositoryOk()
        {
            // Arrange
            var comment = Comment.Create("text", "title", 1, 10).Data!;

            _taskRepositoryMock
                .Setup(r => r.GetCommentById(5))
                .Returns(Result<Comment>.Ok(comment));

            // Act
            var result = _sut.GetCommentById(5);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            _taskRepositoryMock.Verify(r => r.GetCommentById(5), Times.Once);
        }

        [Fact]
        public void GetCommentById_ShouldReturnFail_WhenRepositoryFail()
        {
            // Arrange
            _taskRepositoryMock
                .Setup(r => r.GetCommentById(5))
                .Returns(Result<Comment>.Fail("err"));

            // Act
            var result = _sut.GetCommentById(5);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("err", result.Error);
            _taskRepositoryMock.Verify(r => r.GetCommentById(5), Times.Once);
        }
    }
}
