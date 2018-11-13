using System;
using System.Collections.Generic;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.Logging;
using OpenTracing.Propagation;
using OpenTracing.Tag;

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

        public static IScope StartServerSpan(ITracer tracer, IDictionary<string, string> headers, string operationName)
        {
            ISpanBuilder spanBuilder;
            try
            {
                ISpanContext parentSpanCtx = tracer.Extract(BuiltinFormats.HttpHeaders, new TextMapExtractAdapter(headers));

                spanBuilder = tracer.BuildSpan(operationName);
                if (parentSpanCtx != null)
                {
                    spanBuilder = spanBuilder.AsChildOf(parentSpanCtx);
                }
            }
            catch (Exception)
            {
                spanBuilder = tracer.BuildSpan(operationName);
            }

            // TODO could add more tags like http.url
            return spanBuilder.WithTag(Tags.SpanKind, Tags.SpanKindServer).StartActive(true);
        }
    }
}