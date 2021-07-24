namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Web;

    public class TaskBookAction
    {
        public int AddTaskBookResource(ArrayList ResourceList, Guid ProjectCode, string TaskCode, string TaskBookCode, out string Msg)
        {
            Msg = "";
            int num = 0;
            string str = string.Empty;
            string sqlString = "";
            if (ResourceList.Count > 0)
            {
                for (int i = 0; i < ResourceList.Count; i++)
                {
                    object obj2 = sqlString;
                    object obj3 = string.Concat(new object[] { obj2, " if not exists(select top 1 NoteID from EPM_Book_Resource where ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' and TaskBookCode='", TaskBookCode, "' and ResourceCode='", ((TaskBookResource) ResourceList[i]).ResourceCode, "') " }) + " insert into EPM_Book_Resource (ProjectCode,TaskCode,TaskBookCode,ResourceCode,Quantity,UnitPrice,ResourceStyle,FactQuantity)";
                    object obj4 = string.Concat(new object[] { obj3, " values ('", ((TaskBookResource) ResourceList[i]).ProjectCode, "','", ((TaskBookResource) ResourceList[i]).TaskCode, "','", ((TaskBookResource) ResourceList[i]).TaskBookCode, "'," }) + " '" + ((TaskBookResource) ResourceList[i]).ResourceCode + "',";
                    object obj5 = string.Concat(new object[] { obj4, " ", ((TaskBookResource) ResourceList[i]).Quantity, "," });
                    object obj6 = string.Concat(new object[] { obj5, " ", ((TaskBookResource) ResourceList[i]).UnitPrice, "," });
                    object obj7 = string.Concat(new object[] { obj6, " '", ((TaskBookResource) ResourceList[i]).ResourceStyle, "'," });
                    object obj8 = string.Concat(new object[] { obj7, " '", ((TaskBookResource) ResourceList[i]).FactQuantity, "')" }) + " else " + " update EPM_Book_Resource set ";
                    object obj9 = string.Concat(new object[] { obj8, " Quantity=", ((TaskBookResource) ResourceList[i]).Quantity, ", " });
                    object obj10 = string.Concat(new object[] { obj9, " UnitPrice=", ((TaskBookResource) ResourceList[i]).UnitPrice, " " }) + " where ";
                    sqlString = string.Concat(new object[] { obj10, " ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' and TaskBookCode='", TaskBookCode, "' and ResourceCode='", ((TaskBookResource) ResourceList[i]).ResourceCode, "' " });
                    str = str + ",'" + ((TaskBookResource) ResourceList[i]).ResourceCode + "'";
                    if (!this.CheckBudget(ProjectCode, TaskCode, TaskBookCode, ((TaskBookResource) ResourceList[i]).ResourceCode, ((TaskBookResource) ResourceList[i]).Quantity, ((TaskBookResource) ResourceList[i]).ResourceStyle, out Msg))
                    {
                        return 2;
                    }
                }
            }
            if (str != string.Empty)
            {
                object obj11 = sqlString + " delete from EPM_Book_Resource where ";
                sqlString = string.Concat(new object[] { obj11, " ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' and TaskBookCode='", TaskBookCode, "' and ResourceCode not in (", str.Trim(new char[] { ',' }), ") " });
            }
            else
            {
                object obj12 = sqlString + " delete from EPM_Book_Resource where ";
                sqlString = string.Concat(new object[] { obj12, " ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' and TaskBookCode='", TaskBookCode, "' " });
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                num = 1;
            }
            catch
            {
            }
            return num;
        }

        private bool CheckBudget(Guid ProjectCode, string TaskCode, string TaskBookCode, string ResourceCode, decimal Quantity, int ResourceStyle, out string Msg)
        {
            Msg = "";
            string[] strArray = (string[]) HttpContext.Current.Session["AlertTypes"];
            string alertCode = strArray[ResourceStyle - 1];
            AlertPoint point = new AlertPoint(alertCode);
            new SchedulePlanAction();
            ResourceItemAction action = new ResourceItemAction();
            AlertMessage message = new AlertMessage();
            UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
            if (currentUserInfo != null)
            {
                message.ManInput = currentUserInfo.UserCode;
            }
            message.MenAlertTo = point.YHDMsOfPeopleAlertTo;
            message.TimeInput = DateTime.Now;
            message.TimeOutput = DateTime.Now;
            message.TimeOver = DateTime.Now.AddDays(point.ValidTimeLong);
            message.PresentimentID = point.pkID;
            if (!point.Options[2].IsSelected)
            {
                string format = "select dbo.Prj_f_CompareBugdet('{0}','{1}','{2}','{3}',{4})";
                decimal num = decimal.Parse(publicDbOpClass.ExecuteScalar(string.Format(format, new object[] { ProjectCode, TaskCode, TaskBookCode, ResourceCode, Quantity })).ToString());
                if (num >= 0M)
                {
                    return true;
                }
                if (point.Options[0].IsSelected)
                {
                    Msg = action.GetResourceItem(ResourceCode).ResourceName + "超出预算指标：" + Math.Abs(num).ToString();
                    return false;
                }
            }
            return true;
        }

        public static int DeleteTaskBookResource(string taskcode, string resCode)
        {
            return publicDbOpClass.ExecSqlString("delete EPM_Book_Resource where TaskBookCode='" + taskcode + "' and ResourceCode='" + resCode + "'");
        }

        public void DeleteTaskResource(string strProjectCode, string strTaskCode, string strTaskBookCode, string type)
        {
            string sqlString = string.Empty;
            if (type == "add")
            {
                string str2 = sqlString;
                sqlString = str2 + "delete from EPM_Book_Resource where ProjectCode='" + strProjectCode + "' and TaskCode='" + strTaskCode + "' and TaskBookCode='" + strTaskBookCode + "' ";
            }
            if (type == "edit")
            {
                string str3 = sqlString;
                string str4 = str3 + "delete from EPM_Book_ConstructTask where ProjectCode='" + strProjectCode + "' and TaskCode='" + strTaskCode + "' and TaskBookCode='" + strTaskBookCode + "' ";
                string str5 = str4 + "delete from EPM_Book_Resource where ProjectCode='" + strProjectCode + "' and TaskCode='" + strTaskCode + "' and TaskBookCode='" + strTaskBookCode + "' ";
                sqlString = str5 + "update EPM_Task_TaskList set CompleteCount=case when isnull(Quantity,0)= 0 then 0 else (isnull(dbo.f_Construct_GetOverQuantity(ProjectCode,TaskCode),0)/isnull(Quantity,0))*100 end where ProjectCode='" + strProjectCode + "' and TaskCode='" + strTaskCode + "' ";
            }
            try
            {
                publicDbOpClass.DataTableQuary(sqlString);
            }
            catch
            {
            }
        }

        public static int DeleteTempResource(string usercode)
        {
            return publicDbOpClass.ExecSqlString("delete from EPM_Res_TempResource where UserCode='" + usercode + "'");
        }

        public static int DeleteTempResource(string usercode, string resCode)
        {
            return publicDbOpClass.ExecSqlString("delete from EPM_Res_TempResource where UserCode='" + usercode + "' and ResourceCode='" + resCode + "'");
        }

        public static DataTable DoAlert(Guid ProjectCode, string TaskCode, int ResourceStyle, string TaskBookCode)
        {
            string format = "Select * from dbo.Prj_f_GetCompare('{0}','{1}',{2},'{3}')";
            return publicDbOpClass.DataTableQuary(string.Format(format, new object[] { ProjectCode, TaskCode, ResourceStyle, TaskBookCode }));
        }

        public DataTable GetConstructResourceGather(Guid projectCode, Guid taskBookCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", projectCode), new SqlParameter("@TaskBookCode", taskBookCode) };
            return publicDbOpClass.ExecuteDataTable("EPM_P_Construct_ResourceGather", commandParameters);
        }

        public static string GetResourceStyle(string code)
        {
            return Convert.ToString(publicDbOpClass.ExecuteScalar("select ResourceStyle from EPM_Res_Resource where ResourceCode='" + code + "'"));
        }

        public DataTable GetTaskBook(Guid ProjectCode, string TaskCode, string taskBookCode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(((str + " select b.*,isnull(b.Quantity,0) as TotalQuantity,isnull(b.Quantity,0)-isnull(b.CompleteCount,0) as LeavlQuantity,isnull(a.TodayComplete,0) as TodayComplete,a.TodayWorkRemark,a.WorkContent ") + " from EPM_Book_ConstructTask a join EPM_Task_TaskList b on a.ProjectCode=b.ProjectCode and a.TaskCode=b.TaskCode and b.WbsType=1 " + " where ") + " a.TaskBookCode='" + taskBookCode + "' ");
        }

        public DataTable GetTaskBook1(Guid ProjectCode, string sTaskCode, string tbc)
        {
            string str = "";
            string str2 = string.Empty;
            if (sTaskCode.Length > 0)
            {
                foreach (string str3 in sTaskCode.Split(new char[] { ',' }))
                {
                    str2 = str2 + ",'" + str3 + "'";
                }
            }
            else
            {
                str2 = " '" + string.Empty + "' ";
            }
            str2 = str2.Trim(new char[] { ',' });
            object obj2 = ((str + "select *,isnull(Quantity,0) as TotalQuantity,isnull(Quantity,0)-isnull(dbo.f_Construct_GetOverQuantity(ProjectCode,TaskCode),0) as LeavlQuantity," + " 0 as TodayComplete,'' as TodayWorkRemark,'' as WorkContent") + " from EPM_Task_TaskList where TaskCode in (" + str2 + ") ") + " and Taskcode not in (select TaskCode from EPM_Book_ConstructTask where TaskBookCode = '" + tbc + "') ";
            object obj3 = (string.Concat(new object[] { obj2, " and Projectcode = '", ProjectCode, "' union" }) + " select b.*,isnull(b.Quantity,0) as TotalQuantity,isnull(b.Quantity,0)-isnull(dbo.f_Construct_GetOverQuantity(b.ProjectCode,b.TaskCode),0) as LeavlQuantity,isnull(a.TodayComplete,0) as TodayComplete,a.TodayWorkRemark,a.WorkContent ") + " from EPM_Task_TaskList b left join EPM_Book_ConstructTask a on a.ProjectCode=b.ProjectCode and a.TaskCode=b.TaskCode and b.WbsType=1 " + " where ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj3, " b.ProjectCode='", ProjectCode, "' and b.TaskCode in (", str2, ") and b.WbsType=1 " }) + " and a.taskBookCode = '" + tbc + "'");
        }

        public DataTable GetTaskBook2(Guid ProjectCode, string strTaskBookCode)
        {
            object obj2 = ("" + " select b.*,isnull(b.Quantity,0) as TotalQuantity,(isnull(b.Quantity,0)-isnull(dbo.f_Construct_GetOverQuantity(b.ProjectCode,b.TaskCode),0)) as LeavlQuantity,isnull(a.TodayComplete,0) as TodayComplete,a.TodayWorkRemark,a.WorkContent ") + " from EPM_Book_ConstructTask a left join EPM_Task_TaskList b on a.ProjectCode=b.ProjectCode and a.TaskCode=b.TaskCode and b.WbsType=1 " + " where ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " a.ProjectCode='", ProjectCode, "' and a.TaskBookCode='", strTaskBookCode, "' " }));
        }

        public DataTable GetTaskTable(string strProjectCode, string sTaskCode)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (sTaskCode.Length > 0)
            {
                foreach (string str3 in sTaskCode.Split(new char[] { ',' }))
                {
                    str2 = str2 + ",'" + str3 + "'";
                }
            }
            else
            {
                str2 = " '" + string.Empty + "' ";
            }
            str2 = str2.Trim(new char[] { ',' });
            string str4 = str + " select a.TaskName,isnull(a.Quantity,0) as TotalQuantity, (isnull(a.Quantity,0)-isnull(dbo.f_Construct_GetOverQuantity(a.ProjectCode,a.TaskCode),0)) as LeavlQuantity,a.TaskCode from EPM_Task_TaskList a " + " where ";
            return publicDbOpClass.DataTableQuary(str4 + " a.ProjectCode='" + strProjectCode + "' and a.TaskCode in (" + str2 + ") and a.WbsType=1 ");
        }

        public static DataTable GetTempResource(string usercode)
        {
            return publicDbOpClass.DataTableQuary("select *,0 as Quantity from EPM_Res_TempResource where UserCode='" + usercode + "'");
        }

        public static DataTable GetTempResourceOfBookTask(string usercode)
        {
            return publicDbOpClass.DataTableQuary("select NoteId,'1',ResourceCode,'0',Price as UnitPrice,ResourceStyle,price as FactQuantiey,price as CostType,ResourceName,UnitName,Specification,UserCode,0 as quantity  from EPM_Res_TempResource where UserCode='" + usercode + "'");
        }

        public DataTable QueryInitRationList(Guid projectCode, string taskCode)
        {
            string str = "";
            string str2 = str + " select a.RationItem,b.ItemName as RationName,b.ItemUnit as UnitName,a.Quantity,'' as Remark from EPM_Task_Ration a left join EPM_Ration_PrivateItem b ";
            return publicDbOpClass.DataTableQuary(str2 + " on a.VersionCode = b.VersionCode and a.RationItem = b.ItemCode where a.ProjectCode='" + projectCode.ToString() + "' and a.TaskCode='" + taskCode + "'");
        }

        public bool TaskBookCode(string TaskBookCode)
        {
            bool flag = false;
            string str = "";
            if (publicDbOpClass.DataTableQuary(str + "select * from EPM_Book_ConstructTask where TaskBookCode ='" + TaskBookCode + "'").Rows.Count > 1)
            {
                flag = true;
            }
            return flag;
        }

        public int TaskBookIns(ArrayList arr)
        {
            int num = 0;
            string sqlString = "";
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    object obj2 = sqlString + " insert into EPM_Book_ConstructTask (TaskBookCode,ProjectCode,TaskCode,ConstructUnit,ConstructDate," + "RecordPerson,QualityAndSafety,TodayComplete,WorkContent,TodayWorkRemark)";
                    object obj3 = string.Concat(new object[] { obj2, " values('", ((ConstructTaskBook) arr[i]).TaskBookCode, "','", ((ConstructTaskBook) arr[i]).ProjectCode, "'," });
                    object obj4 = string.Concat(new object[] { obj3, "'", ((ConstructTaskBook) arr[i]).TaskCode, "','", ((ConstructTaskBook) arr[i]).ConstructUnit, "'," });
                    object obj5 = string.Concat(new object[] { obj4, "'", ((ConstructTaskBook) arr[i]).ConstructDate, "','", ((ConstructTaskBook) arr[i]).RecordPerson, "'," });
                    string str2 = string.Concat(new object[] { obj5, "'", ((ConstructTaskBook) arr[i]).QualityAndSafety, "','", ((ConstructTaskBook) arr[i]).TodayComplete, "'," });
                    object obj6 = (str2 + "'" + ((ConstructTaskBook) arr[i]).WorkContent + "','" + ((ConstructTaskBook) arr[i]).TodayWorkRemark + "')") + " update EPM_Task_TaskList set CompleteCount=case when isnull(Quantity,0)= 0 then 0 else (isnull(dbo.f_Construct_GetOverQuantity(ProjectCode,TaskCode),0)/isnull(Quantity,0))*100 end";
                    sqlString = string.Concat(new object[] { obj6, " where ProjectCode='", ((ConstructTaskBook) arr[i]).ProjectCode, "' and TaskCode='", ((ConstructTaskBook) arr[i]).TaskCode, "' " });
                }
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                num = 1;
            }
            catch
            {
            }
            return num;
        }

        public int TaskBookIns(ConstructTaskBook ctb, ArrayList RationList, ArrayList ResourceList)
        {
            string str = "begin";
            object obj2 = str + " insert into EPM_Book_ConstructTask (TaskBookCode,ProjectCode,TaskCode,ConstructUnit,ConstructDate,RecordPerson,QualityAndSafety,TodayComplete,WorkContent,TodayWorkRemark)";
            object obj3 = string.Concat(new object[] { 
                obj2, " values('", ctb.TaskBookCode, "','", ctb.ProjectCode, "','", ctb.TaskCode, "','", ctb.ConstructUnit, "','", ctb.ConstructDate, "','", ctb.RecordPerson, "','", ctb.QualityAndSafety, "','", 
                ctb.TodayComplete, "','", ctb.WorkContent, "','", ctb.TodayWorkRemark, "')"
             });
            str = string.Concat(new object[] { obj3, " update EPM_Task_TaskList set CompleteCount=case when isnull(Quantity,0)= 0 then 0 else (isnull(dbo.f_Construct_GetOverQuantity(ProjectCode,TaskCode),0)/isnull(Quantity,0))*100 end where ProjectCode='", ctb.ProjectCode, "' and TaskCode='", ctb.TaskCode, "' " });
            for (int i = 0; i < ResourceList.Count; i++)
            {
                object obj4 = ((str + " insert into EPM_Book_Resource (TaskBookCode,ResourceCode,Quantity,UnitPrice,ResourceStyle,FactQuantity)") + " values ('" + ((TaskBookResource) ResourceList[i]).TaskBookCode + "',") + " '" + ((TaskBookResource) ResourceList[i]).ResourceCode + "',";
                object obj5 = string.Concat(new object[] { obj4, " '", ((TaskBookResource) ResourceList[i]).Quantity, "'," });
                object obj6 = string.Concat(new object[] { obj5, " '", ((TaskBookResource) ResourceList[i]).UnitPrice, "'," });
                object obj7 = string.Concat(new object[] { obj6, " '", ((TaskBookResource) ResourceList[i]).ResourceStyle, "'," });
                str = string.Concat(new object[] { obj7, " '", ((TaskBookResource) ResourceList[i]).FactQuantity, "')" });
            }
            return publicDbOpClass.ExecSqlString(str + " end");
        }

        public DataTable TaskBookList(Guid PrjCode)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " select distinct TaskBookCode,ProjectCode,ConstructDate,RecordPerson from EPM_Book_ConstructTask where ProjectCode = '", PrjCode, "'order by ConstructDate desc " }));
        }

        public DataTable TaskBookList(Guid PrjCode, string strTaskBookCode)
        {
            string str = "";
            object obj2 = str + " select distinct TaskBookCode,ProjectCode,ConstructDate,RecordPerson,QualityAndSafety from EPM_Book_ConstructTask  ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where ProjectCode = '", PrjCode, "' and TaskBookCode='", strTaskBookCode, "' " }));
        }

        public DataTable TaskBookList(Guid PrjCode, string taskCode, string TaskBillCode)
        {
            string str = "";
            object obj2 = str + " select a.*,(select TaskName from EPM_Task_TaskList b where b.TaskCode =a.TaskCode and b.ProjectCode = a.ProjectCode and b.WbsType=1) as TaskName ," + " (select CorpName from XPM_Basic_ContactCorp where CorpID =ConstructUnit) as UnitName ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " from EPM_Book_ConstructTask a where a.ProjectCode = '", PrjCode, "' and a.TaskCode ='", taskCode, "' and a.TaskBookCode = '", TaskBillCode, "' " }));
        }

        public int taskbookRationIns(ArrayList RationList)
        {
            string sqlString = " begin";
            for (int i = 0; i < RationList.Count; i++)
            {
                object obj2 = ((sqlString + " insert into EPM_Book_Ration (TaskBookCode,RationCode,Quantity,Remark) ") + " values ('" + ((TaskBookRation) RationList[i]).TaskBookCode + "',") + " '" + ((TaskBookRation) RationList[i]).RationCode + "',";
                sqlString = string.Concat(new object[] { obj2, "'", ((TaskBookRation) RationList[i]).Quantity, "'," }) + "'" + ((TaskBookRation) RationList[i]).Remark + "')";
            }
            sqlString = " end";
            return publicDbOpClass.ExecSqlString(sqlString);
        }

        public DataTable TaskBookRationList(string TaskBookCode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary((str + "select a.*,a.RationCode as RationItem,b.ItemName as RationName,b.ItemUnit as UnitName from EPM_Book_Ration") + " a left join EPM_Ration_PrivateItem b on a.RationCode=b.ItemCode where a.TaskBookCode ='" + TaskBookCode + "'");
        }

        public int TaskBookResourceIns(ArrayList ResourceList)
        {
            string str = " begin ";
            for (int i = 0; i < ResourceList.Count; i++)
            {
                object obj2 = ((str + " insert into EPM_Book_Resource (TaskBookCode,ResourceCode,Quantity,UnitPrice,ResourceStyle,FactQuantity)") + " values ('" + ((TaskBookResource) ResourceList[i]).TaskBookCode + "',") + " '" + ((TaskBookResource) ResourceList[i]).ResourceCode + "',";
                object obj3 = string.Concat(new object[] { obj2, " '", ((TaskBookResource) ResourceList[i]).Quantity, "'," });
                object obj4 = string.Concat(new object[] { obj3, " '", ((TaskBookResource) ResourceList[i]).UnitPrice, "'," });
                object obj5 = string.Concat(new object[] { obj4, " '", ((TaskBookResource) ResourceList[i]).ResourceStyle, "'," });
                str = string.Concat(new object[] { obj5, " '", ((TaskBookResource) ResourceList[i]).FactQuantity, "')" });
            }
            return publicDbOpClass.ExecSqlString(str + " end");
        }

        public DataTable TaskBookResourceList(string TaskBookCode)
        {
            string str = "";
            str = "select a.*,a.UnitPrice as price,b.resourcename,b.unitname,b.specification from EPM_Book_Resource a  ";
            return publicDbOpClass.DataTableQuary((str + " left join epm_v_res_resourcebasic b on b.ResourceCode = a.ResourceCode ") + " where a.TaskBookCode='" + TaskBookCode + "'");
        }

        public DataTable TaskBookResourceList(string TaskBookCode, int style)
        {
            string str = "";
            str = "select a.*,a.Quantity,b.resourcename,b.unitname,b.specification from EPM_Book_Resource a  ";
            object obj2 = str + " left join epm_v_res_resourcebasic b on b.ResourceCode = a.ResourceCode ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where a.TaskBookCode='", TaskBookCode, "' and a.ResourceStyle = ", style }));
        }

        public DataTable TaskBookResourceList(string strProjectCode, string strTaskCode, string TaskBookCode)
        {
            string str = "";
            str = "select a.*,a.Quantity,a.UnitPrice as price,b.resourcename,b.unitname,b.specification from EPM_Book_Resource a  ";
            string str2 = str + " left join epm_v_res_resourcebasic b on b.ResourceCode = a.ResourceCode ";
            return publicDbOpClass.DataTableQuary(str2 + " where a.ProjectCode='" + strProjectCode + "' and a.TaskCode='" + strTaskCode + "' and a.TaskBookCode='" + TaskBookCode + "' ");
        }

        public int TaskBookUp(ArrayList arrctb, ArrayList arr)
        {
            string sqlString = "";
            if (arrctb.Count > 0)
            {
                for (int i = 0; i < arrctb.Count; i++)
                {
                    object obj2 = sqlString;
                    object obj3 = (string.Concat(new object[] { obj2, " if exists(select top 1 NoteID from EPM_Book_ConstructTask where ProjectCode = '", ((ConstructTaskBook) arrctb[0]).ProjectCode, "' and TaskCode='", ((ConstructTaskBook) arrctb[i]).TaskCode, "' and TaskBookCode='", ((ConstructTaskBook) arrctb[i]).TaskBookCode, "') " }) + " update EPM_Book_ConstructTask set ") + " RecordPerson = '" + ((ConstructTaskBook) arrctb[0]).RecordPerson + "',";
                    object obj4 = string.Concat(new object[] { obj3, " ConstructDate = '", ((ConstructTaskBook) arrctb[0]).ConstructDate, "'," }) + " QualityAndSafety='" + ((ConstructTaskBook) arrctb[0]).QualityAndSafety + "', ";
                    object obj5 = (string.Concat(new object[] { obj4, " TodayComplete='", ((ConstructTaskBook) arrctb[i]).TodayComplete, "', " }) + " WorkContent='" + ((ConstructTaskBook) arrctb[i]).WorkContent + "', ") + " TodayWorkRemark='" + ((ConstructTaskBook) arrctb[i]).TodayWorkRemark + "' ";
                    object obj6 = string.Concat(new object[] { obj5, " where ProjectCode = '", ((ConstructTaskBook) arrctb[0]).ProjectCode, "' and TaskCode='", ((ConstructTaskBook) arrctb[i]).TaskCode, "' and TaskBookCode='", ((ConstructTaskBook) arrctb[i]).TaskBookCode, "' " }) + " else ";
                    sqlString = string.Concat(new object[] { 
                        obj6, " insert into EPM_Book_ConstructTask values('", ((ConstructTaskBook) arrctb[i]).TaskBookCode, "','", ((ConstructTaskBook) arrctb[i]).ProjectCode, "','", ((ConstructTaskBook) arrctb[i]).TaskCode, "','", ((ConstructTaskBook) arrctb[i]).ConstructUnit, "','", ((ConstructTaskBook) arrctb[i]).ConstructDate, "','", ((ConstructTaskBook) arrctb[i]).RecordPerson, "','", ((ConstructTaskBook) arrctb[i]).QualityAndSafety, "','", 
                        ((ConstructTaskBook) arrctb[i]).TodayComplete, "','", ((ConstructTaskBook) arrctb[i]).WorkContent, "','", ((ConstructTaskBook) arrctb[i]).TodayWorkRemark, "')"
                     });
                }
            }
            if (arr.Count > 0)
            {
                for (int j = 0; j < arr.Count; j++)
                {
                    object obj7 = (sqlString + " update EPM_Task_TaskList set ") + " CompleteCount=case when isnull(Quantity,0)= 0 then 0 else (isnull(dbo.f_Construct_GetOverQuantity(ProjectCode,TaskCode),0)/isnull(Quantity,0))*100 end " + " where ";
                    sqlString = string.Concat(new object[] { obj7, " ProjectCode='", ((TaskListInfo) arr[j]).ProjectCode, "' and TaskCode='", ((TaskListInfo) arr[j]).TaskCode, "' and WbsType=1 " });
                }
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
            }
            return 0;
        }

        public int TaskCodeBookDel(string strTaskBookCode)
        {
            string str = "begin ";
            return publicDbOpClass.ExecSqlString(((((((((str + "declare @pc nvarchar(50) " + "declare @tc nvarchar(50) ") + "select @pc=ProjectCode, @tc=TaskCode from EPM_Book_ConstructTask where TaskBookCode='" + strTaskBookCode + "' ") + "delete EPM_Book_Resource where TaskBookCode = '" + strTaskBookCode + "' ") + "delete EPM_Book_ConstructTask where TaskBookCode = '" + strTaskBookCode + "' ") + "update EPM_Task_TaskList ") + "set  CompleteCount=" + "case ") + "when isnull(Quantity,0)= 0 then 0 " + "else (isnull(dbo.f_Construct_GetOverQuantity(@pc,@tc),0)/isnull(Quantity,0))*100") + " end" + " from EPM_Task_TaskList") + " where WbsType=1 and ProjectCode=@pc" + " end");
        }

        public int TaskCodeBookrationDel(int noteid)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + "delete EPM_Book_Ration where NoteID = " + noteid);
        }
    }
}

