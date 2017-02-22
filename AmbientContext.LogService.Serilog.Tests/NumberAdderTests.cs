using AmbientContext.LogService.Serilog.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AmbientContext.LogService.Serilog.Tests
{
    [TestClass]
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

        [TestMethod]
        public void AddingNumbers_WhenSumIsNotDivisibleBy10_ShouldLogWarning()
        {
            var sut = CreateSut();

            sut.Add(3,4);

            _loggerMock.Received().Warning(Arg.Any<string>(), Arg.Any<object[]>());
        }
    }
}