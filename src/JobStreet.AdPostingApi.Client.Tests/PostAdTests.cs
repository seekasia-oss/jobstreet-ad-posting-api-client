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
    public class PostAdTests : IDisposable
    {
        private const string AdvertisementLink = "/advertisement";
        private const string CreationIdForAdWithMinimumRequiredData = "20150914-134527-00012";
        private const string CreationIdForAdWithMaximumRequiredData = "20150914-134527-00097";
        private const string CreationIdForAdWithDuplicateTemplateCustomFields = "20160120-162020-00000";
        private const string RequestId = "PactRequestId";

        private IBuilderInitializer MinimumFieldsInitializer => new MinimumFieldsInitializer();

        private IBuilderInitializer AllFieldsInitializer => new AllFieldsInitializer();

        public PostAdTests(AdPostingApiPactService adPostingApiPactService)
        {
            this.Fixture = new AdPostingApiFixture(adPostingApiPactService);
        }

        public void Dispose()
        {
            this.Fixture.Dispose();
        }

        [Fact]
        public async Task PostAdWithRequiredFieldValuesOnly()
        {
            const string advertisementId = "75b2b1fc-9050-4f45-a632-ec6b7ac2bb4a";
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{advertisementId}";
            var viewRenderedAdvertisementLink = $"{AdvertisementLink}/{advertisementId}/view";
            var location = $"http://localhost{link}";

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job ad with required field values only")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 200,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementVersion1 },
                            { "Location", location },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new AdvertisementResponseContentBuilder(this.MinimumFieldsInitializer)
                            .WithState(AdvertisementState.Open.ToString())
                            .WithId(advertisementId)
                            .WithLink("self", link)
                            .WithLink("view", viewRenderedAdvertisementLink)
                            .Build()
                    });

            var requestModel = new AdvertisementModelBuilder(this.MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).Build();

            AdvertisementResource result;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                result = await client.CreateAdvertisementAsync(requestModel);
            }

            AdvertisementResource expectedResult = new AdvertisementResourceBuilder(this.MinimumFieldsInitializer)
                .WithId(new Guid(advertisementId))
                .WithLinks(advertisementId)
                .Build();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task PostAdWithRequiredAndOptionalFieldValues()
        {
            const string advertisementId = "75b2b1fc-9050-4f45-a632-ec6b7ac2bb4a";
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{advertisementId}";
            var viewRenderedAdvertisementLink = $"{AdvertisementLink}/{advertisementId}/view";
            var location = $"http://localhost{link}";

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job ad with required and optional field values")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.AllFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMaximumRequiredData)
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 200,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", ResponseContentTypes.AdvertisementVersion1 },
                            { "Location", location },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new AdvertisementResponseContentBuilder(this.AllFieldsInitializer)
                            .WithState(AdvertisementState.Open.ToString())
                            .WithId(advertisementId)
                            .WithLink("self", link)
                            .WithLink("view", viewRenderedAdvertisementLink)
                            .Build()
                    });

            var requestModel = new AdvertisementModelBuilder(this.AllFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMaximumRequiredData).Build();

            AdvertisementResource result;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                result = await client.CreateAdvertisementAsync(requestModel);
            }

            AdvertisementResource expectedResult = new AdvertisementResourceBuilder(this.AllFieldsInitializer)
                .WithId(new Guid(advertisementId))
                .WithLinks(advertisementId)
                .Build();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task PostAdWithInvalidFieldValues()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job ad with invalid field values")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
                            .WithSalaryMinimum((decimal)-1.0)
                            .WithSalaryMaximum((decimal)3000.0)
                            .WithSalaryCurrencyCode(1)
                            .WithApplicationEmail("klang(at)seekasia.domain")
                            .WithApplicationFormUrl("htp://ww.seekasia.domain/apply")
                            .WithJobTitle("Temporary part-time libraries North-West inter-library loan business unit administration assistant")
                            .Build()
                    }
                )
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

            ValidationException exception;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                exception = await Assert.ThrowsAsync<ValidationException>(
                    async () =>
                        await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
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
                    HttpMethod.Post,
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

            exception.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task PostAdWithInvalidSalaryData()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job ad with invalid salary data")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .WithSalaryMinimum((decimal)4000.0)
                            .WithSalaryMaximum((decimal)3000.0)
                            .Build()
                    }
                )
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
                                new { field = "salary.maximum", code = "InvalidValue" }
                            }
                        }
                    });

            ValidationException exception;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                exception = await Assert.ThrowsAsync<ValidationException>(
                    async () =>
                        await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .WithSalaryMinimum((decimal)4000.0)
                            .WithSalaryMaximum((decimal)3000.0)
                            .Build()));
            }

            var expectedException =
                new ValidationException(
                    RequestId,
                    HttpMethod.Post,
                    new AdvertisementErrorResponse
                    {
                        Message = "Validation Failure",
                        Errors = new[] { new AdvertisementError { Field = "salary.maximum", Code = "InvalidValue" } }
                    });

            exception.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task PostAdWithInvalidJobDescription()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job ad with invalid job description")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
                            .WithJobDescription("Ad details with <a href='www.youtube.com'>a link</a> and incomplete <h2> element")
                            .Build()
                    }
                )
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
                                new { field = "jobDescription", code = "InvalidFormat" }
                            }
                        }
                    });

            ValidationException exception;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                exception = await Assert.ThrowsAsync<ValidationException>(
                    async () =>
                        await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
                            .WithJobDescription("Ad details with <a href='www.youtube.com'>a link</a> and incomplete <h2> element")
                            .Build()));
            }

            var expectedException =
                new ValidationException(
                    RequestId,
                    HttpMethod.Post,
                    new AdvertisementErrorResponse
                    {
                        Message = "Validation Failure",
                        Errors = new[] { new AdvertisementError { Field = "jobDescription", Code = "InvalidFormat" } }
                    });

            exception.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task PostAdWithNoCreationId()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job ad without a creation id")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
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
                                new { field = "creationId", code = "Required" }
                            }
                        }
                    });

            ValidationException exception;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                exception = await Assert.ThrowsAsync<ValidationException>(
                    async () => await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(this.MinimumFieldsInitializer).Build()));
            }

            exception.ShouldBeEquivalentToException(
                new ValidationException(
                    RequestId,
                    HttpMethod.Post,
                    new AdvertisementErrorResponse
                    {
                        Message = "Validation Failure",
                        Errors = new[] { new AdvertisementError { Field = "creationId", Code = "Required" } }
                    }));
        }

        [Fact]
        public async Task PostAdWithExistingCreationId()
        {
            const string creationId = "CreationIdOf8e2fde50-bc5f-4a12-9cfb-812e50500184";
            const string advertisementId = "8e2fde50-bc5f-4a12-9cfb-812e50500184";
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var location = $"http://localhost{AdvertisementLink}/{advertisementId}";

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .Given("There is a standout advertisement with maximum data")
                .UponReceiving($"a POST advertisement request to create a job ad with the same creation id '{creationId}'")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer).WithRequestCreationId(creationId).Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 409,
                        Headers = new Dictionary<string, string>
                        {
                            { "Location", location },
                            { "Content-Type", ResponseContentTypes.AdvertisementErrorVersion1 },
                            { "X-Request-Id", RequestId }
                        },
                        Body = new
                        {
                            message = "Conflict",
                            errors = new[] { new { field = "creationId", code = "AlreadyExists" } }
                        }
                    });

            CreationIdAlreadyExistsException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<CreationIdAlreadyExistsException>(
                    async () => await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(this.MinimumFieldsInitializer).WithRequestCreationId(creationId).Build()));
            }

            var expectedException = new CreationIdAlreadyExistsException(RequestId, new Uri(location),
                new AdvertisementErrorResponse
                {
                    Message = "Conflict",
                    Errors = new[] { new AdvertisementError { Field = "creationId", Code = "AlreadyExists" } }
                });

            actualException.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task PostAdWithAnInvalidAdvertiserId()
        {
            var oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job ad with an invalid advertiser id")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .WithAdvertiserId("1234ABC")
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
                            errors = new[] { new { code = "InvalidValue" } }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(this.MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).WithAdvertiserId("1234ABC").Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.CreateAdvertisementAsync(requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    RequestId,
                    403,
                    new AdvertisementErrorResponse
                    {
                        Message = "Forbidden",
                        Errors = new[] { new AdvertisementError { Code = "InvalidValue" } }
                    }));
        }

        [Fact]
        public async Task PostAdUsingDisabledRequestorAccount()
        {
            var oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .Given("The requestor's account is disabled")
                .UponReceiving("a POST advertisement request to create a job")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .WithAdvertiserId("1001000001")
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
                            errors = new[] { new { code = "AccountError" } }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .WithAdvertiserId("1001000001")
                            .Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.CreateAdvertisementAsync(requestModel));
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
        public async Task PostAdWhereAdvertiserNotRelatedToRequestor()
        {
            var oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a POST advertisement request to create a job for an advertiser not related to the requestor's account")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", RequestContentTypes.AdvertisementVersion1 },
                            { "Accept", $"{ResponseContentTypes.AdvertisementVersion1}, {ResponseContentTypes.AdvertisementErrorVersion1}" },
                            { "User-Agent", AdPostingApiFixture.UserAgentHeaderValue }
                        },
                        Body = new AdvertisementContentBuilder(this.MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .WithAdvertiserId("999888777")
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

            var requestModel = new AdvertisementModelBuilder(this.MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).WithAdvertiserId("999888777").Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.CreateAdvertisementAsync(requestModel));
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

        private AdPostingApiFixture Fixture { get; }
    }
}