using System;
using System.Linq;
using SEEK.AdPostingApi.Client.Models;

namespace SEEK.AdPostingApi.Client.Tests.Framework
{
    public class AdvertisementModelBuilder : AdvertisementModelBuilder<Advertisement>
    {
        public AdvertisementModelBuilder(IBuilderInitializer initializer = null) : base(initializer)
        {
            initializer?.Initialize(this);
        }
    }

    public class AdvertisementModelBuilder<TAdvertisement> where TAdvertisement : Advertisement, new()
    {
        #region CENTRALISED CODE START
        private string _advertiserId;
        private string _jobTitle;
        private int? _employmentType;
        private Models.JobStreet.LocationModel[] _location;
        private decimal _salaryMinimum;
        private decimal _salaryMaximum;
        private int? _salaryCurrencyCode;
        private bool _salaryDisplay;
        private string _jobDescription;
        private int? _jobSpecialization;
        private int? _jobRole;
        private int?[] _educationLevel;
        private int? _positionLevel;
        private int? _yearOfExperience;
        private int?[] _site;
        private DateTime _postingDate;
        private string[] _standOutBullet;
        private string _creationId;
        private Models.Action _action;
        private string _applicationFormUrl;
        private string _applicationEmail;
        private Brand _brand;
        #endregion CENTRALISED CODE END

        #region JOBSTREET CODE START
        private int?[] _fieldOfStudy;
        private string[] _skill;
        private bool _blindAd;
        private int?[] _language;
        private int? _templateId;
        #endregion JOBSTREET CODE END

        /*private string _agentId;
        private string _advertiserId;
        private string _creationId;
        private string _jobTitle;
        private string _searchJobTitle;
        private string _jobSummary;
        private string _advertisementDetails;
        private AdvertisementType _advertisementType;
        private WorkType _workType;
        private string _locationId;
        private string _areaId;
        private string _granularLocationCountry;
        private string _granularLocationState;
        private string _granularLocationCity;
        private string _granularLocationPostCode;
        private string _subclassificationId;
        private SalaryType _salaryType;
        private decimal _salaryMinimum;
        private decimal _salaryMaximum;
        private string _salaryDetails;
        private string _contactName;
        private string _contactPhone;
        private string _contactEmail;
        private string _videoUrl;
        private VideoPosition? _videoPosition;
        private string _applicationEmail;
        private string _applicationFormUrl;
        private string _endApplicationUrl;
        private int? _screenId;
        private string _jobReference;
        private string _agentJobReference;
        private int? _templateId;
        private TemplateItem[] _templateItems;
        private int? _standoutLogoId;
        private string[] _standoutBullets;
        private AdditionalPropertyType[] _additionalPropertyTypes;
        private ProcessingOptionsType[] _processingOptionsTypes;
        private string _recruiterEmail;
        private string _recruiterFullName;
        private string _recruiterTeamName;*/

        protected AdvertisementModelBuilder(IBuilderInitializer initializer = null)
        {
            initializer?.Initialize(this);
        }

        public AdvertisementModelBuilder<TAdvertisement> WithAdvertiserId(string advertiserId)
        {
            this._advertiserId = advertiserId;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithJobTitle(string jobTitle)
        {
            this._jobTitle = jobTitle;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithEmploymentType(int employmentType)
        {
            this._employmentType = employmentType;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithLocation(params Models.JobStreet.LocationModel[] location)
        {
            this._location = location.ToArray();        
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithSalaryMinimum(decimal minimum)
        {
            this._salaryMinimum = minimum;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithSalaryMaximum(decimal maximum)
        {
            this._salaryMaximum = maximum;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithSalaryCurrencyCode(int? currencyCode)
        {
            this._salaryCurrencyCode = currencyCode;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithSalaryDisplay(bool display)
        {
            this._salaryDisplay = display;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithJobDescription(string jobDescription)
        {
            this._jobDescription = jobDescription;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithJobSpecialization(int? jobSpecialization)
        {
            this._jobSpecialization = jobSpecialization;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithJobRole(int? jobRole)
        {
            this._jobRole = jobRole;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithEducationLevel(int?[] educationLevel)
        {
            this._educationLevel = educationLevel?.ToArray();
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithPositionLevel(int? positionLevel)
        {
            this._positionLevel = positionLevel;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithYearOfExperience(int? yearOfExperience)
        {
            this._yearOfExperience = yearOfExperience;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithSite(int?[] site)
        {
            this._site = site.ToArray();
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithPostingDate(DateTime postingDate)
        {
            this._postingDate = postingDate;

            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithStandOutBullet(params string[] standOutBullet)
        {
            this._standOutBullet = standOutBullet?.ToArray();
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithRequestCreationId(string creationId)
        {
            this._creationId = creationId;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithAction(Models.Action action)
        {
            this._action = action;            
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithApplicationFormUrl(string applicationFormUrl)
        {
            this._applicationFormUrl = applicationFormUrl;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithApplicationEmail(string applicationEmail)
        {
            this._applicationEmail = applicationEmail;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithFieldOfStudy(int?[] fieldOfStudy)
        {
            this._fieldOfStudy = fieldOfStudy.ToArray();
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithSkill(string[] skill)
        {
            this._skill = skill.ToArray();
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithBlindAd(bool blindAd)
        {
            this._blindAd = blindAd;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithLanguage(int?[] language)
        {
            this._language = language;
            return this;
        }

        public AdvertisementModelBuilder<TAdvertisement> WithTemplateId(int? id)
        {
            this._templateId = id;
            return this;
        }

        /*public AdvertisementModelBuilder<TAdvertisement> WithBrand(Brand brand)
        {
            this._brand = brand;
            return this;
        }*/

        public virtual TAdvertisement Build()
        {
            return new TAdvertisement
            {
                AdvertiserId = this._advertiserId,
                TemplateId = this._templateId,
                JobTitle = this._jobTitle,
                EmploymentType = this._employmentType,
                Salary = new Models.JobStreet.SalaryModel
                {
                    Minimum = this._salaryMinimum,
                    Maximum = this._salaryMaximum,
                    CurrencyCode = this._salaryCurrencyCode,
                    Display = this._salaryDisplay
                },
                Location = this._location?.ToArray(),
                JobDescription = this._jobDescription,
                JobSpecialization = this._jobSpecialization,
                JobRole = this._jobRole,
                EducationLevel = this._educationLevel.ToArray(),
                FieldOfStudy = this._fieldOfStudy?.ToArray(),
                PositionLevel = this._positionLevel,
                YearOfExperience = this._yearOfExperience,
                Skill = this._skill?.ToArray(),
                ApplicationEmail = this._applicationEmail,
                Site = this._site?.ToArray(),
                BlindAd = this._blindAd,
                Language = this._language,
                PostingDate = this._postingDate,
                Action = this._action,
                ApplicationFormUrl = this._applicationFormUrl,
                StandOutBullet = this._standOutBullet?.ToArray(),
                CreationId = this._creationId,
                //Brand = this._brand
            };
        }
    }
}