namespace CoordinatorConversorLib.Models.Points
{
    public interface IGeographicPoint
    {
        IGeographicPointValue X { get; set; }

        IGeographicPointValue Y { get; set; }

        object Clone();
    }
}