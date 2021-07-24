namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ptOfficeResDepotAction
    {
        public int Add(ptOfficeResDepot model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_Depot(");
            builder.Append("CorpCode,DepotName,Remark,IsValid");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.DepotName + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.IsValid + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int DepotID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_Depot ");
            builder.Append(" where DepotID=" + DepotID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int DepotID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 DepotID from OA_OfficeRes_Depot where DepotID=" + DepotID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DepotID,CorpCode,DepotName,Remark,IsValid ");
            builder.Append(" FROM OA_OfficeRes_Depot ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResDepot GetModel(int DepotID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" DepotID,CorpCode,DepotName,Remark,IsValid ");
            builder.Append(" from OA_OfficeRes_Depot ");
            builder.Append(" where DepotID=" + DepotID);
            ptOfficeResDepot depot = new ptOfficeResDepot();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["DepotID"].ToString() != "")
            {
                depot.DepotID = int.Parse(set.Tables[0].Rows[0]["DepotID"].ToString());
            }
            depot.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            depot.DepotName = set.Tables[0].Rows[0]["DepotName"].ToString();
            depot.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            depot.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            return depot;
        }

        public int Update(ptOfficeResDepot model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_Depot set ");
            builder.Append("CorpCode='" + model.CorpCode + "',");
            builder.Append("DepotName='" + model.DepotName + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("IsValid='" + model.IsValid + "'");
            builder.Append(" where DepotID=" + model.DepotID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

