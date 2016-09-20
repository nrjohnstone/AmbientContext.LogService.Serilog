using System;

namespace AmbientContext.LogService.Serilog
{
    public interface ILogContext
    {
        IDisposable PushProperty(string name, object value);
        IDisposable SetCorrelationId(object correlationId);
    }
}