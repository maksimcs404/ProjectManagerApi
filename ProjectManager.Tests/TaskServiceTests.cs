using Moq;
using ProjectManager.Application.Services;
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
    }
}
