using System;

namespace JobStreet.AdPostingApi.Client.Hal
{
    public interface IResource
    {
        Links Links { get; set; }

        Uri Uri { get; }

        void Initialise(Client client);
    }
}