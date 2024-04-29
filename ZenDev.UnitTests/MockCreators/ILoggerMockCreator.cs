using Microsoft.Extensions.Logging;
using Moq;

namespace ZenDev.UnitTests.MockCreators
{
    public class ILoggerMockCreator<TCategoryName>
    {
        public ILogger<TCategoryName> Create()
        {
            return Mock.Of<ILogger<TCategoryName>>();
        }
    }
}