using cdod.Schema;
using cdod.Schema.Mutations;
using cdod.Schema.Queries;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<MutationUser>()
    .AddTypeExtension<MutationParent>()
    .AddTypeExtension<MutationSchool>()
    .AddTypeExtension<MutationStudent>()
    .AddTypeExtension<MutationCourse>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

//builder.Services.AddPooledDbContextFactory<CdodDbContext>(o => o.UseMySql("server=localhost;user=root;password=Student;database=test_db1;", new MySqlServerVersion(new Version("8.0.28"))));
string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<CdodDbContext>(o => o.UseNpgsql(connectionString).LogTo(Console.WriteLine));
//builder.Services.AddDbContext<CdodDbContext>();

var app = builder.Build();

app.MapGraphQL("/");

app.Run();
