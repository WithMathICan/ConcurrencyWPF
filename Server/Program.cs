var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
