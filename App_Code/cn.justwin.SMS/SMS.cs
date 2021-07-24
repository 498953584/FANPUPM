namespace cn.justwin.SMS
{
    using cn.justwin.Web;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SMS
    {
        public static string ConnectionString()
        {
            return ConfigHelper.Get("SMS");
        }

        public SqlConnection MSConnection()
        {
            SqlConnection connection = new SqlConnection {
                ConnectionString = ConfigHelper.Get("SMS")
            };
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        public ArrayList Send(string userName, string mbNo, string msg, string sendTime, string comport, string report)
        {
            bool flag = false;
            string str = "";
            ArrayList list = new ArrayList();
            if (!string.IsNullOrEmpty(msg) && !string.IsNullOrEmpty(mbNo))
            {
                if (string.IsNullOrEmpty(userName))
                {
                    userName = "  ";
                }
                if (string.IsNullOrEmpty(sendTime))
                {
                    sendTime = DateTime.Now.ToString();
                }
                if (string.IsNullOrEmpty(comport))
                {
                    comport = "0";
                }
                if (string.IsNullOrEmpty(report))
                {
                    report = "0";
                }
                ArrayList list2 = this.SendSMS(userName, mbNo, msg, sendTime, comport, report);
                flag = (bool) list2[0];
                str = (string) list2[1];
            }
            list.Add(flag);
            list.Add(str);
            return list;
        }

        private ArrayList SendSMS(string userName, string mbNo, string msg, string sendTime, string comport, string report)
        {
            bool flag = false;
            string str = "";
            ArrayList list = new ArrayList();
            SqlConnection connection = this.MSConnection();
            SqlCommand command = new SqlCommand();
            StringBuilder builder = new StringBuilder();
            builder.Append("begin insert into OutBox(username,Mbno,Msg,SendTime,ComPort,Report)");
            builder.Append(" values('" + userName + "','" + mbNo + "','" + msg.Replace("'", "''") + "','" + sendTime + "'," + comport + "," + report);
            builder.Append(") select @@identity update outbox set username=username+convert(nvarchar(50),@@identity) where id=@@identity end");
            try
            {
                string str2 = builder.ToString();
                command.Connection = connection;
                command.CommandText = str2;
                command.Transaction = connection.BeginTransaction();
                string str3 = command.ExecuteScalar().ToString();
                command.Transaction.Commit();
                if (str3 != "")
                {
                    flag = true;
                    str = str3;
                }
            }
            catch
            {
                command.Transaction.Rollback();
                connection.Close();
                command.Dispose();
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
            list.Add(flag);
            list.Add(str);
            return list;
        }
    }
}

