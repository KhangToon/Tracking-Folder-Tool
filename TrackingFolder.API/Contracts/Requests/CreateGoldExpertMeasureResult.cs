namespace TrackingFolder.API.Contracts.Requests
{   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Date"></param>
    /// <param name="Time"></param>
    /// <param name="Reading"></param>
    /// <param name="ElapsedTimeTotal"></param>
    /// <param name="Ni"></param>
    /// <param name="NiPlusMinus"></param>
    /// <param name="Cu"></param>
    /// <param name="CuPlusMinus"></param>
    /// <param name="Zn"></param>
    /// <param name="ZnPlusMinus"></param>
    /// <param name="Ag"></param>
    /// <param name="AgPlusMinus"></param>
    /// <param name="Au"></param>
    /// <param name="AuPlusMinus"></param>
    /// <param name="PassFail"></param>
    /// <param name="LiveTimeTotal"></param>
    public record CreateGoldExpertMeasureResult(
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
