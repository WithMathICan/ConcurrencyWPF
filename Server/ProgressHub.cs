using Microsoft.AspNetCore.SignalR;

namespace Server {
    public class ProgressHub : Hub {
        public async Task StartLongRunningTask() {
            Console.WriteLine("Operation started");
            try {
                for (int i = 0; i <= 100; i += 10) {
                    await Task.Delay(500);
                    await Clients.Caller.SendAsync("UpdateProgress", i / 100.0);
                }
                string result = "Task compleated! The result: " + Guid.NewGuid().ToString();
                await Clients.Caller.SendAsync("ReceiveResult", result);
            } catch (Exception ex) {
                Console.WriteLine($"Ошибка в хабе: {ex.Message}");
                throw; 
            }
        }
    }
}