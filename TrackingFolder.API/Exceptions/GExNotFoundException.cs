namespace TrackingFolder.API.Exceptions
{
    public class GExNotFoundException : Exception
    {
        public GExNotFoundException() : base("Not found any measure result") { }
    }
}
