namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class TableAccountAction
    {
        public bool delAccountModelRecord(int ID)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Ent_Quality_Model where id = " + ID);
        }

        public DataTable getAccountModelList(string AccountType)
        {
            return publicDbOpClass.DataTableQuary("select * from Ent_Quality_Model where Flag = '" + AccountType + "'");
        }

        public TableAccountModelInfo getOneAccountModel(int Id)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from Ent_Quality_Model where id = " + Id);
            TableAccountModelInfo info = new TableAccountModelInfo();
            if (table.Rows.Count > 0)
            {
                info.ID = Convert.ToInt32(table.Rows[0]["id"]);
                info.ModelName = table.Rows[0]["ModelName"].ToString();
                info.FilePath = table.Rows[0]["FilePath"].ToString();
                info.Flag = table.Rows[0]["Flag"].ToString();
            }
            return info;
        }

        public bool insAccountModelRecord(string ModelName, string FilePath, string Flag)
        {
            string str2 = "insert into Ent_Quality_Model (ModelName,FilePath,Flag) values ";
            return publicDbOpClass.NonQuerySqlString(str2 + " ('" + ModelName + "','" + FilePath + "','" + Flag + "')");
        }

        public bool updAccountModelRecord(string ModelName, int ID)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update Ent_Quality_Model set ModelName = '", ModelName, "' where id = ", ID }));
        }
    }
}

