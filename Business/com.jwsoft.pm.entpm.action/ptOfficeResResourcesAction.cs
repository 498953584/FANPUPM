namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ptOfficeResResourcesAction
    {
        public int Add(ptOfficeResResources model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_Resources(");
            builder.Append("TypeCode,ResCode,ResName,UseType,GetType,Unit,PlanFee");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.TypeCode + "',");
            builder.Append("'" + model.ResCode + "',");
            builder.Append("'" + model.ResName + "',");
            builder.Append("'" + model.UseType + "',");
            builder.Append("'" + model.GetType + "',");
            builder.Append("'" + model.Unit + "',");
            builder.Append(model.PlanFee);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_Resources ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 RecordID from OA_OfficeRes_Resources where RecordID=" + RecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,TypeCode,ResCode,ResName,UseType,GetType,Unit,PlanFee ");
            builder.Append(" FROM OA_OfficeRes_Resources ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResResources GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select RecordID,TypeCode,ResCode,ResName,UseType,GetType,Unit,PlanFee ");
            builder.Append(" from OA_OfficeRes_Resources ");
            builder.Append(" where RecordID=" + RecordID + " ");
            ptOfficeResResources resources = new ptOfficeResResources();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                resources.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            resources.TypeCode = set.Tables[0].Rows[0]["TypeCode"].ToString();
            resources.ResCode = set.Tables[0].Rows[0]["ResCode"].ToString();
            resources.ResName = set.Tables[0].Rows[0]["ResName"].ToString();
            resources.UseType = set.Tables[0].Rows[0]["UseType"].ToString();
            resources.GetType = set.Tables[0].Rows[0]["GetType"].ToString();
            resources.Unit = set.Tables[0].Rows[0]["Unit"].ToString();
            if (set.Tables[0].Rows[0]["PlanFee"].ToString() != "")
            {
                resources.PlanFee = decimal.Parse(set.Tables[0].Rows[0]["PlanFee"].ToString());
            }
            return resources;
        }

        public int Update(ptOfficeResResources model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_Resources set ");
            builder.Append("ResName='" + model.ResName + "',");
            builder.Append("UseType='" + model.UseType + "',");
            builder.Append("GetType='" + model.GetType + "',");
            builder.Append("Unit='" + model.Unit + "',");
            builder.Append("PlanFee=" + model.PlanFee);
            builder.Append(" where RecordID=" + model.RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

