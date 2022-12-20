namespace MockHttpClientSample.Services
{
    public interface IUtilityService
    {
        Task<bool> IsValidUrl(string url);
    }
}