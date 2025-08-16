using Microsoft.AspNetCore.SignalR;

namespace Server {
    internal class Program {
        private static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR(options => options.AddFilter<LoggingHubFilter>());
            var app = builder.Build();
            app.MapHub<ProgressHub>("/progressHub");

            app.MapGet("/", () => "Hello World!");
            app.MapGet("/api/get-string", async () => {
                await Task.Delay(5000);
                Random rand = new();
                if (rand.Next(3) == 1) {
                    throw new Exception("Error on server");
                } else {
                    return "String from server";
                }
            });

            app.Run();
        }
    }
}