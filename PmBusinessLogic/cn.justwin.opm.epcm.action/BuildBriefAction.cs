namespace cn.justwin.opm.epcm.action
{
    using cn.justwin.opm.epcm.model;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class BuildBriefAction
    {
        public bool Add(BuildBriefModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into opm_epcm_brief(briefid,sn,type,record,buildsituation, ");
            builder.Append("manasituation,problem,adduser,addtime,remark,prjid,flowstate,briefType) ");
            builder.Append("values(");
            builder.Append("'" + model.BriefId + "',");
            builder.Append("'" + model.SN + "',");
            builder.Append("'" + model.Type + "',");
            builder.Append("'" + model.Record + "',");
            builder.Append("'" + model.BuildSituation + "',");
            builder.Append("'" + model.ManaSituation + "',");
            builder.Append("'" + model.Problem + "',");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.PrjId + "',");
            builder.Append(model.FlowState + ",");
            builder.Append("'" + model.BriefType + "')");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(string briefId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from  opm_epcm_brief  ");
            builder.Append("where briefId in(" + briefId + ")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public DataTable Exists(string sn, string pc)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from  opm_epcm_brief  ");
            builder.Append("where sn='" + sn + "' ");
            builder.Append("and prjId='" + pc + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetBriefBypc(string pc, string type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select b.*,p.prjname from  opm_epcm_brief  b inner join pt_prjinfo as p on p.prjguid=b.prjid  ");
            builder.Append("where prjId='" + pc + "' and type='" + type + "' order by addtime desc");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetBriefByType(string pc, string briefType, string type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select b.*,p.prjname from  opm_epcm_brief  b inner join pt_prjinfo as p on p.prjguid=b.prjid  ");
            builder.Append("where prjId='" + pc + "' and type='" + type + "'");
            if (briefType != "")
            {
                builder.Append("and briefType like '%" + briefType + "%' ");
            }
            builder.Append("and flowState=1");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetModel(string briefId, string pc)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select b.*,p.prjname,p.PrjGuid from  opm_epcm_brief  as b inner join pt_prjinfo as p on p.prjguid=b.prjid ");
            builder.Append("where briefId='" + briefId + "' ");
            if (pc != "")
            {
                builder.Append("and prjId='" + pc + "'");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public bool Update(BuildBriefModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update  opm_epcm_brief  set ");
            builder.Append("sn='" + model.SN + "',");
            builder.Append("type='" + model.Type + "',");
            builder.Append("record='" + model.Record + "',");
            builder.Append("buildsituation='" + model.BuildSituation + "',");
            builder.Append("manasituation='" + model.ManaSituation + "',");
            builder.Append("problem='" + model.Problem + "',");
            builder.Append("adduser='" + model.AddUser + "',");
            builder.Append("addtime='" + model.AddTime + "',");
            builder.Append("remark='" + model.Remark + "',");
            builder.Append("prjid='" + model.PrjId + "',");
            builder.Append("brieftype='" + model.BriefType + "',");
            builder.Append("flowstate=" + model.FlowState + " ");
            builder.Append("where briefid='" + model.BriefId + "'");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

