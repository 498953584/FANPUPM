namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;

    public class CalendarInfoAction
    {
        public int Add(OACalendarInfo model, OACalendarRemindSet model2)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin");
            builder.Append(" insert into OA_Calendar_Info(");
            builder.Append("UserCode,RecordDate,Title,Content,IsRemind,InfoGuid");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.Title + "',");
            builder.Append("'" + model.Content + "',");
            builder.Append("'" + model.IsRemind + "',");
            builder.Append("'" + model.InfoGuid + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            builder.Append(" end");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 == null)
            {
                return 0;
            }
            if (model.IsRemind == "1")
            {
                this.SmsOrMessageAdd(model2, Convert.ToInt32(obj2));
            }
            return 1;
        }

        public int Delete(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Calendar_Info ");
            builder.Append(" where RecordID=" + RecordID);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,UserCode,RecordDate,Title,Content,IsRemind,InfoGuid ");
            builder.Append(" FROM OA_Calendar_Info ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OACalendarInfo GetModel(int RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(" RecordID,UserCode,RecordDate,Title,Content,IsRemind,InfoGuid  ");
            builder.Append(" from OA_Calendar_Info ");
            builder.Append(" where RecordID=" + RecordID);
            OACalendarInfo info = new OACalendarInfo();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
            {
                info.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
            }
            info.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                info.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            info.Title = set.Tables[0].Rows[0]["Title"].ToString();
            info.Content = set.Tables[0].Rows[0]["Content"].ToString();
            info.IsRemind = set.Tables[0].Rows[0]["IsRemind"].ToString();
            info.InfoGuid = (Guid) set.Tables[0].Rows[0]["InfoGuid"];
            return info;
        }

        private void SmsOrMessageAdd(OACalendarRemindSet model, int recordid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into OA_Calendar_RemindSet(");
            builder.Append("InfoGuid,IsSms,IsMessage,RemindType,RemindHour,RemindMinute,EndDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.InfoGuid + "',");
            builder.Append("'" + model.IsSms + "',");
            builder.Append("'" + model.IsMessage + "',");
            builder.Append(model.RemindType + ",");
            builder.Append(model.RemindHour + ",");
            builder.Append(model.RemindMinute + ",");
            builder.Append("'" + model.EndDate + "'");
            builder.Append(")");
            PublicInterface.PTDBSJDelete(recordid.ToString(), "001");
            PublicInterface.PTDBSJTodayDelete(recordid.ToString(), "001");
            PublicInterface.SMSLogDelete(recordid.ToString(), "001");
            PublicInterface.SMSLogTodayDelete(recordid.ToString(), "001");
            if (model.IsSms == "1")
            {
                SMSLog log;
                log = new SMSLog {
                    SendUser = HttpContext.Current.Session["yhdm"].ToString(),
                    SendTime = model.EndDate.Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                    ReceiveUser = HttpContext.Current.Session["yhdm"].ToString(),
                    Message = this.strMessage(recordid),
                    V_LXBM = "001",
                    I_XGID = recordid.ToString()
                };
                PublicInterface.SendSmsMsg(log);
            }
            if (model.IsMessage == "1")
            {
                PTDBSJ ptdbsj = new PTDBSJ {
                    V_LXBM = "001",
                    I_XGID = recordid.ToString(),
                    DTM_DBSJ = model.EndDate.Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                    V_Content = this.strMessage(recordid),
                    V_DBLJ = "?rid=" + recordid,
                    V_YHDM = HttpContext.Current.Session["yhdm"].ToString()
                };
                PublicInterface.SendSysMsg(ptdbsj);
            }
            publicDbOpClass.ExecSqlString(builder.ToString());
        }

        private string strMessage(int rid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,UserCode,RecordDate,Title,Content,IsRemind ");
            builder.Append(" FROM OA_Calendar_Info ");
            builder.Append(" where RecordID=" + rid);
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                return ("日程管理消息：" + table.Rows[0]["Content"].ToString());
            }
            return "日程管理消息：";
        }

        public int Update(OACalendarInfo model, OACalendarRemindSet model2)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin");
            builder.Append(" update OA_Calendar_Info set ");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("Title='" + model.Title + "',");
            builder.Append("Content='" + model.Content + "',");
            builder.Append("IsRemind='" + model.IsRemind + "'");
            builder.Append(" where InfoGuid='" + model.InfoGuid + "'");
            builder.Append(" update OA_Calendar_RemindSet set ");
            builder.Append("IsMessage='" + model2.IsMessage + "',");
            builder.Append("RemindHour=" + model2.RemindHour + ",");
            builder.Append("RemindMinute=" + model2.RemindMinute + ",");
            builder.Append("EndDate='" + model2.EndDate + "'");
            builder.Append(" where InfoGuid='" + model2.InfoGuid + "'");
            builder.Append(" update OA_Calendar_RemindSet set ");
            builder.Append("IsSms='" + model2.IsSms + "',");
            builder.Append("RemindHour=" + model2.RemindHour + ",");
            builder.Append("RemindMinute=" + model2.RemindMinute + ",");
            builder.Append("EndDate='" + model2.EndDate + "'");
            builder.Append(" where InfoGuid='" + model2.InfoGuid + "'");
            DataTable table = publicDbOpClass.DataTableQuary("select * from OA_Calendar_Info where InfoGuid='" + model.InfoGuid + "'");
            string content = model.Content;
            if (model.Content.Length > 70)
            {
                content = model.Content.Substring(0, 60) + "...";
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                builder.Append(" update PT_SMSLog set Message = '日程管理消息：" + content + "' where I_XGID = '" + table.Rows[i]["RecordID"].ToString() + "'");
                builder.Append(" update PT_SMSLog_Today set Message = '日程管理消息：" + content + "' where I_XGID = '" + table.Rows[i]["RecordID"].ToString() + "'");
                builder.Append(" update PT_DBSJ set V_Content = '" + content + "' where I_XGID = '" + table.Rows[i]["RecordID"].ToString() + "'");
                builder.Append(" update PT_DBSJ_Today set V_Content = '" + content + "' where I_XGID = '" + table.Rows[i]["RecordID"].ToString() + "'");
            }
            builder.Append(" end");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

