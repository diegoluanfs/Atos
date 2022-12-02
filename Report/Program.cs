using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using report._Common;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;

string MyPolicy = "MyPolicy";
var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

Helper.Configuration = configuration;

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyPolicy,
    cors =>
    {
        cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.Configure<JsonOptions>(options =>
{
    //options.SerializerOptions.Converters.Add(new DecimalCustom());
    //options.SerializerOptions.Converters.Add(new Int32Custom());
    //options.SerializerOptions.Converters.Add(new DateTimeCustom());




    options.SerializerOptions.PropertyNameCaseInsensitive = false;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.WriteIndented = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IKeyManager, KeyManager>();

builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report API - Version 1", Version = "v1.0" });
    c.SwaggerDoc("v2", new OpenApiInfo() { Title = "Report API - Version 2", Version = "v2.0" });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    //c.EnableAnnotations();
    // c.DocumentFilter<DocumentFilter>();


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'APIKey' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'APIKey 12345abcdef'",
        Name = "APIKey",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityDefinition("APIVersion", new OpenApiSecurityScheme
    {
        Description = @"Client Version",
        Name = "APIVersion",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "APIVersion"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                      new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
            });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                      new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "APIVersion"
                            },
                        },
                        new List<string>()
                    }
            });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
    //  c.SchemaFilter<SchemaFilter>();

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyPolicy);

app.MapControllers();

app.Run();
