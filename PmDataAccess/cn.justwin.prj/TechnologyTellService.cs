namespace cn.justwin.prj
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Text;

    public class TechnologyTellService
    {
        public DataTable GetDataTable(string _where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Prj_V_TechnologyJD ");
            if (_where != "")
            {
                builder.Append("where ");
                builder.Append(_where);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public bool Update(int _mark, int _filestype, int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Prj_TechnologyTell ").Append(" SET ");
            builder.Append(" [mark] = ").Append(_mark).Append(" ,");
            builder.Append(" filesType=").Append(_filestype).Append(" WHERE MainID= ").Append(id);
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }
    }
}

