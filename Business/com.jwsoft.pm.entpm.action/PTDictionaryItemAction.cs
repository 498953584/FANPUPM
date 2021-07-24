namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PTDictionaryItemAction
    {
        public int Add(PTDictionaryItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DictionaryItem(");
            builder.Append("ClassID,DisplayValue,IsValid");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.ClassID + ",");
            builder.Append("'" + model.DisplayValue + "',");
            builder.Append("'" + model.IsValid + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int KeyValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_DictionaryItem ");
            builder.Append(" where KeyValue=" + KeyValue + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetClassList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClassID,SubSystemCode,BussinessCode,ClassName ");
            builder.Append(" FROM PT_DictionaryClass ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ClassID,KeyValue,DisplayValue,IsValid ");
            builder.Append(" FROM PT_DictionaryItem ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public PTDictionaryItem GetModel(int KeyValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" ClassID,KeyValue,DisplayValue,IsValid ");
            builder.Append(" from PT_DictionaryItem ");
            builder.Append(" where KeyValue=" + KeyValue + " ");
            PTDictionaryItem item = new PTDictionaryItem();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                item.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            if (set.Tables[0].Rows[0]["KeyValue"].ToString() != "")
            {
                item.KeyValue = int.Parse(set.Tables[0].Rows[0]["KeyValue"].ToString());
            }
            item.DisplayValue = set.Tables[0].Rows[0]["DisplayValue"].ToString();
            item.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            return item;
        }

        public int Update(PTDictionaryItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update PT_DictionaryItem set ");
            builder.Append("DisplayValue='" + model.DisplayValue + "',");
            builder.Append("IsValid='" + model.IsValid + "'");
            builder.Append(" where KeyValue=" + model.KeyValue + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

