public class GoogleMapsDistanceMatrixResponse
{
    public List<GoogleMapsDistanceMatrixRow>? rows { get; set; }
}

public class GoogleMapsDistanceMatrixRow
{
    public List<GoogleMapsDistanceMatrixElement>? elements { get; set; }
}

public class GoogleMapsDistanceMatrixElement
{
    public GoogleMapsDistanceMatrixText? distance { get; set; }
    public GoogleMapsDistanceMatrixText? duration { get; set; }
}

public class GoogleMapsDistanceMatrixText
{
    public string? text { get; set; }
}