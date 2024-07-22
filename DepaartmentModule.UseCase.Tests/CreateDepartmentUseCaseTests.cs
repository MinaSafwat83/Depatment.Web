using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DepartmentModule.Core.Model;
using DepartmentModule.UseCase.Departments;
using DepartmentModule.UseCase.Departments.Interfaces;
using DepartmentModule.UseCase.DTOs;
using DepartmentModule.UseCase.PluginInterface;

namespace DepartmentModule.Tests.UseCase.Departments
{
    public class CreateDepartmentUseCaseTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly CreateDepartmentUseCase _createDepartmentUseCase;

        public CreateDepartmentUseCaseTests()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
            _createDepartmentUseCase = new CreateDepartmentUseCase(_mockDepartmentRepository.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WithNullCreateDepDTO_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _createDepartmentUseCase.ExecuteAsync(null));
        }

        [Fact]
        public async Task ExecuteAsync_WithEmptyName_ThrowsArgumentException()
        {
            // Arrange
            var createDepDTO = new CreateDepDTO { Name = string.Empty };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _createDepartmentUseCase.ExecuteAsync(createDepDTO));

            Assert.Equal("Department name is required. (Parameter 'Name')", exception.Message);
        }

        [Fact]
        public async Task ExecuteAsync_WithParentDepartmentIdNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var createDepDTO = new CreateDepDTO
            {
                Name = "HR",
                ParentDepartmentId = 1
            };
            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Department)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _createDepartmentUseCase.ExecuteAsync(createDepDTO));
            Assert.Equal("Parent department not found.", exception.Message);
        }

      
        [Fact]
        public async Task ExecuteAsync_WithParentDepartmentId_CreatesAndReturnsDepartmentWithParent()
        {
            // Arrange
            var parentDepartment = new Department { Id = 1, Name = "IT" };
            var createDepDTO = new CreateDepDTO
            {
                Name = "HR",
                ParentDepartmentId = 1,
                Logo = null
            };

            _mockDepartmentRepository.Setup(repo => repo.GetDepartmentByIdAsync(createDepDTO.ParentDepartmentId.Value))
                .ReturnsAsync(parentDepartment);
            _mockDepartmentRepository.Setup(repo => repo.AddAsync(It.IsAny<Department>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _createDepartmentUseCase.ExecuteAsync(createDepDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createDepDTO.Name, result.Name);
            Assert.Equal(createDepDTO.ParentDepartmentId, result.ParentDepartmentId);
            Assert.Equal(parentDepartment, result.ParentDepartment);
            _mockDepartmentRepository.Verify(repo => repo.AddAsync(It.IsAny<Department>()), Times.Once);
        }
    }
}
