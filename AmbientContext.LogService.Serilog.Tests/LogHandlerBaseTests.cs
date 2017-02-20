using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AmbientContext.LogService.Serilog.Tests
{
    [TestClass]
    public class LogHandlerBaseTests
    {
        private readonly ILogger _loggerMock;

        public class TestErrorMetricsHandler : LogHandlerBase
        {
            public int VerboseMetric { get; private set; }

            public override void Verbose(string messageTemplate)
            {
                VerboseMetric++;
            }
        }

        public class FooHandler : LogHandlerBase
        {
            public int VerboseCalled { get; private set; }

            public override void Verbose(string messageTemplate)
            {
                VerboseCalled++;
            }
        }

        public LogHandlerBaseTests()
        {
            _loggerMock = Substitute.For<ILogger>();
        }

        [TestMethod]
        public void WhenAHandlerIsAdded_ShouldCallHandlerAndBaseLogger()
        {
            AmbientLogService sut = new AmbientLogService();

            var errorMetricsHandler = new TestErrorMetricsHandler();
            AmbientLogService.AddLogHandler(errorMetricsHandler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Verbose("Some Message");

            _loggerMock.Received(1).Verbose(Arg.Any<string>());
            errorMetricsHandler.VerboseMetric.Should().Be(1);
        }

        [TestMethod]
        public void WhenMultipleHandlersAreAdded_ShouldCallAllHandlersAndBaseLogger()
        {
            AmbientLogService sut = new AmbientLogService();

            var errorMetricsHandler = new TestErrorMetricsHandler();
            var fooHandler = new FooHandler();
            AmbientLogService.AddLogHandler(errorMetricsHandler);
            AmbientLogService.AddLogHandler(fooHandler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Verbose("Some Message");

            _loggerMock.Received(1).Verbose(Arg.Any<string>());
            errorMetricsHandler.VerboseMetric.Should().Be(1);
            fooHandler.VerboseCalled.Should().Be(1);
        }

        [TestMethod]
        public void Verbose_WithStringOverridden_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Verbose("Some Message");
            
            handler.Received(1).Verbose(Arg.Any<string>());
        }
    }
}
