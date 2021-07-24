namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.Configuration;

    public class SafetyMeasureAction
    {
        public static int Add(SafetyMeasureInfo model)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            Guid prjCode = model.PrjCode;
            builder2.Append("PrjCode,");
            builder3.Append("'" + model.PrjCode.ToString() + "',");
            if (model.ScheduleCode != null)
            {
                builder2.Append("ScheduleCode,");
                builder3.Append("'" + model.ScheduleCode + "',");
            }
            if (model.SaftyMeasure != null)
            {
                builder2.Append("SaftyMeasure,");
                builder3.Append("'" + model.SaftyMeasure + "',");
            }
            if (model.Remark != null)
            {
                builder2.Append("Remark,");
                builder3.Append("'" + model.Remark + "',");
            }
            int mark = model.Mark;
            builder2.Append("mark,");
            builder3.Append(model.Mark + ",");
            int filesType = model.FilesType;
            builder2.Append("filesType,");
            builder3.Append(model.FilesType + ",");
            builder.Append("insert into Ent_Safty_Measure(");
            builder.Append(builder2.ToString().Remove(builder2.Length - 1));
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(builder3.ToString().Remove(builder3.Length - 1));
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static int DEL(string ID)
        {
            return publicDbOpClass.ExecSqlString(" delete from Ent_Safty_Measure where i_id = '" + ID + "'");
        }

        public static DataTable GetPageData(string strWhere)
        {
            string saftyViewName = GetSaftyViewName();
            return publicDbOpClass.GetPageData(strWhere, saftyViewName, "i_id desc");
        }

        private static SafetyMeasureInfo GetSafetyMeasureInfoFromDataRow(DataRow dr)
        {
            return new SafetyMeasureInfo { PrjCode = (Guid) dr["PrjCode"], ScheduleCode = dr["ScheduleCode"].ToString(), ScheduleName = dr["ScheduleName"].ToString(), SaftyMeasure = dr["SaftyMeasure"].ToString(), Remark = dr["Remark"].ToString() };
        }

        public SafetyMeasureInfo GetSafetyMeasureModel(string i_id)
        {
            SafetyMeasureInfo info = new SafetyMeasureInfo();
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
            if ((singleRow.Rows[0]["ScheduleCode"] != null) && (singleRow.Rows[0]["ScheduleCode"].ToString() != ""))
            {
                info.ScheduleCode = singleRow.Rows[0]["ScheduleCode"].ToString();
            }
            if ((singleRow.Rows[0]["SaftyMeasure"] != null) && (singleRow.Rows[0]["SaftyMeasure"].ToString() != ""))
            {
                info.SaftyMeasure = singleRow.Rows[0]["SaftyMeasure"].ToString();
            }
            if ((singleRow.Rows[0]["TaskName"] != null) && (singleRow.Rows[0]["TaskName"].ToString() != ""))
            {
                info.TaskName = singleRow.Rows[0]["TaskName"].ToString();
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

        public static string GetSaftyViewName()
        {
            string str = "v_Safty_Measure";
            string str2 = WebConfigurationManager.AppSettings["IsNewProject"];
            if (str2 == "1")
            {
                str = "v_Safty_Measure_New";
            }
            return str;
        }

        public static DataTable GetSingleRow(string i_id)
        {
            string saftyViewName = GetSaftyViewName();
            return publicDbOpClass.DataTableQuary(string.Format("select * from " + saftyViewName + " where i_id={0}", i_id));
        }

        public static SafetyMeasureInfo GetSingleSafetyMeasureInfo(Guid projectCode, string scheduleCode)
        {
            SafetyMeasureInfo safetyMeasureInfoFromDataRow = new SafetyMeasureInfo();
            string saftyViewName = GetSaftyViewName();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from " + saftyViewName + " where PrjCode = '" + projectCode.ToString() + "' and ScheduleCode = '" + scheduleCode + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    safetyMeasureInfoFromDataRow = GetSafetyMeasureInfoFromDataRow(table.Rows[0]);
                }
            }
            return safetyMeasureInfoFromDataRow;
        }

        public static int Update(SafetyMeasureInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ent_Safty_Measure set ");
            Guid prjCode = model.PrjCode;
            builder.Append("PrjCode='" + model.PrjCode + "',");
            if (model.ScheduleCode != null)
            {
                builder.Append("ScheduleCode='" + model.ScheduleCode + "',");
            }
            if (model.SaftyMeasure != null)
            {
                builder.Append("SaftyMeasure='" + model.SaftyMeasure + "',");
            }
            if (model.Remark != null)
            {
                builder.Append("Remark='" + model.Remark + "',");
            }
            builder.Append("mark=" + model.Mark + ",");
            builder.Append("filesType=" + model.FilesType + ",");
            int startIndex = builder.ToString().LastIndexOf(",");
            builder.Remove(startIndex, 1);
            builder.Append(" where i_id=" + model.i_id);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

