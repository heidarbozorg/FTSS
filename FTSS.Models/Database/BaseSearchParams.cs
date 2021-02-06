using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FTSS.Models.Database
{
    public class BaseSearchParams : BaseDataModelWithToken, IValidatableObject
    {
        [Required]
        public int StartIndex { get; set; }

        [Required]
        public int PageSize { get; set; }

        
        /// <summary>
        /// Validation results
        /// </summary>
        public ICollection<ValidationResult> ValidationResults;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (this.StartIndex < 0)
                results.Add(new ValidationResult("StartIndex could not be under zero.", new string[] { "StartIndex" }));

            if (this.PageSize <= 0)
                results.Add(new ValidationResult("PageSize could not be equal or less than zero.", new string[] { "PageSize" }));
            
            return results;
        }

        /// <summary>
        /// Check validations
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            ValidationResults = new List<ValidationResult>();
            var rst = Validator.TryValidateObject(this, new ValidationContext(this, null, null), ValidationResults, false);
            return rst;
        }        
    }
}
