namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SalaryItemAction
    {
        public int Add(SalaryItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin ");
            if (model.ItemType == "0")
            {
                builder.Append(" update HR_Salary_Item set ItemType= '1' ");
            }
            builder.Append(" insert into HR_Salary_Item(");
            builder.Append("ItemName,ItemType,IsValid");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ItemName + "',");
            builder.Append("'" + model.ItemType + "',");
            builder.Append("'" + model.IsValid + "'");
            builder.Append(")");
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_Item ");
            builder.Append(" where ItemID=" + ItemID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ItemID,ItemName,ItemType,IsValid ");
            builder.Append(" FROM HR_Salary_Item ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryItemModel GetModel(int ItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" ItemID,ItemName,ItemType,IsValid ");
            builder.Append(" from HR_Salary_Item ");
            builder.Append(" where ItemID=" + ItemID + " ");
            SalaryItemModel model = new SalaryItemModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ItemID"].ToString() != "")
            {
                model.ItemID = int.Parse(set.Tables[0].Rows[0]["ItemID"].ToString());
            }
            model.ItemName = set.Tables[0].Rows[0]["ItemName"].ToString();
            model.ItemType = set.Tables[0].Rows[0]["ItemType"].ToString();
            model.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            return model;
        }

        public int Update(SalaryItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin ");
            if (model.ItemType == "0")
            {
                builder.Append(" update HR_Salary_Item set ItemType= '1' ");
            }
            builder.Append(" update HR_Salary_Item set ");
            builder.Append("ItemName='" + model.ItemName + "',");
            builder.Append("ItemType='" + model.ItemType + "',");
            builder.Append("IsValid='" + model.IsValid + "'");
            builder.Append(" where ItemID=" + model.ItemID + " ");
            builder.Append(" end ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

