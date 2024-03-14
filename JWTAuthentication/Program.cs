using JWTAuthentication.Common;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddTransient<IUserAuthService, UserAuthService>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Adding Authorize button to work with JWT in Swagger
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//  adding authentication schema for authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(optionts => 
    {
        optionts.TokenValidationParameters = new TokenValidationParameters
        {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
           ValidateIssuer = false,
           ValidateAudience = false
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
