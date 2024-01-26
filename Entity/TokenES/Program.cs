using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReyBanPac.ModeloCanonico.Type;
using ReyBanPac.ModeloCanonico.Utils;
using ReyBanPac.TokenES.Constans;
using ReyBanPac.TokenES.Controllers.Contract;
using ReyBanPac.TokenES.Controllers.Impl;
using ReyBanPac.TokenES.Repository.Context;
using ReyBanPac.TokenES.Repository.Contract;
using ReyBanPac.TokenES.Repository.Impl;
using ReyBanPac.TokenES.Service.Contract;
using ReyBanPac.TokenES.Service.Impl;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

#region Consumer Key DB
var Host = builder.Configuration.GetSection("Parametros:HostApi").Value;
var Path = builder.Configuration.GetSection("ApiUrls:CONSULTAR_DB").Value;

var Url = string.Concat(Host, Path);

#if DEBUG
Url = "https://localhost:7234/api/us/integracionlegado";
#endif

CredencialType CredentialType = new CredencialType();// { User="plopdpd_usdb", Pass="TTgyNipqcCRaOGEl"};

try
{
    using (var client = new HttpClient(Utils.OffSSLClient()))
    {
        var Response = await client.GetAsync(Url);
        Response.EnsureSuccessStatusCode();
        var Json = await Response.Content.ReadAsStringAsync();
        //Mapping
        CredentialType = JsonSerializer.Deserialize<CredencialType>(Json);
        //Valida
        if (CredentialType == null)
        {
            throw new ArgumentException("Error al obtener la credencial");
        }
    }
}

catch (Exception)
{
    string logFilePath = "app.log";

    // Configura el StreamWriter para escribir en el archivo de registro.
    using (StreamWriter streamWriter = new StreamWriter(logFilePath, append: true))
    {
        // Registra un mensaje en el archivo de registro.
        LogToFile(streamWriter, "Inicio de la aplicación.");

        // Aquí puedes realizar otras operaciones y registrar más mensajes según sea necesario.

        // Registra otro mensaje.
        LogToFile(streamWriter, "Fin de la aplicación.");
    }
    throw new ArgumentException("Error al obtener la credencial");
}

var Connection = builder.Configuration.GetConnectionString("SQLConnection");
if (!string.IsNullOrEmpty(Connection))
{
    Connection = Connection.Replace("{User}", CredentialType.User)
        .Replace("{Pass}", Utils.Base64Decode(CredentialType.Pass));
}

builder.Services.AddDbContext<Db>(options => options.UseSqlServer(Connection));
#endregion

#region Cors

// Configurar la politica CORS con la configuracion cargada
builder.Services.AddCors(options =>
{
    options.AddPolicy("NUXT", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
#endregion

#region Autenticacion
IConfigurationSection AU = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AU["Issuer"],

            ValidateAudience = true,
            ValidAudience = AU["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AU["Secret"]))
        };
    });
#endregion


#region Logger File

var DirLogs = builder.Configuration.GetSection("Parametros:DirLogs").Value;
builder.Logging.AddFile($"{DirLogs}Logs_{General.Nombre_Servicio}{General.Tipo_Servicio}-{{Date}}.txt");

#endregion
// Add services to the container.
builder.Services.AddScoped<IController, ControllerImpl>();
builder.Services.AddScoped<IService, ServiceImpl>();
builder.Services.AddScoped<IRepository, RepositoryImpl>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = General.Nombre_Servicio + "-" + General.Tipo_Servicio, Version = "v1" });

    // Configura la autenticación
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Token de autorización en el formato 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }
});

});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", General.Nombre_Servicio + "-" + General.Tipo_Servicio + " v1");
});

app.UseCors("NUXT");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void LogToFile(StreamWriter streamWriter, string message)
{
    // Obtén la marca de tiempo actual.
    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    // Construye el mensaje de registro con la marca de tiempo.
    string logMessage = $"{timestamp} - {message}";

    // Escribe el mensaje en el archivo de registro.
    streamWriter.WriteLine(logMessage);

    // Asegúrate de que el mensaje se guarde en el archivo inmediatamente.
    streamWriter.Flush();
}
