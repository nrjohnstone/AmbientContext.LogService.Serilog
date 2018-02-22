using System;
using System.Threading;
using NSubstitute;
using Xunit;

namespace AmbientContext.LogService.Serilog.Tests
{
    public static class AmbientLogServiceLock
    {
        public static Mutex AddHandlerLock = new Mutex();
    }

    public class AmbientLogServiceTests
    {
        private static readonly ILogger _loggerMock = Substitute.For<ILogger>();
        private static readonly LogHandlerBase _handler1 = Substitute.For<LogHandlerBase>();
        private static readonly LogHandlerBase _handler2 = Substitute.For<LogHandlerBase>();
        private static readonly object _lock = new object();
        private static volatile bool _logserviceConfigured = false;

        public AmbientLogServiceTests()
        {
            lock (_lock)
            {
                if (!_logserviceConfigured)
                {
                    AmbientLogServiceLock.AddHandlerLock.WaitOne();
                    AmbientLogService.AddLogHandler(_handler1);
                    AmbientLogService.AddLogHandler(_handler2);
                    AmbientLogService.Create = () => _loggerMock;
                    _logserviceConfigured = true;
                    AmbientLogServiceLock.AddHandlerLock.ReleaseMutex();
                }
            }
        }

        [Fact]
        public void Verbose_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();

            AmbientLogService.Create = () => _loggerMock;
            string message = Guid.NewGuid().ToString();

            sut.Verbose(message);

            _handler1.Received(1).Verbose(message);
        }

        [Fact]
        public void Verbose_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Verbose(message, 1, 2);

            _handler1.Received(1).Verbose(message, 1, 2);
        }

        [Fact]
        public void Verbose_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();
            var exception = new Exception();

            sut.Verbose(exception, message);

            _handler1.Received(1).Verbose(exception, message);
        }

        [Fact]
        public void Debug_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Debug(message);

            _handler1.Received(1).Debug(message);
        }

        [Fact]
        public void Debug_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Debug(message, 1, 2);

            _handler1.Received(1).Debug(message, 1, 2);
        }

        [Fact]
        public void Debug_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();
            var exception = new Exception();

            sut.Debug(exception, message);

            _handler1.Received(1).Debug(exception, message);
        }

        [Fact]
        public void Information_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Information(message);

            _handler1.Received(1).Information(message);
        }

        [Fact]
        public void Information_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Information(message, 1, 2);

            _handler1.Received(1).Information(message, 1, 2);
        }

        [Fact]
        public void Information_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();
            var exception = new Exception();

            sut.Information(exception, message);

            _handler1.Received(1).Information(exception, message);
        }

        [Fact]
        public void Warning_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Warning(message);

            _handler1.Received(1).Warning(message);
        }

        [Fact]
        public void Warning_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Warning(message, 1, 2);

            _handler1.Received(1).Warning(message, 1, 2);
        }

        [Fact]
        public void Warning_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();
            var exception = new Exception();

            sut.Warning(exception, message);

            _handler1.Received(1).Warning(exception, message);
        }

        [Fact]
        public void Error_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Error(message);

            _handler1.Received(1).Error(message);
        }

        [Fact]
        public void Error_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Error(message, 1, 2);

            _handler1.Received(1).Error(message, 1, 2);
        }

        [Fact]
        public void Error_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();
            var exception = new Exception();

            sut.Error(exception, message);

            _handler1.Received(1).Error(exception, message);
        }

        [Fact]
        public void Fatal_WithString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Fatal(message);

            _handler1.Received(1).Fatal(message);
        }

        [Fact]
        public void Fatal_WithStringAndParameters_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Fatal(message, 1, 2);

            _handler1.Received(1).Fatal(message, 1, 2);
        }

        [Fact]
        public void Fatal_WithExceptionString_ShouldCallHandler()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();
            var exception = new Exception();

            sut.Fatal(exception, message);

            _handler1.Received(1).Fatal(exception, message);
        }

        [Fact]
        public void Error_WithExceptionString_ShouldLogAsErrorToSerilog()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();
            var exception = new Exception();

            sut.Error(exception, message);

            sut.Instance.Received(1).Error(exception, message);
        }

        [Fact]
        public void WhenMultipleHandlersAreAdded_ShouldCallAllHandlersAndBaseLogger()
        {
            AmbientLogService sut = new AmbientLogService();
            string message = Guid.NewGuid().ToString();

            sut.Verbose(message);

            _loggerMock.Received(1).Verbose(message);
            _handler1.Received(1).Verbose(message);
            _handler2.Received(1).Verbose(message);
        }
    }
}