using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Server {
    public class ProgressHub : Hub {
        private readonly ConcurrentDictionary<string, CancellationToken> cancellations = new();
        public async Task StartLongRunningTask() {
            try {
                Random rand = new();
                for (int i = 0; i <= 100; i += 10) {
                    await Task.Delay(200);
                    await Clients.Caller.SendAsync("UpdateProgress", i / 100.0);
                    if (i == 60 && rand.Next(3) == 1) throw new Exception("During long process an exception happens. Try again.");
                }
                string result = "Task compleated! The result: " + Guid.NewGuid().ToString();
                await Clients.Caller.SendAsync("ReceiveResult", result);
            } catch (Exception ex) {
                await Clients.Caller.SendAsync("ReceiveError", ex.Message);
            }
        }

        public async Task LongRunningTaskWithCancellation() {
            var taskId = Guid.NewGuid().ToString();
            await Clients.Caller.SendAsync("TaskId", taskId);
            CancellationTokenSource cts = new();
            cancellations.TryAdd(taskId, cts.Token);
            for (int i = 0; i <= 100; i += 10) {
                cts.Token.ThrowIfCancellationRequested();
                await Task.Delay(500, cts.Token);
                await Clients.Caller.SendAsync("UpdateProgress", i / 100.0, cts.Token);
            }
            string result = "Task compleated! The result: " + Guid.NewGuid().ToString();
            await Clients.Caller.SendAsync("ReceiveResult", result, cts.Token);
        }
    }
}