using Microsoft.OpenApi.Models;
using seecreativa_backend.Classifications;
using seecreativa_backend.Clients;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Core.Token;
using seecreativa_backend.Prices;
using seecreativa_backend.Products;
using seecreativa_backend.Users;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your token in the field below"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoConnection"));
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("Token"));

builder.Services.AddUsers();
builder.Services.AddClients();
builder.Services.AddClassifications();
builder.Services.AddPrices();
builder.Services.AddProducts();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
