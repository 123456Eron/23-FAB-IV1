using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GasStationQueue
{
    public partial class MainWindow : Window
    {
        private const int RequestsPerExperiment = 20;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            var simulator = new QueueSystemSimulator(
                arrivalRate: 0.5,   // lambda = 1/2
                serviceRate: 1.0 / 3.0, // mu = 1/3
                maximumSystemSize: 3);

            IList<RequestInfo> requests =
                simulator.RunSimulation(RequestsPerExperiment);

            RequestsGrid.ItemsSource = requests;

            int totalRequests = requests.Count;
            int rejectedCount = requests.Count(r => r.IsRejected);
            int servedCount = totalRequests - rejectedCount;

            double averageWaitingTime = requests
                .Where(r => !r.IsRejected)
                .Select(r => r.ServiceStartTime - r.ArrivalTime)
                .DefaultIfEmpty(0.0)
                .Average();

            double averageSystemTime = requests
                .Where(r => !r.IsRejected)
                .Select(r => r.ServiceEndTime - r.ArrivalTime)
                .DefaultIfEmpty(0.0)
                .Average();

            double totalTime =
                requests.Select(r => r.ServiceEndTime).DefaultIfEmpty(0.0).Max();

            double busyTime = requests
                .Where(r => !r.IsRejected)
                .Sum(r => r.ServiceEndTime - r.ServiceStartTime);

            double utilization = totalTime > 0 ? busyTime / totalTime : 0.0;

            SummaryTextBlock.Text =
                $"Всего заявок: {totalRequests}; " +
                $"обслужено: {servedCount}; " +
                $"отказано: {rejectedCount}; " +
                $"вероятность отказа (по модели): {(double)rejectedCount / totalRequests:0.000}; " +
                $"среднее время ожидания: {averageWaitingTime:0.000}; " +
                $"среднее время в системе: {averageSystemTime:0.000}; " +
                $"коэффициент загрузки колонки: {utilization:0.000}.";
        }
    }
}
