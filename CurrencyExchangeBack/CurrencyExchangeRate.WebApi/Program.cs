using CurrencyExchangeRate.Domain.Repositories.Interfaces;
using CurrencyExchangeRate.Infrastructure.Repositories.Common;
using CurrencyExchangeRate.WebApi.Core;
using CurrencyExchangeRate.WebApi.Infrastructure.Extensions;
using CurrencyExchangeRate.WebApi.Mappings;
using CurrencyExchangeRate.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
builder.Services.AddScoped<ICurrencyExchangeRateService, CurrencyExchangeRateService>();
builder.Services.AddScoped<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>();
builder.Services.AddAutoMapper(typeof(CurrencyExchangeRateProfile));
builder.Services.AddAutoMapper(typeof(PagingDtoProfile));
builder.Services.AddAutoMapper(typeof(CurrencyDtoProfile));
builder.Services.AddDb(builder.Configuration);
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