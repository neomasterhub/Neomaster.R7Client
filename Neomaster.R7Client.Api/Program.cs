using System.Text.Json.Serialization;
using Neomaster.R7Client.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(
  options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddR7Client(builder.Configuration);
builder.Services.AddMemoryCache();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
