namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class HROrgAveragePayAction
    {
        public int Add(ArrayList arr)
        {
            StringBuilder builder = new StringBuilder();
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    builder.Append(string.Concat(new object[] { " if not exists(select top 1 ClassID from HR_Org_AveragePay where RecordID='", ((HROrgAveragePay) arr[i]).RecordID, "' and ClassID='", ((HROrgAveragePay) arr[i]).ClassID, "') " }));
                    builder.Append(" begin ");
                    builder.Append(" insert into HR_Org_AveragePay( ");
                    builder.Append(" RecordID,ClassID,Month1,Month2,Month3,Month4,Month5,Month6,Month7,Month8,Month9,Month10,Month11,Month12,AveragePay,Remark ");
                    builder.Append(" ) ");
                    builder.Append(" values ( ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).RecordID + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).ClassID + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month1 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month2 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month3 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month4 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month5 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month6 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month7 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month8 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month9 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month10 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month11 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Month12 + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).AveragePay + "', ");
                    builder.Append(" '" + ((HROrgAveragePay) arr[i]).Remark + "' ");
                    builder.Append(" ) ");
                    builder.Append(" end ");
                    builder.Append(" else ");
                    builder.Append(" begin ");
                    builder.Append(" update HR_Org_AveragePay set ");
                    builder.Append(" Month1='" + ((HROrgAveragePay) arr[i]).Month1 + "', ");
                    builder.Append(" Month2='" + ((HROrgAveragePay) arr[i]).Month2 + "', ");
                    builder.Append(" Month3='" + ((HROrgAveragePay) arr[i]).Month3 + "', ");
                    builder.Append(" Month4='" + ((HROrgAveragePay) arr[i]).Month4 + "', ");
                    builder.Append(" Month5='" + ((HROrgAveragePay) arr[i]).Month5 + "', ");
                    builder.Append(" Month6='" + ((HROrgAveragePay) arr[i]).Month6 + "', ");
                    builder.Append(" Month7='" + ((HROrgAveragePay) arr[i]).Month7 + "', ");
                    builder.Append(" Month8='" + ((HROrgAveragePay) arr[i]).Month8 + "', ");
                    builder.Append(" Month9='" + ((HROrgAveragePay) arr[i]).Month9 + "', ");
                    builder.Append(" Month10='" + ((HROrgAveragePay) arr[i]).Month10 + "', ");
                    builder.Append(" Month11='" + ((HROrgAveragePay) arr[i]).Month11 + "', ");
                    builder.Append(" Month12='" + ((HROrgAveragePay) arr[i]).Month12 + "', ");
                    builder.Append(" AveragePay='" + ((HROrgAveragePay) arr[i]).AveragePay + "', ");
                    builder.Append(" Remark='" + ((HROrgAveragePay) arr[i]).Remark + "' ");
                    builder.Append(string.Concat(new object[] { " where RecordID='", ((HROrgAveragePay) arr[i]).RecordID, "' and ClassID='", ((HROrgAveragePay) arr[i]).ClassID, "' " }));
                    builder.Append(" end ");
                }
            }
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" delete HR_Org_AveragePay ");
            builder.Append(" where ID=" + ID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select count(1) from HR_Org_AveragePay where ID='" + ID + "' ");
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetHRAveragePay(Guid RecordID, string classID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,RecordID,ClassID,Month1,Month2,Month3,Month4,Month5,Month6,Month7,Month8,Month9,Month10,Month11,Month12,AveragePay,Remark ");
            builder.Append(" FROM HR_Org_AveragePay ");
            builder.Append(string.Concat(new object[] { " where RecordID='", RecordID, "' and ClassID='", classID, "' " }));
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,RecordID,ClassID,Month1,Month2,Month3,Month4,Month5,Month6,Month7,Month8,Month9,Month10,Month11,Month12,AveragePay,Remark ");
            builder.Append(" FROM HR_Org_AveragePay ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public HROrgAveragePay GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *  ");
            builder.Append(" ID,RecordID,ClassID,Month1,Month2,Month3,Month4,Month5,Month6,Month7,Month8,Month9,Month10,Month11,Month12,AveragePay,Remark ");
            builder.Append(" from HR_Org_AveragePay ");
            builder.Append(" where ID=" + ID);
            HROrgAveragePay pay = new HROrgAveragePay();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ID"].ToString() != "")
            {
                pay.ID = int.Parse(set.Tables[0].Rows[0]["ID"].ToString());
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                pay.RecordID = new Guid(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                pay.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month1"].ToString() != "")
            {
                pay.Month1 = decimal.Parse(set.Tables[0].Rows[0]["Month1"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month2"].ToString() != "")
            {
                pay.Month2 = decimal.Parse(set.Tables[0].Rows[0]["Month2"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month3"].ToString() != "")
            {
                pay.Month3 = decimal.Parse(set.Tables[0].Rows[0]["Month3"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month4"].ToString() != "")
            {
                pay.Month4 = decimal.Parse(set.Tables[0].Rows[0]["Month4"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month5"].ToString() != "")
            {
                pay.Month5 = decimal.Parse(set.Tables[0].Rows[0]["Month5"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month6"].ToString() != "")
            {
                pay.Month6 = decimal.Parse(set.Tables[0].Rows[0]["Month6"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month7"].ToString() != "")
            {
                pay.Month7 = decimal.Parse(set.Tables[0].Rows[0]["Month7"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month8"].ToString() != "")
            {
                pay.Month8 = decimal.Parse(set.Tables[0].Rows[0]["Month8"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month9"].ToString() != "")
            {
                pay.Month9 = decimal.Parse(set.Tables[0].Rows[0]["Month9"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month10"].ToString() != "")
            {
                pay.Month10 = decimal.Parse(set.Tables[0].Rows[0]["Month10"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month11"].ToString() != "")
            {
                pay.Month11 = decimal.Parse(set.Tables[0].Rows[0]["Month11"].ToString());
            }
            if (set.Tables[0].Rows[0]["Month12"].ToString() != "")
            {
                pay.Month12 = decimal.Parse(set.Tables[0].Rows[0]["Month12"].ToString());
            }
            if (set.Tables[0].Rows[0]["AveragePay"].ToString() != "")
            {
                pay.AveragePay = decimal.Parse(set.Tables[0].Rows[0]["AveragePay"].ToString());
            }
            pay.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return pay;
        }

        public int Update(HROrgAveragePay model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update HR_Org_AveragePay set ");
            builder.Append(" ClassID='" + model.ClassID + "', ");
            builder.Append(" Month1='" + model.Month1 + "', ");
            builder.Append(" Month2='" + model.Month2 + "', ");
            builder.Append(" Month3='" + model.Month3 + "', ");
            builder.Append(" Month4='" + model.Month4 + "', ");
            builder.Append(" Month5='" + model.Month5 + "', ");
            builder.Append(" Month6='" + model.Month6 + "', ");
            builder.Append(" Month7='" + model.Month7 + "', ");
            builder.Append(" Month8='" + model.Month8 + "', ");
            builder.Append(" Month9='" + model.Month9 + "', ");
            builder.Append(" Month10='" + model.Month10 + "', ");
            builder.Append(" Month11='" + model.Month11 + "', ");
            builder.Append(" Month12='" + model.Month12 + "', ");
            builder.Append(" AveragePay='" + model.AveragePay + "', ");
            builder.Append(" Remark='" + model.Remark + "' ");
            builder.Append(" where ID='" + model.ID + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

