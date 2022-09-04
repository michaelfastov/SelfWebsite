using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Services.Implementations;
using SelfWebsiteApi.Services.Implementations.Auth;
using SelfWebsiteApi.Services.Implementations.ResumeServices;
using SelfWebsiteApi.Services.Interfaces;
using SelfWebsiteApi.Services.Interfaces.Auth;
using SelfWebsiteApi.Services.Interfaces.ResumeServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var origin = builder.Configuration.GetValue<string>("SelfWebsiteAngular:Name");
var angularLink = builder.Configuration.GetValue<string>("SelfWebsiteAngular:Link");

builder.Services.AddCors(options =>
{
    options.AddPolicy(origin, builder =>
    {
        builder.WithOrigins(angularLink)
        //.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SelfWebsiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SelfWebsiteDatabase")));
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Settings:ServerLink"),
            ValidAudience = builder.Configuration.GetValue<string>("Settings:ServerLink"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration.GetValue<string>("Settings:Authorization:SymmetricSecurityKey"))),
        };
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IResumeService, ResumeService>();
builder.Services.AddTransient<ILinkService, LinkService>();
builder.Services.AddTransient<ISectionService, SectionService>();
builder.Services.AddSingleton<IMapperProvider, MapperProvider>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(origin);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
