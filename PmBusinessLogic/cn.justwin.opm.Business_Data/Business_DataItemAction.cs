namespace cn.justwin.opm.Business_Data
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class Business_DataItemAction
    {
        public bool Add(Business_DataItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into OPM_Business_DataItem(UID,CodeID ,PCode ,PName ,FlowState ,DesignDate ,Remark ,AddUser ,AddTime,IsValid)");
            builder.Append(" values( ");
            builder.Append(" '" + model.UID + "', ");
            builder.Append(" '" + model.CodeId + "', ");
            builder.Append(" '" + model.PCode + "', ");
            builder.Append(" '" + model.PName + "', ");
            builder.Append(" " + model.FlowState + ", ");
            DateTime designDate = model.DesignDate;
            builder.Append(" '" + model.DesignDate + "', ");
            builder.Append(" '" + model.Remark + "', ");
            builder.Append(" '" + model.AddUser + "', ");
            builder.Append(" '" + model.AddTime + "', ");
            builder.Append(" '" + model.IsValid + "')");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public string ChkFlowState(string codeId, string prjid)
        {
            if (publicDbOpClass.DataTableQuary("select * from opm_business_dataitem where flowstate=1 and codeid=" + codeId + " and prjid='" + prjid + "' ").Rows.Count > 0)
            {
                return "1";
            }
            return "0";
        }

        public bool Delete(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete from OPM_Business_DataItem where uid in (" + uid + ")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public DataTable GetCodeID(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select CodeID  from OPM_Business_DataItem ");
            builder.Append(" where uid= '" + uid + "' and isvalid=1 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetListByBusType(string sqlWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from (select a.*,(select Designer from OPM_Business_Data where UID=a.CodeID) as Designer, ");
            builder.Append(" (select top 1 operator from wf_instance where id=(select id from wf_instance_main where instanceCode=a.uid) order by theorder desc) as Operator, ");
            builder.Append(" (select top 1 AuditDate from wf_instance where id=(select id from wf_instance_main where instanceCode=a.uid) order by theorder desc) as ExamineDate ");
            builder.Append(" from OPM_Business_DataItem  a  ");
            if ((sqlWhere != null) && (sqlWhere != ""))
            {
                builder.Append(sqlWhere);
            }
            builder.Append(" and isvalid='1' ) as newtable  ");
            builder.Append("order by AddTime desc  ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetModel(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select *  from OPM_Business_DataItem ");
            builder.Append(" where uid= '" + uid + "' and isvalid=1 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public bool Update(Business_DataItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OPM_Business_DataItem set  ");
            builder.Append(" CodeID='" + model.CodeId + "',");
            builder.Append(" PCode='" + model.PCode + "',");
            builder.Append(" PName='" + model.PName + "',");
            DateTime designDate = model.DesignDate;
            builder.Append(" DesignDate='" + model.DesignDate + "',");
            builder.Append(" Remark='" + model.Remark + "'");
            builder.Append(" where UID='" + model.UID + "'");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

