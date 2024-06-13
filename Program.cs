
using DotnetMongoDbApi.Model;
using DotnetMongoDbApi.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.Configure<ProductStoreDatabaseSettings>
        (builder.Configuration.GetSection("ProductDatabase"));

    builder.Services.AddSingleton<ProcuctService>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();
}


app.Run();