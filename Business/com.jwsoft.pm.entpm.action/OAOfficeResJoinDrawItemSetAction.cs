namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class OAOfficeResJoinDrawItemSetAction
    {
        public int Add(OAOfficeResJoinDrawItemSet model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_JoinDrawItemSet(");
            builder.Append("PostLevel,DrawItemID,Number,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.PostLevel + ",");
            builder.Append(model.DrawItemID + ",");
            builder.Append(model.Number + ",");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int PostLevel, int DrawItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_JoinDrawItemSet ");
            builder.Append(string.Concat(new object[] { " where DrawItemID='", DrawItemID, "' and PostLevel='", PostLevel, "'" }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int PostLevel, int DrawItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { "select top 1 DrawItemID from OA_OfficeRes_JoinDrawItemSet where PostLevel=", PostLevel, " and DrawItemID=", DrawItemID }));
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select a.PostLevel,a.DrawItemID,a.Number,a.Remark,c.ResName,c.UseType,c.GetType,c.Unit,c.PlanFee ");
            builder.Append(" from OA_OfficeRes_JoinDrawItemSet a ");
            builder.Append(" join OA_OfficeRes_Resources c on a.DrawItemID=c.RecordID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResJoinDrawItemSet GetModel(int DrawItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" PostLevel,DrawItemID,Number,Remark ");
            builder.Append(" from OA_OfficeRes_JoinDrawItemSet ");
            builder.Append(" where DrawItemID=" + DrawItemID);
            OAOfficeResJoinDrawItemSet set = new OAOfficeResJoinDrawItemSet();
            DataSet set2 = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set2.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set2.Tables[0].Rows[0]["PostLevel"].ToString() != "")
            {
                set.PostLevel = int.Parse(set2.Tables[0].Rows[0]["PostLevel"].ToString());
            }
            if (set2.Tables[0].Rows[0]["DrawItemID"].ToString() != "")
            {
                set.DrawItemID = int.Parse(set2.Tables[0].Rows[0]["DrawItemID"].ToString());
            }
            if (set2.Tables[0].Rows[0]["Number"].ToString() != "")
            {
                set.Number = int.Parse(set2.Tables[0].Rows[0]["Number"].ToString());
            }
            set.Remark = set2.Tables[0].Rows[0]["Remark"].ToString();
            return set;
        }

        public int Update(OAOfficeResJoinDrawItemSet model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_JoinDrawItemSet set ");
            builder.Append("Number=" + model.Number + ",");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where DrawItemID=" + model.DrawItemID);
            builder.Append(" and PostLevel=" + model.PostLevel);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

