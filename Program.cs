using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

using airtext_api.Data;
using airtext_api.Filters;
using airtext_api.Models;

using airtext_api.Repository.UserRepository;
using airtext_api.Repository.AuthRepository;
using airtext_api.Repository.CountryRepository;
using airtext_api.Repository.CompanyRepository;
using airtext_api.Repository.RoleRepository;
using airtext_api.Repository.ContractRepository;


using airtext_api.Service.UserService;
using airtext_api.Service.AuthService;
using airtext_api.Service.CountryService;
using airtext_api.Service.CompanyService;
using airtext_api.Service.RoleService;
using airtext_api.Service.ContractService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
        policy => {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Some Description",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( options => {
    options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidIssuer = "bruno.com",
        ValidAudience = "bruno.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwfhgfhjsgdvjhdsg837483hf8743tfg8734gfyegf7634gf38734"))
    };
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IContractService, ContractService>();



builder.Services.AddScoped<RequireActiveAuthFilter>();

var app = builder.Build();
app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("MyPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
