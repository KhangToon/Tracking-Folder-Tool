namespace TrackingFolder.API.Contracts.Requests
{
    public record UpdateGoldExpertMeasureResult(
        DateTime Date,
        TimeSpan Time,
        double Reading,
        TimeSpan ElapsedTimeTotal,
        double Ni,
        double NiPlusMinus,
        double Cu,
        double CuPlusMinus,
        double Zn,
        double ZnPlusMinus,
        double Ag,
        double AgPlusMinus,
        double Au,
        double AuPlusMinus,
        bool PassFail,
        TimeSpan LiveTimeTotal);
}
