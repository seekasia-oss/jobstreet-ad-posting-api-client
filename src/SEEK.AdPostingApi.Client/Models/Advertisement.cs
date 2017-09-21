using System;
using SEEK.AdPostingApi.Client.Hal;

namespace SEEK.AdPostingApi.Client.Models
{
    [MediaType("application/vnd.seekasia.advertisement+json;version=1")]
    public class Advertisement
    {
        #region CENTRALISED CODE START
        
        public ThirdParties ThirdParties { get; set; }

        public string JobTitle { get; set; }

        public int?[] EmploymentType { get; set; } //int?

        public JobStreet.LocationModel Location { get; set; } //JobStreet.LocationModel[]

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

        public Action Action { get; set; }

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

        #region JOBSDB CODE START 

        /*public int JobIndustry { get; set; }

        public int Nationality { get; set; }

        public string CompanyName { get; set; }

        public bool WorkAuthorization { get; set; }

        public bool WantFreshGrad { get; set; }

        public int LocalResidentOnly { get; set; }

        public int[] Benefits { get; set; }*/

        #endregion JOBSDB CODE END

        /*
        public ThirdParties ThirdParties { get; set; }

        //public string CreationId { get; set; }

        public AdvertisementType AdvertisementType { get; set; }

        //public string JobTitle { get; set; }

        public string SearchJobTitle { get; set; }

        //public Location Location { get; set; }

        public GranularLocation GranularLocation { get; set; }

        public string SubclassificationId { get; set; }

        public WorkType WorkType { get; set; }

        public string JobSummary { get; set; }

        public string AdvertisementDetails { get; set; }

        //public string ApplicationEmail { get; set; }

        //public string ApplicationFormUrl { get; set; }

        public string EndApplicationUrl { get; set; }

        public int? ScreenId { get; set; }

        public string JobReference { get; set; }

        public string AgentJobReference { get; set; }

        //public Salary Salary { get; set; }

        public Contact Contact { get; set; }

        public Template Template { get; set; }

        public Video Video { get; set; }

        //public StandoutAdvertisement Standout { get; set; }

        public AdditionalPropertyType[] AdditionalProperties { get; set; }

        public ProcessingOptionsType[] ProcessingOptions { get; set; }

        public AdvertisementError[] Warnings { get; set; }

        public Recruiter Recruiter { get; set; }
        */

    }
}