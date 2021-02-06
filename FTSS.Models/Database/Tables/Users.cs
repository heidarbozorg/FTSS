using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.Tables
{
    public class Users : Models.Database.BaseDataModelWithToken
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
