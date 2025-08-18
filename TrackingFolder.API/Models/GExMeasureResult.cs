namespace TrackingFolder.API.Models
{
    public class GExMeasureResult(DateTime date, TimeSpan time, double reading, TimeSpan elapsedTimeTotal,
                           double ni, double niPlusMinus, double cu, double cuPlusMinus,
                           double zn, double znPlusMinus, double ag, double agPlusMinus,
                           double au, double auPlusMinus, bool passFail, TimeSpan liveTimeTotal)
    {
        public Guid Id { get; set; }
        public Guid GExMachineId { get; set; }
        public DateTime Date { get; set; } = date;
        public TimeSpan Time { get; set; } = time;
        public double Reading { get; set; } = reading;
        public TimeSpan ElapsedTimeTotal { get; set; } = elapsedTimeTotal;
        public double Ni { get; set; } = ni;
        public double NiPlusMinus { get; set; } = niPlusMinus;
        public double Cu { get; set; } = cu;
        public double CuPlusMinus { get; set; } = cuPlusMinus;
        public double Zn { get; set; } = zn;
        public double ZnPlusMinus { get; set; } = znPlusMinus;
        public double Ag { get; set; } = ag;
        public double AgPlusMinus { get; set; } = agPlusMinus;
        public double Au { get; set; } = au;
        public double AuPlusMinus { get; set; } = auPlusMinus;
        public bool PassFail { get; set; } = passFail;
        public TimeSpan LiveTimeTotal { get; set; } = liveTimeTotal;

        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public long? DeletedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedOn { get; set; }
    }
}
