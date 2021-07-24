namespace cn.justwin.PrjManager
{
    using com.jwsoft.pm.data;
    using PmBusinessLogic;
    using System;
    using System.Data;

    public class SetUpCommit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select pcode from [Sm_Purchase] where PROJECT='" + key.ToString() + "'");
            if (table.Rows.Count > 0)
            {
                string str2 = "where 1=1";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    object obj2 = str2;
                    str2 = string.Concat(new object[] { obj2, " and pscode='", table.Rows[i][0], "'" });
                }
                DataTable table2 = publicDbOpClass.DataTableQuary("select psid from [Sm_Purchase_Stock] " + str2);
                if (table2.Rows.Count > 0)
                {
                    string str4 = "where 1=1 and PurchaseId= '" + table2.Rows[0][0] + "'";
                    for (int j = 1; j < table2.Rows.Count; j++)
                    {
                        object obj3 = str4;
                        str4 = string.Concat(new object[] { obj3, " or PurchaseId= '", table2.Rows[j][0], "'" });
                    }
                    if (publicDbOpClass.DataTableQuary("select * from Con_Balance_Stock " + str4).Rows.Count > 0)
                    {
                        publicDbOpClass.ExecuteSQL("DELETE FROM Con_Balance_Stock " + str4);
                    }
                    publicDbOpClass.ExecuteSQL("DELETE FROM Sm_Purchase_Stock" + str2);
                }
                publicDbOpClass.ExecuteSQL("DELETE FROM Sm_Purchase WHERE PROJECT='" + key.ToString() + "'");
            }
        }

        public void RefuseEvent(object key)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            ProjectInfo.Del(key.ToString());
        }
    }
}

