using Xunit;

namespace JobStreet.AdPostingApi.Client.Tests.Framework
{
    [CollectionDefinition(Name)]
    public class AdPostingApiCollection : ICollectionFixture<AdPostingApiPactService>
    {
        public const string Name = "Ad Posting API Service Collection";
    }
}