using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.DP.EFORM
{
    public class FTSSDBContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">How to connect to the database</param>
        public FTSSDBContext(DbContextOptions<FTSSDBContext> options)
            :base(options)
        {
        }

        public DbQuery<Models.Database.StoredProcedures.SP_Login.Outputs> SP_Login { get; set; }
    }
}
