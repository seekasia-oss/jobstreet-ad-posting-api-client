using System;
using JobStreet.AdPostingApi.Client.Hal;

namespace JobStreet.AdPostingApi.Client.Models
{
    [MediaType("application/vnd.seekasia.advertisement+json;version=1")]
    public class Advertisement
    {
        #region CENTRALISED CODE START
        
        public ThirdParties ThirdParties { get; set; }

        public string JobTitle { get; set; }

        public int?[] EmploymentType { get; set; } 

        public JobStreet.LocationModel Location { get; set; } 

        public JobStreet.SalaryModel Salary { get; set; }

        public string JobDescription { get; set; }

        public int? JobSpecialization { get; set; }

        public int? JobRole { get; set; }

        public int?[] EducationLevel { get; set; }

        public int? PositionLevel { get; set; }

        public int? YearOfExperience { get; set; }

        public int?[] Site { get; set; }

        public DateTime PostingDate { get; set; }

        public string[] StandOutBullet { get; set; }

        public string CreationId { get; set; }

        public string ApplicationFormUrl { get; set; }

        public string ApplicationEmail { get; set; }

        public string CompanyOverview { get; set; }

        public string JobReference { get; set; }

        #endregion CENTRALISED CODE END

        #region JOBSTREET CODE START

        public int?[] FieldOfStudy { get; set; }

        public string[] Skill { get; set; }

        public bool BlindAd { get; set; }

        public int?[] Language { get; set; }

        public int? TemplateId { get; set; }

        #endregion JOBSTREET CODE END
    }
}