namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class MeasureDataAction
    {
        public static int GetMaxId()
        {
            string sqlString = "";
            sqlString = "select ISNULL(max(ID),0)+1 from Prj_TechnologyManage";
            return (int) publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static int GetMeasureCount(string strPrjCode, string bigsort, string smallsort)
        {
            string sqlString = "";
            sqlString = "select count(1) from Prj_TechnologyManage where PrjCode ='" + strPrjCode + "'";
            if (bigsort.ToString() != "")
            {
                sqlString = sqlString + "and (BigSort ='" + bigsort + "')";
            }
            if (smallsort.ToString() != "")
            {
                sqlString = sqlString + "and (SmallSort ='" + smallsort + "')";
            }
            return (int) publicDbOpClass.ExecuteScalar(sqlString);
        }

        public static DataTable GetMeasureList(string strPrjCode, string bigsort, string smallsort)
        {
            string sqlWhere = "";
            sqlWhere = "(1=1)";
            if (strPrjCode.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (PrjCode ='" + strPrjCode + "')";
            }
            if (bigsort.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (BigSort ='" + bigsort + "')";
            }
            if (smallsort.ToString() != "")
            {
                sqlWhere = sqlWhere + "and (SmallSort ='" + smallsort + "')";
            }
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_TechnologyManage", "ID DESC");
        }

        public static DataTable GetMeasureOfSingle(string Id)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_TechnologyManage where ID='" + Id + "'");
        }

        public static DataTable GetModelByGuid(string guid)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_TechnologyManage where TechGuid='" + guid + "'");
        }

        public static int MeasureAdd(MeasureDataInfo objInfo)
        {
            string str = "";
            str = "insert into Prj_TechnologyManage (ID,PrjCode,SerialNumber,ItemName,AccessoriesName,Remark,";
            string str2 = str;
            string str3 = str2 + " BigSort,SmallSort,TechGuid,FlowState,JoinPerson,ReceivePerson) values('" + objInfo.Id.ToString() + "','" + objInfo.PrjCode + "','" + objInfo.SerialNumber + "','";
            str = str3 + objInfo.ItemName + "','" + objInfo.AccessoriesName + "','" + objInfo.Remark + "','" + objInfo.BigSort.ToString() + "','" + objInfo.SmallSort.ToString() + "','" + objInfo.TechGuid + "'";
            int flowState = objInfo.FlowState;
            object obj2 = str;
            string str4 = string.Concat(new object[] { obj2, ",", objInfo.FlowState, "," });
            return publicDbOpClass.ExecSqlString(str4 + "'" + objInfo.JoinPerson.ToString() + "','" + objInfo.ReceivePerson.ToString() + "')");
        }

        public static int MeasureDel(string Id)
        {
            string str = "";
            str = "begin";
            return publicDbOpClass.ExecSqlString(((str + " delete from XPM_Basic_AnnexList where ModuleID='1725' and RecordCode IN ('" + Id + "')") + " delete from Prj_TechnologyManage where ID IN ('" + Id + "')") + " end");
        }

        public static int MeasureUpd(MeasureDataInfo objInfo)
        {
            string str2 = "update Prj_TechnologyManage set ItemName='" + objInfo.ItemName + "',Remark='" + objInfo.Remark;
            return publicDbOpClass.ExecSqlString(str2 + "',JoinPerson='" + objInfo.JoinPerson + "',ReceivePerson='" + objInfo.ReceivePerson + "' where ID='" + objInfo.Id.ToString() + "'");
        }
    }
}

