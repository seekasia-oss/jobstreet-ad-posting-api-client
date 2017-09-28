using System.Collections.Generic;
using System.Linq;
using System;
using JobStreet.AdPostingApi.Client.Models;

namespace JobStreet.AdPostingApi.Client.Tests.Framework
{
    public class AllFieldsInitializer : IBuilderInitializer
    {
        private readonly IBuilderInitializer _minimumFieldsInitializer = new MinimumFieldsInitializer();

        public AllFieldsInitializer()
        {
        }

        public void Initialize(AdvertisementContentBuilder builder)
        {
            this._minimumFieldsInitializer.Initialize(builder);

            builder
                .WithTemplateId(GetDefaultTemplateId())
                .WithSkill(GetDefaultSkill())
                .WithApplicationEmail(GetDefaultApplicationEmail())
                .WithRequestCreationId(GetDefaultCreationId())
                .WithLanguage(GetDefaultLanguage())
                .WithApplicationFormUrl(GetDefaultApplicationFormUrl())
                .WithStandOutBullet(GetDefaultStandoutBullet())
                .WithCompanyOverview(GetDefaultCompanyOverview())
                .WithLocationArea(GetDefaultLocationArea())
                .WithJobReference(GetDefaultJobReference());
        }

        public void Initialize<TAdvertisement>(AdvertisementModelBuilder<TAdvertisement> builder)
            where TAdvertisement : Advertisement, new()
        {
            this._minimumFieldsInitializer.Initialize(builder);

            builder
                .WithTemplateId(GetDefaultTemplateId())
                .WithSkill(GetDefaultSkill())
                .WithApplicationEmail(GetDefaultApplicationEmail())
                .WithRequestCreationId(GetDefaultCreationId())
                .WithLanguage(GetDefaultLanguage())
                .WithApplicationFormUrl(GetDefaultApplicationFormUrl())
                .WithStandOutBullet(GetDefaultStandoutBullet())
                .WithCompanyOverview(GetDefaultCompanyOverview())
                .WithLocationArea(GetDefaultLocationArea())
                .WithJobReference(GetDefaultJobReference());
        }

        private int GetDefaultTemplateId()
        {
            return 12345;
        }

        private string[] GetDefaultSkill()
        {
            return new string[] { "php", "java" };
        }

        private string GetDefaultApplicationEmail()
        {
            return "default@seekasia.com";
        }

        private string GetDefaultApplicationFormUrl()
        {
            return "http://seekasia.com/";
        }

        private string GetDefaultCreationId()
        {
            return "20150914";
        }

        private int?[] GetDefaultLanguage()
        {
            return new int?[] { 1, 2 };
        }

        private string[] GetDefaultStandoutBullet()
        {
            return new string[] { "Good", "Best", "Awesome" };
        }

        private string GetDefaultCompanyOverview()
        {
            return "this is company overview";
        }

        private string GetDefaultJobReference()
        {
            return "job1234";
        }

        private string GetDefaultLocationArea() 
        {
            return "Test Area";
        }
    }
}