namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ptOfficeResClassAction
    {
        public int Add(ptOfficeResClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_Class(");
            builder.Append("TypeCode,TypeName,Remark,IsValid,UserCode,RecordDate,i_ChildNum");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.TypeCode + "',");
            builder.Append("'" + model.TypeName + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append(model.i_ChildNum);
            builder.Append(")");
            builder.Append(" update OA_OfficeRes_Class set i_ChildNum=i_ChildNum+1 where TypeCode=left('" + model.TypeCode + "',len('" + model.TypeCode + "')-4) ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string TypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete OA_OfficeRes_Class ");
            builder.Append(" where TypeCode='" + TypeCode + "' ");
            builder.Append(" update OA_OfficeRes_Class set i_ChildNum=i_ChildNum-1 where TypeCode=left('" + TypeCode + "',len('" + TypeCode + "')-4) ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(string TypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 TypeCode from OA_OfficeRes_Class where TypeCode='" + TypeCode + "'");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeCode,left(TypeCode,len(TypeCode)-4) as ParentCode,TypeName,Remark,i_xh,IsValid,UserCode,RecordDate,i_ChildNum ");
            builder.Append(" FROM OA_OfficeRes_Class ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ptOfficeResClass GetModel(string TypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select TypeCode,TypeName,Remark,i_xh,IsValid,UserCode,RecordDate,i_ChildNum ");
            builder.Append(" from OA_OfficeRes_Class ");
            builder.Append(" where TypeCode='" + TypeCode + "'");
            ptOfficeResClass class2 = new ptOfficeResClass();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            class2.TypeCode = set.Tables[0].Rows[0]["TypeCode"].ToString();
            class2.TypeName = set.Tables[0].Rows[0]["TypeName"].ToString();
            class2.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            if (set.Tables[0].Rows[0]["i_xh"].ToString() != "")
            {
                class2.i_xh = int.Parse(set.Tables[0].Rows[0]["i_xh"].ToString());
            }
            class2.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            class2.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                class2.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            if (set.Tables[0].Rows[0]["i_ChildNum"].ToString() != "")
            {
                class2.i_ChildNum = int.Parse(set.Tables[0].Rows[0]["i_ChildNum"].ToString());
            }
            return class2;
        }

        public int Update(ptOfficeResClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update OA_OfficeRes_Class set ");
            builder.Append(" TypeName='" + model.TypeName + "', ");
            builder.Append(" Remark='" + model.Remark + "', ");
            builder.Append(" UserCode='" + model.UserCode + "', ");
            builder.Append(" RecordDate='" + model.RecordDate + "' ");
            builder.Append(" where TypeCode='" + model.TypeCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

