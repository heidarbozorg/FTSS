using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database
{
    public class BaseSearchParams : BaseDataModelWithToken
    {
        public int StartIndex { get; set; }
        
        public int PageSize { get; set; }
    }
}
