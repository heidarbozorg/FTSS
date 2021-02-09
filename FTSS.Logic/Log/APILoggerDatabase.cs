using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    public class APILoggerDatabase : IAPILogger
    {
        private readonly Database.IDatabaseContext _databaseContext;
        private readonly AutoMapper.IMapper _mapper;

        public APILoggerDatabase(Database.IDatabaseContext databaseContext, AutoMapper.IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Save API log at database
        /// </summary>
        /// <param name="data"></param>
        public void Save(Models.API.Log data)
        {
            //var dbModel = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs(data);
            var dbModel = _mapper.Map<Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>(data);
            _databaseContext.SP_APILog_Insert(dbModel);
        }
    }
}
