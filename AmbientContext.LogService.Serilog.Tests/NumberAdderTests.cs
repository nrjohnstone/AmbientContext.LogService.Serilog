using AmbientContext.LogService.Serilog.Example;
using NSubstitute;
using Xunit;

namespace AmbientContext.LogService.Serilog.Tests
{
    public class NumberAdderTests
    {
        private readonly ILogger _loggerMock;

        public NumberAdderTests()
        {
            _loggerMock = Substitute.For<ILogger>();
        }

        private NumberAdder CreateSut()
        {
            var sut = new NumberAdder();
            sut.Logger.Instance = _loggerMock;
            return sut;
        }

        [Fact]
        public void AddingNumbers_WhenSumIsNotDivisibleBy10_ShouldLogWarning()
        {
            var sut = CreateSut();

            sut.Add(3,4);

            _loggerMock.Received().Warning(Arg.Any<string>(), Arg.Any<object[]>());
        }
    }
}