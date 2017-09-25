using SEEK.AdPostingApi.Client.Models;
using System;

namespace SEEK.AdPostingApi.Client.Tests.Framework
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
                .WithLocationArea(GetDefaultLocationArea())
                .WithJobDescription(GetDefaultJobDescription())
                .WithJobSpecialization(GetDefaultJobSpecialization())
                .WithJobRole(GetDefaultJobRole())
                .WithEducationLevel(GetDefaultEducationLevel())
                .WithFieldOfStudy(GetDefaultFieldOfStudy())
                .WithPositionLevel(GetDefaultPositionLevel())
                .WithYearOfExperience(GetDefaultYearOfExperience())
                .WithSite(GetDefaultSite())
                .WithBlindAd(GetDefaultBlindAd())
                .WithPostingDate(GetDefaultPostingDate())
                .WithAction(GetDefaultAction());
            /*builder
                //.WithAdvertisementDetails(this.GetDefaultAdvertisementDetails())
                .WithAdvertiserId(this.GetDefaultAdvertiserId())
                //.WithAdvertisementType(AdvertisementType.Classic.ToString())
                //.WithJobSummary(this.GetDefaultJobSummary())
                .WithJobTitle(this.GetDefaultJobTitle())
                //.WithLocationId(this.GetDefaultLocationId())
                //.WithLocationAreaId(this.GetDefaultLocationAreaId())
                .WithSalaryMinimum(this.GetDefaultSalaryMinimum())
                .WithSalaryMaximum(this.GetDefaultSalaryMaximum())
                //.WithSalaryType(this.GetDefaultSalaryType().ToString())
                //.WithSubclassificationId(this.GetDefaultSubclassificationId())
                //.WithWorkType(this.GetDefaultWorkType().ToString())
                //.WithRecruiterFullName(this.GetDefaultRecruiterFullName())
                //.WithRecruiterEmail(this.GetDefaultRecruiterEmail());*/
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
                .WithLocationArea(GetDefaultLocationArea())
                .WithJobDescription(GetDefaultJobDescription())
                .WithJobSpecialization(GetDefaultJobSpecialization())
                .WithJobRole(GetDefaultJobRole())
                .WithEducationLevel(GetDefaultEducationLevel())
                .WithFieldOfStudy(GetDefaultFieldOfStudy())
                .WithPositionLevel(GetDefaultPositionLevel())
                .WithYearOfExperience(GetDefaultYearOfExperience())
                .WithSite(GetDefaultSite())
                .WithBlindAd(GetDefaultBlindAd())
                .WithPostingDate(GetDefaultPostingDate())
                .WithAction(GetDefaultAction());
            /*builder
                .WithAdvertiserId(this.GetDefaultAdvertiserId())
                //.WithAdvertisementType(AdvertisementType.Classic)
                .WithJobTitle(this.GetDefaultJobTitle())
                //.WithLocationArea(this.GetDefaultLocationId(), this.GetDefaultLocationAreaId())
                //.WithSubclassificationId(this.GetDefaultSubclassificationId())
                //.WithWorkType(this.GetDefaultWorkType())
                //.WithSalaryType(this.GetDefaultSalaryType())
                .WithSalaryMinimum(this.GetDefaultSalaryMinimum())
                .WithSalaryMaximum(this.GetDefaultSalaryMaximum())
                //.WithJobSummary(this.GetDefaultJobSummary())
                //.WithAdvertisementDetails(this.GetDefaultAdvertisementDetails())
                //.WithRecruiterFullName(this.GetDefaultRecruiterFullName())
                //.WithRecruiterEmail(this.GetDefaultRecruiterEmail());*/
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

        private string GetDefaultLocationArea()
        {
            return "Test Area";
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

        private Brand GetDefaultBrand()
        {
            return Brand.jobstreet;
        }

        private bool GetDefaultBlindAd()
        {
            return false;
        }

        private DateTime GetDefaultPostingDate()
        {
            return new DateTime(2017, 09, 30);
        }

        private Models.Action GetDefaultAction()
        {
            return Models.Action.none; // Models.Action.none;
        }

        /* ------------------------------------------------------------------------------------------------------------ */

        /*private string GetDefaultAdvertisementDetails()
        {
            return "Exciting, do I need to say more?";
        }*/

        /*private string GetDefaultAdvertiserId()
        {
            return "1";
        }*/

        /*private string GetDefaultJobSummary()
        {
            return "Developer job";
        }*/

        /*private string GetDefaultJobTitle()
        {
            return "Exciting Senior Developer role in a great CBD location. Great $$$";
        }*/

        /*private string GetDefaultLocationAreaId()
        {
            return "RussiaEasternEurope";
        }*/

        /*private string GetDefaultLocationId()
        {
            return "EuropeRussia";
        }*/

        /*private decimal GetDefaultSalaryMaximum()
        {
            return 119999;
        }

        private decimal GetDefaultSalaryMinimum()
        {
            return 100000;
        }*/

        /*private SalaryType GetDefaultSalaryType()
        {
            return SalaryType.AnnualPackage;
        }*/

        /*private string GetDefaultSubclassificationId()
        {
            return "AerospaceEngineering";
        }*/

        /*private WorkType GetDefaultWorkType()
        {
            return WorkType.FullTime;
        }*/

        /*private string GetDefaultRecruiterFullName()
        {
            return "Recruiter Full Name";
        }

        private string GetDefaultRecruiterEmail()
        {
            return "recruiter@email.com";
        }*/
    }
}