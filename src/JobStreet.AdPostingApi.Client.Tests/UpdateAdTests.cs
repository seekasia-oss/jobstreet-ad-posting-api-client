using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using PactNet.Mocks.MockHttpService.Models;
using JobStreet.AdPostingApi.Client.Models;
using JobStreet.AdPostingApi.Client.Resources;
using JobStreet.AdPostingApi.Client.Tests.Framework;
using Xunit;

namespace JobStreet.AdPostingApi.Client.Tests
{
    [Collection(AdPostingApiCollection.Name)]
    public class UpdateAdTests : IDisposable
    {
        private const string AdvertisementLink = "/advertisement";
        private const string AdvertisementId = "8e2fde50-bc5f-4a12-9cfb-812e50500184";
        private const string RequestId = "PactRequestId";

        private IBuilderInitializer MinimumFieldsInitializer => new MinimumFieldsInitializer();

        private IBuilderInitializer AllFieldsInitializer => new AllFieldsInitializer();

        public UpdateAdTests(AdPostingApiPactService adPostingApiPactService)
        {
            this.Fixture = new AdPostingApiFixture(adPostingApiPactService);
        }

        public void Dispose()
        {
            this.Fixture.Dispose();
        }

        [Fact]
        public async Task UpdateExistingAdvertisementUsingHalSelfLink()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";
            var viewRenderedAdvertisementLink = $"{AdvertisementLink}/{AdvertisementId}/view";

            this.SetupPactForUpdatingExistingAdvertisement(link, oAuth2Token, viewRenderedAdvertisementLink);

