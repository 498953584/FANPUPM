namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.Web;
    using PmBusinessLogic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class BudgetAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            string str = ConfigHelper.Get("IsWBSRelevance");
            BudTaskChangeService service = new BudTaskChangeService();
            BudTaskService service2 = new BudTaskService();
            BudTaskResourceService service3 = new BudTaskResourceService();
            cn.justwin.Domain.Entities.BudTaskChange byId = service.GetById(key.ToString());
            IList<cn.justwin.Domain.Entities.BudTask> byProject = service2.GetByProject(byId.PrjId, 0x3e7);
            IList<int> years = service2.GetYears(byId.PrjId);
            if (years != null)
            {
                foreach (int num in years)
                {
                    IList<cn.justwin.Domain.Entities.BudTask> yearTask = service2.GetYearTask(byProject, num);
                    foreach (cn.justwin.Domain.Entities.BudTask task in yearTask)
                    {
                        service2.Add(task);
                    }
                    foreach (int num2 in service2.GetMonths(yearTask, num))
                    {
                        foreach (cn.justwin.Domain.Entities.BudTask task2 in service2.GetMonthTasks(yearTask, num, num2))
                        {
                            service2.Add(task2);
                        }
                    }
                }
            }
            if (str == "1")
            {
                service3.AddTaskResource(byId.PrjId);
            }
        }

        public void CommitEvent_old(object key)
        {
            string str = key.ToString();
            string cmdText = "\r\n\t\t\t\t--DECLARE @taskChangeId nvarchar(200);\r\n\t\t\t\tWITH cteTask AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT DISTINCT CT.TaskId AS OldTaskId, T.TaskCode, TC.PrjId, TC.Version\r\n\t\t\t\t\tFROM Bud_ConsTask CT\r\n\t\t\t\t\tJOIN Bud_ConsReport CR ON CR.ConsReportId = CT.ConsReportId\r\n\t\t\t\t\tJOIN Bud_TaskChange TC ON TC.PrjId = CR.PrjId\r\n\t\t\t\t\tJOIN Bud_Task T ON T.TaskId = CT.TaskId\r\n\t\t\t\t\tWHERE TC.TaskChangeId = @taskChangeId\r\n\t\t\t\t), cteTask2 AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT cteTask.*, T.TaskId AS NewTaskId\r\n\t\t\t\t\tFROM cteTask \r\n\t\t\t\t\tJOIN Bud_Task T ON T.TaskCode = cteTask.TaskCode\r\n\t\t\t\t\t\tAND T.PrjId = cteTask.PrjId\r\n\t\t\t\t\tJOIN vGetCurBudVersion C ON C.PrjId = cteTask.PrjId \r\n\t\t\t\t\t\tAND T.Version = C.CurVersion\r\n\t\t\t\t)\r\n\t\t\t\tSELECT * FROM cteTask2\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@taskChangeId", str) };
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
            string str3 = "\r\n                WITH cteTask AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT DISTINCT CT.TaskId AS OldTaskId, T.TaskCode,\r\n                    TC.PrjId, TC.Version\r\n\t\t\t\t\tFROM Bud_ConsTask CT\r\n\t\t\t\t\tJOIN Bud_ConsReport CR ON CR.ConsReportId = CT.ConsReportId\r\n\t\t\t\t\tJOIN Bud_TaskChange TC ON TC.PrjId = CR.PrjId\r\n\t\t\t\t\tJOIN Bud_Task T ON T.TaskId = CT.TaskId\r\n\t\t\t\t\tWHERE TC.TaskChangeId = @taskChangeId\r\n\t\t\t\t)\r\n                , cteTask2 AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT cteTask.*, T.TaskId AS NewTaskId\r\n\t\t\t\t\tFROM vGetCurBudVersion C JOIN cteTask ON C.PrjId = cteTask.PrjId\r\n                    LEFT JOIN Bud_Task T ON T.TaskCode = cteTask.TaskCode\r\n\t\t\t\t\tAND T.Version = C.CurVersion\r\n\t\t\t\t)\r\n\t\t\t\tSELECT * FROM cteTask2\r\n                WHERE NewTaskId IS null\r\n                ";
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@taskChangeId", str) };
            DataTable table2 = SqlHelper.ExecuteQuery(CommandType.Text, str3, parameterArray2);
            string str4 = "\r\n               WITH cteTask AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT DISTINCT CPT.TaskId AS OldTaskId, T.TaskCode,\r\n                    TC.PrjId, TC.Version\r\n\t\t\t\t\tFROM Con_Payout_Target CPT\r\n\t\t\t\t\tJOIN Bud_Task T ON T.TaskId = CPT.TaskId\r\n\t\t\t\t\tJOIN Bud_TaskChange TC ON TC.PrjId = T.PrjId\r\n\t\t\t\t\tWHERE TC.TaskChangeId = @taskChangeId\r\n\t\t\t\t)\r\n                , cteTask2 AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT cteTask.*, T.TaskId AS NewTaskId\r\n\t\t\t\t\tFROM vGetCurBudVersion C JOIN cteTask ON C.PrjId = cteTask.PrjId\r\n                    LEFT JOIN Bud_Task T ON T.TaskCode = cteTask.TaskCode\r\n\t\t\t\t\tAND T.Version = C.CurVersion\r\n\t\t\t\t)\r\n\t\t\t\tSELECT * FROM cteTask2\r\n                WHERE NewTaskId IS null\r\n                ";
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@taskChangeId", str) };
            DataTable table3 = SqlHelper.ExecuteQuery(CommandType.Text, str4, parameterArray3);
            Hashtable hashtable = new Hashtable();
            foreach (DataRow row in table2.Rows)
            {
                string str5 = "SELECT * FROM Bud_ConsTask WHERE TaskId=@TaskId";
                SqlParameter parameter = new SqlParameter("@TaskId", SqlDbType.NVarChar, 500) {
                    Value = row["OldTaskId"].ToString()
                };
                DataTable table4 = SqlHelper.ExecuteQuery(CommandType.Text, str5, new SqlParameter[] { parameter });
                hashtable.Add(row["OldTaskId"].ToString(), table4);
            }
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                foreach (DataRow row2 in table.Rows)
                {
                    string str6 = "UPDATE Bud_ConsTask SET TaskId =@newTaskId WHERE TaskId = @oldTaskId";
                    string str7 = "UPDATE Con_Payout_Target SET TaskId =@newTaskId WHERE TaskId = @oldTaskId";
                    SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@newTaskId", row2["NewTaskId"].ToString()), new SqlParameter("@oldTaskId", row2["OldTaskId"].ToString()) };
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str6, parameterArray4);
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str7, parameterArray4);
                }
                foreach (DataRow row3 in table3.Rows)
                {
                    this.DelConPayoutTarget(trans, row3["OldTaskId"].ToString());
                }
                foreach (DataRow row4 in table2.Rows)
                {
                    this.DelBudConsTask(trans, row4["OldTaskId"].ToString());
                    DataTable dtConsTask = (DataTable) hashtable[row4["OldTaskId"].ToString()];
                    this.DelBudConsReport(trans, dtConsTask);
                }
                trans.Commit();
            }
        }

        private void DelBudConsReport(SqlTransaction trans, DataTable dtConsTask)
        {
            foreach (DataRow row in dtConsTask.Rows)
            {
                string str = row["ConsReportId"].ToString();
                string str2 = "With  cteConsTaskCount AS\r\n                                (\r\n                                SELECT ConsReportId,COUNT(*) ConsTaskCount  FROM Bud_ConsTask\r\n                                WHERE ConsReportId=@consReportId group by ConsReportId\r\n                                ),cteConsTaskCount2 AS\r\n                                (\r\n                                SELECT ConsPeport.ConsReportId,ISNULL(ConsTaskCount,0) ConsTaskCount \r\n                                FROM Bud_ConsReport ConsPeport LEFT JOIN cteConsTaskCount\r\n                                ON ConsPeport.ConsReportId=cteConsTaskCount.ConsReportId\r\n                                WHERE ConsPeport.ConsReportId=@consReportId\r\n                                )\r\n                                DELETE Bud_ConsReport FROM Bud_ConsReport ConsPeport INNER JOIN cteConsTaskCount2\r\n                                ON  ConsPeport.ConsReportId=cteConsTaskCount2.ConsReportId\r\n                                WHERE ConsTaskCount=0";
                SqlParameter parameter = new SqlParameter("@consReportId", SqlDbType.NVarChar, 500) {
                    Value = str
                };
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str2.ToString(), new SqlParameter[] { parameter });
            }
        }

        private void DelBudConsTask(SqlTransaction trans, string taskId)
        {
            string str = "Delete Bud_ConsTask Where TaskId=@TaskId";
            SqlParameter parameter = new SqlParameter("@TaskId", SqlDbType.NVarChar, 500) {
                Value = taskId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
        }

        private void DelBudTask(SqlTransaction trans, string taskId)
        {
            string str = "Delete Bud_Task Where TaskId=@TaskId";
            SqlParameter parameter = new SqlParameter("@TaskId", SqlDbType.NVarChar, 500) {
                Value = taskId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
        }

        private void DelConPayoutTarget(SqlTransaction trans, string taskId)
        {
            string str = "Delete Con_Payout_Target Where TaskId=@TaskId";
            SqlParameter parameter = new SqlParameter("@TaskId", SqlDbType.NVarChar, 500) {
                Value = taskId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
        }

        private void DelTaskChange(SqlTransaction trans, string taskChangeId)
        {
            string str = "Delete Bud_TaskChange Where TaskChangeId=@taskChangeId";
            SqlParameter parameter = new SqlParameter("@taskChangeId", SqlDbType.NVarChar, 500) {
                Value = taskChangeId
            };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
        }

        private void DelTaskRes(SqlTransaction trans, string prjId, int version)
        {
            string str = "Delete Bud_TaskResource WHERE PrjGuid=@PrjId and Versions=@Version ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@Version", version) };
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), commandParameters);
        }

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            cn.justwin.Domain.BudTaskChange model = cn.justwin.Domain.BudTaskChange.GetModel(key.ToString());
            if (model != null)
            {
                Hashtable hashtable = new Hashtable();
                IList<cn.justwin.Domain.BudTask> allByPrjId = cn.justwin.Domain.BudTask.GetAllByPrjId(model.PrjId);
                foreach (cn.justwin.Domain.BudTask task in allByPrjId)
                {
                    string cmdText = "SELECT * FROM Bud_ConsTask WHERE TaskId=@TaskId";
                    SqlParameter parameter = new SqlParameter("@TaskId", SqlDbType.NVarChar, 500) {
                        Value = task.Id
                    };
                    DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
                    hashtable.Add(task.Id, table);
                }
                using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    foreach (cn.justwin.Domain.BudTask task2 in allByPrjId)
                    {
                        this.DelConPayoutTarget(trans, task2.Id);
                        this.DelBudConsTask(trans, task2.Id);
                        DataTable dtConsTask = (DataTable) hashtable[task2.Id];
                        this.DelBudConsReport(trans, dtConsTask);
                        this.DelBudTask(trans, task2.Id);
                    }
                    this.DelTaskRes(trans, model.PrjId, !model.Version.HasValue ? 1 : Convert.ToInt32(model.Version));
                    this.DelTaskChange(trans, key.ToString());
                    trans.Commit();
                }
            }
        }
    }
}

