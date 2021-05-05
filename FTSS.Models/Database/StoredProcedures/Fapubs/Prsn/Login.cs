using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.Fapubs.Prsn
{
	public class Login
	{
		[Required(ErrorMessage = "نام کاربری اجباری می باشد.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "پسورد اجباری می باشد.")]
		public string Password { get; set; }
		public string Tozihat { get; set; }
		public string FCMId { get; set; }
		[Required(ErrorMessage = "نام کاربری کاربر دیوایس اجباری می باشد.")]
		public string WAPIUserName { get; set; }
		[Required(ErrorMessage = "پسورد کاربر دیوایس اجباری می باشد.")]
		public string WAPIPassword { get; set; }
	}
}
