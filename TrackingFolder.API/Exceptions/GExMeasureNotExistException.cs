namespace TrackingFolder.API.Exceptions
{   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public class GExMeasureNotExistException(Guid id) : Exception($"Not found target goldExpert measure result with Id: {id}") { }
}
