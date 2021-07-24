namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class OAOfficeResJoinDrawItemAction
    {
        public int Add(OAOfficeResJoinDrawItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_OfficeRes_JoinDrawItem(");
            builder.Append("UserCode,DrawItemID,FactNumber,SumFee,DrawDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.UserCode + "',");
            builder.Append(model.DrawItemID + ",");
            builder.Append(model.FactNumber + ",");
            builder.Append(model.SumFee + ",");
            builder.Append("'" + model.DrawDate + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Add(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    builder.Append(string.Concat(new object[] { " if not exists(select top 1 UserCode from OA_OfficeRes_JoinDrawItem where UserCode='", ((OAOfficeResJoinDrawItem) arr[i]).UserCode, "' and DrawItemID='", ((OAOfficeResJoinDrawItem) arr[i]).DrawItemID, "') " }));
                    builder.Append(" begin ");
                    builder.Append(" insert into OA_OfficeRes_JoinDrawItem( ");
                    builder.Append(" UserCode,DrawItemID,FactNumber,SumFee,DrawDate ");
                    builder.Append(" )");
                    builder.Append(" values ( ");
                    builder.Append(" '" + ((OAOfficeResJoinDrawItem) arr[i]).UserCode + "', ");
                    builder.Append(" " + ((OAOfficeResJoinDrawItem) arr[i]).DrawItemID + ", ");
                    builder.Append(" " + ((OAOfficeResJoinDrawItem) arr[i]).FactNumber + ", ");
                    builder.Append(" " + ((OAOfficeResJoinDrawItem) arr[i]).SumFee + ", ");
                    builder.Append(" '" + ((OAOfficeResJoinDrawItem) arr[i]).DrawDate + "' ");
                    builder.Append(" ) ");
                    builder.Append(" end ");
                    builder.Append(" else ");
                    builder.Append(" begin ");
                    builder.Append(" update OA_OfficeRes_JoinDrawItem set ");
                    builder.Append(" FactNumber=" + ((OAOfficeResJoinDrawItem) arr[i]).FactNumber + ", ");
                    builder.Append(" SumFee=" + ((OAOfficeResJoinDrawItem) arr[i]).SumFee + " ");
                    builder.Append(string.Concat(new object[] { " where UserCode='", ((OAOfficeResJoinDrawItem) arr[i]).UserCode, "' and DrawItemID='", ((OAOfficeResJoinDrawItem) arr[i]).DrawItemID, "' " }));
                    builder.Append(" end ");
                }
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(string UserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_OfficeRes_JoinDrawItem ");
            builder.Append(" where UserCode='" + UserCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(string UserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 UserCode from OA_OfficeRes_JoinDrawItem where UserCode='" + UserCode + "'");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserCode,DrawItemID,FactNumber,SumFee,DrawDate ");
            builder.Append(" FROM OA_OfficeRes_JoinDrawItem ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OAOfficeResJoinDrawItem GetModel(string UserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" UserCode,DrawItemID,FactNumber,SumFee,DrawDate ");
            builder.Append(" from OA_OfficeRes_JoinDrawItem ");
            builder.Append(" where UserCode='" + UserCode + "'");
            OAOfficeResJoinDrawItem item = new OAOfficeResJoinDrawItem();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            item.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["DrawItemID"].ToString() != "")
            {
                item.DrawItemID = int.Parse(set.Tables[0].Rows[0]["DrawItemID"].ToString());
            }
            if (set.Tables[0].Rows[0]["FactNumber"].ToString() != "")
            {
                item.FactNumber = int.Parse(set.Tables[0].Rows[0]["FactNumber"].ToString());
            }
            if (set.Tables[0].Rows[0]["SumFee"].ToString() != "")
            {
                item.SumFee = decimal.Parse(set.Tables[0].Rows[0]["SumFee"].ToString());
            }
            if (set.Tables[0].Rows[0]["DrawDate"].ToString() != "")
            {
                item.DrawDate = DateTime.Parse(set.Tables[0].Rows[0]["DrawDate"].ToString());
            }
            return item;
        }

        public int Update(OAOfficeResJoinDrawItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_OfficeRes_JoinDrawItem set ");
            builder.Append("FactNumber=" + model.FactNumber + ",");
            builder.Append("SumFee=" + model.SumFee + ",");
            builder.Append("DrawDate='" + model.DrawDate + "'");
            builder.Append(" where UserCode='" + model.UserCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

