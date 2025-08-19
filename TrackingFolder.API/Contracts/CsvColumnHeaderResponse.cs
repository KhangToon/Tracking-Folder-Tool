namespace TrackingFolder.API.Contracts
{
    public class CsvColumnHeaderResponse
    {
        public Guid Id { get; set; }
        public string GExSerial { get; set; } = string.Empty;
        public string HeaderName { get; set; } = string.Empty;
    }
}
