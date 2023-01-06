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
    new ManagementLogic<Product>(new AdoProductDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "ProductShop1")
);
builder.Services.AddScoped<IManagementLogic<Person>>(_ =>  
    new ManagementLogic<Person>(new AdoPersonDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "CustomersShop1")
);

builder.Services.AddScoped<IAnalytic>(_ =>
    new StatsAnalytic(
        new ManagementLogic<Cart>(new AdoCartDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "CartsShop1"),
        new ManagementLogic<Order>(new AdoOrderDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "OrdersShop1"),
        new ManagementLogic<Person>(new AdoPersonDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "CustomersShop1"),
        new ManagementLogic<Product>(new AdoProductDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "ProductShop1"),
        new ManagementLogic<CartDetails>(new AdoCartDetailsDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "CartsDetailsShop1"),
        new ManagementLogic<OrderDetails>(new AdoOrderDetailsDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "OrdersDetailsShop1")
));
builder.Services.AddScoped<IAuth>(_ =>
    new Auth(
        new ManagementLogic<Shop>(new AdoShopDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "Shops"),
        new ManagementLogic<Person>(new AdoPersonDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "Mandants"),
        new ManagementLogic<AppKey>(new AdoAppKeyDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "AppKeys")
        
));

builder.Services.AddScoped<IOrderManagementLogic>(_ =>
    new OrderManagementLogic(
        new ManagementLogic<Cart>(new AdoCartDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "CartsShop1"),
        new ManagementLogic<Order>(new AdoOrderDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "OrdersShop1"),
        new ManagementLogic<Product>(new AdoProductDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "ProductShop1") ,  
        new ManagementLogic<CartDetails>(new AdoCartDetailsDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "CartsDetailsShop1" ),
        new ManagementLogic<OrderDetails>(new AdoOrderDetailsDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "OrdersDetailsShop1")
));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(
    builder => builder.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddOpenApiDocument(
    settings => settings.PostProcess = doc => doc.Info.Title = "CaaS");
//builder.Services.AddHostedService<QueuedUpdateService>();
//builder.Services.AddSingleton<UpdateChannel>();

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

