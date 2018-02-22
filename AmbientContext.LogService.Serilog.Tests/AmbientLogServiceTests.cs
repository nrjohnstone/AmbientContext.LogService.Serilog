using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AmbientContext.LogService.Serilog.Tests
{
    [TestClass]
    public class AmbientLogServiceTests
    {
        private readonly ILogger _loggerMock;

        public AmbientLogServiceTests()
        {
            _loggerMock = Substitute.For<ILogger>();
        }

        [TestMethod]
        public void Verbose_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Verbose("Some Message");

            handler.Received(1).Verbose(Arg.Any<string>());
        }

        [TestMethod]
        public void Verbose_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Verbose("Some Message", 1, 2);

            handler.Received(1).Verbose(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [TestMethod]
        public void Verbose_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Verbose(new Exception(), "Some Message");

            handler.Received(1).Verbose(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [TestMethod]
        public void Debug_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Debug("Some Message");

            handler.Received(1).Debug(Arg.Any<string>());
        }

        [TestMethod]
        public void Debug_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Debug("Some Message", 1, 2);

            handler.Received(1).Debug(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [TestMethod]
        public void Debug_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Debug(new Exception(), "Some Message");

            handler.Received(1).Debug(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [TestMethod]
        public void Information_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Information("Some Message");

            handler.Received(1).Information(Arg.Any<string>());
        }

        [TestMethod]
        public void Information_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Information("Some Message", 1, 2);

            handler.Received(1).Information(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [TestMethod]
        public void Information_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Information(new Exception(), "Some Message");

            handler.Received(1).Information(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [TestMethod]
        public void Warning_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Warning("Some Message");

            handler.Received(1).Warning(Arg.Any<string>());
        }

        [TestMethod]
        public void Warning_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Warning("Some Message", 1, 2);

            handler.Received(1).Warning(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [TestMethod]
        public void Warning_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Warning(new Exception(), "Some Message");

            handler.Received(1).Warning(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [TestMethod]
        public void Error_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Error("Some Message");

            handler.Received(1).Error(Arg.Any<string>());
        }

        [TestMethod]
        public void Error_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Error("Some Message", 1, 2);

            handler.Received(1).Error(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [TestMethod]
        public void Error_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Error(new Exception(), "Some Message");

            handler.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [TestMethod]
        public void Fatal_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Fatal("Some Message");

            handler.Received(1).Fatal(Arg.Any<string>());
        }

        [TestMethod]
        public void Fatal_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Fatal("Some Message", 1, 2);

            handler.Received(1).Fatal(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [TestMethod]
        public void Fatal_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Fatal(new Exception(), "Some Message");

            handler.Received(1).Fatal(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [TestMethod]
        public void Error_WithExceptionString_ShouldLogAsErrorToSerilog()
        {
            AmbientLogService sut = new AmbientLogService();
            var handler = Substitute.For<LogHandlerBase>();

            AmbientLogService.AddLogHandler(handler);
            AmbientLogService.Create = () => _loggerMock;

            sut.Error(new Exception(), "Some Message");

            sut.Instance.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
        }


    }
}