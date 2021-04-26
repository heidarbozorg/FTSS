using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database
{
    public class BaseDataModelWithToken
    {
        public class WAPI
		{
            public string WAPIToken
            {
                get;
                set;
            }

        }
        public class WAPIUserToken
        {
            public string WAPIToken
            {
                get;
                set;
            }
            public string UserToken
            {
                get;
                set;
            }

        }
        public class User
		{
            public string UserToken
            {
                get;
                set;
            }
        }



    }
}
