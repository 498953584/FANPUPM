namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ProgressImplementAction
    {
        public static bool AddEvaluate(ProgressEvaluateInfo objInfo, bool isEdit)
        {
            string sqlString = "select * from Prj_ProgressPlan_Appraise where MainId='" + objInfo.MainID + "'";
            if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
            {
                if (!isEdit)
                {
                    objInfo.MainID = GetNewAppraiseId();
                    AddEvaluate(objInfo, isEdit);
                }
                else
                {
                    sqlString = string.Format("update Prj_ProgressPlan_Appraise set AppraisePeople='{0}',AppraiseTime='{1}',Appraise='{2}' where MainID='{3}'", new object[] { objInfo.AppraisePeople, objInfo.AppraiseTime, objInfo.Appraise, objInfo.MainID });
                }
            }
            else
            {
                string str2 = "insert into Prj_ProgressPlan_Appraise values('" + objInfo.MainID + "','";
                sqlString = (str2 + objInfo.ParentMainID + "','" + objInfo.AppraisePeople + "','" + objInfo.AppraiseTime.ToString()) + "','" + objInfo.Appraise + "')";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool DelEvaluation(string MainID)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Prj_ProgressPlan_Appraise where MainId='" + MainID + "'");
        }

        public static bool DelImplement(string ImplementId)
        {
            string sqlString = "select * from Prj_ProgressPlan_Appraise where ParentMainID='" + ImplementId + "'";
            string str2 = "";
            if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
            {
                str2 = "Delete Prj_ProgressPlan_Appraise where ParentMainID='" + ImplementId + "'";
            }
            return publicDbOpClass.NonQuerySqlString(str2 + "delete Prj_ProgressPlan_Child where mainid='" + ImplementId + "'");
        }

        public static bool EditEvaluation(ProgressEvaluateInfo objInfo)
        {
            return publicDbOpClass.NonQuerySqlString("update Prj_ProgressPlan_Appraise set Appraise='" + objInfo.Appraise + "',AppraisePeople='" + objInfo.AppraisePeople + "',AppraiseTime='" + objInfo.AppraiseTime.ToString() + "' where MainId='" + objInfo.MainID + "'");
        }

        private ProgressEvaluateInfo FormatToEvaModel(DataRow dr)
        {
            return new ProgressEvaluateInfo { MainID = dr["MainID"].ToString(), ParentMainID = dr["ParentMainID"].ToString(), Appraise = dr["Appraise"].ToString(), AppraisePeople = dr["AppraisePeople"].ToString(), AppraiseTime = DateTime.Parse(dr["AppraiseTime"].ToString()) };
        }

        private ProgressImplementInfo FormatToImpMedel(DataRow dr)
        {
            ProgressImplementInfo info = new ProgressImplementInfo {
                MainID = dr["MainID"].ToString(),
                PlanId = dr["PlanId"].ToString(),
                Remark = dr["Remark"].ToString(),
                ExaminePeople = dr["ExaminePeople"].ToString(),
                ActualizeCircs = dr["ActualizeCircs"].ToString(),
                ExamineTime = DateTime.Parse(dr["ExamineTime"].ToString()),
                FillPeople = dr["FillPeople"].ToString(),
                FillTime = DateTime.Parse(dr["FillTime"].ToString())
            };
            foreach (DataRow row in publicDbOpClass.DataTableQuary(string.Format("select AnnexName from XPM_Basic_AnnexList where AnnexType=1724 and RecordCode='{0}'", info.MainID)).Rows)
            {
                info.AnnexName = info.AnnexName + row[0].ToString();
            }
            info.ProgressGuid = dr["ProgressGuid"].ToString();
            return info;
        }

        public ProgressEvaluateInfo GetEvaluateInfo(string MainId)
        {
            string sqlString = string.Format("select * from Prj_ProgressPlan_Appraise where MainID='{0}'", MainId);
            ProgressEvaluateInfo info = new ProgressEvaluateInfo();
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (table.Rows.Count > 0)
            {
                info = this.FormatToEvaModel(table.Rows[0]);
            }
            table.Dispose();
            return info;
        }

        public ProgressEvaluateCollection GetEvaluateInfos(string MainId)
        {
            string sqlString = string.Format("select * from Prj_ProgressPlan_Appraise where ParentMainID='{0}'", MainId);
            ProgressEvaluateCollection evaluates = new ProgressEvaluateCollection();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                evaluates.Add(this.FormatToEvaModel(row));
            }
            return evaluates;
        }

        public ProgressImplementInfo GetImplementInfo(string MainId)
        {
            string sqlString = string.Format("select * from Prj_ProgressPlan_Child where mainid='{0}'", MainId);
            ProgressImplementInfo info = new ProgressImplementInfo();
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (table.Rows.Count > 0)
            {
                info = this.FormatToImpMedel(table.Rows[0]);
            }
            table.Dispose();
            return info;
        }

        public ProgressImplementCollection GetImplementInfos(string planId)
        {
            string sqlString = string.Format("select * from Prj_ProgressPlan_Child where PlanId='{0}'", planId);
            ProgressImplementCollection implements = new ProgressImplementCollection();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                implements.Add(this.FormatToImpMedel(row));
            }
            return implements;
        }

        public static string GetNewAppraiseId()
        {
            string sqlString = "select isnull(max(cast(mainid as decimal(20))),0)+1 from Prj_ProgressPlan_Appraise";
            return publicDbOpClass.ExecuteScalar(sqlString).ToString();
        }

        public static string GetNewImplementId()
        {
            string sqlString = "select isnull(max(cast(mainid as decimal(20))),0)+1 from Prj_ProgressPlan_Child";
            return publicDbOpClass.ExecuteScalar(sqlString).ToString();
        }

        public static bool SaveImplement(ProgressImplementInfo objInfo)
        {
            string sqlString = "select * from Prj_ProgressPlan_Child where MainId='" + objInfo.MainID + "'";
            if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
            {
                objInfo.MainID = GetNewImplementId();
                SaveImplement(objInfo);
            }
            else
            {
                string str2 = "insert into Prj_ProgressPlan_Child values('" + objInfo.MainID + "','" + objInfo.PlanId + "','";
                string str3 = str2 + objInfo.FillPeople + "','" + objInfo.FillTime.ToString() + "','" + objInfo.ExaminePeople + "','";
                sqlString = str3 + objInfo.ExamineTime.ToString() + "','" + objInfo.ActualizeCircs + "','" + objInfo.Remark + "','" + objInfo.ProgressGuid + "')";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool UpdateImplement(ProgressImplementInfo objInfo)
        {
            string str;
            if (publicDbOpClass.DataTableQuary(string.Format("select * from Prj_ProgressPlan_Child where MainId='{0}'", objInfo.MainID)).Rows.Count > 0)
            {
                string str2 = "update Prj_ProgressPlan_Child set FillPeople='" + objInfo.FillPeople + "',FillTime='";
                string str3 = str2 + objInfo.FillTime.ToString() + "',ExaminePeople='" + objInfo.ExaminePeople + "',ExamineTime='";
                string str4 = str3 + objInfo.ExamineTime.ToString() + "',ActualizeCircs='" + objInfo.ActualizeCircs + "',Remark='";
                str = str4 + objInfo.Remark + "' where MainId='" + objInfo.MainID + "'";
            }
            else
            {
                string str5 = "insert into Prj_ProgressPlan_Child values('" + objInfo.MainID + "','" + objInfo.PlanId + "','";
                string str6 = str5 + objInfo.FillPeople + "','" + objInfo.FillTime.ToString() + "','" + objInfo.ExaminePeople + "','";
                str = str6 + objInfo.ExamineTime.ToString() + "','" + objInfo.ActualizeCircs + "','" + objInfo.Remark + "')";
            }
            return publicDbOpClass.NonQuerySqlString(str);
        }
    }
}

