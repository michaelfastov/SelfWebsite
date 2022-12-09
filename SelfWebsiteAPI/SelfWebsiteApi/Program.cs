using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using NuGet.ProjectModel;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Middlewares;
using SelfWebsiteApi.Models.GraphQL;
using SelfWebsiteApi.Services.Implementations;
using SelfWebsiteApi.Services.Implementations.Auth;
using SelfWebsiteApi.Services.Implementations.Elastic;
using SelfWebsiteApi.Services.Implementations.EntityFramework;
using SelfWebsiteApi.Services.Implementations.Mongo;
using SelfWebsiteApi.Services.Interfaces;
using SelfWebsiteApi.Services.Interfaces.Auth;
using SelfWebsiteApi.Services.Interfaces.Elastic;
using SelfWebsiteApi.Services.Interfaces.EntityFramework;
using SelfWebsiteApi.Services.Interfaces.Mongo;
using System.Net;
using System.Text;
using Telegram.Bot;
using TelegramBot.PixivLinksBot;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var origin = builder.Configuration.GetValue<string>("SelfWebsiteAngular:Name");
var azueAngularLink = builder.Configuration.GetValue<string>("SelfWebsiteAngular:AzureAngularLink");
var domainLink = builder.Configuration.GetValue<string>("SelfWebsiteAngular:DomainLink");

builder.Services.Configure<ElasticSettings>(
    builder.Configuration.GetSection("ElasticSettings"));
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<EntityFrameworkSettings>(
    builder.Configuration.GetSection("EntityFrameworkSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(origin, builder =>
    {
        builder.WithOrigins(azueAngularLink, domainLink)
        //.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();//Important for SignalR
    });
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

//GraphQL setup
builder.Services
    .AddTransient<Query>()
    .AddGraphQLServer()
    .AddQueryType<Query>();

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


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
builder.Services.AddSingleton<IElasticClientProvider, ElasticClientProvider>();
builder.Services.AddTransient<IElasticResumeService, ElasticResumeService>();
builder.Services.AddTransient<IEfResumeService, EfResumeService>();
builder.Services.AddTransient<IEfLinkService, EfLinkService>();
builder.Services.AddTransient<IEfSectionService, EfSectionService>();
builder.Services.AddSingleton<IMongoCollectionProvider, MongoCollectionProvider>();
builder.Services.AddTransient<IMongoResumeService, MongoResumeService>();
builder.Services.AddTransient<IResumeService, ResumeService>();

builder.Services.AddSingleton<IMapperProvider, MapperProvider>();

//TelegramBot
if (builder.Configuration.GetValue<bool>("TelegramBots:IsActive"))
{
    builder.Services.AddHostedService<PixivLinksBotWebhookService>();
    builder.Services.AddScoped<PixivLinksBotService>();
    builder.Services.AddHttpClient("PixivLinksBotClient")
        .AddTypedClient<ITelegramBotClient>(
        httpClient => new TelegramBotClient(builder.Configuration.GetValue<string>("TelegramBots:PixivLinksBotToken"), httpClient));
}
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
IdentityModelEventSource.ShowPII = true;
app.UseSwagger();
app.UseSwaggerUI();
//}

app.MapGraphQL();

app.UseExceptionHandler(
             options =>
             {
                 options.Run(
                     async context =>
                     {
                         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                         context.Response.ContentType = "text/html";
                         var ex = context.Features.Get<IExceptionHandlerFeature>();
                         if (ex != null)
                         {
                             var err = $"Error: {ex.Error.Message}\nStackTrace: {ex.Error.StackTrace}";
                             await context.Response.WriteAsync(err).ConfigureAwait(false);
                         }
                     });
             }
         );


app.UseHttpsRedirection();
app.UseRequestCulture();

app.UseRouting();
app.UseCors(origin);
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    var token = builder.Configuration.GetValue<string>("TelegramBots:PixivLinksBotToken");
    endpoints.MapControllerRoute(
        name: "PixivLinksBotWebhook",
        pattern: $"{token}",
        new { controller = "PixivLinksBot", action = "Post" });
    endpoints.MapHub<PixivLinksHub>("/pixivLinks");
    endpoints.MapControllers();
});

app.Run();
