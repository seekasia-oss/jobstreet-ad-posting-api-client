namespace SEEK.AdPostingApi.Client
{
    public enum Environment
    {
        [EnvironmentUrl("https://adposting-integration.jobstreet.com")]
        Integration,

        [EnvironmentUrl("https://adposting.cloud.seek.com.au")]
        Production
    }
}