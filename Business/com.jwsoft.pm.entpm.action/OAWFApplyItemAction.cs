namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAWFApplyItemAction
    {
        public int Add(OAWFApplyItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_WF_ApplyItem(");
            builder.Append("RecordID,AuditState,UserCode,RecordDate,BusinessClass,Title,Remark,OriginalName,FilePath,TemplateID");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.BusinessClass + "',");
            builder.Append("'" + model.Title + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.OriginalName + "',");
            builder.Append("'" + model.FilePath + "',");
            builder.Append(model.TemplateID);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_WF_ApplyItem ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetBusinessClassList(int TempID)
        {
            return publicDbOpClass.DataTableQuary("select * from wf_template where TemplateID = " + TempID);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,AuditState,UserCode,RecordDate,BusinessClass,Title,Remark,OriginalName,FilePath,TemplateID ");
            builder.Append(" FROM OA_WF_ApplyItem ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAWFApplyItem GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select RecordID,AuditState,UserCode,RecordDate,BusinessClass,Title,Remark,OriginalName,FilePath,TemplateID ");
            builder.Append(" from OA_WF_ApplyItem ");
            builder.Append(" where RecordID='" + RecordID + "' ");
            OAWFApplyItem item = new OAWFApplyItem();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                item.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                item.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
            }
            item.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                item.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            item.BusinessClass = set.Tables[0].Rows[0]["BusinessClass"].ToString();
            item.Title = set.Tables[0].Rows[0]["Title"].ToString();
            item.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            item.OriginalName = set.Tables[0].Rows[0]["OriginalName"].ToString();
            item.FilePath = set.Tables[0].Rows[0]["FilePath"].ToString();
            if (set.Tables[0].Rows[0]["TemplateID"].ToString() != "")
            {
                item.TemplateID = (int) set.Tables[0].Rows[0]["TemplateID"];
            }
            return item;
        }

        public int Update(OAWFApplyItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_WF_ApplyItem set ");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("Title='" + model.Title + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("OriginalName='" + model.OriginalName + "',");
            builder.Append("FilePath='" + model.FilePath + "',");
            builder.Append("TemplateID=" + model.TemplateID);
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

