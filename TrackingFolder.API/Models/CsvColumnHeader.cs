namespace TrackingFolder.API.Models
{
    public class CsvColumnHeader
    {
        public Guid Id { get; set; }
        public string GExSerial { get; set; } = "Default";
        public string HeaderName { get; set; } = string.Empty;
    }
}
