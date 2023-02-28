using CaaS.Dal.Ado;
using CaaS.Domain;
using CaaS.Features;
using CaaS.Logic;
using Dal.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(option => {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = ConfigurationUtil.GetConfiguration()["JWT:ValidAudience"],
            ValidIssuer = ConfigurationUtil.GetConfiguration()["JWT:ValidIssuer"],
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationUtil.GetConfiguration()["JWT:Key"]))
            };
        });

builder.Services.AddMvc();
builder.Services.AddScoped<IManagementLogic<Product>>(_ =>
    new ManagementLogic<Product>(new AdoProductDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "Products")
);

builder.Services.AddScoped<IManagementLogic<Person>>(_ =>
    new ManagementLogic<Person>(new AdoPersonDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "Customers")
);
builder.Services.AddScoped<IManagementLogic<Address>>(_ =>    
    new ManagementLogic<Address>(new AdoAddressDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "Addresses")
);

builder.Services.AddScoped<IAnalytic>(_ =>
    new StatsAnalytic(
        DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")));
builder.Services.AddScoped<IAuth>(_ =>
    new Auth(
        new AdoPersonDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new AdoAppKeyDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new string[] {"Mandants","AppKeys"} ) 
);

builder.Services.AddScoped<IOrderManagementLogic>(_ =>
    new OrderManagementLogic(
        new AdoCartDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new AdoOrderDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new AdoProductDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new AdoCartDetailsDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new AdoOrderDetailsDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new AdoPersonDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")),
        new string[] { "Carts", "Orders", "Products", "CartsDetails", "OrdersDetails","Customers" }
        ));


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(
    builder => builder.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddOpenApiDocument(
    settings => settings.PostProcess = doc => doc.Info.Title = "CaaS");


var app = builder.Build();


// Configure Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseOpenApi();
app.UseSwaggerUi3(settings => settings.Path = "/swagger");
app.UseReDoc(settings => settings.Path = "/redoc");
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


// Configure Endpoints
app.MapControllers();


app.Run();

