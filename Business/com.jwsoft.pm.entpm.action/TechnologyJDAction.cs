namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class TechnologyJDAction
    {
        public static int Add(TechnologyJDInfo objInfo)
        {
            string str = "";
            str = "insert into Prj_TechnologyTell (MainID,PrjCode,SerialNumber,FillDate,FillPeople,PrejectName,ConstructionUnit,";
            string str2 = str + "ByTellUnit,TellPeople,ByTellPeople,TellLocus,TellDate,TellContentAbstract,Remark,TechGuid,FlowState) values ('";
            string str3 = str2 + objInfo.MainId.ToString() + "','" + objInfo.PrjCode + "','" + objInfo.SeriaNumber + "','" + objInfo.FillDate.ToShortDateString() + "','";
            object obj2 = str3 + objInfo.FillPeople + "','" + objInfo.PrejectName + "','" + objInfo.ConstructionUnit + "','" + objInfo.ByTellUnit + "','";
            string str4 = string.Concat(new object[] { obj2, objInfo.TellPeople, "','", objInfo.ByTellPeople, "','", objInfo.TellLocus, "','", objInfo.TellDate, "','" });
            str = str4 + objInfo.TellContentAbstract + "','" + objInfo.Remark + "','" + objInfo.TechGuid + "'";
            int flowState = objInfo.FlowState;
            object obj3 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, ",", objInfo.FlowState, ")" }));
        }

        public static int Del(string strId)
        {
            string str = "";
            str = "begin";
            return publicDbOpClass.ExecSqlString(((str + " delete from XPM_Basic_AnnexList where ModuleID='1728' and RecordCode='" + strId + "'") + " delete from Prj_TechnologyTell where MainID='" + strId + "'") + " end");
        }

        public static int GetMaxId()
        {
            string sqlString = "";
            sqlString = "select ISNULL(max(MainID),0)+1 from Prj_TechnologyTell";
            return (int) publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static DataTable GetModelByGuid(string guid)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_TechnologyTell where TechGuid='" + guid + "'");
        }

        public static int GetTechnologyCount(string strPrjCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_V_TechnologyJD where PrjCode ='" + strPrjCode + "'");
        }

        public static DataTable GetTechnologyList(string strPrjCode)
        {
            string sqlWhere = "1=1";
            if (strPrjCode.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (PrjCode ='" + strPrjCode + "')";
            }
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_V_TechnologyJD", "MainID desc");
        }

        public static DataTable GetTechnologyOfSingle(string Id)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_V_TechnologyJD where MainID='" + Id + "'");
        }

        public static int Upd(TechnologyJDInfo objInfo)
        {
            string str2 = "update Prj_TechnologyTell set FillDate='" + objInfo.FillDate.ToShortDateString() + "',FillPeople='" + objInfo.FillPeople + "',ConstructionUnit='";
            object obj2 = str2 + objInfo.ConstructionUnit + "',ByTellUnit='" + objInfo.ByTellUnit + "',TellPeople='" + objInfo.TellPeople + "',ByTellPeople='";
            object obj3 = string.Concat(new object[] { obj2, objInfo.ByTellPeople, "',TellLocus='", objInfo.TellLocus, "',TellDate='", objInfo.TellDate, "',TellContentAbstract='", objInfo.TellContentAbstract, "',Remark='" });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, objInfo.Remark, "' where MainID='", objInfo.MainId, "'" }));
        }
    }
}

