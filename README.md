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

## Tutorials by Language

  * [C# tutorial](./csharp/)
