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

        private async Task DoWorkAsync(IProgress<int> progress, CancellationToken cancellationToken) {
            for (int i = 0; i <= 100; i += 10) {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(500, cancellationToken); // Имитация работы
                progress.Report(i); // Сообщаем о прогрессе
            }
        }
    }
}