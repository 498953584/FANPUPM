namespace cn.justwin.Ent_Ept_EquipmentsDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class EquipmentsDAL
    {
        public void Add(string EquipmentUniqueCode, string project)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ent_Ept_Equipments_Stock(");
            builder.Append("EquipmentUniqueCode,project)");
            builder.Append(" values (");
            builder.Append("@EquipmentUniqueCode,@project)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@EquipmentUniqueCode", SqlDbType.VarChar, 0x40), new SqlParameter("@project", SqlDbType.VarChar, 0x40) };
            commandParameters[0].Value = EquipmentUniqueCode;
            commandParameters[1].Value = project;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Exists(string EquipmentUniqueCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Ent_Ept_Equipments_Stock where EquipmentUniqueCode='" + EquipmentUniqueCode + "'");
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
            int num = 0;
            if ((table != null) && (table.Rows.Count > 0))
            {
                num = 1;
            }
            return (num > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Ent_Ept_Equipments ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public string projectName(string EquipmentUniqueCode)
        {
            string str = "";
            string cmdText = "SELECT * FROM  Ent_Ept_Equipments_Stock eees  WHERE eees.EquipmentUniqueCode='" + EquipmentUniqueCode + "';";
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
            if ((table != null) && (table.Rows.Count > 0))
            {
                string str3 = table.Rows[0]["project"].ToString();
                if (str3 != "")
                {
                    cmdText = "select * from PT_PrjInfo where PrjGuid='" + str3 + "'";
                    DataTable table2 = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
                    if ((table2 != null) && (table2.Rows.Count > 0))
                    {
                        str = table2.Rows[0]["PrjName"].ToString();
                    }
                }
            }
            return str;
        }

        public int Update(string EquipmentUniqueCode, string project)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ent_Ept_Equipments_Stock set  project='" + project + "' where EquipmentUniqueCode='" + EquipmentUniqueCode + "'");
            int num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
            if (num == 0)
            {
                return 0;
            }
            return Convert.ToInt32(num);
        }
    }
}

