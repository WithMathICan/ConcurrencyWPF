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
            TextBoxFromServer.Text = "Result";
            using var client = new HttpClient();
            _cts = new CancellationTokenSource();
            try {
                ProgressText.Text = "Sending request ...";
                FromServerProgressBar.Value = 10;
                var response = await client.GetStringAsync("http://localhost:5000/api/get-string", _cts.Token);
                TextBoxFromServer.Text = response;
                ProgressText.Text = "Responce obtained";
                FromServerProgressBar.Value = 100;
            } catch (OperationCanceledException) {
                ProgressText.Text = "Operation was cancelled.";
                FromServerProgressBar.Value = 0;
            } catch (Exception) {
                FromServerProgressBar.Value = 0;
                ProgressText.Text = "An error occurred";
            }
        }
    }
}