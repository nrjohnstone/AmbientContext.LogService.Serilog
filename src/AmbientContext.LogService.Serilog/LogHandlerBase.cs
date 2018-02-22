using System;

namespace AmbientContext.LogService.Serilog
{
    /// <summary>
    /// Base class for implementing custom handlers for log messages.
    /// Be sure to catch/handle all exceptions to ensure that log messages
    /// still make it to Serilog
    /// </summary>
    public abstract class LogHandlerBase : ILogger
    {
        public virtual void Verbose(string messageTemplate)
        {

        }

        public virtual void Verbose(string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Debug(string messageTemplate)
        {

        }

        public virtual void Debug(string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Information(string messageTemplate)
        {

        }

        public virtual void Information(string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Warning(string messageTemplate)
        {

        }

        public virtual void Warning(string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Fatal(string messageTemplate)
        {

        }

        public virtual void Fatal(string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Error(string messageTemplate)
        {

        }

        public virtual void Error(string messageTemplate, params object[] propertyValues)
        {

        }

        public virtual void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {

        }

        public ILogContext LogContext { get; }
    }
}