using System;
using System.Net;
using System.Runtime.Serialization;

namespace JobStreet.AdPostingApi.Client
{
    [Serializable]
    public class AdvertisementNotFoundException : RequestException
    {
        public AdvertisementNotFoundException(string requestId) : base(requestId, (int)HttpStatusCode.NotFound, "The advertisement does not exist.")
        {
        }

        protected AdvertisementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}