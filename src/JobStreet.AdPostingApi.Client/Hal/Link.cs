using Tavis.UriTemplates;

namespace JobStreet.AdPostingApi.Client.Hal
{
    public class Link
    {
        public string Href { get; set; }

        public bool Templated { get; set; }

        public string Resolve(object parameters)
        {
            if (!this.Templated)
            {
                return this.Href;
            }

            return new UriTemplate(this.Href)
                .AddParameters(parameters)
                .Resolve();
        }
    }
}