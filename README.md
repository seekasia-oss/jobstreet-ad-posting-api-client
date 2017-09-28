# JobStreet's Job Ad Posting API Client

## What It Is
1. A .NET version of JobStreet's Job Ad Posting API Client which can be installed via [NuGet](https://www.nuget.org/packages/JobStreet.AdPostingApi.Client).
2. It comprises of the following three projects:
    1. *JobStreet.AdPostingApi.Client* - Source code of the API client
    2. *JobStreet.AdPostingApi.SampleConsumer* - Example code using the API client to make requests to the Job Ad Posting API
    3. *JobStreet.AdPostingApi.Client.Tests* - Contract (PACT) tests between the API client and the Job Ad Posting API.

## What It Does
1. Exchanges the OAuth 2.0 credentials (client key and client secret) for an OAuth 2.0 access token.
2. Using the OAuth 2.0 access token:
    1. Retrieves the API links from the Job Ad Posting API.
    2. Depending on the operation (create, update, retrieve or expire):
        1. Builds the appropriate API link for the operation.
        2. Makes the appropriate request to the API.

## Resources

1. [Job Ad Posting API Documentation](https://devportal.seek.com.au)
2. [NuGet Package](https://www.nuget.org/packages/JobStreet.AdPostingApi.Client)
3. [Release Notes](https://github.com/SEEK-Jobs/ad-posting-api-client/releases)
4. [Contract (PACT) Between the API Client and the Job Ad Posting API](https://github.com/SEEK-Jobs/ad-posting-api-client/blob/master/pact/README.md)

## Usage

### Install the API Client
Via NuGet with `Install-Package JobStreet.AdPostingApi.Client`

### Initializing the API Client
To initialize a client, the following values are needed:
* **Client Key** [Required]: the client key for getting an OAuth 2 access token
* **Client Secret** [Required]: the client secret for getting an OAuth 2 access token
* **Environment** [Optional]: the environment to which your consumer will integrate with, either "Integration" or "Production" can be supplied. Without supplying anything, "Production" will be used by default.
* **AdPostingApiBaseUrl** [Optional]: the URL of the Job Ad Posting API. Without supplying anything, the Production URL will be used by default.

### Example Code: Construct an API Client to Create a Job Advertisement

```c#
IAdPostingApiClient postingClient = new AdPostingApiClient("<client id>", "<client secret>", Environment.Integration);

var ad = new Advertisement
{
    CreationId = "Sample Consumer 20151001 114732 1234567",
    ThirdParties = new ThirdParties { AdvertiserId = "<Advertiser Id>" },
    JobTitle = "A Job Title",
	EmploymentType = new int[] { 1 },
	Salary = new SalaryModel
    {
        Minimum = 1500,
        Maximum = 2400,
		CurrencyCode = 1,
		Display = true
    },
	Location = new LocationModel
    {
        Id = new int[] { 50300 },
        Area = "Central market"
    },
	JobDescription = "Experience Required",
	JobSpecialization = 191,
	JobRole = 1333,
	EducationLevel = new int[] { 4, 5 },
	FieldOfStudy = new int[] { 8 },
	PositionLevel = 16,
	YearOfExperience = 2,
	Site = new int[] { 1, 2 },
	Language = new int[] { 1, 2, 3 },
	PostingDate = new DateTime(2017, 9, 30),
	StandOutBullet = new string[] { "Good", "Best", "Awesome" },
	ApplicationFormUrl = "www.seekasia.com",
	ApplicationEmail = "testing@emaildomain.com",
	CompanyOverview = "This is for company overview",
	JobReference = "Sample Job 1234",
	Skill = new string[] { "php", ".net", "java" },
	BlindAd = false,
	TemplateId = 12345
};

AdvertisementResource advertisement = await postingClient.CreateAdvertisementAsync(ad);
```
