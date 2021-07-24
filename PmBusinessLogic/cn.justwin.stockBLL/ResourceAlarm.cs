namespace cn.justwin.stockBLL
{
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;

    public class ResourceAlarm
    {
        private Dictionary<string, decimal> expectResource;
        private string message;
        private bool overpass;
        private string planCode;
        private string planGuid;
        private ProjectPlan prjPlan;

        public ResourceAlarm(ProjectPlan projectPlan, Dictionary<string, decimal> expectResource, string planCode, string planGuid)
        {
            this.prjPlan = projectPlan;
            this.expectResource = expectResource;
            this.planCode = planCode;
            this.planGuid = planGuid;
        }

        public void AddAlarm()
        {
            new MaterialWantPlan();
            List<string> prjUsers = this.prjPlan.GetPrjUsers(this.prjPlan.ProjectCode);
            if (prjUsers != null)
            {
                PTDbsjBll bll = new PTDbsjBll();
                foreach (string str in prjUsers)
                {
                    PTDbsjModel model = new PTDbsjModel {
                        C_OpenFlag = "0",
                        DTM_DBSJ = DateTime.Now,
                        I_XGID = this.planGuid,
                        V_Content = string.Format("物资需求计划:{0} 超出预算", this.planCode),
                        V_DBLJ = "StockManage/basicset/WantplanView.aspx?ic=" + this.planGuid,
                        V_LXBM = "021",
                        V_TPLJ = "new_Mail.gif",
                        V_YHDM = str
                    };
                    try
                    {
                        bll.Add(model);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public bool ClacExceed(Dictionary<string, decimal> factResource, ref string resourceName, ref string level)
        {
            bool flag = false;
            foreach (string str in this.expectResource.Keys)
            {
                if (this.expectResource[str] != 0M)
                {
                    bool flag2 = false;
                    if (!factResource.ContainsKey(str))
                    {
                        flag2 = true;
                    }
                    else if (factResource[str] == 0M)
                    {
                        level = "高";
                    }
                    else if (this.expectResource[str] <= factResource[str])
                    {
                        flag2 = false;
                    }
                    else
                    {
                        flag2 = true;
                        decimal diffRate = (this.expectResource[str] - factResource[str]) / factResource[str];
                        level = GetAlarmLevel(diffRate);
                    }
                    if (flag2)
                    {
                        flag = true;
                        if (string.IsNullOrEmpty(resourceName))
                        {
                            resourceName = this.prjPlan.GetResourceNameByCode(str);
                        }
                    }
                }
            }
            return flag;
        }

        public static string GetAlarmLevel(decimal diffRate)
        {
            string str = string.Empty;
            double num = ((double) diffRate) * -100.0;
            if (StockParameter.IsHighPlanAlarm && (num >= StockParameter.HighPlanAlarmLimit))
            {
                return "高";
            }
            if ((StockParameter.IsMidPlanAlarm && (num >= StockParameter.MidPlanAlarmLowerLimit)) && (num <= StockParameter.MidPlanAlarmUpperLimit))
            {
                return "中";
            }
            if ((StockParameter.IsLowPlanAlarm && (num >= StockParameter.LowPlanAlarmLowerLimit)) && (num <= StockParameter.LowPlanAlarmUpperLimit))
            {
                str = "低";
            }
            return str;
        }

        public void Validate()
        {
            Dictionary<string, decimal> factResource = this.prjPlan.GetFactResource();
            string resourceName = string.Empty;
            string level = string.Empty;
            bool flag = this.ClacExceed(factResource, ref resourceName, ref level);
            if (StockParameter.ProjectAlarm == SmEnum.SmSetValue.Alarm.ToString())
            {
                this.overpass = true;
                if (flag)
                {
                    this.AddAlarm();
                    this.message = resourceName + " 超出预算数量";
                }
            }
            else if (flag)
            {
                this.message = resourceName + " 超出预算数量";
                this.overpass = false;
            }
            else
            {
                this.AddAlarm();
                this.overpass = true;
            }
        }

        public Dictionary<string, decimal> ExpectResource
        {
            get
            {
                return this.expectResource;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public bool Overpass
        {
            get
            {
                return this.overpass;
            }
        }

        public string PlanCode
        {
            get
            {
                return this.planCode;
            }
        }

        public string PlanGuid
        {
            get
            {
                return this.planGuid;
            }
        }

        public ProjectPlan PrjPlan
        {
            get
            {
                return this.prjPlan;
            }
        }
    }
}

