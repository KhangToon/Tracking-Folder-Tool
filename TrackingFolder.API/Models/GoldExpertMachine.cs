namespace TrackingFolder.API.Models
{
    public class GoldExpertMachine
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FolderPath { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public long? DeletedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public long? ModifiedOn { get; set; }

        public List<GExMeasureResult> GXMeasureResults { get; set; } = [];
    }
}
