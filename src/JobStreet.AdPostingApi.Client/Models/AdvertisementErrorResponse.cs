using JobStreet.AdPostingApi.Client.Hal;

namespace JobStreet.AdPostingApi.Client.Models
{
    [MediaType("application/vnd.seekasia.advertisement-error+json;version=1")]
    public class AdvertisementErrorResponse
    {
        public string Message { get; set; }

        public AdvertisementError[] Errors { get; set; }
    }
}