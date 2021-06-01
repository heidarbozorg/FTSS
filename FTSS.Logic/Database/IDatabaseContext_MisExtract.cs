using FTSS.Models.Database;
using FTSS.Models.Database.StoredProcedures.MisExtract.dbo;
using FTSS.Models.Database.StoredProcedures.MisExtract.Rep;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database
{
    public interface IDatabaseContext_MisExtract
    {
        DBResult SP_APILog_Insert(Models.Database.StoredProcedures.SP_APILog_Insert.Inputs inputs);
        DBResult Sp_AmalkardKarkhaneh_Coal_Kharid(Sp_AmalkardKarkhaneh_Coal_Kharid.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Coal_KharidMahiane(Sp_AmalkardKarkhaneh_Coal_KharidMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Coal_Mojudi(Sp_AmalkardKarkhaneh_Coal_Mojudi.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Coal_MojudiMahiane(Sp_AmalkardKarkhaneh_Coal_MojudiMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Consantre_Ersal(Sp_AmalkardKarkhaneh_Consantre_Ersal.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane(Sp_AmalkardKarkhaneh_Consantre_ErsalMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Consantre_Mojudi(Sp_AmalkardKarkhaneh_Consantre_Mojudi.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane(Sp_AmalkardKarkhaneh_Consantre_MojudiMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Consantre_Tolid(Sp_AmalkardKarkhaneh_Consantre_Tolid.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh_Consantre_TolidMahiane(Sp_AmalkardKarkhaneh_Consantre_TolidMahiane.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhanehStop5(Sp_AmalkardKarkhanehStop5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhanehStopMahiane5(Sp_AmalkardKarkhanehStopMahiane5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhanehVorodi5(Sp_AmalkardKarkhanehVorodi5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhanehVorodiMahiane5(Sp_AmalkardKarkhanehVorodiMahiane5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh(Sp_AmalKardKarkhaneh.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh2_1(Sp_AmalKardKarkhaneh2_1.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh2_4(Sp_AmalkardKarkhaneh2_4.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult Sp_AmalkardKarkhaneh2_5(Sp_AmalkardKarkhaneh2_5.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult SP_madan_iden_List(SP_madan_iden_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult SP_SalersHead_List(SP_SalersHead_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult SP_group_List(SP_group_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
        DBResult SP_version_List(SP_version_List.Inputs inputs, string key, string issuer, IHttpContextAccessor _IHttpContextAccessor);
    }
}
