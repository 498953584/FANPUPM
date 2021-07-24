namespace cn.justwin.opm
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class PrjStageSetAction
    {
        public bool Add(PrjStageSetModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("begin ");
            builder.Append(" update OPM_Prj_StageSet set childcount = childcount+1 where StageCode = '" + model.ParentStageCode + "'");
            builder.Append(" and prjid = '" + model.PrjID + "'");
            builder.Append(" insert into OPM_Prj_StageSet(PrjStageID,PrjID,StageCode,ParentStageCode,PrjStageName,Podepom,IsFlow,I_xh,");
            builder.Append("AddUser,AddTime,ReadWrite,Remark,ParentID,ChildCount)");
            builder.Append(" values (");
            builder.Append("'" + model.PrjStageID + "',");
            builder.Append("'" + model.PrjID + "',");
            builder.Append("'" + model.StageCode + "',");
            builder.Append("'" + model.ParentStageCode + "',");
            builder.Append("'" + model.PrjStageName + "',");
            builder.Append("'" + model.Podepom + "',");
            builder.Append("'" + model.IsFlow + "',");
            builder.Append(model.I_xh1 + ",");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("'" + model.ReadWrite + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.ParentID + "',");
            builder.Append(model.ChildCount);
            builder.Append(")");
            builder.Append(" end");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(string prjStageId, string pc)
        {
            string str2 = "";
            return publicDbOpClass.NonQuerySqlString(str2 + " delete from OPM_Prj_StageSet where PrjStageID in " + prjStageId + "   and PrjID='" + pc + "'");
        }

        public DataTable ExistsTemp(string stageId, string prjId)
        {
            StringBuilder builder = new StringBuilder();
            if (prjId == "00000000-0000-0000-0000-000000000000")
            {
                builder.Append("select * from opm_prj_stageset where prjstageid in " + stageId + " and prjid<>'" + prjId + "'");
            }
            else
            {
                builder.Append("select * from opm_prj_stageinfo where prjstageid in " + stageId + " and prjid='" + prjId + "'");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetFlowState(string stageId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select isflow from opm_prj_stageset where prjstageid='" + stageId + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetModel(string stageId, string pc)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from opm_prj_stageset ");
            builder.Append("where prjstageid='" + stageId + "' and prjId='" + pc + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPrjList(string prjID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from OPM_Prj_StageSet ");
            builder.Append(" where PrjID='" + prjID + "' order by I_xh  ASC");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPrjListByParentId(string stageCode, string pc)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OPM_Prj_StageSet where ParentStageCode='" + stageCode + "' and prjid='" + pc + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public bool UpChildCount(string stageCode, string prjId)
        {
            string str5 = "select prjid,parentStageCode from OPM_Prj_StageSet ";
            DataTable table = publicDbOpClass.DataTableQuary(str5 + " where stageCode = '" + stageCode + "' and prjid='" + prjId + "'");
            string str2 = table.Rows[0]["prjid"].ToString();
            string str3 = table.Rows[0]["parentStageCode"].ToString();
            string str6 = "";
            return publicDbOpClass.NonQuerySqlString(str6 + " update OPM_Prj_StageSet set childcount = childcount-1 where StageCode = '" + str3 + "' and prjid='" + str2 + "'");
        }

        public bool Update(PrjStageSetModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OPM_Prj_StageSet set ");
            builder.Append("PrjStageName='" + model.PrjStageName + "',");
            builder.Append("IsFlow='" + model.IsFlow + "',");
            builder.Append("ReadWrite='" + model.ReadWrite + "',");
            builder.Append("I_xh=" + model.I_xh1 + ",");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where PrjStageID='" + model.PrjStageID + "'");
            builder.Append(" and PrjID='" + model.PrjID + "'");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

