using JobStreet.AdPostingApi.Client.Models;
using System;

namespace JobStreet.AdPostingApi.Client.Tests.Framework
{
    public class MinimumFieldsInitializer : IBuilderInitializer
    {
        public void Initialize(AdvertisementContentBuilder builder)
        {
            builder
                .WithAdvertiserId(GetDefaultAdvertiserId())
                .WithJobTitle(GetDefaultJobTitle())
                .WithEmploymentType(GetDefaultEmploymentType())
                .WithSalaryMinimum(GetDefaultSalaryMinimum())
                .WithSalaryMaximum(GetDefaultSalaryMaximum())
                .WithSalaryCurrencyCode(GetDefaultSalaryCurrencyCode())
                .WithSalaryDisplay(GetDefaultSalaryDisplay())
                .WithLocationId(GetDefaultLocationId())
                .WithJobDescription(GetDefaultJobDescription())
                .WithJobSpecialization(GetDefaultJobSpecialization())
                .WithJobRole(GetDefaultJobRole())
                .WithEducationLevel(GetDefaultEducationLevel())
                .WithFieldOfStudy(GetDefaultFieldOfStudy())
                .WithPositionLevel(GetDefaultPositionLevel())
                .WithYearOfExperience(GetDefaultYearOfExperience())
                .WithSite(GetDefaultSite())
                .WithBlindAd(GetDefaultBlindAd()) 
                .WithPostingDate(GetDefaultPostingDate());           
        }

        public void Initialize<TAdvertisement>(AdvertisementModelBuilder<TAdvertisement> builder)
            where TAdvertisement : Advertisement, new()
        {
            builder
                .WithAdvertiserId(this.GetDefaultAdvertiserId())
                .WithJobTitle(GetDefaultJobTitle())
                .WithEmploymentType(GetDefaultEmploymentType())
                .WithSalaryMinimum(GetDefaultSalaryMinimum())
                .WithSalaryMaximum(GetDefaultSalaryMaximum())
                .WithSalaryCurrencyCode(GetDefaultSalaryCurrencyCode())
                .WithSalaryDisplay(GetDefaultSalaryDisplay())
                .WithLocationId(GetDefaultLocationId())
                .WithJobDescription(GetDefaultJobDescription())
                .WithJobSpecialization(GetDefaultJobSpecialization())
                .WithJobRole(GetDefaultJobRole())
                .WithEducationLevel(GetDefaultEducationLevel())
                .WithFieldOfStudy(GetDefaultFieldOfStudy())
                .WithPositionLevel(GetDefaultPositionLevel())
                .WithYearOfExperience(GetDefaultYearOfExperience())
                .WithSite(GetDefaultSite())
                .WithBlindAd(GetDefaultBlindAd())
                .WithPostingDate(GetDefaultPostingDate());
        }

        private string GetDefaultAdvertiserId()
        {
            return "1001000001";
        }

        private string GetDefaultJobTitle()
        {
            return "Front-end Developer";
        }

        private int?[] GetDefaultEmploymentType()
        {
            return new int?[] { 3 };
        }

        private decimal GetDefaultSalaryMinimum()
        {
            return (decimal)1000.0;
        }

        private decimal GetDefaultSalaryMaximum()
        {
            return (decimal)1599.0;
        }

        private int GetDefaultSalaryCurrencyCode()
        {
            return 1;
        }

        private bool GetDefaultSalaryDisplay()
        {
            return true;
        }

        private int?[] GetDefaultLocationId()
        {
            return new int?[] { 50001 };
        }

        private string GetDefaultJobDescription()
        {
            return "This is job description";
        }

        private int GetDefaultJobSpecialization()
        {
            return 191;
        }

        private int GetDefaultJobRole()
        {
            return 1333;
        }

        private int?[] GetDefaultEducationLevel()
        {
            return new int?[] { 4, 5};
        }

        private int?[] GetDefaultFieldOfStudy()
        {
            return new int?[] { 8 };
        }

        private int? GetDefaultPositionLevel()
        {
            return 16;
        }

        private int GetDefaultYearOfExperience()
        {
            return 2;
        }

        private int?[] GetDefaultSite()
        {
            return new int?[] { 1, 2 };
        }

        private bool GetDefaultBlindAd()
        {
            return false;
        }

        private DateTime GetDefaultPostingDate()
        {
            return new DateTime(2017, 09, 30);
        }
    }
}