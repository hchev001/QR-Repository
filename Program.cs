using InventoryManagement.Data;
using InventoryManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryApiDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var db_pass = builder.Configuration["DB_CONNECTION_PASS"];
    options.UseNpgsql($"{connectionString}{db_pass};");
});

builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        if (builder.Environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
        }
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params) =>
            {
                return expires == null || expires > DateTime.UtcNow;
            },
            ValidAudience = "your domain or site",
            ValidIssuer = "your domain or site",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your secret code"))
        };
    });

// Add services to the container.
builder.Services.AddTransient<IAssetService, AssetService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICollectionService, CollectionService>();

builder.Services.AddControllers();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
