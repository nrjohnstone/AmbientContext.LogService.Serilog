using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace AmbientContext.LogService.Serilog
{
    public class AmbientLogService : AmbientService<ILogger>, ILogger
    {
        protected override ILogger DefaultCreate()
        {
            return new SerilogLogAdapter(Log.Logger);
        }

        public void Verbose(string messageTemplate) =>
           Instance.Verbose(messageTemplate);

        public void Verbose(string messageTemplate, params object[] propertyValues) =>
            Instance.Verbose(messageTemplate, propertyValues);

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues) =>
            Instance.Verbose(exception, messageTemplate, propertyValues);

        public void Debug(string messageTemplate) =>
            Instance.Debug(messageTemplate);

        public void Debug(string messageTemplate, params object[] propertyValues) =>
            Instance.Debug(messageTemplate, propertyValues);

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues) =>
            Instance.Debug(exception, messageTemplate, propertyValues);

        public void Information(string messageTemplate) =>
            Instance.Information(messageTemplate);

        public void Information(string messageTemplate, params object[] propertyValues) =>
            Instance.Information(messageTemplate, propertyValues);

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues) =>
            Instance.Information(exception, messageTemplate, propertyValues);

        public void Warning(string messageTemplate) =>
            Instance.Warning(messageTemplate);

        public void Warning(string messageTemplate, params object[] propertyValues) =>
            Instance.Warning(messageTemplate, propertyValues);

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues) =>
            Instance.Warning(exception, messageTemplate, propertyValues);

        public void Fatal(string messageTemplate) =>
            Instance.Fatal(messageTemplate);

        public void Fatal(string messageTemplate, params object[] propertyValues) =>
            Instance.Fatal(messageTemplate, propertyValues);

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues) =>
            Instance.Fatal(exception, messageTemplate, propertyValues);

        public void Error(string messageTemplate) =>
            Instance.Error(messageTemplate);

        public void Error(string messageTemplate, params object[] propertyValues) =>
            Instance.Error(messageTemplate, propertyValues);

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues) =>
            Instance.Error(exception, messageTemplate, propertyValues);

        public ILogContext LogContext => Instance.LogContext;
    }
}
