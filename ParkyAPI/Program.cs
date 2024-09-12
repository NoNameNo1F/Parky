using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Models.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using ParkyAPI;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
// Add Repository Pattern
builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(ParkyMappings));
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y =>
{
    y.RequireHttpsMetadata = false;
    y.SaveToken = true;
    y.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// builder.Services.AddSwaggerGen(options =>
// {
//     // options.SwaggerDoc("ParkyOpenAPISpecNP",
//     options.SwaggerDoc("ParkyOpenAPISpec",
//         new Microsoft.OpenApi.Models.OpenApiInfo()
//         {
//             Title = "Parky API",
//             Version = "1",
//             Description = "Udemy Parky API",
//             Contact = new Microsoft.OpenApi.Models.OpenApiContact()
//             {
//                 Email = "nonameno1f@gmail.com",
//                 Name = "Nguyen Cao Nam Vu",
//                 Url = new Uri("https://www.fb.com/NoNameNo1F")
//             },
//             License = new Microsoft.OpenApi.Models.OpenApiLicense()
//             {
//                 Name = "MIT License",
//                 Url = new Uri("https://opensource.org/licenses/MIT")
//             }
//         }
//     );

//     // options.SwaggerDoc("ParkyOpenAPISpecTrails",
//     //     new Microsoft.OpenApi.Models.OpenApiInfo()
//     //     {
//     //         Title = "Parky API (Trails)",
//     //         Version = "1",
//     //         Description = "Udemy Parky API Trails",
//     //         Contact = new Microsoft.OpenApi.Models.OpenApiContact()
//     //         {
//     //             Email = "nonameno1f@gmail.com",
//     //             Name = "Nguyen Cao Nam Vu",
//     //             Url = new Uri("https://www.fb.com/NoNameNo1F")
//     //         },
//     //         License = new Microsoft.OpenApi.Models.OpenApiLicense()
//     //         {
//     //             Name = "MIT License",
//     //             Url = new Uri("https://opensource.org/licenses/MIT")
//     //         }
//     //     }
//     // );
//     var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
//     options.IncludeXmlComments(cmlCommentsFullPath);
// });
var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    // app.UseSwaggerUI(options =>
    // {
    //     // options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecNP/swagger.json", "Parky API NP");
    //     options.SwaggerEndpoint("/swagger/ParkyOpenAPISpec/swagger.json", "Parky API");
    //     // options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecTrails/swagger.json", "Parky API Trails");
    //     options.RoutePrefix = "";
    // });
}
app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                desc.GroupName.ToUpperInvariant());
        }
        options.RoutePrefix = "";
    }
);
app.UseCors();
app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
