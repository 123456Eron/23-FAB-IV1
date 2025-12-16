namespace GasStationQueue
{
    public class RequestInfo
    {
        public int Id { get; set; }

        public double ArrivalTime { get; set; }

        public double ServiceStartTime { get; set; }

        public double ServiceEndTime { get; set; }

        public bool IsRejected { get; set; }
    }
}
