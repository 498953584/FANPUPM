namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalaryTIAction
    {
        public int Add(SalaryTIModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_TempletItem(");
            builder.Append("TempletID,ItemID,ItemName,IsEdit,IsDisplay,Formula,TheOrder");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.TempletID + ",");
            builder.Append(model.ItemID + ",");
            builder.Append("'" + model.ItemName + "',");
            builder.Append("'" + model.IsEdit + "',");
            builder.Append("'" + model.IsDisplay + "',");
            builder.Append("'" + model.Formula + "',");
            builder.Append(model.TheOrder);
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int TempletID, int ItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_TempletItem ");
            builder.Append(string.Concat(new object[] { " where TempletID=", TempletID, " and ItemID=", ItemID, " " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public string GetFormula(int temp, long item)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select Formula from HR_Salary_TempletItem where TempletID = ", temp, " and ItemID = ", item }));
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["Formula"].ToString();
            }
            return "";
        }

        public string GetItemName(int temp, long item)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select ItemName from HR_Salary_TempletItem where TempletID = ", temp, " and ItemID = ", item }));
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["ItemName"].ToString();
            }
            return "";
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TempletID,ItemID,ItemName,IsEdit,IsDisplay,Formula,TheOrder ");
            builder.Append(" FROM HR_Salary_TempletItem ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryTIModel GetModel(int TempletID, int ItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" TempletID,ItemID,ItemName,IsEdit,IsDisplay,Formula,TheOrder ");
            builder.Append(" from HR_Salary_TempletItem ");
            builder.Append(string.Concat(new object[] { " where TempletID=", TempletID, " and ItemID=", ItemID, " " }));
            SalaryTIModel model = new SalaryTIModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["TempletID"].ToString() != "")
            {
                model.TempletID = int.Parse(set.Tables[0].Rows[0]["TempletID"].ToString());
            }
            if (set.Tables[0].Rows[0]["ItemID"].ToString() != "")
            {
                model.ItemID = int.Parse(set.Tables[0].Rows[0]["ItemID"].ToString());
            }
            model.ItemName = set.Tables[0].Rows[0]["ItemName"].ToString();
            model.IsEdit = set.Tables[0].Rows[0]["IsEdit"].ToString();
            model.IsDisplay = set.Tables[0].Rows[0]["IsDisplay"].ToString();
            model.Formula = set.Tables[0].Rows[0]["Formula"].ToString();
            if (set.Tables[0].Rows[0]["TheOrder"].ToString() != "")
            {
                model.TheOrder = new int?(int.Parse(set.Tables[0].Rows[0]["TheOrder"].ToString()));
            }
            return model;
        }

        public string StrItemName(int ItemID)
        {
            DataTable table = publicDbOpClass.DataTableQuary(" SELECT * FROM [HR_Salary_Item] where ItemID = " + ItemID);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["ItemName"].ToString();
            }
            return "";
        }

        public int Update(SalaryTIModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_TempletItem set ");
            builder.Append("ItemName='" + model.ItemName + "',");
            builder.Append("IsEdit='" + model.IsEdit + "',");
            builder.Append("IsDisplay='" + model.IsDisplay + "',");
            builder.Append("Formula='" + model.Formula + "',");
            builder.Append("TheOrder=" + model.TheOrder);
            builder.Append(string.Concat(new object[] { " where TempletID=", model.TempletID, " and ItemID=", model.ItemID, " " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

