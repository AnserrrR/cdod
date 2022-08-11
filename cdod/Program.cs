using System.Text;
using cdod.Schema;
using cdod.Schema.Mutations;
using cdod.Schema.Queries;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using cdod.Services.DataLoaders;
using cdod.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>()
    .AddType<SchoolType>()
    .AddType<StudentType>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<MutationUser>()
    .AddTypeExtension<MutationParent>()
    .AddTypeExtension<MutationSchool>()
    .AddTypeExtension<MutationStudent>()
    .AddTypeExtension<MutationCourse>()
    //.AddTypeExtension<MutationGroup>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

// ƒÀﬂ ¿”Õ“»‘» ¿÷»»
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Issuer"),
            ValidateIssuer = true,
            ValidAudience = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Audience"),
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenSettings").GetValue<string>("Key"))),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();
//

//builder.Services.AddPooledDbContextFactory<CdodDbContext>(o => o.UseMySql("server=localhost;user=root;password=Student;database=test_db1;", new MySqlServerVersion(new Version("8.0.28"))));
string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<CdodDbContext>(o => o.UseNpgsql(connectionString).LogTo(Console.WriteLine));
//builder.Services.AddDbContext<CdodDbContext>();

//builder.Services.AddScoped<StudentsRepository>();
builder.Services.AddScoped<SchoolDataLoader>();
builder.Services.AddScoped<StcDataLoader>();
builder.Services.AddScoped<ParentDataLoader>();
builder.Services.AddScoped<UserDataLoader>();
builder.Services.AddScoped<CourseDataLoader>();
builder.Services.AddScoped<GroupByStudentIdCourseIdDataLoader>();
builder.Services.AddScoped<PayNotesDataLoader>();
builder.Services.AddScoped<PayInfoDataLoader>();
builder.Services.AddScoped<TeacherDataLoader>();
builder.Services.AddScoped<PostDataLoader>();
builder.Services.AddScoped<StudentsCountDataLoader>();
builder.Services.AddScoped<StudentDataLoader>();
builder.Services.AddScoped<GroupDataLoader>();
builder.Services.AddScoped<LessonDataLoader>();

//Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

//Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//AUTH
app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL("/");

app.Run();


