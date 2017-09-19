namespace SEEK.AdPostingApi.Client
{
    public enum Environment
    {
        /*[EnvironmentUrl("https://adposting-integration.cloud.seek.com.au")]
        Integration,*/
        [EnvironmentUrl("https://ntkkt5dwpl.execute-api.ap-southeast-1.amazonaws.com/dev/api")]
        Integration,

        [EnvironmentUrl("https://adposting.cloud.seek.com.au")]
        Production
    }
}