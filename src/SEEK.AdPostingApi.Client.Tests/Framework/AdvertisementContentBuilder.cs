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
            object agentId = PropertyExists(this.AdvertisementModel, "thirdParties") &&
                             PropertyExists(this.AdvertisementModel.thirdParties, "agentId")
                ? this.AdvertisementModel.thirdParties.agentId
                : null;

            this.CreateOrRemoveThirdParties(advertiserId, agentId);
            return this;
        }

        public AdvertisementContentBuilder WithAgentId(object agentId)
        {
            object advertiserId = PropertyExists(this.AdvertisementModel, "thirdParties") &&
                                  PropertyExists(this.AdvertisementModel.thirdParties, "advertiserId")
                ? this.AdvertisementModel.thirdParties.advertiserId
                : null;

            this.CreateOrRemoveThirdParties(advertiserId, agentId);
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

        public AdvertisementContentBuilder WithLocationId(object locationId)
        {
            this.EnsureLocationPropertyExists();
            this.AdvertisementModel.location.id = locationId;
            return this;
        }

        public AdvertisementContentBuilder WithLocationArea(object locationArea)
        {
            this.EnsureLocationPropertyExists();
            this.AdvertisementModel.location.area = locationArea;
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
                    TryRemoveProperty(this.AdvertisementModel.salary, "currencyCode");
                }
            }
            else
            {
                this.EnsureSalaryPropertyExists();
                this.AdvertisementModel.salary.currencyCode = currencyCode;
            }
            return this;
        }

        public AdvertisementContentBuilder WithSalaryDisplay(object display)
        {
            this.EnsureSalaryPropertyExists();
            this.AdvertisementModel.salary.display = display;
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

        public AdvertisementContentBuilder WithEducationLevel(object educationLevel)
        {
            if (educationLevel == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "educationLevel");
            }
            else
            {
                this.AdvertisementModel.educationLevel = educationLevel; //?.Clone();
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

        public AdvertisementContentBuilder WithSite(object site)
        {
            if (site == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "site");
            }
            else
            {
                this.AdvertisementModel.site = site; //?.Clone();
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
            this.AdvertisementModel.standOutBullet = standOutBullet?.Clone();
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

        public AdvertisementContentBuilder WithCompanyOverview(object companyOverview)
        {
            this.AdvertisementModel.companyOverview = companyOverview;
            return this;
        }

        public AdvertisementContentBuilder WithJobReference(object jobReference)
        {
            this.AdvertisementModel.jobReference = jobReference;
            return this;
        }

        public AdvertisementContentBuilder WithFieldOfStudy(object fieldOfStudy)
        {
            if (fieldOfStudy == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "fieldOfStudy");
            }
            else
            {
                this.AdvertisementModel.fieldOfStudy = fieldOfStudy; //?.Clone();
            }
            return this;
        }

        public AdvertisementContentBuilder WithSkill(object skill)
        {
            this.AdvertisementModel.skill = skill; //?.Clone();
            return this;
        }

        public AdvertisementContentBuilder WithBlindAd(object blindAd)
        {
            this.AdvertisementModel.blindAd = blindAd;
            return this;
        }

        public AdvertisementContentBuilder WithLanguage(object language)
        {
            if (language == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "language");
            }
            else
            {
                this.AdvertisementModel.language = language; //?.Clone();
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

        private void CreateOrRemoveThirdParties(object advertiserId, object agentId)
        {
            if (advertiserId == null && agentId == null)
            {
                TryRemoveProperty(this.AdvertisementModel, "thirdParties");
                return;
            }

            if (!PropertyExists(this.AdvertisementModel, "thirdParties"))
            {
                this.AdvertisementModel.thirdParties = new ExpandoObject();
            }

            if (advertiserId == null)
            {
                TryRemoveProperty(this.AdvertisementModel.thirdParties, "advertiserId");
            }
            else
            {
                this.AdvertisementModel.thirdParties.advertiserId = advertiserId;
            }

            if (agentId == null)
            {
                TryRemoveProperty(this.AdvertisementModel.thirdParties, "agentId");
            }
            else
            {
                this.AdvertisementModel.thirdParties.agentId = agentId;
            }
        }

        private void EnsureSalaryPropertyExists()
        {
            if (!((IDictionary<string, object>)this.AdvertisementModel).ContainsKey("salary"))
            {
                this.AdvertisementModel.salary = new ExpandoObject();
            }
        }

        private void EnsureLocationPropertyExists()
        {
            if (!((IDictionary<string, object>)this.AdvertisementModel).ContainsKey("location"))
            {
                this.AdvertisementModel.location = new ExpandoObject();
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