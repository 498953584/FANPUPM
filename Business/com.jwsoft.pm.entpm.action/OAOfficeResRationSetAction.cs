namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OAOfficeResRationSetAction
    {
        public int Add(OAOfficeResRationSet model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_RationSet(");
            builder.Append("RationType,RecordCode,Ration,Remark,UserCode,RecordDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RationType + "',");
            builder.Append("'" + model.RecordCode + "',");
            builder.Append(model.Ration + ",");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_RationSet ");
            builder.Append(" where ID=" + ID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 ID from OA_OfficeRes_RationSet where ID=" + ID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RationType,RecordCode,Ration,Remark,UserCode,RecordDate ");
            builder.Append(" FROM OA_OfficeRes_RationSet ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResRationSet GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" RationType,RecordCode,Ration,Remark,UserCode,RecordDate ");
            builder.Append(" from OA_OfficeRes_RationSet ");
            builder.Append(" where ID=" + ID);
            OAOfficeResRationSet set = new OAOfficeResRationSet();
            DataSet set2 = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set2.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            set.RationType = set2.Tables[0].Rows[0]["RationType"].ToString();
            set.RecordCode = set2.Tables[0].Rows[0]["RecordCode"].ToString();
            if (set2.Tables[0].Rows[0]["Ration"].ToString() != "")
            {
                set.Ration = decimal.Parse(set2.Tables[0].Rows[0]["Ration"].ToString());
            }
            set.Remark = set2.Tables[0].Rows[0]["Remark"].ToString();
            set.UserCode = set2.Tables[0].Rows[0]["UserCode"].ToString();
            if (set2.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                set.RecordDate = DateTime.Parse(set2.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            return set;
        }

        public int Update(OAOfficeResRationSet model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_RationSet set ");
            builder.Append("RationType='" + model.RationType + "',");
            builder.Append("RecordCode='" + model.RecordCode + "',");
            builder.Append("Ration=" + model.Ration + ",");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "'");
            builder.Append(" where RecordCode=" + model.RecordCode);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Update(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    builder.Append(" if not exists(select top 1 RationType from OA_OfficeRes_RationSet where RationType='" + ((OAOfficeResRationSet) arr[i]).RationType + "' and RecordCode='" + ((OAOfficeResRationSet) arr[i]).RecordCode + "') ");
                    builder.Append(" begin ");
                    builder.Append(" insert into OA_OfficeRes_RationSet( ");
                    builder.Append(" RationType,RecordCode,Ration,Remark,UserCode,RecordDate ");
                    builder.Append(" ) ");
                    builder.Append(" values ( ");
                    builder.Append(" '" + ((OAOfficeResRationSet) arr[i]).RationType + "', ");
                    builder.Append(" '" + ((OAOfficeResRationSet) arr[i]).RecordCode + "', ");
                    builder.Append(" " + ((OAOfficeResRationSet) arr[i]).Ration + ", ");
                    builder.Append(" '" + ((OAOfficeResRationSet) arr[i]).Remark + "', ");
                    builder.Append(" '" + ((OAOfficeResRationSet) arr[i]).UserCode + "', ");
                    builder.Append(" '" + ((OAOfficeResRationSet) arr[i]).RecordDate + "' ");
                    builder.Append(" ) ");
                    builder.Append(" end ");
                    builder.Append(" else ");
                    builder.Append(" begin ");
                    builder.Append(" update OA_OfficeRes_RationSet set ");
                    builder.Append(" Ration=" + ((OAOfficeResRationSet) arr[i]).Ration + ", ");
                    builder.Append(" Remark='" + ((OAOfficeResRationSet) arr[i]).Remark + "' ");
                    builder.Append(" where ");
                    builder.Append(" RationType='" + ((OAOfficeResRationSet) arr[i]).RationType + "' ");
                    builder.Append(" and RecordCode='" + ((OAOfficeResRationSet) arr[i]).RecordCode + "' ");
                    builder.Append(" end ");
                }
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

