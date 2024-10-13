var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AspireSample_WebApi>("aspiresample-webapi");

builder.AddProject<Projects.AspireSample_BackgroundWorker>("aspiresample-backgroundworker");

builder.Build().Run();
