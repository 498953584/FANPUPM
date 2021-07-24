namespace cn.justwin.stockBLL.epm.datum
{
    using cn.justwin.DAL;
    using cn.justwin.epm.datum;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CriterionLogic
    {
        private CriterionService dal = new CriterionService();

        public DataTable getDtModelByID(string idCODE)
        {
            if (idCODE != "")
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("SELECT * FROM EPM_Datum_Criterion WHERE CriterionCode='" + idCODE + "'");
                DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
                if (table != null)
                {
                    return table;
                }
            }
            return null;
        }

        public bool UpdateByID(string id, SqlTransaction trans)
        {
            string str = string.Empty;
            string[] strArray = id.Split(new char[] { ',' });
            if (strArray.Length > 0)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        str = str + "'" + strArray[i].ToString() + "',";
                    }
                }
            }
            str = str.Remove(str.Length - 1);
            return this.dal.UpdateByID(str, trans);
        }
    }
}

