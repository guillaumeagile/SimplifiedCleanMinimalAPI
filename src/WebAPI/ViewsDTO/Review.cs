namespace CleanMinimalApi.WebAPI.ViewsDTO;

public record Review
{
    public Review()
    {
    }

    public Review(int stars)
    {
        this.Stars = stars;
    }

    public int Stars { get; init; }


}
