namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ProgressPlanAction
    {
        public static bool DelPlanRecord(string planId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Format("delete Prj_ProgressPlan where planid='{0}'", planId));
        }

        public bool EntAuditPlan(ProgressPlanInfo objInfo)
        {
            string str2 = "Update Prj_ProgressPlan set AuditState=" + objInfo.AuditState.ToString() + ",PermissionView='" + objInfo.PermissionView;
            return publicDbOpClass.NonQuerySqlString(str2 + "',PermissionPeople='" + objInfo.PermissionPeople + "' where planid='" + objInfo.PlanId + "'");
        }

        private ProgressPlanInfo FormatToInfo(DataRow dr)
        {
            ProgressPlanInfo info = new ProgressPlanInfo {
                ApplicationName = dr["ApplicationName"].ToString(),
                AuditState = int.Parse(dr["AuditState"].ToString()),
                CompletedDate = DateTime.Parse(dr["CompletedDate"].ToString()),
                DeclareUnitView = dr["DeclareUnitView"].ToString(),
                PanelView = dr["PanelView"].ToString(),
                PermissionPeople = dr["PermissionPeople"].ToString(),
                PermissionView = dr["PermissionView"].ToString(),
                PlanId = dr["PlanId"].ToString(),
                PlanCode = dr["plancode"].ToString(),
                PlanSort = dr["PlanSort"].ToString(),
                PrjCode = dr["PrjCode"].ToString()
            };
            string sqlString = string.Format("select PrjName from Prj_ProjectInfo where prjCode='{0}'", dr["PrjCode"].ToString());
            info.PrjName = (publicDbOpClass.ExecuteScalar(sqlString) != null) ? publicDbOpClass.ExecuteScalar(sqlString).ToString() : "";
            info.Remark = dr["Remark"].ToString();
            info.ResultsHolders = dr["ResultsHolders"].ToString();
            info.ResultsName = dr["ResultsName"].ToString();
            info.VettingCommitteeView = dr["VettingCommitteeView"].ToString();
            info.ProgressGuid = dr["ProgressGuid"].ToString();
            return info;
        }

        public ProgressPlanCollection GetAuditedProgressPlanInfos(string prjCode)
        {
            string sqlString = "Select * from Prj_ProgressPlan where AuditState = 4";
            ProgressPlanCollection plans = new ProgressPlanCollection();
            if (prjCode != "")
            {
                sqlString = sqlString + " and prjCode='" + prjCode + "'";
            }
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                plans.Add(this.FormatToInfo(row));
            }
            return plans;
        }

        public ProgressPlanCollection GetEntProgressPlanInfos()
        {
            string sqlString = "Select * from Prj_ProgressPlan where AuditState>1";
            ProgressPlanCollection plans = new ProgressPlanCollection();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                plans.Add(this.FormatToInfo(row));
            }
            return plans;
        }

        public ProgressPlanCollection GetEntProgressPlanInfos(string prjCode, string State)
        {
            string sqlString = "Select * from Prj_ProgressPlan where ";
            if (State == "Auditing")
            {
                sqlString = sqlString + " AuditState =2 ";
            }
            else if (State == "View")
            {
                sqlString = sqlString + " AuditState=4 ";
            }
            else if (State == "List")
            {
                sqlString = sqlString + " AuditState in(-1,1,3) ";
            }
            sqlString = sqlString + " and prjCode='" + prjCode + "'";
            ProgressPlanCollection plans = new ProgressPlanCollection();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                plans.Add(this.FormatToInfo(row));
            }
            return plans;
        }

        public static string GetNewPlanId()
        {
            return publicDbOpClass.ExecuteScalar("select isnull(max(cast (planid as decimal(20))),0)+1 from dbo.Prj_ProgressPlan").ToString();
        }

        public ProgressPlanInfo GetOnePlanInfo(string planId)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_ProgressPlan where planid='" + planId + "'");
            ProgressPlanInfo info = new ProgressPlanInfo();
            if (table.Rows.Count != 0)
            {
                info = this.FormatToInfo(table.Rows[0]);
            }
            table.Dispose();
            return info;
        }

        public static DataTable GetPlanSortList()
        {
            return publicDbOpClass.DataTableQuary("select distinct plansort from Prj_ProgressPlan");
        }

        public ProgressPlanCollection GetPpmProgressPlanInfos(string prjCode)
        {
            string sqlString = string.Format("select * from Prj_ProgressPlan where prjCode='{0}' order by CompletedDate desc", prjCode);
            ProgressPlanCollection plans = new ProgressPlanCollection();
            foreach (DataRow row in publicDbOpClass.DataTableQuary(sqlString).Rows)
            {
                plans.Add(this.FormatToInfo(row));
            }
            return plans;
        }

        private bool PlanIsAtDataBase(string planId)
        {
            if (publicDbOpClass.DataTableQuary(string.Format("select planId from Prj_ProgressPlan where planid='{0}'", planId)).Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool PpmAuditPlan(ProgressPlanInfo objInfo)
        {
            string sqlString = "";
            if (this.PlanIsAtDataBase(objInfo.PlanId))
            {
                string str2 = "Update Prj_ProgressPlan set AuditState=" + objInfo.AuditState.ToString() + ",DeclareUnitView='" + objInfo.DeclareUnitView;
                string str3 = str2 + "',PanelView='" + objInfo.PanelView + "',VettingCommitteeView='" + objInfo.VettingCommitteeView;
                sqlString = str3 + "',Remark='" + objInfo.Remark + "' where planid='" + objInfo.PlanId + "'";
            }
            else
            {
                sqlString = "insert into Prj_ProgressPlan(PlanId,AuditState,DeclareUnitView,PanelView,VettingCommitteeView,Remark)";
                string str4 = sqlString;
                string str5 = str4 + " values('" + objInfo.PlanId + "'," + objInfo.AuditState.ToString() + ",'" + objInfo.DeclareUnitView + "','" + objInfo.PanelView;
                sqlString = str5 + "','" + objInfo.VettingCommitteeView + "','" + objInfo.Remark + "')";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool UpdatePlan(ProgressPlanInfo objInfo)
        {
            string sqlString = "";
            if (this.PlanIsAtDataBase(objInfo.PlanId))
            {
                string str2 = "Update Prj_ProgressPlan set PlanCode='" + objInfo.PlanCode + "',PrjCode='" + objInfo.PrjCode;
                string str3 = str2 + "',ResultsName='" + objInfo.ResultsName + "',ResultsHolders='" + objInfo.ResultsHolders;
                string str4 = str3 + "',ApplicationName='" + objInfo.ApplicationName + "',PlanSort='" + objInfo.PlanSort;
                sqlString = (str4 + "',CompletedDate = '" + objInfo.CompletedDate.ToShortDateString() + "' ,Remark='" + objInfo.Remark + "'  where ") + " planid='" + objInfo.PlanId + "'";
            }
            else
            {
                sqlString = "insert into Prj_ProgressPlan(PlanId,Remark,PlanCode,PrjCode,ResultsName,ResultsHolders,ApplicationName,PlanSort,CompletedDate,ProgressGuid)";
                string str5 = sqlString;
                object obj2 = str5 + " values('" + objInfo.PlanId + "','" + objInfo.Remark + "','" + objInfo.PlanCode + "','" + objInfo.PrjCode + "','" + objInfo.ResultsName + "','" + objInfo.ResultsHolders;
                sqlString = string.Concat(new object[] { obj2, "','", objInfo.ApplicationName, "','", objInfo.PlanSort, "','", objInfo.CompletedDate, "','", objInfo.ProgressGuid, "')" });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

