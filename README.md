# OpenTracing Tutorials

A collection of tutorials for the OpenTracing API (https://opentracing.io).

## Prerequisites

Docker for Windows will need to be installed and downloaded from https://docs.docker.com/docker-for-windows/install/

Once docker is installed you will need to enable kubernetes under Settings -> Kubernetes -> Enable Kubernetes (make sure swarm is not selected)

The tutorials are using CNCF Jaeger (https://jaegertracing.io) as the tracing backend.
For this tutorial, we'll start Jaeger via Kubernetes with the default in-memory storage, exposing only the required ports. 

```
kubectl apply -f all-in-one.yml
```

Once the backend starts, the Jaeger UI will be accessible at http://localhost:8888.

Note: All the demos require you to run the jaeger agent on your local machine. The agent acts as a buffer between your application and the collector and communicates over UDP. The agent will collect all your logs and ship them off to the collector for you, the agent running locally over UDP creates minimal overhead to your application.

Open a command prompt in the jaeger binaries folder and run the following command:
```
jaeger-agent.exe --collector.host-port "localhost:14267"
```


## Tutorials by Language

  * [C# tutorial](./csharp/)
