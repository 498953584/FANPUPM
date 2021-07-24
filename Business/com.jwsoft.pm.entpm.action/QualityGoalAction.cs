namespace com.jwsoft.pm.entpm.action
{
    using cn.justwin.Web;
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.web.WebControls;
    using System;
    using System.Data;
    using System.Web.Configuration;

    public class QualityGoalAction
    {
        public static int ADD(QualityGoalInfo QGI)
        {
            string str = " insert into Ent_Quality_Goal(PrjCode,ScheduleCode,QualityGoal,Remark,mark,filesType) values (";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " '", QGI.PrjCode, "','", QGI.ScheduleCode, "','", QGI.QualityGoal, "','", QGI.Remark, "','", QGI.Mark, "','", QGI.FilesType, "')" }));
        }

        public static int DEL(string ID)
        {
            return publicDbOpClass.ExecSqlString(" delete from Ent_Quality_Goal where i_id = '" + ID + "'");
        }

        public static int DEL(string[] inStr)
        {
            string arrayToInStr = StringHelper.GetArrayToInStr(inStr);
            return publicDbOpClass.ExecSqlString(" delete from Ent_Quality_Goal where i_id IN (" + arrayToInStr + ")");
        }

        public DataTable GetDatafromQuality(object PageControl, int iPageSize, string strWhere)
        {
            int num = 1;
            PaginationControl control = (PaginationControl) PageControl;
            num = ((publicDbOpClass.GetRecordCount("Ent_Quality_File", strWhere) - 1) / iPageSize) + 1;
            control.PageCount = num;
            return publicDbOpClass.GetRecordFromPage("Ent_Quality_File", "Id", iPageSize, control.CurrentPageIndex, 1, strWhere);
        }

        public static QualityGoalInfo GetModel(string i_id)
        {
            QualityGoalInfo info = new QualityGoalInfo();
            DataTable singleRow = GetSingleRow(i_id);
            if (singleRow.Rows.Count <= 0)
            {
                return null;
            }
            if ((singleRow.Rows[0]["i_id"] != null) && (singleRow.Rows[0]["i_id"].ToString() != ""))
            {
                info.i_id = int.Parse(singleRow.Rows[0]["i_id"].ToString());
            }
            if ((singleRow.Rows[0]["PrjCode"] != null) && (singleRow.Rows[0]["PrjCode"].ToString() != ""))
            {
                info.PrjCode = new Guid(singleRow.Rows[0]["PrjCode"].ToString());
            }
            if ((singleRow.Rows[0]["TaskName"] != null) && (singleRow.Rows[0]["TaskName"].ToString() != ""))
            {
                info.ScheduleName = singleRow.Rows[0]["TaskName"].ToString();
            }
            if ((singleRow.Rows[0]["QualityGoal"] != null) && (singleRow.Rows[0]["QualityGoal"].ToString() != ""))
            {
                info.QualityGoal = singleRow.Rows[0]["QualityGoal"].ToString();
            }
            if ((singleRow.Rows[0]["Remark"] != null) && (singleRow.Rows[0]["Remark"].ToString() != ""))
            {
                info.Remark = singleRow.Rows[0]["Remark"].ToString();
            }
            if ((singleRow.Rows[0]["mark"] != null) && (singleRow.Rows[0]["mark"].ToString() != ""))
            {
                info.Mark = int.Parse(singleRow.Rows[0]["mark"].ToString());
            }
            if ((singleRow.Rows[0]["filesType"] != null) && (singleRow.Rows[0]["filesType"].ToString() != ""))
            {
                info.FilesType = int.Parse(singleRow.Rows[0]["filesType"].ToString());
            }
            return info;
        }

        private static QualityGoalInfo GetQualityGoalInfoFromDataRow(DataRow dr)
        {
            return new QualityGoalInfo { PrjCode = (Guid) dr["PrjCode"], ScheduleCode = dr["ScheduleCode"].ToString(), ScheduleName = dr["ScheduleName"].ToString(), QualityGoal = dr["QualityGoal"].ToString(), Remark = dr["Remark"].ToString() };
        }

        public static QualityGoalInfo GetSingleQualityGoalInfo(Guid projectCode, string scheduleCode)
        {
            QualityGoalInfo info = new QualityGoalInfo();
            string str2 = getViewName();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from " + str2 + " where PrjCode = '" + projectCode.ToString() + "' and ScheduleCode = '" + scheduleCode + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    return GetQualityGoalInfoFromDataRow(table.Rows[0]);
                }
            }
            return info;
        }

        public static DataTable GetSingleRow(string i_id)
        {
            string str = getViewName();
            return publicDbOpClass.DataTableQuary("select * from " + str + " where i_id = '" + i_id + "'");
        }

        public static string getViewName()
        {
            string str = "v_Quality_Goal";
            if (WebConfigurationManager.AppSettings["IsNewProject"].ToString() == "1")
            {
                str = "v_Quality_Goal_New";
            }
            return str;
        }

        public int InsertFile(QualityFileInfo objinfo)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { "insert into Ent_Quality_File(modelkey,title,bz,prjcode,urlpath) values('", objinfo.modelkey, "','", objinfo.title, "','", objinfo.bz, "','", objinfo.prjcode, "','", objinfo.urlpath, "')" }));
        }

        public static int Update(QualityGoalInfo QGI)
        {
            object obj2 = " update Ent_Quality_Goal set QualityGoal = '" + QGI.QualityGoal + "',";
            object obj3 = string.Concat(new object[] { obj2, " PrjCode = '", QGI.PrjCode, "',ScheduleCode = '", QGI.ScheduleCode, "',Remark = '", QGI.Remark, "',mark='", QGI.Mark, "',filesType='", QGI.FilesType, "' " });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " where i_id = '", QGI.i_id, "'" }));
        }
    }
}

