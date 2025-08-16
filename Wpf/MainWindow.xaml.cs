using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace Wpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private CancellationTokenSource? _cts;

        public MainWindow() {
            InitializeComponent();
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e) {
            _cts = new CancellationTokenSource();
            var progress = new Progress<int>(value => ProgressBar.Value = value);
            try {
                await DoWorkAsync(progress, _cts.Token);
            } catch (OperationCanceledException) {
                MessageBox.Show("Operation was cancelled.");
                ProgressBar.Value = 0;
            } catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            } finally {
                _cts.Dispose();
                _cts = null;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            _cts?.Cancel();
        }

        private static async Task DoWorkAsync(IProgress<int> progress, CancellationToken cancellationToken) {
            for (int i = 0; i <= 100; i += 10) {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(500, cancellationToken); 
                progress.Report(i);
            }
        }

        private async void LoadFromServerButton_Click(object sender, RoutedEventArgs e) {
            var progress = new Progress<int>(value => FromServerProgressBar.Value = value);
            var progressText = new Progress<string>(value => ProgressText.Text = value);
            _cts = new CancellationTokenSource();
            try {
                var response = await LoadString(progress, progressText, _cts.Token);
                TextBoxFromServer.Text = response;
            } catch (OperationCanceledException) {
                ProgressText.Text = "Operation was cancelled.";
                FromServerProgressBar.Value = 0;
            } catch (Exception) {
                FromServerProgressBar.Value = 0;
                ProgressText.Text = "An error occurred";
            } finally {
                _cts.Dispose();
                _cts = null;
            }
        }

        private static async Task<string> LoadString(IProgress<int> progress, IProgress<string> progressText, CancellationToken cancellationToken) {
            using var client = new HttpClient();
            progress.Report(10);
            progressText.Report("Sending request ...");
            var response = await client.GetStringAsync("http://localhost:5000/api/get-string", cancellationToken);
            progress.Report(100);
            progressText.Report("Successfully finished");
            return response;
        }

        private async void LoadButtonServerProgress_Click(object sender, RoutedEventArgs e) {
            IProgress<double> progress = new Progress<double>(value => {
                ProgressBarServer.Value = value*100;
                ProgressTextServer.Text = $"Progress: {(value * 100):F1}%";
            });
            IProgress<string> stringResult = new Progress<string>(message => TextBoxFromServerProgress.Text = message);

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/progressHub") 
                .Build();

            connection.On<double>("UpdateProgress", value => progress.Report(value));
            connection.On<string>("ReceiveResult", message => stringResult.Report(message));

            try {
                await connection.StartAsync();
                await connection.InvokeAsync("StartLongRunningTask");
            } catch (OperationCanceledException) {
                ProgressTextServer.Text = "Operation cancelled";
            } catch (Exception ex) {
                ProgressText.Text = "An error occurred";
            }
            ProgressBarServer.Value = 0;
        }
    }
}