            Advertisement requestModel = this.SetupModelForExistingAdvertisement(new AdvertisementModelBuilder(this.AllFieldsInitializer)).Build();
            AdvertisementResource result;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                result = await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link), requestModel);
            }

            AdvertisementResource expectedResult = this.SetupModelForExistingAdvertisement(
                new AdvertisementResourceBuilder(this.AllFieldsInitializer).WithId(new Guid(AdvertisementId)).WithLinks(AdvertisementId)).Build();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateExistingAdvertisementUsingHalTemplateWithAdvertisementId()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";
            var viewRenderedAdvertisementLink = $"{AdvertisementLink}/{AdvertisementId}/view";

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.SetupPactForUpdatingExistingAdvertisement(link, oAuth2Token, viewRenderedAdvertisementLink);

            Advertisement requestModel = this.SetupModelForExistingAdvertisement(new AdvertisementModelBuilder(this.AllFieldsInitializer)).Build();
            AdvertisementResource result;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                result = await client.UpdateAdvertisementAsync(new Guid(AdvertisementId), requestModel);
            }

            AdvertisementResource expectedResult = this.SetupModelForExistingAdvertisement(
                new AdvertisementResourceBuilder(this.AllFieldsInitializer).WithId(new Guid(AdvertisementId)).WithLinks(AdvertisementId)).Build();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        private void SetupPactForUpdatingExistingAdvertisement(string link, OAuth2Token oAuth2Token, string viewRenderedAdvertisementLink)
        {
            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving("a PUT advertisement request")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Put,
                    Path = link,
                    Headers = new Dictionary<string, string>
                    {
                        {"Authorization", "Bearer " + oAuth2Token.AccessToken},
                        {"Content-Type", RequestContentTypes.AdvertisementVersion1},
                        {"Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}"},
                        { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                    },
                    Body = new AdvertisementContentBuilder(this.AllFieldsInitializer)
                        .WithJobTitle("Exciting Senior Developer role in a great CBD location. Great $$$ - updated")
                        .WithApplicationFormUrl("http://FakeATS.com.au")
                        .WithStandOutBullet("new Uzi", "new Remington Model", "new AK-47")
                        .Build()
                })
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 200,
                        Headers = new Dictionary<string, string>
                        {
                            {"Content-Type", ResponseContentTypes.AdvertisementVersion1},
                            {"X-Request-Id", RequestId}
                        },
                        Body = new AdvertisementResponseContentBuilder(this.AllFieldsInitializer)
                            .WithState(AdvertisementState.Open.ToString())
                            .WithId(AdvertisementId)
                            .WithLink("self", link)
                            .WithLink("view", viewRenderedAdvertisementLink)
                            .WithJobTitle("Exciting Senior Developer role in a great CBD location. Great $$$ - updated")
                            .WithApplicationFormUrl("http://FakeATS.com.au")
                            .WithStandOutBullet("new Uzi", "new Remington Model", "new AK-47")
                            .Build()
                    });
        }

        private AdvertisementModelBuilder<TAdvertisement> SetupModelForExistingAdvertisement<TAdvertisement>(
            AdvertisementModelBuilder<TAdvertisement> builder) where TAdvertisement : Advertisement, new()
        {
            return builder
                .WithJobTitle("Exciting Senior Developer role in a great CBD location. Great $$$ - updated")
                .WithApplicationFormUrl("http://FakeATS.com.au")
                .WithStandOutBullet("new Uzi", "new Remington Model", "new AK-47");
        }

        [Fact]
        public async Task UpdateNonExistentAdvertisement()
        {
            const string advertisementId = "9b650105-7434-473f-8293-4e23b7e0e064";

            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{advertisementId}";

            this.Fixture.AdPostingApiService
                .UponReceiving("a PUT advertisement request for a non-existent advertisement")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Put,
                    Path = link,
                    Headers = new Dictionary<string, string>
                    {
                        { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                        { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                        { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                        { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                    },
                    Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                        .WithJobDescription("This advertisement should not exist.")
                        .Build()
                })
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 404,
                        Headers = new Dictionary<string, string> { { "X-Request-Id", RequestId } }
                    });

            AdvertisementNotFoundException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<AdvertisementNotFoundException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link),
                        new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithJobDescription("This advertisement should not exist.")
                            .Build()));
            }

            actualException.ShouldBeEquivalentToException(new AdvertisementNotFoundException(RequestId));
        }

        [Fact]
        public async Task UpdateWithInvalidFieldValues()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";

            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving("a PUT advertisement request for advertisement with invalid field values")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Put,
                    Path = link,
                    Headers = new Dictionary<string, string>
                    {
                        { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                        { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                        { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                        { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                    },
                    Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                        .WithSalaryMinimum((decimal)-1.0)
                        .WithSalaryMaximum((decimal)3000.0)
                        .WithSalaryCurrencyCode(1)
                        .WithApplicationEmail("klang(at)seekasia.domain")
                        .WithApplicationFormUrl("htp://ww.seekasia.domain/apply")
                        .WithJobTitle("Temporary part-time libraries North-West inter-library loan business unit administration assistant")
                        .Build()
                })
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 422,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementErrorVersion1 },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new
                        {
                            message = "Validation Failure",
                            errors = new[]
                            {
                                new { field = "applicationEmail", code = "InvalidEmailAddress" },
                                new { field = "applicationFormUrl", code = "InvalidUrl" },
                                new { field = "salary.minimum", code = "ValueOutOfRange" },
                                new { field = "jobTitle", code = "MaxLengthExceeded" },
                                new { field = "salary.display", code = "Required" }
                            }
                        }
                    });

            ValidationException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<ValidationException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link),
                        new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithSalaryMinimum((decimal)-1.0)
                            .WithSalaryMaximum((decimal)3000.0)
                            .WithSalaryCurrencyCode(1)
                            .WithApplicationEmail("klang(at)seekasia.domain")
                            .WithApplicationFormUrl("htp://ww.seekasia.domain/apply")
                            .WithJobTitle("Temporary part-time libraries North-West inter-library loan business unit administration assistant")
                            .Build()));
            }

            var expectedException =
                new ValidationException(
                    RequestId,
                    HttpMethod.Put,
                    new AdvertisementErrorResponse
                    {
                        Message = "Validation Failure",
                        Errors = new[]
                        {
                            new AdvertisementError { Field = "applicationEmail", Code = "InvalidEmailAddress" },
                            new AdvertisementError { Field = "applicationFormUrl", Code = "InvalidUrl" },
                            new AdvertisementError { Field = "salary.minimum", Code = "ValueOutOfRange" },
                            new AdvertisementError { Field = "jobTitle", Code = "MaxLengthExceeded" },
                            new AdvertisementError { Field = "salary.display", Code = "Required" }
                        }
                    });

            actualException.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task UpdateWithInvalidSalaryData()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";

            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving("a PUT advertisement request for advertisement with invalid salary data")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Put,
                    Path = link,
                    Headers = new Dictionary<string, string>
                    {
                        { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                        { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                        { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                        { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                    },
                    Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                        .WithSalaryMinimum((decimal)4000.0)
                        .WithSalaryMaximum((decimal)3000.0)
                        .Build()
                })
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 422,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementErrorVersion1 },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new
                        {
                            message = "Validation Failure",
                            errors = new[] { new { field = "salary.maximum", code = "InvalidValue" } }
                        }
                    });

            ValidationException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<ValidationException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link),
                        new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithSalaryMinimum((decimal)4000.0)
                            .WithSalaryMaximum((decimal)3000.0)
                            .Build()));
            }

            var expectedException =
                new ValidationException(
                    RequestId,
                    HttpMethod.Put,
                    new AdvertisementErrorResponse
                    {
                        Message = "Validation Failure",
                        Errors = new[] { new AdvertisementError { Field = "salary.maximum", Code = "InvalidValue" } }
                    });

            actualException.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task UpdateWithInvalidJobDescription()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";

            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving("a PUT advertisement request for advertisement with invalid job description")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Put,
                    Path = link,
                    Headers = new Dictionary<string, string>
                    {
                        {"Authorization", "Bearer " + oAuth2Token.AccessToken},
                        {"Content-Type", RequestContentTypes.AdvertisementVersion1},
                        {"Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}"},
                        {"User-Agent", AdPostingApiFixture.UserAgentHeaderValue}
                    },
                    Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                        .WithJobDescription("Ad details with <a href='www.youtube.com'>a link</a> and incomplete <h2> element")
                        .Build()
                })
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 422,
                        Headers = new Dictionary<string, string>
                        {
                            {"Content-Type", ResponseContentTypes.AdvertisementErrorVersion1},
                            {"X-Request-Id", RequestId}
                        },
                        Body = new
                        {
                            message = "Validation Failure",
                            errors = new[] { new { field = "jobDescription", code = "InvalidFormat" } }
                        }
                    });

            ValidationException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<ValidationException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link),
                        new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithJobDescription("Ad details with <a href='www.youtube.com'>a link</a> and incomplete <h2> element")
                            .Build()));
            }

            var expectedException =
                new ValidationException(
                    RequestId,
                    HttpMethod.Put,
                    new AdvertisementErrorResponse
                    {
                        Message = "Validation Failure",
                        Errors = new[] { new AdvertisementError { Field = "jobDescription", Code = "InvalidFormat" } }
                    });

            actualException.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task UpdateAdWithADifferentAdvertiserToTheOneOwningTheJob()
        {
            var oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";

            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving("a PUT advertisement request to update a job ad with a different advertiser from the one owning the job")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Put,
                        Path = link,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.AllFieldsInitializer)
                            .WithAdvertiserId("99887766")
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 403,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementErrorVersion1 },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[] { new { code = "RelationshipError" } }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(this.AllFieldsInitializer)
                .WithAdvertiserId("99887766")
                .Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link), requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    RequestId,
                    403,
                    new AdvertisementErrorResponse
                    {
                        Message = "Forbidden",
                        Errors = new[] { new AdvertisementError { Code = "RelationshipError" } }
                    }));
        }

        [Fact]
        public async Task UpdateAdUsingDisabledRequestorAccount()
        {
            var oAuth2Token = new OAuth2TokenBuilder().WithAccessToken(AccessTokens.ValidAccessToken_Disabled).Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";

            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving("a PUT advertisement request to update a job using a disabled requestor account")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Put,
                        Path = link,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.AllFieldsInitializer).Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 403,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementErrorVersion1 },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[] { new { code = "AccountError" } }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(this.AllFieldsInitializer).Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link), requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    RequestId,
                    403,
                    new AdvertisementErrorResponse
                    {
                        Message = "Forbidden",
                        Errors = new[] { new AdvertisementError { Code = "AccountError" } }
                    }));
        }

        [Fact]
        public async Task UpdateAdWhereAdvertiserNotRelatedToRequestor()
        {
            var oAuth2Token = new OAuth2TokenBuilder().WithAccessToken(AccessTokens.OtherThirdPartyUploader).Build();
            var link = $"{AdvertisementLink}/{AdvertisementId}";

            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving("a PUT advertisement request to update a job for an advertiser not related to the requestor's account")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Put,
                        Path = link,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.AllFieldsInitializer).Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 403,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementErrorVersion1 },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[] { new { code = "RelationshipError" } }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(this.AllFieldsInitializer).Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link), requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    RequestId,
                    403,
                    new AdvertisementErrorResponse
                    {
                        Message = "Forbidden",
                        Errors = new[] { new AdvertisementError { Code = "RelationshipError" } }
                    }));
        }

        [Fact]
        public async Task UpdateExpiredAdvertisement()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/c294088d-ff50-4374-bc38-7fa805790e3e";

            this.Fixture.AdPostingApiService
                .Given("There is an expired advertisement")
                .UponReceiving("a PUT advertisement request to update an expired advertisement")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Put,
                        Path = link,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer).Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 403,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementErrorVersion1 },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[] { new { code = "Expired" } }
                        }
                    });

            Advertisement requestModel = new AdvertisementModelBuilder(this.MinimumFieldsInitializer).Build();
            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.UpdateAdvertisementAsync(new Uri(this.Fixture.AdPostingApiServiceBaseUri, link), requestModel));
            }

            var expectedException =
                new UnauthorizedException(
                    RequestId,
                    403,
                    new AdvertisementErrorResponse
                    {
                        Message = "Forbidden",
                        Errors = new[] { new AdvertisementError { Code = "Expired" } }
                    });

            actualException.ShouldBeEquivalentToException(expectedException);
        }

        private AdPostingApiFixture Fixture { get; }
    }
}