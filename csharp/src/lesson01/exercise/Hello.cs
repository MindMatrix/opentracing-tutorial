using System;
using Microsoft.Extensions.Logging;
using OpenTracing.Util;
using Jaeger;
using Jaeger.Samplers;

namespace OpenTracing.Tutorial.Lesson01.Exercise
{
    internal class Hello
    {
        private readonly ITracer _tracer;
        private readonly ILogger<Hello> _logger;

        public Hello(OpenTracing.ITracer tracer, ILoggerFactory loggerFactory)
        {
            _tracer = tracer;
            _logger = loggerFactory.CreateLogger<Hello>();
        }

        public void SayHello(string helloTo)
        {
            var span = _tracer.BuildSpan("say-hello").Start();
            var helloString = $"Hello, {helloTo}!";
            _logger.LogInformation(helloString);
            span.Finish();
        }

        public static void Main(string[] args)
        {
            if (args.Length != 1)
                args = new string[] { "OpenTrace" };

            using (var loggerFactory = new LoggerFactory().AddConsole())
            {
                var helloTo = args[0];
                using (var tracer = InitTracer("hello-world", loggerFactory))
                {
                    new Hello(tracer, loggerFactory).SayHello(helloTo);
                }
            }

            //this will keep the window open when running the project inside VS2017
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey();
            }
        }


        private static Tracer InitTracer(string serviceName, ILoggerFactory loggerFactory)
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
