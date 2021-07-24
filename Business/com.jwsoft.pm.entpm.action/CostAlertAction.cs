namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class CostAlertAction
    {
        private string _AlertID;
        private AlertMessage _ObjAm;
        private AlertPoint _ObjAp;
        private string _UserCode;

        public CostAlertAction()
        {
        }

        public CostAlertAction(string AlertCode)
        {
            this.AlertID = AlertCode;
            this._ObjAp = new AlertPoint(AlertCode);
        }

        public void DoAllProjectAlert(Guid ProjectCode)
        {
            string[] projectFactBudgetCost = this.GetProjectFactBudgetCost(ProjectCode);
            if (decimal.Parse(projectFactBudgetCost[1]) > decimal.Parse(projectFactBudgetCost[0]))
            {
                this.InitAlertMessage(ProjectCode, "");
                this._ObjAm.Message = PrjInfoAction.GetProjectNameOfCode(ProjectCode.ToString()) + "&nbsp;&nbsp;项目实际成本超出预算成本（" + projectFactBudgetCost[1] + "/" + projectFactBudgetCost[0] + "）.";
                this._ObjAm.Send();
            }
        }

        public void DoOneTaskAlert(Guid ProjectCode, string TaskCode)
        {
            foreach (DataRow row in this.GetOneTaskFactBudgetCost(ProjectCode, TaskCode).Rows)
            {
                decimal num = (row["FactCost"] == DBNull.Value) ? 0M : decimal.Parse(row["FactCost"].ToString());
                decimal num2 = (row["BudgetCost"] == DBNull.Value) ? 0M : decimal.Parse(row["BudgetCost"].ToString());
                if (num > num2)
                {
                    SchedulePlanAction action = new SchedulePlanAction();
                    this.InitAlertMessage(ProjectCode, TaskCode);
                    this._ObjAm.Message = PrjInfoAction.GetProjectNameOfCode(ProjectCode.ToString()) + "&nbsp;&nbsp;" + action.GetTaskName(ProjectCode, TaskCode) + "实际成本超出预算成本（" + num.ToString() + "/" + num2.ToString() + "）.";
                    this._ObjAm.Send();
                }
            }
        }

        private DataTable GetOneTaskFactBudgetCost(Guid ProjectCode, string TaskCode)
        {
            string format = "select * from Prj_f_GetOneTaskFactBudgetCost('{0}','{1}')";
            return publicDbOpClass.DataTableQuary(string.Format(format, ProjectCode, TaskCode));
        }

        private string[] GetProjectFactBudgetCost(Guid ProjectCode)
        {
            string[] strArray = new string[2];
            string sqlString = "select isnull(sum(Quantity*SynthPrice)+(select sum([money]) from EPM_Cost_Cbs where PrjCode = '" + ProjectCode.ToString() + "' and CostType = 2),0) as BudgetCost  from EPM_Task_TaskList a where a.ProjectCode = '" + ProjectCode.ToString() + "'\tand wbstype =1";
            strArray[0] = publicDbOpClass.ExecuteScalar(sqlString).ToString();
            sqlString = "select isnull(sum(Price)+(select isnull(sum(Quantity*UnitPrice),0) from EPM_Book_Resource where ProjectCode = '" + ProjectCode.ToString() + "' and TaskBookCode in (select distinct TaskBookCode from EPM_Book_ConstructTask)),0) as FactCost from EPM_CostImportChild Child,Prj_CostImport Main where Child.[ID] = Main.[ID] and AuditResult = 1 and PrjCode = '" + ProjectCode.ToString() + "'";
            strArray[1] = publicDbOpClass.ExecuteScalar(sqlString).ToString();
            return strArray;
        }

        private void InitAlertMessage(Guid ProjectCode, string TaskCode)
        {
            this._ObjAm = new AlertMessage();
            this._ObjAm.ManInput = this.UserCode;
            this._ObjAm.MenAlertTo = this._ObjAp.YHDMsOfPeopleAlertTo;
            this._ObjAm.PresentimentID = this._ObjAp.pkID;
            this._ObjAm.PrjCode = ProjectCode.ToString();
            this._ObjAm.TimeInput = DateTime.Now;
            this._ObjAm.TimeOutput = DateTime.Now;
            this._ObjAm.TimeOver = this._ObjAm.TimeOutput.AddDays(this._ObjAp.ValidTimeLong);
        }

        public string AlertID
        {
            get
            {
                return this._AlertID;
            }
            set
            {
                this._AlertID = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }
    }
}

