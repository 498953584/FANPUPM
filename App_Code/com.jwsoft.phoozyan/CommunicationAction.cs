namespace com.jwsoft.phoozyan
{
    using com.jwsoft.common.data;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class CommunicationAction
    {
        public string BackDept(string WkpDeptId)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select v_bmmc from pt_d_bm where i_bmdm='" + WkpDeptId + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["v_bmmc"].ToString();
            }
            return "";
        }

        public string[] BackDeptAndID(string UserName)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select i_bmdm from pt_yhmc where v_xm='" + UserName + "'");
            string[] strArray = new string[2];
            if (table.Rows.Count > 0)
            {
                strArray[0] = table.Rows[0]["i_bmdm"].ToString();
                table = publicDbOpClass.DataTableQuary("select v_bmmc from pt_d_bm where i_bmdm='" + strArray[0] + "' ");
                strArray[1] = table.Rows[0]["v_bmmc"].ToString();
                return strArray;
            }
            strArray[0] = "";
            strArray[1] = "";
            return null;
        }

        public static string[] BackDeptOrID(string UserCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select i_bmdm from pt_yhmc where v_yhdm='" + UserCode + "'");
            string[] strArray = new string[2];
            if (table.Rows.Count > 0)
            {
                strArray[0] = table.Rows[0]["i_bmdm"].ToString();
                table = publicDbOpClass.DataTableQuary("select v_bmmc from pt_d_bm where i_bmdm='" + strArray[0] + "' ");
                strArray[1] = table.Rows[0]["v_bmmc"].ToString();
                return strArray;
            }
            strArray[0] = "";
            strArray[1] = "";
            return null;
        }

        public string BackUserCode(string username)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select v_yhdm from pt_yhmc where v_xm='" + username + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["v_yhdm"].ToString();
            }
            return "";
        }

        public string BackUserName(string usercode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select v_xm from pt_yhmc where v_yhdm='" + usercode + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["v_xm"].ToString();
            }
            return "";
        }

        public bool ExecuteResult(string sqlstr)
        {
            bool flag;
            SqlConnection connection = new Conn().SqlConnectionSystem();
            SqlCommand command = new SqlCommand();
            SqlTransaction transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandText = sqlstr;
            command.ExecuteNonQuery();
            try
            {
                transaction.Commit();
                flag = true;
            }
            catch
            {
                transaction.Rollback();
                flag = false;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
            return flag;
        }

        public string[] GetCommNames(string[] myPersons)
        {
            int length = myPersons.Length;
            string[] strArray = new string[length];
            for (int i = 0; i < length; i++)
            {
                strArray[i] = this.BackUserName(myPersons[i]);
            }
            return strArray;
        }

        public string[] GetCommPersons(string menucode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_yhmc_Privilege where c_mkdm='" + menucode + "'");
            string[] strArray = new string[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                strArray[i] = table.Rows[i]["V_YHDM"].ToString();
            }
            return strArray;
        }

        public string GetNamebyPhone(string phoneNumber)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select v_xm from pt_yhmc where mobilephonecode='" + phoneNumber + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return phoneNumber;
        }
    }
}

