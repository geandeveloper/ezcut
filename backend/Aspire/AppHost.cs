using Aspire.Hosting.Yarp;
using Aspire.Hosting.Yarp.Transforms;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder
    .AddProject<Api>("api")
    .WithReplicas(2);

var gateway = builder
    .AddYarp("gateway")
    .WithHostPort(5000)
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute(api);

        yarp.AddRoute("/api/{**catch-all}", api)
            .WithMatchMethods("GET", "POST")
            .WithTransformPathRemovePrefix("/api");
    });

builder.Build().Run();
