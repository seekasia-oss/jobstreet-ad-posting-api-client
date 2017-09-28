using System;

namespace JobStreet.AdPostingApi.Client.Hal
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MediaTypeAttribute : Attribute
    {
        public string MediaType { get; }

        public MediaTypeAttribute(string mediaType)
        {
            this.MediaType = mediaType;
        }
    }
}