using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.Logging;

namespace OpenTracing.Tutorial.Library
{
    public static class Tracing
    {
        public static Tracer Init(string serviceName, ILoggerFactory loggerFactory)
        {
            var samplerConfig = new Configuration.SamplerConfiguration(loggerFactory)
                .WithType(ConstSampler.Type)
                .WithParam(1);

            var senderConfig = new Configuration.SenderConfiguration(loggerFactory)
                .WithEndpoint("http://localhost:14268/api/traces");

            var reporterConfig = new Configuration.ReporterConfiguration(loggerFactory)
                .WithLogSpans(true)
                .WithSender(senderConfig);

            return (Tracer)new Configuration(serviceName, loggerFactory)
                .WithSampler(samplerConfig)
                .WithReporter(reporterConfig)
                .GetTracer();
        }
    }
}