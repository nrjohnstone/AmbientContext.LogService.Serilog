using System;
using StatsdClient;

namespace AmbientContext.LogService.Serilog.Example
{
    public class StatsdLogHandler : LogHandlerBase
    {
        private const string MetricNameLogEventsFatal = "Serilog.Example.LogEventsFatal";
        private readonly Statsd _statsd;

        public StatsdLogHandler(Statsd statsd)
        {
            _statsd = statsd;
        }

        public override void Fatal(string messageTemplate)
        {
            ReportFatal();
        }

        public override void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            ReportFatal();
        }

        public override void Fatal(string messageTemplate, params object[] propertyValues)
        {
            ReportFatal();
        }

        private void ReportFatal()
        {
            _statsd.LogCount(MetricNameLogEventsFatal);
        }
    }
}