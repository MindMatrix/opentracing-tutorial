using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using OpenTracing.Tutorial.Library;
using Jaeger;

namespace OpenTracing.Tutorial.Lesson03.Solution.Server.Controllers
{
    [Route("api/format")]
    public class FormatController : Controller
    {
        private readonly ITracer _tracer;

        public FormatController(ITracer tracer)
        {
            _tracer = tracer;
        }

        public string Get(string helloTo)
        {
            var headers = Request.Headers.ToDictionary(k => k.Key, v => v.Value.First());
            using (var scope = Tracing.StartServerSpan(_tracer, headers, "format-controller"))
            {
                var formattedHelloString = $"Hello, {helloTo}!";
                scope.Span.Log(new Dictionary<string, object>
                {
                    [LogFields.Event] = "string-format",
                    ["value"] = formattedHelloString
                });
                return formattedHelloString;
            }   
        }
    }
}