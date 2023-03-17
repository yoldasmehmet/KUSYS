using KUSYS.Common.Entities;
using Library.Common.Entities;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<KUSYSDBContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Transient);
builder.Services.AddSwaggerGen();
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.UseMigration<Program>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app=builder.UseJwtTokenAuthorization(); //SSO için yapılması gereken iki şeyden ilki, diğeri application.json içerisinde SSO servisinin linkinin yazılmasıdır.

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
app.MapControllers();
app.UseLogger();
app.Run();
