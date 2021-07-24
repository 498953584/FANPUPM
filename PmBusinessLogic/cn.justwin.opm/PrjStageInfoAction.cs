namespace cn.justwin.opm
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class PrjStageInfoAction
    {
        public bool Add(PrjStageInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OPM_Prj_StageInfo(InfoID, PrjStageID,InfoCode,InfoName,InfoContent,FlowState,IsValid,IsFile,FileType,AddUser,AddTime,PrjID,Remark)");
            builder.Append(" values(");
            builder.Append("'" + model.InfoID + "',");
            builder.Append("'" + model.PrjStageID + "',");
            builder.Append("'" + model.InfoCode + "',");
            builder.Append("'" + model.InfoName + "',");
            builder.Append("'" + model.InfoContent + "',");
            builder.Append(model.FlowState + ",");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("'" + model.IsFile + "',");
            builder.Append(model.FileType + ",");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("'" + model.PrjID + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool AddPrepareDoc(PrjStageInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OPM_Prj_StageInfo(InfoID, PrjStageID,InfoCode,InfoName,InfoContent,FlowState,IsValid,IsFile,FileType,AddUser,AddTime,Remark,CodeID,BusinessName)");
            builder.Append(" values(");
            builder.Append("'" + model.InfoID + "',");
            builder.Append("'" + model.PrjStageID + "',");
            builder.Append("'" + model.InfoCode + "',");
            builder.Append("'" + model.InfoName + "',");
            builder.Append("'" + model.InfoContent + "',");
            builder.Append("-1,");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("'" + model.IsFile + "',");
            builder.Append(model.FileType + ",");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append(model.CodeID + ",");
            builder.Append("'" + model.BusinessName + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool AddPriceDoc(PrjStageInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OPM_Prj_StageInfo(InfoID, PrjStageID,InfoCode,InfoName,InfoContent,FlowState,IsValid,IsFile,FileType,AddUser,AddTime,Remark,BusinessName,codeID,corpID)");
            builder.Append(" values(");
            builder.Append("'" + model.InfoID + "',");
            builder.Append("'" + model.PrjStageID + "',");
            builder.Append("'" + model.InfoCode + "',");
            builder.Append("'" + model.InfoName + "',");
            builder.Append("'" + model.InfoContent + "',");
            builder.Append("-1,");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("'" + model.IsFile + "',");
            builder.Append(model.FileType + ",");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.BusinessName + "',");
            builder.Append(model.CodeID + ",");
            builder.Append(model.CorpID);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(string infoID)
        {
            return publicDbOpClass.NonQuerySqlString("delete from OPM_Prj_StageInfo where InfoID='" + infoID + "'");
        }

        public bool DeleteDoc(string infoID)
        {
            return publicDbOpClass.NonQuerySqlString("delete from OPM_Prj_StageInfo where InfoID in (" + infoID + ")");
        }

        public DataTable Exists(string infoCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from opm_prj_stageinfo where infocode='" + infoCode + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable ExistsUpdate(string infoCode, string infoId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from opm_prj_stageinfo where infocode='" + infoCode + "'");
            builder.Append(" and infoid<>'" + infoId + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetCorpModelById(string infoId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OPM_Prj_StageInfo join XPM_Basic_ContactCorp on XPM_Basic_ContactCorp.corpID= OPM_Prj_StageInfo.corpID where InfoID='" + infoId + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int GetCount(string prjStageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from OPM_Prj_StageInfo");
            builder.Append(" where PrjStageID='" + prjStageId + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetModelById(string infoId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OPM_Prj_StageInfo where InfoID='" + infoId + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPriceDocInfo(string PrjStageID, int pageCount, string businessName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 10 * from ( ");
            builder.Append("select row_number() over(order by InfoID) ");
            builder.Append(" as num,* from OPM_Prj_StageInfo ");
            builder.Append(") newtable join XPM_Basic_ContactCorp ");
            builder.Append(" on newtable.CorpID=XPM_Basic_ContactCorp.CorpID");
            builder.Append(" where num > 10*(" + pageCount + "-1)");
            builder.Append(" and PrjStageID='" + PrjStageID + "'");
            builder.Append(" and businessName='" + businessName + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPrjInfoBy(string PrjCode, string PrjName, string Owner, string TOP)
        {
            string str = "select " + TOP + " prjName,prjGuid,prjManager from PT_Prjinfo where  [IsValid]=1";
            if (!string.IsNullOrEmpty(PrjCode))
            {
                str = str + " and PT_Prjinfo.PrjCode like '%" + PrjCode + "%'";
            }
            if (!string.IsNullOrEmpty(PrjName))
            {
                str = str + " and PT_Prjinfo.PrjName like '%" + PrjName + "%'";
            }
            if (!string.IsNullOrEmpty(Owner))
            {
                str = str + " and PT_Prjinfo.Owner like '%" + Owner + "%'";
            }
            return publicDbOpClass.DataTableQuary((str + "ORDER BY userDefined_Date DESC,TypeCode ASC").ToString());
        }

        public DataTable GetPrjStageInfo(string PrjStageID, int pageCount, string businessName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 10 * from ( ");
            builder.Append("select row_number() over(order by InfoID) ");
            builder.Append(" as num,* from OPM_Prj_StageInfo ");
            builder.Append(") newtable where num > 10*(" + pageCount + "-1)");
            builder.Append(" and PrjStageID='" + PrjStageID + "'");
            builder.Append(" and businessName='" + businessName + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPrjStageInfo(string PrjStageID, string pc, int pageCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from OPM_Prj_StageInfo where PrjStageID='" + PrjStageID + "'");
            builder.Append("  and PrjID in ('" + pc + "')");
            builder.Append("  order by addtime ASC");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable LoadStageByTreeNodes(string PrjStageID, int pageCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 10 * from ( ");
            builder.Append("select row_number() over(order by InfoID) ");
            builder.Append(" as num,* from OPM_Prj_StageInfo ");
            builder.Append(") newtable where num > 10*(" + pageCount + "-1)");
            builder.Append(" and PrjStageID='" + PrjStageID + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int MaxIxh()
        {
            string sqlString = "select max(i_xh) from OPM_Prj_StageInfo";
            return publicDbOpClass.ExecSqlString(sqlString);
        }

        public DataTable SearchInfos(string Code, string prjid)
        {
            string str = "select OPM_Prj_StageInfo.* ,pt_prjinfo.prjname from OPM_Prj_StageInfo join pt_prjinfo ";
            str = str + "on pt_prjinfo.prjguid=OPM_Prj_StageInfo.PrjID where 1=1  ";
            if (prjid != "")
            {
                str = str + " and OPM_Prj_StageInfo.PrjID = '" + prjid + "'";
            }
            return publicDbOpClass.DataTableQuary(str + "and InfoName  like '%" + Code.Trim() + "%'");
        }

        public DataTable SearchInfos(string Code, string prjid, string stage)
        {
            string sqlString = "select a.* ,b.prjname,c.PrjStageName from OPM_Prj_StageInfo a left join pt_prjinfo b ";
            sqlString = sqlString + "on b.prjguid=a.PrjID  left join OPM_Prj_StageSet c on a.PrjStageID=c.PrjStageID  and a.prjid=c.prjid  where 1=1  ";
            if ((prjid != "") && (prjid != "''"))
            {
                sqlString = sqlString + " and a.PrjID in (" + prjid + ")";
            }
            if (Code.Trim() != "")
            {
                sqlString = sqlString + "and a.InfoName  like '%" + Code.Trim() + "%'";
            }
            if (stage != "")
            {
                sqlString = sqlString + "and c.PrjStageName like '%" + stage + "%'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int Update(PrjStageInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update OPM_Prj_StageInfo SET");
            builder.Append(" InfoCode=");
            builder.Append("'" + model.InfoCode + "',");
            builder.Append("InfoName=");
            builder.Append("'" + model.InfoName + "',");
            builder.Append("InfoContent=");
            builder.Append("'" + model.InfoContent + "',");
            builder.Append("IsValid=");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("IsFile=");
            builder.Append("'" + model.IsFile + "',");
            builder.Append("FileType=");
            builder.Append(model.FileType + ",");
            builder.Append("AddUser=");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("AddTime=");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("Remark=");
            builder.Append("'" + model.Remark + "'");
            builder.Append(" where ");
            builder.Append("InfoID=");
            builder.Append("'" + model.InfoID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateDoc(PrjStageInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update OPM_Prj_StageInfo SET");
            builder.Append(" InfoCode=");
            builder.Append("'" + model.InfoCode + "',");
            builder.Append("InfoName=");
            builder.Append("'" + model.InfoName + "',");
            builder.Append("InfoContent=");
            builder.Append("'" + model.InfoContent + "',");
            builder.Append("FlowState=");
            builder.Append("-1,");
            builder.Append("IsValid=");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("IsFile=");
            builder.Append("'" + model.IsFile + "',");
            builder.Append("FileType=");
            builder.Append(model.FileType + ",");
            builder.Append("CorpID=");
            builder.Append(model.CorpID + ",");
            builder.Append("CodeID=");
            builder.Append(model.CodeID + ",");
            builder.Append("AddUser=");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("AddTime=");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("Remark=");
            builder.Append("'" + model.Remark + "'");
            builder.Append(" where ");
            builder.Append("InfoID=");
            builder.Append("'" + model.InfoID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

