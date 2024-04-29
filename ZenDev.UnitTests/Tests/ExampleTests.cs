using Microsoft.Extensions.Logging;
using ZenDev.BusinessLogic.Models;
using ZenDev.BusinessLogic.Services;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;
using ZenDev.UnitTests.MockCreators;

namespace ZenDev.UnitTests.Tests
{
    [TestClass]
    public class ExampleTests
    {
        private readonly ZenDevDbContext _zenDevDbContextMock;
        private readonly ILogger<ExampleService> _exampleServiceLoggerMock;

        public ExampleTests()
        {
            var exampleEntities = new List<ExampleEntity>
            {
                new ExampleEntity
                {
                    Id = 1,
                    Name = "Example Name",
                    Code = "Example Code",
                    Description = "Example Description",
                },
            };

            var zenDevDbContextMockCreator = new ZenDevDbContextMockCreator();
            _zenDevDbContextMock = zenDevDbContextMockCreator.Create(exampleEntities);

            var loggerMockCreator = new ILoggerMockCreator<ExampleService>();
            _exampleServiceLoggerMock = loggerMockCreator.Create();
        }

        [TestMethod]
        public void GetExampleByIdReturnsExampleEntity()
        {
            // Arrange
            var exampleService = new ExampleService(_zenDevDbContextMock, _exampleServiceLoggerMock);

            // Act
            var result = exampleService.GetExampleByIdAsync(1).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ExampleEntity?>(result);
        }

        [TestMethod]
        public void GetAllExamplesReturnsAllExamples()
        {
            // Arrange
            var exampleService = new ExampleService(_zenDevDbContextMock, _exampleServiceLoggerMock);

            // Act
            var result = exampleService.GetAllExamplesAsync().GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<List<ExampleEntity>>(result);
            Assert.IsTrue(result.Count() == 1);
        }
    }
}
