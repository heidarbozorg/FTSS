using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_User_Delete
    {
        public class Inputs : BaseDataModelWithToken, IValidatableObject
        {
            [Required]
            [Range(1, int.MaxValue)]
            public int UserId { get; set; }

            /// <summary>
            /// Validation results
            /// </summary>
            public ICollection<ValidationResult> ValidationResults;


            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                var results = new List<ValidationResult>();
                if (string.IsNullOrEmpty(this.Token))
                    results.Add(new ValidationResult("Token could not be empty.", new string[] { "Token" }));

                if (this.UserId <= 0)
                    results.Add(new ValidationResult("Invalid UserId.", new string[] { "UserId" }));

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
}