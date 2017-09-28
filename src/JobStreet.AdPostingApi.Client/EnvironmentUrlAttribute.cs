using System;

namespace JobStreet.AdPostingApi.Client
{
    internal class EnvironmentUrlAttribute : Attribute
    {
        public EnvironmentUrlAttribute(string uri)
        {
            this.Uri = new Uri(uri);
        }

        public Uri Uri { get; }
    }
}