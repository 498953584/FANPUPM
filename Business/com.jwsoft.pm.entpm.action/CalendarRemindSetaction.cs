namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;
    using System.Web;

    public class CalendarRemindSetaction
    {
        public int Add(OACalendarRemindSet model, OACalendarInfo model2, DateTime RDate)
        {
            publicDbOpClass.ExecSqlString(" delete OA_Calendar_Info where InfoGuid = '" + model.InfoGuid + "' ");
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin");
            builder.Append(" delete OA_Calendar_RemindSet where InfoGuid = '" + model.InfoGuid + "' ");
            builder.Append(" insert into OA_Calendar_RemindSet(");
            builder.Append(" InfoGuid,IsSms,IsMessage,RemindType,RemindHour,RemindMinute,EndDate");
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
            builder.Append(" end");
            DataTable table = publicDbOpClass.DataTableQuary("select * from OA_Calendar_Info where InfoGuid = '" + model.InfoGuid + "' ");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                PublicInterface.PTDBSJDelete(table.Rows[i]["RecordID"].ToString(), "001");
                PublicInterface.PTDBSJTodayDelete(table.Rows[i]["RecordID"].ToString(), "001");
                PublicInterface.SMSLogDelete(table.Rows[i]["RecordID"].ToString(), "001");
                PublicInterface.SMSLogTodayDelete(table.Rows[i]["RecordID"].ToString(), "001");
            }
            this.SmsOrMessageAdd(model, model2, RDate);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        private int CalendarInfoAdd(OACalendarInfo model, DateTime rdate)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Calendar_Info(");
            builder.Append("InfoGuid,UserCode,RecordDate,Title,Content,IsRemind");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.InfoGuid + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + rdate + "',");
            builder.Append("'" + model.Title + "',");
            builder.Append("'" + model.Content + "',");
            builder.Append("'" + model.IsRemind + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }

        public int Delete(decimal RemindDate)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_Calendar_RemindSet ");
            builder.Append(" where RemindDate=" + RemindDate);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RecordID,IsSms,IsMessage,RemindType,RemindDate,RemindDay,RemindHour,RemindMinute,EndDate ");
            builder.Append(" FROM OA_Calendar_RemindSet ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public OACalendarRemindSet GetModel(Guid InfoGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select ");
            builder.Append(" InfoGuid,IsSms,IsMessage,RemindType,RemindHour,RemindMinute,EndDate ");
            builder.Append(" from OA_Calendar_RemindSet ");
            builder.Append(" where InfoGuid='" + InfoGuid + "'");
            OACalendarRemindSet set = new OACalendarRemindSet();
            DataSet set2 = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set2.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set2.Tables[0].Rows[0]["InfoGuid"].ToString() != "")
            {
                set.InfoGuid = new Guid(set2.Tables[0].Rows[0]["InfoGuid"].ToString());
            }
            set.IsSms = set2.Tables[0].Rows[0]["IsSms"].ToString();
            set.IsMessage = set2.Tables[0].Rows[0]["IsMessage"].ToString();
            if (set2.Tables[0].Rows[0]["RemindType"].ToString() != "")
            {
                set.RemindType = int.Parse(set2.Tables[0].Rows[0]["RemindType"].ToString());
            }
            if (set2.Tables[0].Rows[0]["RemindHour"].ToString() != "")
            {
                set.RemindHour = int.Parse(set2.Tables[0].Rows[0]["RemindHour"].ToString());
            }
            if (set2.Tables[0].Rows[0]["RemindMinute"].ToString() != "")
            {
                set.RemindMinute = int.Parse(set2.Tables[0].Rows[0]["RemindMinute"].ToString());
            }
            if (set2.Tables[0].Rows[0]["EndDate"].ToString() != "")
            {
                set.EndDate = DateTime.Parse(set2.Tables[0].Rows[0]["EndDate"].ToString());
            }
            return set;
        }

        private double MonthEquals(DateTime dt1, DateTime dt2)
        {
            return Convert.ToDouble(publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select datediff(mm,'", dt1, "','", dt2, "') as dt" })).Rows[0]["dt"].ToString());
        }

        private void SmsOrMessageAdd(OACalendarRemindSet model, OACalendarInfo model2, DateTime RDate)
        {
            double num4;
            int num5;
            int num = 0;
            switch (model.RemindType)
            {
                case 2:
                {
                    TimeSpan span = model.EndDate.Date.Subtract(RDate);
                    for (int i = 0; i <= span.Days; i++)
                    {
                        num = this.CalendarInfoAdd(model2, RDate.AddDays(Convert.ToDouble(i)).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute));
                        if (model.IsSms == "1")
                        {
                            SMSLog log;
                            log = new SMSLog {
                                SendUser = HttpContext.Current.Session["yhdm"].ToString(),
                                SendTime = RDate.AddDays(Convert.ToDouble(i)).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                                ReceiveUser = HttpContext.Current.Session["yhdm"].ToString(),
                                Message = "日程管理消息：" + model2.Content,
                                V_LXBM = "001",
                                I_XGID = num.ToString()
                            };
                            PublicInterface.SendSmsMsg(log);
                        }
                        if (model.IsMessage == "1")
                        {
                            PTDBSJ ptdbsj = new PTDBSJ {
                                V_LXBM = "001",
                                I_XGID = num.ToString(),
                                C_OpenFlag = "0",
                                DTM_DBSJ = RDate.AddDays(Convert.ToDouble(i)).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                                V_Content = model2.Content,
                                V_TPLJ = "",
                                V_DBLJ = "?rid=" + num,
                                V_YHDM = HttpContext.Current.Session["yhdm"].ToString()
                            };
                            PublicInterface.SendSysMsg(ptdbsj);
                        }
                    }
                    return;
                }
                case 3:
                {
                    TimeSpan span2 = model.EndDate.Date.Subtract(RDate);
                    for (int j = 0; j <= span2.Days; j++)
                    {
                        if (Convert.ToInt32(RDate.AddDays(Convert.ToDouble(j)).DayOfWeek) == Convert.ToInt32(RDate.DayOfWeek))
                        {
                            num = this.CalendarInfoAdd(model2, RDate.AddDays(Convert.ToDouble(j)).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute));
                            if (model.IsSms == "1")
                            {
                                SMSLog log2;
                                log2 = new SMSLog {
                                    SendUser = HttpContext.Current.Session["yhdm"].ToString(),
                                    SendTime = RDate.AddDays(Convert.ToDouble(j)).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                                    ReceiveUser = HttpContext.Current.Session["yhdm"].ToString(),
                                    Message = "日程管理消息：" + model2.Content,
                                    V_LXBM = "001",
                                    I_XGID = num.ToString()
                                };
                                PublicInterface.SendSmsMsg(log2);
                            }
                            if (model.IsMessage == "1")
                            {
                                PTDBSJ ptdbsj2 = new PTDBSJ {
                                    V_LXBM = "001",
                                    I_XGID = num.ToString(),
                                    C_OpenFlag = "0",
                                    DTM_DBSJ = RDate.AddDays(Convert.ToDouble(j)).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                                    V_Content = model2.Content,
                                    V_TPLJ = "",
                                    V_DBLJ = "?rid=" + num.ToString(),
                                    V_YHDM = HttpContext.Current.Session["yhdm"].ToString()
                                };
                                PublicInterface.SendSysMsg(ptdbsj2);
                            }
                        }
                    }
                    return;
                }
                case 4:
                    num4 = this.MonthEquals(RDate, model.EndDate);
                    num5 = 0;
                    break;

                default:
                    return;
            }
            while (num5 <= num4)
            {
                num = this.CalendarInfoAdd(model2, RDate.AddMonths(num5).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute));
                if (model.IsSms == "1")
                {
                    SMSLog log3;
                    log3 = new SMSLog {
                        SendUser = HttpContext.Current.Session["yhdm"].ToString(),
                        SendTime = RDate.AddMonths(num5).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                        ReceiveUser = HttpContext.Current.Session["yhdm"].ToString(),
                        Message = "日程管理消息：" + model2.Content,
                        V_LXBM = "001",
                        I_XGID = num.ToString()
                    };
                    PublicInterface.SendSmsMsg(log3);
                }
                if (model.IsMessage == "1")
                {
                    PTDBSJ ptdbsj3 = new PTDBSJ {
                        V_LXBM = "001",
                        I_XGID = num.ToString(),
                        C_OpenFlag = "0",
                        DTM_DBSJ = RDate.AddMonths(num5).Date.AddHours((double) model.RemindHour).AddMinutes((double) model.RemindMinute),
                        V_Content = model2.Content,
                        V_TPLJ = "",
                        V_DBLJ = "?rid=" + num.ToString(),
                        V_YHDM = HttpContext.Current.Session["yhdm"].ToString()
                    };
                    PublicInterface.SendSysMsg(ptdbsj3);
                }
                num5++;
            }
        }
    }
}

