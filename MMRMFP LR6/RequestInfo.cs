namespace GasStationQueue
{
    public class RequestInfo
    {
        double arrivalTime, serviceStartTime, serviceEndTime;
        public int Id { get; set; }

        public double ArrivalTime
        { 
            get => arrivalTime;
            set 
            {
                arrivalTime = Math.Round(value, 1);
            }
        }

        public double ServiceStartTime
        {
            get => serviceStartTime;
            set
            {
                serviceStartTime = Math.Round(value, 1);
            }
        }

        public double ServiceEndTime
        {
            get => serviceEndTime;
            set
            {
                serviceEndTime = Math.Round(value, 1);
            }
        }

        public bool IsRejected { get; set; }
    }
}
