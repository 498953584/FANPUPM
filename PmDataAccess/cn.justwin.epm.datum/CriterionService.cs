namespace cn.justwin.epm.datum
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class CriterionService
    {
        public bool UpdateByID(string id, SqlTransaction trans)
        {
            if (!(id != ""))
            {
                return false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE EPM_Datum_Criterion").Append(" ");
            builder.Append(" SET ").Append(" [mark] = 1 ").Append(" WHERE  CriterionCode in(" + id + ")");
            return (SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), null) > 0);
        }
    }
}

