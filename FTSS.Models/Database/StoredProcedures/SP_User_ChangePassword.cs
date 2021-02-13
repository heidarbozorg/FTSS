using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_ChangePassword
{
    public class Inputs : BaseDataModelWithToken, IValidatableObject
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        /// <summary>
        /// Validation results
        /// </summary>
        public ICollection<ValidationResult> ValidationResults;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Token))
                results.Add(new ValidationResult("Token could not be empty.", new string[] { "Token" }));

            if (string.IsNullOrEmpty(this.OldPassword))
                results.Add(new ValidationResult("OldPassword could not be empty.", new string[] { "OldPassword" }));

            if (string.IsNullOrEmpty(this.NewPassword))
                results.Add(new ValidationResult("NewPassword could not be empty.", new string[] { "NewPassword" }));

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

    public class Outputs : SingleId
    { }
}
