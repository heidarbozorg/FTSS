using System;
using System.ComponentModel.DataAnnotations;

namespace FTSS.Models.Database
{
    public class BaseSearchParams : BaseDataModelWithToken
    {
        [Required(ErrorMessage = "StartIndex is a required field.")]
        [Range(0, int.MaxValue, ErrorMessage = "StartIndex should be greater or equal to zero.")]
        public int StartIndex { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Range(minimum: 1, maximum: 100)]
        public byte PageSize { get; set; }

        public int ActualSize { get; set; }
    }
}
