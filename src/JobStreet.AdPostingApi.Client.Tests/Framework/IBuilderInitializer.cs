using JobStreet.AdPostingApi.Client.Models;

namespace JobStreet.AdPostingApi.Client.Tests.Framework
{
    public interface IBuilderInitializer
    {
        void Initialize(AdvertisementContentBuilder builder);

        void Initialize<TAdvertisement>(AdvertisementModelBuilder<TAdvertisement> builder) where TAdvertisement : Advertisement, new();
    }
}