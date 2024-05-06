
var builder = DistributedApplication.CreateBuilder(args);

var mainDbUsername = builder.AddParameter("postgres-username");
var mainDbPassword = builder.AddParameter("postgres-password");

var mainDb = builder
    .AddPostgres("main-db", mainDbUsername, mainDbPassword)
    .WithDataVolume()
    .AddDatabase("dometrain");

var redis = builder.AddRedis("redis");

var cartDb = builder.AddAzureCosmosDB("cosmosdb")
    .AddDatabase("cartdb");

builder.AddProject<Projects.Dometrain_Monolith_Api>("dometrain-api")
    .WithReplicas(5)
    .WithReference(mainDb)
    .WithReference(cartDb)
    .WithReference(redis);

builder.Build().Run();
