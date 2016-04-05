﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using PactNet.Mocks.MockHttpService.Models;
using SEEK.AdPostingApi.Client;
using SEEK.AdPostingApi.Client.Hal;
using SEEK.AdPostingApi.Client.Models;
using SEEK.AdPostingApi.Client.Resources;
using Xunit;

namespace SEEK.AdPostingApi.Client.Tests
{
    [Collection(AdPostingApiCollection.Name)]
    public class PostAdTests : IDisposable
    {
        private const string AdvertisementLink = "/advertisement";
        private const string CreationIdForAdWithMinimumRequiredData = "20150914-134527-00012";
        private const string CreationIdForAdWithMaximumRequiredData = "20150914-134527-00097";
        private const string CreationIdForAdWithDuplicateTemplateCustomFields = "20160120-162020-00000";

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
        public async Task PostAdWithMinimumRequiredData()
        {
            const string advertisementId = "75b2b1fc-9050-4f45-a632-ec6b7ac2bb4a";
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{advertisementId}";
            var viewRenderedAdvertisementLink = $"{AdvertisementLink}/{advertisementId}/view";
            var location = $"http://localhost{link}";

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job ad with minimum required data")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 202,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement+json; version=1; charset=utf-8" },
                            { "Location", location }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
                            .WithResponseLink("self", link)
                            .WithResponseLink("view", viewRenderedAdvertisementLink)
                            .Build()
                    });

            var requestModel = new AdvertisementModelBuilder(MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).Build();

            AdvertisementResource result;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                result = await client.CreateAdvertisementAsync(requestModel);
            }

            var expectedResult = new AdvertisementResource
            {
                Links = new Links(this.Fixture.AdPostingApiServiceBaseUri)
                {
                    { "self", new Link { Href = link } },
                    { "view", new Link { Href = viewRenderedAdvertisementLink } }
                }
            };

            new AdvertisementModelBuilder(MinimumFieldsInitializer, expectedResult).Build();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task PostAdWithMaximumData()
        {
            const string advertisementId = "75b2b1fc-9050-4f45-a632-ec6b7ac2bb4a";
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();
            var link = $"{AdvertisementLink}/{advertisementId}";
            var viewRenderedAdvertisementLink = $"{AdvertisementLink}/{advertisementId}/view";
            var location = $"http://localhost{link}";

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job ad with maximum required data")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(AllFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMaximumRequiredData)
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 202,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement+json; version=1; charset=utf-8" },
                            { "Location", location }
                        },
                        Body = new AdvertisementContentBuilder(AllFieldsInitializer)
                            .WithState(AdvertisementState.Open.ToString())
                            .WithResponseLink("self", link)
                            .WithResponseLink("view", viewRenderedAdvertisementLink)
                            .Build()
                    });

            var requestModel = new AdvertisementModelBuilder(AllFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMaximumRequiredData).Build();

            AdvertisementResource result;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                result = await client.CreateAdvertisementAsync(requestModel);
            }

            var expectedResult = new AdvertisementResource
            {
                Links = new Links(this.Fixture.AdPostingApiServiceBaseUri)
                {
                    { "self", new Link { Href = link } },
                    { "view", new Link { Href = viewRenderedAdvertisementLink } }
                }
            };

            new AdvertisementModelBuilder(AllFieldsInitializer, expectedResult).Build();

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task PostAdWithWrongData()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job ad with bad data")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
                            .WithAdvertiserId(null)
                            .WithAdvertisementType(AdvertisementType.StandOut.ToString())
                            .WithSalaryMinimum(-1.0)
                            .WithVideoUrl("htp://www.youtube.com/v/abc".PadRight(260, '!'))
                            .WithVideoPosition(VideoPosition.Below.ToString())
                            .WithStandoutBullets("new Uzi", "new Remington Model".PadRight(85, '!'), "new AK-47")
                            .WithApplicationEmail("someone(at)some.domain")
                            .WithApplicationFormUrl("htp://somecompany.domain/apply")
                            .WithTemplateItems(
                                new KeyValuePair<object, object>("Template Line 1", "Template Value 1"),
                                new KeyValuePair<object, object>("", "value2".PadRight(3010, '!')))
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 422,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
                        },
                        Body = new
                        {
                            message = "Validation Failure",
                            errors = new[]
                            {
                                new { field = "advertiserId", code = "Required" },
                                new { field = "applicationEmail", code = "InvalidEmailAddress" },
                                new { field = "applicationFormUrl", code = "InvalidUrl" },
                                new { field = "salary.minimum", code = "ValueOutOfRange" },
                                new { field = "standout.bullets[1]", code = "MaxLengthExceeded" },
                                new { field = "template.items[1].name", code = "Required" },
                                new { field = "template.items[1].value", code = "MaxLengthExceeded" },
                                new { field = "video.url", code = "MaxLengthExceeded" },
                                new { field = "video.url", code = "RegexPatternNotMatched" }
                            }
                        }
                    });

            ValidationException exception;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                exception = await Assert.ThrowsAsync<ValidationException>(
                    async () =>
                        await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
                            .WithAdvertiserId(null)
                            .WithAdvertisementType(AdvertisementType.StandOut)
                            .WithSalaryMinimum(-1)
                            .WithVideoUrl("htp://www.youtube.com/v/abc".PadRight(260, '!'))
                            .WithVideoPosition(VideoPosition.Below)
                            .WithStandoutBullets("new Uzi", "new Remington Model".PadRight(85, '!'), "new AK-47")
                            .WithApplicationEmail("someone(at)some.domain")
                            .WithApplicationFormUrl("htp://somecompany.domain/apply")
                            .WithTemplateItems(
                                new TemplateItemModel { Name = "Template Line 1", Value = "Template Value 1" },
                                new TemplateItemModel { Name = "", Value = "value2".PadRight(3010, '!') })
                            .Build()));
            }

            var expectedException = new ValidationException(
                HttpMethod.Post,
                new ValidationMessage
                {
                    Message = "Validation Failure",
                    Errors = new[]
                    {
                        new ValidationData { Field = "advertiserId", Code = "Required" },
                        new ValidationData { Field = "applicationEmail", Code = "InvalidEmailAddress" },
                        new ValidationData { Field = "applicationFormUrl", Code = "InvalidUrl" },
                        new ValidationData { Field = "salary.minimum", Code = "ValueOutOfRange" },
                        new ValidationData { Field = "standout.bullets[1]", Code = "MaxLengthExceeded" },
                        new ValidationData { Field = "template.items[1].name", Code = "Required" },
                        new ValidationData { Field = "template.items[1].value", Code = "MaxLengthExceeded" },
                        new ValidationData { Field = "video.url", Code = "MaxLengthExceeded" },
                        new ValidationData { Field = "video.url", Code = "RegexPatternNotMatched" }
                    }
                });

            exception.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task PostAdWithInvalidAdvertisementDetails()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job ad with invalid advertisement details")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
                            .WithAdvertisementDetails("Ad details with <a href='www.youtube.com'>a link</a> and incomplete <h2> element")
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 422,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
                        },
                        Body = new
                        {
                            message = "Validation Failure",
                            errors = new[]
                            {
                                new { field = "advertisementDetails", code = "InvalidFormat" }
                            }
                        }
                    });

            ValidationException exception;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                exception = await Assert.ThrowsAsync<ValidationException>(
                    async () =>
                        await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId("20150914-134527-00109")
                            .WithAdvertisementDetails("Ad details with <a href='www.youtube.com'>a link</a> and incomplete <h2> element")
                            .Build()));
            }

            var expectedException = new ValidationException(
                HttpMethod.Post,
                new ValidationMessage
                {
                    Message = "Validation Failure",
                    Errors = new[]
                    {
                        new ValidationData { Field = "advertisementDetails", Code = "InvalidFormat" }
                    }
                });

            exception.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task PostAdWithNoCreationId()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job ad without a creation id")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer).Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 422,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
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
                    async () => await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(MinimumFieldsInitializer).Build()));
            }

            exception.ShouldBeEquivalentToException(
                new ValidationException(
                    HttpMethod.Post,
                    new ValidationMessage
                    {
                        Message = "Validation Failure",
                        Errors = new[] { new ValidationData { Field = "creationId", Code = "Required" } }
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
                .Given("There is a pending standout advertisement with maximum data")
                .UponReceiving($"a request to create a job ad with the same creation id '{creationId}'")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer).WithRequestCreationId(creationId).Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 409,
                        Headers = new Dictionary<string, string> { { "Location", location } }
                    });

            AdvertisementAlreadyExistsException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<AdvertisementAlreadyExistsException>(
                    async () => await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(MinimumFieldsInitializer).WithRequestCreationId(creationId).Build()));
            }

            var expectedException = new AdvertisementAlreadyExistsException(new Uri(location));

            actualException.ShouldBeEquivalentToException(expectedException);
        }

        [Fact]
        public async Task PostAdWithANonIntegerAdvertiserName()
        {
            var oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job ad with a non integer advertiser name")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
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
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[]
                            {
                                new { code = "InvalidValue" }
                            }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).WithAdvertiserId("1234ABC").Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.CreateAdvertisementAsync(requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    new ForbiddenMessage
                    {
                        Message = "Forbidden",
                        Errors = new[] { new ForbiddenMessageData { Code = "InvalidValue" } }
                    }
                    ));
        }

        [Fact]
        public async Task PostAgentAdWithEmptyAgentId()
        {
            var oAuth2Token = new OAuth2TokenBuilder().WithAccessToken(AccessTokens.ValidAgentAccessToken).Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create an agent job where the agent id is not supplied")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .WithAgentId("")
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 403,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[]
                            {
                                new { code = "Required" }
                            }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).WithAgentId("").Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.CreateAdvertisementAsync(requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    new ForbiddenMessage
                    {
                        Message = "Forbidden",
                        Errors = new[] { new ForbiddenMessageData { Code = "Required" } }
                    }
                    ));
        }

        [Fact]
        public async Task PostAdWithDisabledThirdPartyUploader()
        {
            var oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .Given("The third party uploader account is disabled")
                .UponReceiving("a request to create a job with a disabled third party uploader")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithMinimumRequiredData)
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 403,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[]
                            {
                                new { code = "AccountError" }
                            }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.CreateAdvertisementAsync(requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    new ForbiddenMessage
                    {
                        Message = "Forbidden",
                        Errors = new[] { new ForbiddenMessageData { Code = "AccountError" } }
                    }
                    ));
        }

        [Fact]
        public async Task PostAdWhereAdvertiserNotRelatedToThirdPartyUploader()
        {
            var oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job for an advertiser not related to the third party uploader")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
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
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
                        },
                        Body = new
                        {
                            message = "Forbidden",
                            errors = new[]
                            {
                                new { code = "RelationshipError" }
                            }
                        }
                    });

            var requestModel = new AdvertisementModelBuilder(MinimumFieldsInitializer).WithRequestCreationId(CreationIdForAdWithMinimumRequiredData).WithAdvertiserId("999888777").Build();

            UnauthorizedException actualException;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                actualException = await Assert.ThrowsAsync<UnauthorizedException>(
                    async () => await client.CreateAdvertisementAsync(requestModel));
            }

            actualException.ShouldBeEquivalentToException(
                new UnauthorizedException(
                    new ForbiddenMessage
                    {
                        Message = "Forbidden",
                        Errors = new[] { new ForbiddenMessageData { Code = "RelationshipError" } }
                    }
                    ));
        }

        [Fact]
        public async Task PostAdWithDuplicateTemplateCustomFieldNames()
        {
            OAuth2Token oAuth2Token = new OAuth2TokenBuilder().Build();

            this.Fixture.RegisterIndexPageInteractions(oAuth2Token);

            this.Fixture.AdPostingApiService
                .UponReceiving("a request to create a job ad with duplicated names for template custom fields")
                .With(
                    new ProviderServiceRequest
                    {
                        Method = HttpVerb.Post,
                        Path = AdvertisementLink,
                        Headers = new Dictionary<string, string>
                        {
                            { "Authorization", "Bearer " + oAuth2Token.AccessToken },
                            { "Content-Type", "application/vnd.seek.advertisement+json; charset=utf-8" }
                        },
                        Body = new AdvertisementContentBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithDuplicateTemplateCustomFields)
                            .WithTemplateItems(
                                new KeyValuePair<object, object>("FieldNameA", "Template Value 1"),
                                new KeyValuePair<object, object>("FieldNameB", "Template Value 2"),
                                new KeyValuePair<object, object>("FieldNameA", "Template Value 3"))
                            .Build()
                    }
                )
                .WillRespondWith(
                    new ProviderServiceResponse
                    {
                        Status = 422,
                        Headers = new Dictionary<string, string>
                        {
                            { "Content-Type", "application/vnd.seek.advertisement-error+json; version=1; charset=utf-8" }
                        },
                        Body = new
                        {
                            message = "Validation Failure",
                            errors = new[]
                            {
                                new { field = "template.items[0]", code = "AlreadySpecified" },
                                new { field = "template.items[2]", code = "AlreadySpecified" }
                            }
                        }
                    });
            ValidationException exception;

            using (AdPostingApiClient client = this.Fixture.GetClient(oAuth2Token))
            {
                exception = await Assert.ThrowsAsync<ValidationException>(
                    async () =>
                        await client.CreateAdvertisementAsync(new AdvertisementModelBuilder(MinimumFieldsInitializer)
                            .WithRequestCreationId(CreationIdForAdWithDuplicateTemplateCustomFields)
                            .WithTemplateItems(
                                new TemplateItemModel { Name = "FieldNameA", Value = "Template Value 1" },
                                new TemplateItemModel { Name = "FieldNameB", Value = "Template Value 2" },
                                new TemplateItemModel { Name = "FieldNameA", Value = "Template Value 3" })
                            .Build()));
            }

            var expectedException = new ValidationException(
                HttpMethod.Post,
                new ValidationMessage
                {
                    Message = "Validation Failure",
                    Errors = new[]
                    {
                        new ValidationData { Field = "template.items[0]", Code = "AlreadySpecified" },
                        new ValidationData { Field = "template.items[2]", Code = "AlreadySpecified" }
                    }
                });

            exception.ShouldBeEquivalentToException(expectedException);
        }

        private AdPostingApiFixture Fixture { get; }
    }
}