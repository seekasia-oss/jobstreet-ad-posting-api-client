using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SEEK.AdPostingApi.Client.Tests.Framework
{
    public class AdvertisementContentBuilder
    {
        protected readonly dynamic AdvertisementModel = new ExpandoObject();

        public AdvertisementContentBuilder(IBuilderInitializer initializer)
        {
            initializer?.Initialize(this);
        }

        public dynamic Build()
        {
            return ((IDictionary<string, object>)this.AdvertisementModel).Clone();
        }

        public AdvertisementContentBuilder WithAdvertiserId(object advertiserId)
        {
            this.AdvertisementModel.advertiserId = advertiserId;
            return this;
        }
        
        public AdvertisementContentBuilder WithJobTitle(object jobTitle)
        {
            this.AdvertisementModel.jobTitle = jobTitle;
            return this;
        }

        public AdvertisementContentBuilder WithEmploymentType(object employmentType)
        {
            if (employmentType == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "employmentType");
            }
            else
            {
                this.AdvertisementModel.employmentType = employmentType;
            }
            return this;
        }

        public AdvertisementContentBuilder WithLocation(params object[] location)
        {
            this.AdvertisementModel.location = location?.Clone<object[]>();
            return this;
        }

        public AdvertisementContentBuilder WithSalaryMinimum(object minimum)
        {
            this.EnsureSalaryPropertyExists();
            this.AdvertisementModel.salary.minimum = minimum;
            return this;
        }

        public AdvertisementContentBuilder WithSalaryMaximum(object maximum)
        {
            this.EnsureSalaryPropertyExists();
            this.AdvertisementModel.salary.maximum = maximum;
            return this;
        }

        public AdvertisementContentBuilder WithSalaryCurrencyCode(object currencyCode)
        {
            if (currencyCode == null)
            {
                if (PropertyExists(this.AdvertisementModel, "salary"))
                {
                    TryRemoveProperty(this.AdvertisementModel.salary, "salaryCurrencyCode");
                }
            }
            else
            {
                this.EnsureSalaryPropertyExists();
                this.AdvertisementModel.salary.salaryCurrencyCode = currencyCode;
            }
            return this;
        }

        public AdvertisementContentBuilder WithSalaryDisplay(object display)
        {
            this.EnsureSalaryPropertyExists();
            this.AdvertisementModel.salary.salaryDisplay = display;
            return this;
        }

        public AdvertisementContentBuilder WithJobDescription(object jobDescription)
        {
            this.AdvertisementModel.jobDescription = jobDescription;
            return this;
        }

        public AdvertisementContentBuilder WithJobSpecialization(object jobSpecialization)
        {
            if (jobSpecialization == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "jobSpecialization");
            }
            else
            {
                this.AdvertisementModel.jobSpecialization = jobSpecialization;
            }
            return this;
        }

        public AdvertisementContentBuilder WithJobRole(object jobRole)
        {
            if (jobRole == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "jobRole");
            }
            else
            {
                this.AdvertisementModel.jobRole = jobRole;
            }
            return this;
        }

        public AdvertisementContentBuilder WithEducationLevel(params object[] educationLevel)
        {
            if (educationLevel == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "educationLevel");
            }
            else
            {
                this.AdvertisementModel.educationLevel = educationLevel?.Clone<object[]>();
            }
            return this;
        }

        public AdvertisementContentBuilder WithPositionLevel(object positionLevel)
        {
            if (positionLevel == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "positionLevel");
            }
            else
            {
                this.AdvertisementModel.positionLevel = positionLevel;
            }
            return this;
        }

        public AdvertisementContentBuilder WithYearOfExperience(object yearOfExperience)
        {
            if (yearOfExperience == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "yearOfExperience");
            }
            else
            {
                this.AdvertisementModel.yearOfExperience = yearOfExperience;
            }
            return this;
        }

        public AdvertisementContentBuilder WithSite(params object[] site)
        {
            if (site == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "site");
            }
            else
            {
                this.AdvertisementModel.site = site?.Clone<object[]>();
            }
            return this;
        }

        public AdvertisementContentBuilder WithPostingDate(object postingDate)
        {
            this.AdvertisementModel.postingDate = postingDate;
            return this;
        }

        public AdvertisementContentBuilder WithStandOutBullet(params object[] standOutBullet)
        {
            this.AdvertisementModel.standOutBullet = standOutBullet?.Clone<object[]>();
            return this;
        }

        public AdvertisementContentBuilder WithRequestCreationId(object creationId)
        {
            this.AdvertisementModel.creationId = creationId;
            return this;
        }

        public AdvertisementContentBuilder WithAction(object action)
        {
            this.AdvertisementModel.action = action;
            return this;
        }

        public AdvertisementContentBuilder WithApplicationFormUrl(object applicationFormUrl)
        {
            this.AdvertisementModel.applicationFormUrl = applicationFormUrl;
            return this;
        }

        public AdvertisementContentBuilder WithApplicationEmail(object applicationEmail)
        {
            this.AdvertisementModel.applicationEmail = applicationEmail;
            return this;
        }

        public AdvertisementContentBuilder WithFieldOfStudy(params object[] fieldOfStudy)
        {
            if (fieldOfStudy == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "fieldOfStudy");
            }
            else
            {
                this.AdvertisementModel.fieldOfStudy = fieldOfStudy?.Clone<object[]>();
            }
            return this;
        }

        public AdvertisementContentBuilder WithSkill(params object[] skill)
        {
            this.AdvertisementModel.skill = skill?.Clone<object[]>();
            return this;
        }

        public AdvertisementContentBuilder WithBlindAd(object blindAd)
        {
            this.AdvertisementModel.blindAd = blindAd;
            return this;
        }

        public AdvertisementContentBuilder WithLanguage(params object[] language)
        {
            if (language == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "language");
            }
            else
            {
                this.AdvertisementModel.language = language?.Clone<object[]>();
            }
            return this;
        }

        public AdvertisementContentBuilder WithTemplateId(object id)
        {
            if (id == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "templateId");
            }
            else
            {
                this.AdvertisementModel.templateId = id;
            }
            return this;
        }

        public AdvertisementContentBuilder WithBrand(object brand)
        {
            this.AdvertisementModel.brand = brand;
            return this;
        }

        private void EnsureSalaryPropertyExists()
        {
            if (!((IDictionary<string, object>)this.AdvertisementModel).ContainsKey("salary"))
            {
                this.AdvertisementModel.salary = new ExpandoObject();
            }
        }

        private bool PropertyExists(dynamic model, string propertyName)
        {
            return ((IDictionary<string, object>)model).ContainsKey(propertyName);
        }

        private void TryRemoveProperty(dynamic model, string propertyName)
        {
            var dictionary = model as IDictionary<string, object>;
            if (dictionary == null) return;

            if (dictionary.ContainsKey(propertyName))
            {
                dictionary.Remove(propertyName);
            }
        }
    }
}