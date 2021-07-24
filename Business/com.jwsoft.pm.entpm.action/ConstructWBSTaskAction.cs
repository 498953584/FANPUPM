namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.sysManage.publicStringOperation;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.Configuration;

    public class ConstructWBSTaskAction
    {
        public int BidScheduleAdd(WBSBidTask bid)
        {
            bid.TaskCode = PublicClass.CheckString(bid.TaskCode);
            string str2 = (" begin " + " insert into EPM_Task_TaskList(ParentTaskCode,TaskCode,TaskName,") + " Quantity,QuantityUnit,ChildNum,ProjectCode,WorkLayer,IsValid,Cost,SynthPrice, " + " StartDate,EndDate,Remark,WbsType,ContractPrice)";
            object obj2 = (((((str2 + " values('" + bid.ParentTaskCode + "','" + bid.TaskCode + "','" + bid.TaskName + "',") + bid.Quantity.ToString() + ",") + "'" + bid.QuantityUnit + "',") + "0,") + "'" + bid.ProjectCode.ToString() + "',") + ((bid.TaskCode.Length / 3)).ToString() + ",";
            string str3 = string.Concat(new object[] { obj2, "1,0,", bid.SynthPrice.ToString(), ",'", bid.StartDate, "','", bid.EndDate, "','", bid.Remark, "','", bid.WbsType.ToString(), "',", bid.SynthPrice.ToString(), ")" });
            return publicDbOpClass.ExecSqlString((str3 + " update EPM_Task_TaskList set ChildNum = ChildNum + 1 where ProjectCode = '" + bid.ProjectCode.ToString() + "' and TaskCode = '" + bid.ParentTaskCode + "' and IsValid = 1 and WbsType=" + bid.WbsType.ToString()) + " end ");
        }

        public int BidScheduleUp(WBSBidTask bid, string IsAlter)
        {
            string str = "";
            str = " update EPM_Task_TaskList ";
            string str2 = str + " set TaskName = '" + bid.TaskName + "',";
            object obj2 = str2 + " Quantity = " + bid.Quantity.ToString() + ",QuantityUnit = '" + bid.QuantityUnit + "',";
            object obj3 = string.Concat(new object[] { obj2, " SynthPrice=", bid.SynthPrice.ToString(), ",StartDate = '", bid.StartDate, "', EndDate='", bid.EndDate, "', " });
            string str3 = string.Concat(new object[] { obj3, " WorkLayer = '", bid.WorkLayer, "', " });
            object obj4 = (str3 + " SumPrice=" + bid.sumPrice.ToString() + ",IsAlter='" + IsAlter + "'") + " ,ContractPrice=" + bid.SynthPrice.ToString();
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " where NoteID = ", bid.NoteID, " and IsValid = 1 and ProjectCode = '", bid.ProjectCode, "'" }));
        }

        public int BidTaskDel(int NoteID)
        {
            string str = "select ProjectCode,TaskCode,ParentTaskCode,WbsType from EPM_Task_TaskList ";
            object obj2 = str;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where NoteID = '", NoteID, "'" }));
            string str2 = table.Rows[0]["ProjectCode"].ToString();
            string str3 = table.Rows[0]["TaskCode"].ToString();
            string str4 = table.Rows[0]["ParentTaskCode"].ToString();
            int num = (int) table.Rows[0]["Wbstype"];
            string str5 = "begin";
            string str6 = str5 + " update EPM_Task_TaskList set ChildNum = ChildNum-1 where TaskCode = '" + str4 + "'  ";
            string str7 = (str6 + " and ProjectCode = '" + str2 + "' and WbsType=" + num.ToString()) + " delete from EPM_Task_Resource where ProjectCode = '" + str2 + "'";
            object obj3 = str7 + " and  TaskCode = '" + str3 + "' and WbsType=" + num.ToString();
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " delete EPM_Task_TaskList where NoteID = ", NoteID, " and IsValid = 1" }) + " end");
        }

        public void ClearTempTableData(Guid projectCode, Guid pageCode, string userCode)
        {
            publicDbOpClass.ExecSqlString(string.Concat(new object[] { " delete from EPM_Sch_TempSchedule where ProjectCode='", projectCode, "' and PageCode='", pageCode, "' and UserCode='", userCode, "' " }));
        }

        public int ContUpdateScheduleAdd(WBSBidTask bid, string ContractCode, string ContRecordID)
        {
            string str2 = (" begin " + " insert into EPM_Con_UpdateTaskList(ContractCode,ParentTaskCode,TaskCode,TaskName,") + " Quantity,QuantityUnit,ChildNum,ProjectCode,WorkLayer,IsValid,Cost,SynthPrice, " + " StartDate,EndDate,Remark,WbsType,ContractUpdateID,ContractPrice)";
            object obj2 = (((((str2 + " values('" + ContractCode + "','" + bid.ParentTaskCode + "','" + bid.TaskCode + "','" + bid.TaskName + "',") + bid.Quantity.ToString() + ",") + "'" + bid.QuantityUnit + "',") + "0,") + "'" + bid.ProjectCode.ToString() + "',") + ((bid.TaskCode.Length / 3)).ToString() + ",";
            string str3 = string.Concat(new object[] { obj2, "1,0,", bid.SynthPrice.ToString(), ",'", bid.StartDate, "','", bid.EndDate, "','", bid.Remark, "','", bid.WbsType.ToString(), "','", ContRecordID, "',", bid.SynthPrice.ToString(), ")" });
            return publicDbOpClass.ExecSqlString((str3 + " update EPM_Con_UpdateTaskList set ChildNum = ChildNum + 1 where ProjectCode = '" + bid.ProjectCode.ToString() + "' and ContractCode='" + ContractCode + "' and TaskCode = '" + bid.ParentTaskCode + "' and IsValid = 1 and WbsType=" + bid.WbsType.ToString() + " and ContractUpdateID = '" + ContRecordID + "'") + " end ");
        }

        public int ContUpdateScheduleUp(WBSBidTask bid, string ContractCode, string ContRecordID)
        {
            string str = "";
            str = " update EPM_Con_UpdateTaskList ";
            string str2 = str + " set TaskName = '" + bid.TaskName + "',";
            object obj2 = str2 + " Quantity = " + bid.Quantity.ToString() + ",QuantityUnit = '" + bid.QuantityUnit + "',";
            object obj3 = string.Concat(new object[] { obj2, " SynthPrice=", bid.SynthPrice.ToString(), ",StartDate = '", bid.StartDate, "', EndDate='", bid.EndDate, "',Remark = '", bid.Remark, "', " });
            object obj4 = (string.Concat(new object[] { obj3, " WorkLayer = '", bid.WorkLayer, "' " }) + " ,SumPrice=" + bid.sumPrice.ToString()) + " ,ContractPrice=" + bid.SynthPrice.ToString();
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " where NoteID = ", bid.NoteID, " and IsValid = 1 and ProjectCode = '", bid.ProjectCode, "' and ContractCode='", ContractCode, "' and ContractUpdateID = '", ContRecordID, "' " }));
        }

        public int ContUpdateTaskDel(int NoteID)
        {
            string str = "select ProjectCode,TaskCode,ParentTaskCode,WbsType,ContractUpdateID from EPM_Con_UpdateTaskList ";
            object obj2 = str;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where NoteID = '", NoteID, "'" }));
            string str2 = table.Rows[0]["ProjectCode"].ToString();
            table.Rows[0]["TaskCode"].ToString();
            string str3 = table.Rows[0]["ParentTaskCode"].ToString();
            int num = (int) table.Rows[0]["Wbstype"];
            string str4 = table.Rows[0]["ContractUpdateID"].ToString();
            string str5 = "begin";
            string str6 = str5 + " update EPM_Con_UpdateTaskList set ChildNum = ChildNum-1 where TaskCode = '" + str3 + "'  ";
            object obj3 = str6 + " and ProjectCode = '" + str2 + "' and WbsType=" + num.ToString() + " and ContractUpdateID = '" + str4 + "' ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " delete EPM_Con_UpdateTaskList where NoteID = ", NoteID, " and IsValid = 1" }) + " end");
        }

        public WBSBidTaskCollection GetAllBidScheduleList(Guid projectCode, int wbsType)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            string str = WebConfigurationManager.AppSettings["IsNewProject"].ToString();
            string sqlString = "";
            if (str == "1")
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("\r\n                    WITH ctetasks \r\n                         AS (SELECT TaskId, \r\n                                    TaskName, \r\n                                    ParentId, \r\n                                    OrderNumber \r\n                             FROM   Bud_Task \r\n                             WHERE  PrjId = '{0}' \r\n                                    AND Version = (SELECT CurVersion \r\n                                                   FROM   vGetCurBudVersion \r\n                                                   WHERE  PrjId = \r\n                                                  '{0}') \r\n                            ), \r\n                         cteformattasks \r\n                         AS (SELECT TaskId                                AS TaskCode, \r\n                                    TaskName, \r\n                                    ParentId                              AS ParentTaskCode, \r\n                                    OrderNumber, \r\n                                    (SELECT Count(*) \r\n                                     FROM   ctetasks \r\n                                     WHERE  T.TaskId = cteTasks.ParentId) AS ChildNum \r\n                             FROM   ctetasks AS T) \r\n                    SELECT TaskCode, \r\n                           TaskName, \r\n                           ChildNum,\r\n                           ParentTaskCode\r\n                    FROM   cteformattasks \r\n                    ORDER  BY OrderNumber ASC ", projectCode);
                sqlString = builder.ToString();
            }
            else
            {
                sqlString = "select * from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and IsValid = 1 and WbsType=" + wbsType.ToString() + " order by TaskCode asc";
            }
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRowOfNew(table.Rows[i]));
            }
            return tasks;
        }

        public WBSBidTaskCollection GetAllBidScheduleList(Guid projectCode, int wbsType, int workLayer)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select * from EPM_Task_TaskList where workLayer<=", workLayer, " and  ProjectCode='", projectCode.ToString(), "' and IsValid = 1 and WbsType=", wbsType.ToString(), " order by TaskCode asc" }));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRowOfNew(table.Rows[i]));
            }
            return tasks;
        }

        public WBSBidTaskCollection GetAllScheduleList(Guid projectCode, Guid pageCode, string userCode)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            string str = "";
            object obj2 = str;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " select * from EPM_Sch_TempSchedule where ProjectCode='", projectCode, "' and PageCode='", pageCode, "' and UserCode='", userCode, "' order by TaskCode asc " }));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRow(table.Rows[i]));
            }
            return tasks;
        }

        public WBSBidTaskCollection GetAllScheduleList(Guid projectCode, string taskCode, string taskName, int projectType, Guid pageCode, string userCode, int wbsType)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            string str2 = "";
            object obj2 = (str2 + " if exists(select * from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and TaskCode like '" + taskCode + "%' and TaskName like '%" + taskName + "%' and WorkLayer='3' and IsValid = 1 and WbsType=" + wbsType.ToString() + ") ") + " insert into EPM_Sch_TempSchedule(PageCode,UserCode,ProjectCode,TaskCode,ParentTaskCode,TaskName,Quantity,QuantityUnit,WorkLayer,ChildNum,IsValid,Cost,SynthPrice,StartDate,EndDate,Remark,ContractPrice,Safety,Quality,TaskState,Pivotal,WorkDay,CompleteCount) ";
            string str3 = string.Concat(new object[] { obj2, " select '", pageCode, "','", userCode, "',ProjectCode,TaskCode,ParentTaskCode,TaskName,isnull(Quantity,0) as Quantity,QuantityUnit,isnull(WorkLayer,0) as WorkLayer,isnull(ChildNum,0) as ChildNum,IsValid,isnull(Cost,0) as Cost,isnull(SynthPrice,0) as SynthPrice, " }) + " StartDate,EndDate,Remark,isnull(ContractPrice,0) as ContractPrice,Safety,Quality,isnull(TaskState,0) as TaskState,Pivotal,WorkDay,isnull(CompleteCount,0) as CompleteCount ";
            object obj3 = str3 + " from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and TaskCode like '" + taskCode + "%' and TaskName like '%" + taskName + "%' and WorkLayer='3' and IsValid = 1 and WbsType=" + wbsType.ToString() + " order by TaskCode asc ";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj3, " select PageCode,UserCode,ProjectCode,TaskCode,'' as ParentTaskCode,TaskName,Quantity,QuantityUnit,WorkLayer,ChildNum,IsValid,Cost,SynthPrice,StartDate,EndDate,Remark,ContractPrice,Safety,Quality,TaskState,Pivotal,WorkDay,CompleteCount from EPM_Sch_TempSchedule where ProjectCode='", projectCode, "' and PageCode='", pageCode, "' and UserCode='", userCode, "' order by TaskCode asc " }));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRow(table.Rows[i]));
            }
            return tasks;
        }

        public WBSBidTaskCollection GetAllScheduleList(string type, Guid projectCode, DateTime dtStartDate, DateTime dtEndDate, Guid pageCode, string userCode, int wbsType)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            string sqlString = "";
            if (type == "N")
            {
                object obj2 = (sqlString + " if exists(select * from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and IsValid = 1) ") + " insert into EPM_Sch_TempSchedule(PageCode,UserCode,ProjectCode,TaskCode,ParentTaskCode,TaskName,Quantity,QuantityUnit,WorkLayer,ChildNum,IsValid,Cost,SynthPrice,StartDate,EndDate,Remark,ContractPrice,Safety,Quality,TaskState,Pivotal,WorkDay,CompleteCount) ";
                string str2 = string.Concat(new object[] { obj2, " select '", pageCode, "','", userCode, "',ProjectCode,TaskCode,ParentTaskCode,TaskName,isnull(Quantity,0) as Quantity,QuantityUnit,isnull(WorkLayer,0) as WorkLayer,isnull(ChildNum,0) as ChildNum,IsValid,isnull(Cost,0) as Cost,isnull(SynthPrice,0) as SynthPrice, " }) + " StartDate,EndDate,Remark,isnull(ContractPrice,0) as ContractPrice,Safety,Quality,isnull(TaskState,0) as TaskState,Pivotal,WorkDay,isnull(CompleteCount,0) as CompleteCount ";
                object obj3 = str2 + " from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and IsValid = 1 and WbsType=" + wbsType.ToString() + " order by TaskCode asc ";
                sqlString = string.Concat(new object[] { obj3, " select * from EPM_Sch_TempSchedule where ProjectCode='", projectCode, "' and PageCode='", pageCode, "' and UserCode='", userCode, "' order by TaskCode asc " });
            }
            if (type == "S")
            {
                object obj4 = sqlString;
                object obj5 = string.Concat(new object[] { obj4, " if exists(select * from EPM_Task_TaskList where ProjectCode='", projectCode.ToString(), "' and StartDate>='", dtStartDate, "' and StartDate<='", dtEndDate, "' and WorkLayer='3' and IsValid = 1) " }) + " insert into EPM_Sch_TempSchedule(PageCode,UserCode,ProjectCode,TaskCode,ParentTaskCode,TaskName,Quantity,QuantityUnit,WorkLayer,ChildNum,IsValid,Cost,SynthPrice,StartDate,EndDate,Remark,ContractPrice,Safety,Quality,TaskState,Pivotal,WorkDay,CompleteCount) ";
                object obj6 = string.Concat(new object[] { obj5, " select '", pageCode, "','", userCode, "',ProjectCode,TaskCode,ParentTaskCode,TaskName,isnull(Quantity,0) as Quantity,QuantityUnit,isnull(WorkLayer,0) as WorkLayer,isnull(ChildNum,0) as ChildNum,IsValid,isnull(Cost,0) as Cost,isnull(SynthPrice,0) as SynthPrice, " }) + " StartDate,EndDate,Remark,isnull(ContractPrice,0) as ContractPrice,Safety,Quality,isnull(TaskState,0) as TaskState,Pivotal,WorkDay,isnull(CompleteCount,0) as CompleteCount ";
                object obj7 = string.Concat(new object[] { obj6, " from EPM_Task_TaskList where ProjectCode='", projectCode.ToString(), "' and StartDate>='", dtStartDate, "' and StartDate<='", dtEndDate, "' and WorkLayer='3' and IsValid = 1 and WbsType=1 order by TaskCode asc " });
                sqlString = string.Concat(new object[] { obj7, " select PageCode,UserCode,ProjectCode,TaskCode,'' as ParentTaskCode,TaskName,Quantity,QuantityUnit,WorkLayer,ChildNum,IsValid,Cost,SynthPrice,StartDate,EndDate,Remark,ContractPrice,Safety,Quality,TaskState,Pivotal,WorkDay,CompleteCount from EPM_Sch_TempSchedule where ProjectCode='", projectCode, "' and PageCode='", pageCode, "' and UserCode='", userCode, "' order by TaskCode asc " });
            }
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRow(table.Rows[i]));
            }
            return tasks;
        }

        public DataTable getallsubProject(string procode)
        {
            return publicDbOpClass.DataTableQuary("select  distinct ProjectCode, CONVERT(varchar(100), StartDate, 23) as strdata,CONVERT(varchar(100), EndDate, 23) as enddata ,TaskName from EPM_Task_TaskList where ProjectCode='" + procode + "'");
        }

        public WBSBidTaskCollection GetContBillScheduleList(Guid projectCode, int wbsType)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and IsValid = 1 and WbsType=" + wbsType.ToString() + " and IsContractAlter='0' order by TaskCode asc");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRowOfNew(table.Rows[i]));
            }
            return tasks;
        }

        public WBSBidTaskCollection GetContUpdateScheduleList(Guid projectCode, int wbsType, string ContractCode, string ContRecordID)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Con_UpdateTaskList where ProjectCode='" + projectCode.ToString() + "' and ContractCode='" + ContractCode + "' and IsValid = 1 and WbsType=" + wbsType.ToString() + " and ContractUpdateID = '" + ContRecordID + "' order by TaskCode asc");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRowOfNew(table.Rows[i]));
            }
            return tasks;
        }

        private WBSBidTask GetFromDataRow(DataRow dr)
        {
            WBSBidTask task = new WBSBidTask();
            try
            {
                task.NoteID = Convert.ToInt32(dr["NoteId"]);
            }
            catch (Exception)
            {
            }
            task.ParentTaskCode = dr["ParentTaskCode"].ToString();
            task.TaskCode = dr["TaskCode"].ToString();
            task.TaskName = dr["TaskName"].ToString();
            task.Quantity = (dr["Quantity"] == DBNull.Value) ? 0M : ((decimal) dr["Quantity"]);
            task.QuantityUnit = dr["QuantityUnit"].ToString();
            task.ChildNum = (int) dr["ChildNum"];
            task.ProjectCode = (Guid) dr["ProjectCode"];
            task.WorkLayer = (int) dr["WorkLayer"];
            task.IsValid = true;
            task.StartDate = (DateTime) dr["StartDate"];
            task.EndDate = (DateTime) dr["EndDate"];
            task.Cost = (decimal) dr["Cost"];
            task.Safety = dr["Safety"].ToString();
            task.Quality = dr["Quality"].ToString();
            task.TaskState = (dr["TaskState"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TaskState"]);
            task.Pivotal = !(dr["Pivotal"] is DBNull) && ((bool) dr["Pivotal"]);
            task.WorkDay = (dr["WorkDay"] is DBNull) ? 0 : ((int) dr["WorkDay"]);
            task.Remark = dr["Remark"].ToString();
            try
            {
                task.WbsType = (int) dr["WBSType"];
            }
            catch
            {
            }
            task.CompleteCount = (dr["CompleteCount"] == DBNull.Value) ? 0M : ((decimal) dr["CompleteCount"]);
            return task;
        }

        private WBSBidTask GetFromDataRowOfNew(DataRow dr)
        {
            return new WBSBidTask { ParentTaskCode = dr["ParentTaskCode"].ToString(), TaskCode = dr["TaskCode"].ToString(), TaskName = dr["TaskName"].ToString(), ChildNum = (int) dr["ChildNum"] };
        }

        public DataTable GetScheduleCompleteQuantityList(Guid projectCode)
        {
            string str = "select a.NoteID,a.ProjectCode,a.TaskCode,a.TaskName,a.ParentTaskCode,a.ChildNum,";
            return publicDbOpClass.DataTableQuary(((((str + " a.Quantity,a.CompleteCount,isnull(b.SumCompleteQuantity,0)  as SumCompleteQuantity,") + " isnull(a.Quantity-b.SumCompleteQuantity,0) as LeaveQuantity from EPM_Task_TaskList a " + " left join (select TaskCode,sum(TodayComplete) as SumCompleteQuantity from EPM_Book_ConstructTask ") + " where ProjectCode = '" + projectCode.ToString() + "' group by ProjectCode,TaskCode) b ") + " on a.TaskCode = b.TaskCode where a.ProjectCode = '" + projectCode.ToString() + "' and ") + " IsValid = 1 and WbsType='1' order by a.TaskCode asc ");
        }

        public DataTable GetScheduleCompleteQuantityList1(Guid projectCode)
        {
            string str = "select *,convert(decimal(9,2) ,CompleteJeCount1) as CompleteJeCount,convert(decimal(9,2) ,Je1) as Je,convert(decimal(9,2) ,SumCompleteJe1) as SumCompleteJe,convert(decimal(9,2) ,(Je1-SumCompleteJe1)) LeaveJe from ( ";
            return publicDbOpClass.DataTableQuary(((((((str + "select a.NoteID,a.ProjectCode,a.TaskCode,a.TaskName,a.ParentTaskCode,a.ChildNum,") + " a.Quantity,a.CompleteCount,isnull(b.SumCompleteQuantity,0)  as SumCompleteQuantity," + " isnull(a.Quantity-b.SumCompleteQuantity,0) as LeaveQuantity,a.SumPrice AS Je1") + ", (select sum(UnitPrice*quantity)  from EPM_Book_Resource c where ProjectCode = a.ProjectCode and TaskCode=a.TaskCode) as SumCompleteJe1" + ",(case when isnull(a.SumPrice,0)>0 then (isnull((select sum(UnitPrice*quantity)  from EPM_Book_Resource c where ProjectCode = a.ProjectCode and TaskCode=a.TaskCode),0)*100/a.SumPrice) else 0 end) as CompleteJeCount1") + " from EPM_Task_TaskList a " + " left join (select TaskCode,sum(TodayComplete) as SumCompleteQuantity from EPM_Book_ConstructTask ") + " where ProjectCode = '" + projectCode.ToString() + "' group by ProjectCode,TaskCode) b ") + " on a.TaskCode = b.TaskCode where a.ProjectCode = '" + projectCode.ToString() + "' and ") + " IsValid = 1 and WbsType='1'  " + ") A ORDER BY TaskCode asc");
        }

        public WBSBidTask GetSingleTask(Guid projectCode, string taskCode, int wbsType)
        {
            WBSBidTask fromDataRow = new WBSBidTask();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and TaskCode='" + taskCode + "' and  IsValid = 1 and WbsType=" + wbsType.ToString()))
            {
                if (table.Rows.Count == 1)
                {
                    fromDataRow = this.GetFromDataRow(table.Rows[0]);
                }
            }
            return fromDataRow;
        }

        public decimal GetTotalMoney(string prjcode, int type, string taskcode)
        {
            string sqlString = "";
            if (type == 1)
            {
                sqlString = "select cast(sum(isnull(quantity,0)*isnull(UnitPrice,0))AS decimal(18, 2)) from EPM_Task_Resource where Projectcode='" + prjcode + "' and substring(TaskCode,1,3) ='" + taskcode + "'";
            }
            else if (type == 2)
            {
                sqlString = "select cast(sum(isnull(quantity,0)*isnull(UnitPrice,0))AS decimal(18, 2)) from EPM_Task_Resource where Projectcode='" + prjcode + "' and substring(TaskCode,1,6) ='" + taskcode + "'";
            }
            else if (type == 3)
            {
                sqlString = "select cast(sum(isnull(quantity,0)*isnull(UnitPrice,0))AS decimal(18, 2)) from EPM_Task_Resource where Projectcode='" + prjcode + "'  and TaskCode ='" + taskcode + "'";
            }
            try
            {
                return Convert.ToDecimal(publicDbOpClass.ExecuteScalar(sqlString));
            }
            catch
            {
                return 0M;
            }
        }

        public decimal GetTotalMoneya(string prjcode, int type, string taskcode)
        {
            string sqlString = "";
            if (type == 1)
            {
                sqlString = "select cast(isnull(SynthPrice,0))AS decimal(18, 2)) from EPM_Task_TaskList where Projectcode='" + prjcode + "' and substring(TaskCode,1,3) ='" + taskcode + "'";
            }
            else if (type == 2)
            {
                sqlString = "select cast(isnull(SynthPrice,0))AS decimal(18, 2)) from EPM_Task_TaskList where Projectcode='" + prjcode + "' and substring(TaskCode,1,6) ='" + taskcode + "'";
            }
            else if (type == 3)
            {
                sqlString = "select cast(isnull(SynthPrice,0))AS decimal(18, 2)) from EPM_Task_TaskList where Projectcode='" + prjcode + "'  and TaskCode ='" + taskcode + "'";
            }
            try
            {
                return Convert.ToDecimal(publicDbOpClass.ExecuteScalar(sqlString));
            }
            catch
            {
                return 0M;
            }
        }

        public bool IfSaveContUpdateTackCode(string TaskCode, Guid PrjCode, int wbsType, string ContractCode, string ContRecordID)
        {
            bool flag = false;
            string str = "";
            object obj2 = str;
            if (publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "select * from EPM_Con_UpdateTaskList where TaskCode = '", TaskCode, "' and ContractCode='", ContractCode, "' and  ProjectCode = '", PrjCode, "' and IsValid = 1 and WbsType=", wbsType.ToString(), " and ContractUpdateID='", ContRecordID, "'" })).Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        public bool IfSaveTaskCode(string TaskCode, Guid PrjCode, int wbsType)
        {
            bool flag = false;
            string str = "";
            object obj2 = str;
            if (publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "select * from EPM_Task_TaskList where TaskCode = '", TaskCode, "'and  ProjectCode = '", PrjCode, "' and IsValid = 1 and WbsType=", wbsType.ToString() })).Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        public bool IsWBSConfirm(Guid guidProjectCode)
        {
            bool flag = false;
            string sqlString = " select * from PT_PrjInfo where Prjguid='" + guidProjectCode + "' and IsConfirm='1' ";
            try
            {
                if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
                {
                    return true;
                }
                flag = false;
            }
            catch
            {
            }
            return flag;
        }

        public bool IsWBSContUpdateConfirm(string ContractCode, string ContRecordID)
        {
            bool flag = false;
            string sqlString = " select * from EPM_Con_UpdateTaskList  where ContractCode='" + ContractCode + "' and IsAlter='1' and ContractUpdateID = '" + ContRecordID + "' ";
            try
            {
                if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
                {
                    return true;
                }
                flag = false;
            }
            catch
            {
            }
            return flag;
        }

        public DataTable ResSum(Guid projectCode)
        {
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { " SELECT '人工费:' as c1,(SELECT SUM(Fee) AS je    FROM dbo.EPM_Task_Resource a  WHERE (ProjectCode = '", projectCode, "') AND  (ResourceStyle = 1)) c11,      '材料费:' as c2, (SELECT SUM(Fee) AS je    FROM dbo.EPM_Task_Resource a\t  WHERE (ProjectCode = '", projectCode, "') AND  (ResourceStyle = 2)) c21,    '机械费:' as c3, (SELECT SUM(Fee) AS je    FROM dbo.EPM_Task_Resource a\t  WHERE (ProjectCode = '", projectCode, "') AND   (ResourceStyle = 3)) c31 " }));
        }

        public string strTaskName(Guid projectCode, int wbsType, string taskcode)
        {
            return publicDbOpClass.DataTableQuary("select TaskName from EPM_Task_TaskList where ProjectCode='" + projectCode.ToString() + "' and IsValid = 1 and WbsType=" + wbsType.ToString() + " and TaskCode = '" + taskcode + "' order by TaskCode asc").Rows[0]["TaskName"].ToString();
        }

        public bool UpdateTaskContractPrice(DataTable dt)
        {
            string str = "begin";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str2 = str;
                str = str2 + " update EPM_Task_TaskList set ContractPrice = " + dt.Rows[i]["ContractPrice"].ToString() + " where NoteID = " + dt.Rows[i]["ID"].ToString();
            }
            return publicDbOpClass.NonQuerySqlString(str + " end");
        }

        public int UpdateTaskList(WBSBidTask tk)
        {
            object obj2 = ("" + " update EPM_Task_TaskList set ") + " TaskName='" + tk.TaskName + "' ,";
            object obj3 = string.Concat(new object[] { obj2, " Quantity='", tk.Quantity, "' ," }) + " QuantityUnit='" + tk.QuantityUnit + "' ,";
            object obj4 = string.Concat(new object[] { obj3, " StartDate='", tk.StartDate, "' ," });
            object obj5 = ((string.Concat(new object[] { obj4, " EndDate='", tk.EndDate, "' ," }) + " Remark='" + tk.Remark + "' ,") + " Safety='" + tk.Safety + "' ,") + " Quality='" + tk.Quality + "' ,";
            object obj6 = string.Concat(new object[] { obj5, " TaskState='", tk.TaskState, "' ," }) + " Pivotal='" + Convert.ToInt32(tk.Pivotal).ToString() + "' ,";
            object obj7 = string.Concat(new object[] { obj6, " WorkDay='", tk.WorkDay, "' " }) + " where ";
            string str2 = string.Concat(new object[] { obj7, " ProjectCode='", tk.ProjectCode, "' and " });
            object obj8 = (str2 + " TaskCode='" + tk.TaskCode + "' and WbsType=" + tk.WbsType.ToString()) + " declare @StartDate datetime,@EndDate datetime " + " select @StartDate = Min(StartDate),@EndDate = Max(EndDate) from EPM_Task_TaskList where ";
            object obj9 = string.Concat(new object[] { obj8, " ProjectCode='", tk.ProjectCode, "' and ParentTaskCode = (select ParentTaskCode from " });
            object obj10 = string.Concat(new object[] { obj9, " EPM_Task_TaskList where ProjectCode = '", tk.ProjectCode, "' and TaskCode='", tk.TaskCode, "' and WbsType=", tk.WbsType.ToString(), ") and WbsType=", tk.WbsType.ToString() }) + " update EPM_Task_TaskList set StartDate = @StartDate,EndDate = @EndDate where ";
            string sqlString = string.Concat(new object[] { obj10, " ProjectCode='", tk.ProjectCode, "' and TaskCode<>'", tk.TaskCode, "' and charindex(TaskCode,'", tk.TaskCode, "')=1 and WbsType=", tk.WbsType.ToString() });
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int WBSConfirm(Guid guidProjectCode)
        {
            int num = 0;
            string sqlString = " update PT_PrjInfo set IsConfirm='1' where Prjguid='" + guidProjectCode + "' ";
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

        public int WBSContUpdateConfirm(string contcode, string ProjectCode, string ContRecordID)
        {
            int num = Convert.ToInt32(publicDbOpClass.ExecuteScalar("select isnull(max(TaskCode),0)+1 from dbo.EPM_Task_TaskList  where projectCode='" + ProjectCode + "' and len(TaskCode)=3 ").ToString());
            DataTable table = new DataTable {
                TableName = "TaskCode"
            };
            table.Columns.Add("OldTaskCode", typeof(string));
            table.Columns.Add("NewTaskCode", typeof(string));
            DataTable table2 = publicDbOpClass.DataTableQuary("select TaskCode from dbo.EPM_Con_UpdateTaskList  where  contractcode='" + contcode + "' and len(TaskCode)=3 and ContractUpdateID = '" + ContRecordID + "' ");
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                string str2 = table2.Rows[i]["TaskCode"].ToString();
                int num3 = num + i;
                table.Rows.Add(new object[] { str2, num3.ToString().PadLeft(3, '0') });
            }
            int num4 = 0;
            string sqlString = "";
            sqlString = sqlString + " begin ";
            for (int j = 0; j < table.Rows.Count; j++)
            {
                DataRow row = table.Rows[j];
                object obj2 = sqlString;
                object obj3 = string.Concat(new object[] { obj2, " update EPM_Con_UpdateTaskList set TaskCode='", row[1], "'+substring(TaskCode,4,len(TaskCode)-3),IsAlter=1 where contractcode='", contcode, "'and TaskCode like '", row[0], "%'  and ContractUpdateID = '", ContRecordID, "'" });
                sqlString = string.Concat(new object[] { obj3, " update EPM_Con_UpdateTaskList set ParentTaskCode='", row[1], "'+substring(ParentTaskCode,4,len(ParentTaskCode)-3),IsAlter=1 where contractcode='", contcode, "'and ParentTaskCode like '", row[0], "%'  and ContractUpdateID = '", ContRecordID, "'" });
            }
            string str4 = sqlString + "insert into EPM_Task_TaskList(ProjectCode, TaskCode, ParentTaskCode, TaskName, Quantity, QuantityUnit, WorkLayer, ChildNum, IsValid, Cost, SynthPrice, StartDate, EndDate, Remark, ContractPrice, Safety, Quality, TaskState, Pivotal, WorkDay, CompleteCount, ManFee, MaterialFee, MachineFee, JianJFee, GSTotalFee, budgetCost, budgetPrice, budgetQuantity, OrigCode, SumPrice, WbsType, IsAlter, content,IsContractAlter) ";
            sqlString = (str4 + "SELECT ProjectCode, TaskCode, ParentTaskCode, TaskName, Quantity, QuantityUnit, WorkLayer, ChildNum, IsValid, Cost, SynthPrice, StartDate, EndDate, Remark, ContractPrice, Safety, Quality, TaskState, Pivotal, WorkDay, CompleteCount, ManFee, MaterialFee, MachineFee, JianJFee, GSTotalFee, budgetCost, budgetPrice, budgetQuantity, OrigCode, SumPrice, WbsType, IsAlter,0 as content,IsContractAlter FROM dbo.EPM_Con_UpdateTaskList where contractcode='" + contcode + "' and ContractUpdateID = '" + ContRecordID + "'") + " end ";
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                num4 = 1;
            }
            catch
            {
            }
            return num4;
        }

        public bool WBSContUpdateNodeIsCorrect(string ProjectCode, string ContRecordID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Con_UpdateTaskList where ProjectCode = '" + ProjectCode + "' and len(TaskCode) < 9 and ChildNum = 0 and IsAlter = 0");
            bool flag = true;
            if (table.Rows.Count > 0)
            {
                flag = false;
            }
            return flag;
        }

        public bool WBSNodeIsCorrect(Guid ProjectCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Task_TaskList where ProjectCode = '" + ProjectCode + "' and len(TaskCode) < 9 and ChildNum = 0");
            bool flag = true;
            if (table.Rows.Count > 0)
            {
                flag = false;
            }
            return flag;
        }
    }
}

