using System;

namespace JobStreet.AdPostingApi.Client.Hal
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmbeddedAttribute : Attribute
    {
        public string Rel { get; set; }
    }
}