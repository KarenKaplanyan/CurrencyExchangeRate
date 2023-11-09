using CurrencyExchangeRate.WebApi.Core;
using CurrencyExchangeRate.WebApi.Infrastructure.Contexts;
using CurrencyExchangeRate.WebApi.Infrastructure.Mappings;
using CurrencyExchangeRate.WebApi.Infrastructure.Repositories.Common;
using CurrencyExchangeRate.WebApi.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host=="localhost");
    });
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>();
builder.Services.AddAutoMapper(typeof(CurrencyExchangeRateProfile));
builder.Services.AddDbContext<CurrencyDbContext>(
    options => options.UseMySQL(
        builder.Configuration.GetConnectionString("CurrencyExchangeRateDatabase"))
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
        {
            Name = "Bearer",
            BearerFormat = "JWT",
            Scheme = "bearer",
            Description = "Authorization token.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
        };
        options.AddSecurityDefinition("jwt_auth", securityDefinition);
        
        OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Id = "jwt_auth",
                Type = ReferenceType.SecurityScheme
            }
        };
        OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
        {
            {securityScheme, new string[] { }},
        };
        options.AddSecurityRequirement(securityRequirements);
    });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.PUBLISHER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.CLIENT,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();