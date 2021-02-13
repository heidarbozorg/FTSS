using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_Insert
{
    public class Inputs : Models.Database.BaseDataModelWithToken, IValidatableObject
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }        
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Validation results
        /// </summary>
        public ICollection<ValidationResult> ValidationResults;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Token))
                results.Add(new ValidationResult("Token could not be empty.", new string[] { "Token" }));

            if (string.IsNullOrEmpty(this.Email))
                results.Add(new ValidationResult("Email could not be empty.", new string[] { "Email" }));

            if (string.IsNullOrEmpty(this.LastName))
                results.Add(new ValidationResult("LastName could not be empty.", new string[] { "LastName" }));

            if (string.IsNullOrEmpty(this.Password))
                results.Add(new ValidationResult("Password could not be empty.", new string[] { "Password" }));

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
