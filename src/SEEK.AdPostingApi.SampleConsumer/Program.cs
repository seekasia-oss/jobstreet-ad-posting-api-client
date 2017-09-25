using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using SEEK.AdPostingApi.Client;
using SEEK.AdPostingApi.Client.Models;
using SEEK.AdPostingApi.Client.Models.JobStreet;
using SEEK.AdPostingApi.Client.Resources;
using Environment = SEEK.AdPostingApi.Client.Environment;

namespace SEEK.AdPostingApi.SampleConsumer
{
    public class Program
    {
        private static string CreationId = "Sample Consumer " + Guid.NewGuid();
        private const string AdvertiserId = "1001000001";
        private const int BaseRetryIntervalSeconds = 2;
        private const string ClientId = "client1"; //"ClientId"; //"client1";
        private const string ClientSecret = "secret"; //"ClientSecret"; //"secret";

        private static readonly RetryPolicy TransientErrorRetryPolicy = Policy
            .Handle<RequestException>(ex => ex.StatusCode >= 500)
            .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(BaseRetryIntervalSeconds * Math.Pow(2, attempt)),
                    (exception, waitInterval) =>
                    {
                        Console.WriteLine($"Unexpected error, waiting {waitInterval.TotalSeconds} seconds before retrying.");
                    });

        public static void Main()
        {
            Task.Run(ExampleAsync).Wait();
        }

        public static async Task ExampleAsync()
        {
            using (IAdPostingApiClient client = new AdPostingApiClient(ClientId, ClientSecret, Environment.Integration))
            {
                // Create a new advertisement
                Advertisement advertisement = GetExampleAdvertisementToCreate();
                AdvertisementResource createdAdvertisement = await CreateAdvertisementExampleAsync(advertisement, client);

                if (createdAdvertisement != null)
                {
                    // Retrieve advertisement
                    AdvertisementResource advertisementResource = await GetAdvertisementExampleAsync(createdAdvertisement.Id, client);

                    // Modify details on the advertisement
                    advertisementResource.JobTitle = "Senior Dude";
                    advertisementResource.CreationId = CreationId;
                    AdvertisementResource updatedAdvertisementResource = await UpdateAdvertisementExampleAsync(advertisementResource);

                    // Expire the advertisement
                    await ExpireAdvertisementExampleAsync(updatedAdvertisementResource);
                }

                // Get paged summaries of created advertisements
                AdvertisementSummaryPageResource advertisementSummaryPage = await GetAllAdvertisementsExampleAsync(client);
                while (!advertisementSummaryPage.Eof)
                {
                    advertisementSummaryPage = await GetAllAdvertisementsNextPageExampleAsync(advertisementSummaryPage);
                }
            }

            Console.WriteLine("Finished Example. Press a key to exit.");
            Console.ReadKey(true);
        }

        public static async Task<AdvertisementResource> CreateAdvertisementExampleAsync(Advertisement advertisementToCreate, IAdPostingApiClient client)
        {
            AdvertisementResource advertisementResource = null;
            try
            {
                await TransientErrorRetryPolicy.ExecuteAsync(async () => advertisementResource = await client.CreateAdvertisementAsync(advertisementToCreate));
                Console.WriteLine($"Created Advertisement:\n{JsonConvert.SerializeObject(advertisementResource, Formatting.Indented)}");
            }
            catch (RequestException ex)
            {
                LogException(ex);
            }

            return advertisementResource;
        }

        public static async Task<AdvertisementResource> UpdateAdvertisementExampleAsync(AdvertisementResource advertisement)
        {
            AdvertisementResource advertisementResource = null;
            try
            {
                await TransientErrorRetryPolicy.ExecuteAsync(async () => advertisementResource = await advertisement.SaveAsync());
                Console.WriteLine($"Updated Advertisement:\n{JsonConvert.SerializeObject(advertisementResource, Formatting.Indented)}");
            }
            catch (RequestException ex)
            {
                LogException(ex);
            }

            return advertisementResource;
        }

        private static async Task<AdvertisementResource> ExpireAdvertisementExampleAsync(AdvertisementResource advertisement)
        {
            AdvertisementResource advertisementResource = null;
            try
            {
                await TransientErrorRetryPolicy.ExecuteAsync(async () => advertisementResource = await advertisement.ExpireAsync());
                Console.WriteLine($"Expired Advertisement:\n{JsonConvert.SerializeObject(advertisementResource, Formatting.Indented)}");
            }
            catch (RequestException ex)
            {
                LogException(ex);
            }

            return advertisementResource;
        }

        private static async Task<AdvertisementResource> GetAdvertisementExampleAsync(Guid advertisementId, IAdPostingApiClient client)
        {
            AdvertisementResource advertisementResource = null;
            try
            {
                await TransientErrorRetryPolicy.ExecuteAsync(async () => advertisementResource = await client.GetAdvertisementAsync(advertisementId));
                Console.WriteLine($"Retrieved Advertisement:\n{JsonConvert.SerializeObject(advertisementResource, Formatting.Indented)}");
            }
            catch (RequestException ex)
            {
                LogException(ex);
            }

            return advertisementResource;
        }

