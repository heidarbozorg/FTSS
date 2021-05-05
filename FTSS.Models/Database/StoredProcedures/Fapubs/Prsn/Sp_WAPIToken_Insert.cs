using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.Fapubs.Prsn
{
	public class Sp_WAPIToken_Insert
	{
        public class Inputs
        {
            [Required(ErrorMessage = "نام کاربری اجباری می باشد.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "پسورد اجباری می باشد.")]
            public string Password { get; set; }
            public string IpAddress { get; set; }
        }

        public class Outputs
        {
            public string Token { get; set; }
        }
	}
}
