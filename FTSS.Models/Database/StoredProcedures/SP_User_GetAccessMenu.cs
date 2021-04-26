﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures.SP_User_GetAccessMenu
{
    public class  Inputs : BaseDataModelWithToken
    {
        
    }

    public class Outputs
    {
        public int MenuId { get; set; }

        public int MenuId_Parent { get; set; }

        public string MenuTitle { get; set; }

        public string MenuAddress { get; set; }

        /// <summary>
        /// 1 = Application menu
        /// 2 = restful APIs
        /// </summary>
        public int Kind { get; set; }
    }
}
