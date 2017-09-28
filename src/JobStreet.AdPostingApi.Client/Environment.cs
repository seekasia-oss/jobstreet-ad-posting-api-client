namespace JobStreet.AdPostingApi.Client
{
    public enum Environment
    {
        [EnvironmentUrl("https://adposting-integration.jobstreet.com")]
        Integration,

        [EnvironmentUrl("https://adposting.jobstreet.com")]
        Production
    }
}