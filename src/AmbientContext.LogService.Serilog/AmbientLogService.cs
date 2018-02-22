using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace AmbientContext.LogService.Serilog
{
    public class AmbientLogService : AmbientService<ILogger>, ILogger
    {
        protected static readonly List<LogHandlerBase> Handlers = new List<LogHandlerBase>();

        protected override ILogger DefaultCreate()
        {
            return new SerilogLogAdapter(Log.Logger);
        }

        public static void AddLogHandler(LogHandlerBase handler)
        {
            Handlers.Add(handler);
        }

        public void Verbose(string messageTemplate)
        {
            Handlers.ForEach(x => x.Verbose(messageTemplate));
            Instance.Verbose(messageTemplate);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Verbose(messageTemplate, propertyValues));
            Instance.Verbose(messageTemplate, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Verbose(exception, messageTemplate, propertyValues));
            Instance.Verbose(exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate)
        {
            Handlers.ForEach(x => x.Debug(messageTemplate));
            Instance.Debug(messageTemplate);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Debug(messageTemplate, propertyValues));
            Instance.Debug(messageTemplate, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Debug(exception, messageTemplate, propertyValues));
            Instance.Debug(exception, messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate)
        {
            Handlers.ForEach(x => x.Information(messageTemplate));
            Instance.Information(messageTemplate);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Information(messageTemplate, propertyValues));
            Instance.Information(messageTemplate, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Information(exception, messageTemplate, propertyValues));
            Instance.Information(exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate)
        {
            Handlers.ForEach(x => x.Warning(messageTemplate));
            Instance.Warning(messageTemplate);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Warning(messageTemplate, propertyValues));
            Instance.Warning(messageTemplate, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Warning(exception, messageTemplate, propertyValues));
            Instance.Warning(exception, messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate)
        {
            Handlers.ForEach(x => x.Fatal(messageTemplate));
            Instance.Fatal(messageTemplate);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Fatal(messageTemplate, propertyValues));
            Instance.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Fatal(exception, messageTemplate, propertyValues));
            Instance.Fatal(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate)
        {
            Handlers.ForEach(x => x.Error(messageTemplate));
            Instance.Error(messageTemplate);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Error(messageTemplate, propertyValues));
            Instance.Error(messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Handlers.ForEach(x => x.Error(exception, messageTemplate, propertyValues));
            Instance.Error(exception, messageTemplate, propertyValues);
        }

        public ILogContext LogContext => Instance.LogContext;
    }
}
