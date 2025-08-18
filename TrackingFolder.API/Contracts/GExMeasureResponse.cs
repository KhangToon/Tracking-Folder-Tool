namespace TrackingFolder.API.Contracts
{
    public record GExMeasureResponse
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Reading { get; set; }
        public TimeSpan ElapsedTimeTotal { get; set; }
        public double Ni { get; set; }
        public double NiPlusMinus { get; set; }
        public double Cu { get; set; }
        public double CuPlusMinus { get; set; }
        public double Zn { get; set; }
        public double ZnPlusMinus { get; set; }
        public double Ag { get; set; }
        public double AgPlusMinus { get; set; }
        public double Au { get; set; }
        public double AuPlusMinus { get; set; }
        public bool PassFail { get; set; }
        public TimeSpan LiveTimeTotal { get; set; }
    }
}
