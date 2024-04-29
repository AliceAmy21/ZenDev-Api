using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using ZenDev.Persistence;
using ZenDev.Persistence.Entities;

namespace ZenDev.UnitTests.MockCreators
{
    public class ZenDevDbContextMockCreator
    {
        public ZenDevDbContext Create(IEnumerable<ExampleEntity> examples)
        {
            var dbContextMock = new Mock<ZenDevDbContext>();

            SetupExamplesMocks(examples, dbContextMock);

            return dbContextMock.Object;
        }

        private void SetupExamplesMocks(IEnumerable<ExampleEntity> examples, Mock<ZenDevDbContext> dbContextMock)
        {
            dbContextMock.Setup(mock => mock.Examples)
                .ReturnsDbSet(examples);
        }
    }
}