        private static async Task<AdvertisementSummaryPageResource> GetAllAdvertisementsExampleAsync(IAdPostingApiClient client)
        {
            AdvertisementSummaryPageResource advertisementSummaryPage = null;
            try
            {
                await TransientErrorRetryPolicy.ExecuteAsync(async () => advertisementSummaryPage = await client.GetAllAdvertisementsAsync());
                Console.WriteLine($"Retrieve all advertisements:{JsonConvert.SerializeObject(advertisementSummaryPage, Formatting.Indented)}");
            }
            catch (RequestException ex)
            {
                LogException(ex);
            }

            return advertisementSummaryPage;
        }

        private static async Task<AdvertisementSummaryPageResource> GetAllAdvertisementsNextPageExampleAsync(AdvertisementSummaryPageResource summaryPage)
        {
            try
            {
                await TransientErrorRetryPolicy.ExecuteAsync(async () => summaryPage = await summaryPage.NextPageAsync());
                Console.WriteLine($"Next page of advertisements:{JsonConvert.SerializeObject(summaryPage, Formatting.Indented)}");
            }
            catch (RequestException ex)
            {
                LogException(ex);
            }

            return summaryPage;
        }

        private static Advertisement GetExampleAdvertisementToCreate()
        {
            return new Advertisement
            {
                ThirdParties = new ThirdParties { AdvertiserId = AdvertiserId },
                JobTitle = "Senior Front-end Developer",
                EmploymentType = new int?[] { 1 },
                Salary = new SalaryModel
                {
                    Minimum = 1000,
                    Maximum = 3000,
                    CurrencyCode = 1,
                    Display = true
                },
                Location = new LocationModel
                {
                    Id = new int?[] { 50300, 70100 },
                    Area = ""
                },
                JobDescription = "Responsibility You know have knowledge of Laravel and AngularJS. Understand requirement and delivery quality products.",
                JobSpecialization = 191,
                JobRole = 1333,
                EducationLevel = new int?[] { 4, 5 },
                FieldOfStudy = new int?[] { 8 },
                PositionLevel = 16,
                YearOfExperience = 2,
                Site = new int?[] { 1, 2 },
                Language = new int?[] { 1, 2, 3 },
                PostingDate = new DateTime(2017, 9, 30),
                StandOutBullet = new string[] { "Good", "Best", "Awesome" },
                ApplicationFormUrl = "www.seekasia.com",
                ApplicationEmail = "klang@seekasia.com",
                CompanyOverview = "",
                JobReference = "",
                Skill = new string[] { "php", ".net", "java" },
                BlindAd = false,
                TemplateId = 12345,
                CreationId = CreationId
            };
        }

        private static void LogException(RequestException ex, [CallerMemberName] string callerName = "")
        {
            Console.WriteLine($"Error (Status Code: {ex.StatusCode}) while performing `{callerName}`.\r\nMessage: {ex.Message}");

            switch (ex.GetType().Name)
            {
                case nameof(CreationIdAlreadyExistsException):
                    Uri advertisementLink = ((CreationIdAlreadyExistsException)ex).AdvertisementLink;
                    Console.WriteLine($"Advertisement Link:{advertisementLink}");
                    break;

                case nameof(ValidationException):
                    PrintAdvertisementErrors(((ValidationException)ex).Errors);
                    break;

                case nameof(UnauthorizedException):
                    PrintAdvertisementErrors(((UnauthorizedException)ex).Errors);
                    break;

                case nameof(TooManyRequestsException):
                    TimeSpan? retryAfter = ((TooManyRequestsException)ex).RetryAfter;
                    if (retryAfter != null)
                    {
                        Console.WriteLine($"A Retry-After period of {retryAfter.Value.TotalSeconds} seconds is provided.");
                    }
                    break;

                case nameof(RequestException):
                    LogException(ex.ResponseContentType, ex.ResponseContent);
                    break;
            }
        }

        private static void LogException(string responseContentType, string responseContent)
        {
            if (responseContentType != null)
            {
                Console.WriteLine($"Response Content-Type: {responseContentType}");
            }
            if (responseContent != null)
            {
                Console.WriteLine($"Response Content: {responseContent}");
            }
        }

        private static void PrintAdvertisementErrors(AdvertisementError[] errors)
        {
            if (errors.Length < 1) return;

            Console.WriteLine("Advertisement Errors:");

            int counter = 1;
            foreach (AdvertisementError error in errors)
            {
                Console.WriteLine($"  [{counter:##}] Field: '{error.Field}' Code: '{error.Code}' Message: '{error.Message}'");
                counter++;
            }
        }
    }
}