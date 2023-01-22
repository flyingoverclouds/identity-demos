using System.Text;

namespace ApiBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

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


            app.MapGet("/EnvVariables", (HttpContext httpContext) =>
            {
                StringBuilder sb = new StringBuilder();
                foreach (var evname in Environment.GetEnvironmentVariables().Keys)
                {
                    var evValue = Environment.GetEnvironmentVariable(evname.ToString());
                    if (evValue.EndsWith("==") || evname.ToString().ToLower().Contains("key"))
                        evValue = evValue.Substring(0, Math.Min(evValue.Length, 5)) + "**REDACTED**";
                    sb.AppendLine($"{evname.ToString()}={evValue}");
                }
                return sb.ToString();
            }).WithName("GetEnvironmentVariables");


        //    var summaries = new[]
        //    {
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //    app.MapGet("/weatherforecast", (HttpContext httpContext) =>
        //    {
        //        var forecast = Enumerable.Range(1, 5).Select(index =>
        //            new WeatherForecast
        //            {
        //                Date = DateTime.Now.AddDays(index),
        //                TemperatureC = Random.Shared.Next(-20, 55),
        //                Summary = summaries[Random.Shared.Next(summaries.Length)]
        //            })
        //            .ToArray();
        //        return forecast;
        //    })
        //    .WithName("GetWeatherForecast");

            app.Run();
        }
    }
}