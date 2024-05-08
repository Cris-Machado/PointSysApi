using Microsoft.OpenApi.Models;
using Point.API.Presentation.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "PointNow - API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Plaease enter a valid token",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});
builder.Services.AddNativeIoC(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
