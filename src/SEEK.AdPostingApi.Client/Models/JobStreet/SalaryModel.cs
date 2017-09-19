using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEEK.AdPostingApi.Client.Models.JobStreet
{
    public class SalaryModel
    {
        public SalaryModel()
        {
        }

        public SalaryModel(SalaryModel model)
        {
            this.Minimum = model.Minimum;
            this.Maximum = model.Maximum;
            this.CurrencyCode = model.CurrencyCode;
            this.Display = model.Display;
        }

        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; }

        public int? CurrencyCode { get; set; }

        public bool Display { get; set; }
    }
}
