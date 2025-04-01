using API_Sample.Application.Mapper;
using API_Sample.Application.Services;
using API_Sample.Application.Ultilities;
using API_Sample.Data.EF;
using API_Sample.WebApi.Middlewares;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

static void InitUtilitiesService(IServiceCollection services)
{
    services.AddScoped<ISendMailSMTP, SendMailSMTP>();
}

static void InitDaoService(IServiceCollection services)
{
    //services.AddScoped<IS_Image, S_Image>();
    //services.AddScoped<IS_Product, S_Product>();
    services.AddScoped<IS_Account, S_Account>();
    services.AddScoped<IS_Post, S_Post>();
    services.AddScoped<IS_FieldOfActivity, S_FieldOfActivity>();
}

// Add services to the container.

builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnectString")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

InitUtilitiesService(builder.Services);
InitDaoService(builder.Services);



builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.OperationFilter<API.H2ADBSite.Portal.Variables.AddAuthorizationHeaderOperationHeader>();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Sample.WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[]{}
        }
    });
});

//Config IpRateLimit https://github.com/stefanprodan/AspNetCoreRateLimit/wiki/IpRateLimitMiddleware
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var path = Directory.GetCurrentDirectory();
builder.Logging.AddFile($"{path}\\Logs\\Logs.txt");

// jwt
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            // tự cấp token
            ValidateIssuer = false,
            ValidateAudience = false,

            // ký vào token
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin() // Cho phép tất cả các origin (frontend)
                        .AllowAnyMethod() // Cho phép tất cả các phương thức (GET, POST, PUT, DELETE)
                        .AllowAnyHeader()); // Cho phép tất cả header
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelExpandDepth(2);
        c.DefaultModelRendering(ModelRendering.Model);
        c.DefaultModelsExpandDepth(-1);
        c.DisplayOperationId();
        c.DisplayRequestDuration();
        c.DocExpansion(DocExpansion.None);
        c.EnableDeepLinking();
        c.EnableFilter();
        //c.MaxDisplayedTags(5);
        c.ShowExtensions();
        c.ShowCommonExtensions();
        c.EnableValidator();
        //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);
        c.UseRequestInterceptor("(request) => { return request; }");
    });
}
else
{
    app.UseHsts();
    if (builder.Configuration.GetValue<bool>("Swagger:Active"))
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelExpandDepth(2);
            c.DefaultModelRendering(ModelRendering.Model);
            c.DefaultModelsExpandDepth(-1);
            c.DisplayOperationId();
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
            c.EnableFilter();
            //c.MaxDisplayedTags(5);
            c.ShowExtensions();
            c.ShowCommonExtensions();
            c.EnableValidator();
            //c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Head);
            c.UseRequestInterceptor("(request) => { return request; }");
        });
    }
}

app.UseCors("AllowAllOrigins");

app.UseIpRateLimiting(); //Apply IpRateLimit in middleware

app.UseMiddleware<SecurityHeadersMiddleware>();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
