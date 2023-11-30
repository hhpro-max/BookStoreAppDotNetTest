using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//logging the http,s requests 
builder.Host.UseSerilog((ctx,lc)=>lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
//handle cors
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll",b=>b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//use cors here and give the name of the policy we made
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
