namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class HROrgPostLevelAction
    {
        public int Add(HROrgPostLevel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Org_PostLevel(");
            builder.Append("Type,PostLevel,PositionLevel,PostAndRank,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.Type + "',");
            builder.Append(model.PostLevel + ",");
            builder.Append(model.PositionLevel + ",");
            builder.Append("'" + model.PostAndRank + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Org_PostLevel ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int PostLevel, int PositionLevel, string t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { " select top 1 RecordID from HR_Org_PostLevel where PositionLevel='", PositionLevel, "' and PostLevel='", PostLevel, "' and Type='", t, "' " }));
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,Type,PostLevel,PositionLevel,PostAndRank,Remark ");
            builder.Append(" FROM HR_Org_PostLevel ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(HROrgPostLevel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Org_PostLevel set ");
            builder.Append("PostLevel='" + model.PostLevel + "',");
            builder.Append("PositionLevel='" + model.PositionLevel + "',");
            builder.Append("PostAndRank='" + model.PostAndRank + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

