using System.Reflection;
using System.Text.Json.Serialization;
using Neomaster.R7Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(
  options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  c.IncludeXmlComments(xmlPath);
});

// Подключение клиента.
builder.Services.AddR7Client(builder.Configuration);
builder.Services.AddMemoryCache(); // Буфер временного хранения исходных файлов для скачивания сервером документов.

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
