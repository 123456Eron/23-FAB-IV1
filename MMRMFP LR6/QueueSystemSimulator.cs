namespace GasStationQueue
{
    public class QueueSystemSimulator
    {
        private readonly double _arrivalRate;
        private readonly double _serviceRate;
        private readonly int _maximumSystemSize;
        private readonly Random _random;

        public QueueSystemSimulator(double arrivalRate, double serviceRate, int maximumSystemSize)
        {
            _arrivalRate = arrivalRate;
            _serviceRate = serviceRate;
            _maximumSystemSize = maximumSystemSize;
            _random = new Random();
        }

        public IList<RequestInfo> RunSimulation(int numberOfArrivals)
        {
            var requests = new List<RequestInfo>();

            double currentTime = 0.0;
            double nextFreeTime = 0.0;
            var queue = new Queue<RequestInfo>();

            for (int i = 1; i <= numberOfArrivals; i++)
            {
                double interArrivalTime = GenerateExponential(_arrivalRate);
                currentTime += interArrivalTime;

                var request = new RequestInfo
                {
                    Id = i,
                    ArrivalTime = currentTime
                };

                int systemSize = queue.Count;
                if (nextFreeTime > currentTime)
                {
                    systemSize += 1;
                }

                if (systemSize >= _maximumSystemSize)
                {
                    request.IsRejected = true;
                    request.ServiceStartTime = request.ArrivalTime;
                    request.ServiceEndTime = request.ArrivalTime;
                    requests.Add(request);
                    continue;
                }

                if (nextFreeTime <= currentTime)
                {
                    request.ServiceStartTime = currentTime;
                    double serviceTime = GenerateExponential(_serviceRate);
                    request.ServiceEndTime = request.ServiceStartTime + serviceTime;
                    nextFreeTime = request.ServiceEndTime;
                }
                else
                {
                    queue.Enqueue(request);
                }

                requests.Add(request);

                while (queue.Count > 0 && nextFreeTime <= currentTime)
                {
                    var nextRequest = queue.Dequeue();
                    nextRequest.ServiceStartTime = nextFreeTime;
                    double serviceTime = GenerateExponential(_serviceRate);
                    nextRequest.ServiceEndTime = nextRequest.ServiceStartTime + serviceTime;
                    nextFreeTime = nextRequest.ServiceEndTime;
                }
            }

            while (queue.Count > 0)
            {
                var nextRequest = queue.Dequeue();
                if (nextRequest.ServiceStartTime <= 0.0)
                {
                    nextRequest.ServiceStartTime = nextFreeTime;
                }

                double serviceTime = GenerateExponential(_serviceRate);
                nextRequest.ServiceEndTime = nextRequest.ServiceStartTime + serviceTime;
                nextFreeTime = nextRequest.ServiceEndTime;
            }

            return requests;
        }

        private double GenerateExponential(double rate)
        {
            double uniform = _random.NextDouble();
            return -Math.Log(1.0 - uniform) / rate;
        }
    }
}
