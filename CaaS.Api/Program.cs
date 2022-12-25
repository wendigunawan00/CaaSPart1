//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



using CaaS.Dal.Ado;
using CaaS.Domain;
using CaaS.Logic;
//using CaaS.Api.BackgroundServices;
using Dal.Common;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddScoped<IOrderManagementLogic<Person>>(_ =>  
    new OrderManagementLogic<Person>(new AdoPersonDao(DefaultConnectionFactory.FromConfiguration(ConfigurationUtil.GetConfiguration(), "CaaSDbConnection")), "CustomersShop1")
);
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
app.UseOpenApi();
app.UseSwaggerUi3(settings => settings.Path = "/swagger");
app.UseReDoc(settings => settings.Path = "/redoc");
app.UseCors();
app.UseAuthorization();


// Configure Endpoints
app.MapControllers();


app.Run();

