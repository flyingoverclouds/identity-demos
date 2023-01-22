using System;
using System.Text;
using System.Xml.Linq;

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

            #region API method to return all environment  variables
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
                return $"##TIMESTAMP: {DateTime.Now.ToString()}##\n" + sb.ToString();
            }).WithName("GetEnvVariables");
            #endregion

            #region API method to return all headers available in httpcontext
            app.MapGet("/Headers", (HttpContext httpContext) =>
            {
                StringBuilder sb = new StringBuilder();
                foreach (var hname in httpContext.Request.Headers.Keys) {
                    sb.AppendLine($"{hname} = {httpContext.Request.Headers[hname]}");
                }
                return $"##TIMESTAMP: {DateTime.Now.ToString()}##\n" + sb.ToString();
            }).WithName("GetHeaders");
            #endregion


            #region API Method to return current identity informations
            app.MapGet("/IdentityInfo", (HttpContext httpContext) =>
            {
                return $"##TIMESTAMP: {DateTime.Now.ToString()}##\n" + "NOT IMPLEMENTED in ApiBackend";
            }).WithName("GetIdentityInfo");
            #endregion

            app.Run();
        }
    }
}