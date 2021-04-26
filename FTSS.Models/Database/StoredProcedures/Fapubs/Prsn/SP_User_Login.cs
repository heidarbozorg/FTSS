using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.Fapubs.Prsn
{
    public class SP_User_Login
    {
        public class Inputs: BaseDataModelWithToken.WAPI
        {
            [Required(ErrorMessage = "نام کاربری اجباری می باشد.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "پسورد اجباری می باشد.")]
            public string Password { get; set; }
			public string IpAddress { get; set; }
			public string Tozihat { get; set; }
			public string FCMId { get; set; }
		}

        public class Outputs
        {
            public string Token { get; set; }

            /// <summary>
            /// Database token expiration
            /// </summary>
            public DateTime? ExpireDate { get; set; }
            public int? HokmId { get; set; }
            public string AllHokmIds { get; set; }
            public int? EmailId { get; set; }
            public int? SMSId { get; set; }
            public int? PersonelId { get; set; }


        }
    }
}
