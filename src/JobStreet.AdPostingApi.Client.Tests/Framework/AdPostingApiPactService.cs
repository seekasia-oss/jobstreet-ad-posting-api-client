﻿using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace JobStreet.AdPostingApi.Client.Tests.Framework
{
    public class AdPostingApiPactService : IDisposable
    {
        public AdPostingApiPactService()
        {
            this.PactBuilder = new PactBuilder(new PactConfig { PactDir = @"..\..\..\..\pact", LogDir = @"..\..\logs" })
                .ServiceConsumer("Jobstreet Ad Posting API Client")
                .HasPactWith("Ad Posting API");

            this.MockProviderService = this.PactBuilder.MockService(this.MockServerPort);
        }

        public IMockProviderService MockProviderService { get; }

        public Uri MockProviderServiceBaseUri => new Uri($"http://localhost:{this.MockServerPort}");

        private int MockServerPort => 8893;

        private IPactBuilder PactBuilder { get; }

        public void Dispose()
        {
            this.PactBuilder.Build();
        }
    }
}