using System;

namespace AmbientContext.LogService.Serilog
{
    internal class LogContextAdapter : ILogContext
    {
        public IDisposable PushProperty(string name, object value)
        {
            return global::Serilog.Context.LogContext.PushProperty(name, value);
        }

        public IDisposable SetCorrelationId(object correlationId)
        {
            return PushProperty("CorrelationId", correlationId);
        }
    }
}