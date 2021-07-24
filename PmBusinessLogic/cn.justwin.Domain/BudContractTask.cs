namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.stockBLL.Domain;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class BudContractTask
    {
        private string typeName;

        private BudContractTask()
        {
        }

        public static void Add(cn.justwin.Domain.BudContractTask conTask)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ContractTask task = new Bud_ContractTask {
                    TaskId = conTask.Id,
                    TaskCode = conTask.Code,
                    TaskName = conTask.Name,
                    Unit = conTask.Unit,
                    UnitPrice = conTask.UnitPrice,
                    Total = conTask.Total,
                    Quantity = conTask.Quantity,
                    OrderNumber = conTask.OrderNumber,
                    PrjId = conTask.PrjId,
                    Note = conTask.Note,
                    StartDate = conTask.StartDate,
                    EndDate = conTask.EndDate,
                    InputDate = conTask.InputDate,
                    InputUser = conTask.InputUser,
                    ParentId = conTask.ParentId
                };
                entities.AddToBud_ContractTask(task);
                entities.SaveChanges();
            }
        }

        public static bool CheckChilds(string id)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_ContractTask
                    where m.ParentId == id
                    select m).FirstOrDefault<Bud_ContractTask>() != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool CheckCode(string code, string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_ContractTask
                    where (m.TaskCode == code) && (m.PrjId == prjId)
                    select m).FirstOrDefault<Bud_ContractTask>() != null)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static void ClearByPrjId(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<Bud_ContractTask> list = (from m in entities.Bud_ContractTask
                    where m.PrjId == prjId
                    select m).ToList<Bud_ContractTask>();
                if ((list != null) && (list.Count > 0))
                {
                    foreach (Bud_ContractTask task in list)
                    {
                        entities.DeleteObject(task);
                    }
                    entities.SaveChanges();
                }
            }
        }

        public static cn.justwin.Domain.BudContractTask Create(string id, string code, string name, string unit, decimal? unitPrice, decimal quantity, decimal? total, string note, string parentId, string prjId, string orderNumber, DateTime? start, DateTime? end, DateTime inputDate, string inputUser)
        {
            return new cn.justwin.Domain.BudContractTask { Id = id, Name = name, Code = code, Unit = unit, UnitPrice = unitPrice, Quantity = quantity, Note = note, ParentId = parentId, PrjId = prjId, OrderNumber = orderNumber, StartDate = start, EndDate = end, InputDate = inputDate, InputUser = inputUser, Total = total };
        }

        public static void Del(List<string> ids)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<string>.Enumerator enumerator = ids.GetEnumerator())
                {
                    string id;
                    while (enumerator.MoveNext())
                    {
                        id = enumerator.Current;
                        Bud_ContractTask entity = (from m in entities.Bud_ContractTask
                            where m.TaskId == id
                            select m).FirstOrDefault<Bud_ContractTask>();
                        if (entity != null)
                        {
                            entities.DeleteObject(entity);
                        }
                        else
                        {
                            new BudConModifyTaskService().DelByModifyTaskId(id);
                        }
                    }
                }
                entities.SaveChanges();
            }
        }

        public static List<cn.justwin.Domain.BudContractTask> GetAllByPrjId(string prjId)
        {
            List<cn.justwin.Domain.BudContractTask> list = new List<cn.justwin.Domain.BudContractTask>();
            using (pm2Entities entities = new pm2Entities())
            {
                var list2 = (from m in entities.Bud_ContractTask
                    where m.PrjId == prjId
                    orderby m.OrderNumber
                    select new { TaskId = m.TaskId, TaskCode = m.TaskCode, TaskName = m.TaskName, Unit = m.Unit, UnitPrice = m.UnitPrice, Quantity = m.Quantity, Total = m.Total, Note = m.Note, ParentId = m.ParentId, OrderNumber = m.OrderNumber, StartDate = m.StartDate, EndDate = m.EndDate }).ToList();
                if ((list2 == null) || (list2.Count <= 0))
                {
                    return list;
                }
                foreach (var type in list2)
                {
                    cn.justwin.Domain.BudContractTask item = new cn.justwin.Domain.BudContractTask {
                        Id = type.TaskId,
                        Code = type.TaskCode,
                        Name = type.TaskName,
                        Unit = type.Unit,
                        UnitPrice = type.UnitPrice,
                        Quantity = type.Quantity,
                        StartDate = type.StartDate,
                        EndDate = type.EndDate,
                        Note = type.Note,
                        Total = type.Total,
                        OrderNumber = type.OrderNumber
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static Hashtable GetAllSum(string prjId, List<CostAccounting> costAccountingList)
        {
            cn.justwin.Domain.BudContractTask task = new cn.justwin.Domain.BudContractTask();
            List<string> taskId = task.GetTaskId(prjId);
            return task.GetSum(taskId, costAccountingList);
        }

        public static DataTable GetAllTask(string prjId, string taskType, string year, string month)
        {
            string cmdText = "WITH cteType AS\r\n                        (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                                            FROM XPM_Basic_CodeList AS cl\r\n                                            INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                                            WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                        )\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber)AS No,dbo.fn_GetContractTaskCount(TaskId) AS SubCount,cteType.CodeName AS TypeName,\r\n                        ISNULL(Total/NULLIF(Quantity,0.0),0.0) AS UnitPrice ,\r\n                        TaskId,ParentId,OrderNumber,PrjId,TaskCode,TaskName,Unit,Quantity,StartDate,EndDate,Note,IsValid,\r\n                        ISNULL(MainMaterial,0)AS MainMaterial,ISNULL(SubMaterial,0) AS SubMaterial,ISNULL(Labor,0)AS Labor,\r\n                        ContractId,ModifyType,ModifyId,ConstructionPeriod,FeatureDescription,ISNULL(Total,0.0) AS Total\r\n                        FROM Bud_ContractTask Task\r\n                        LEFT JOIN cteType \r\n                        ON LEN(OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                        WHERE PrjId=@PrjId AND TaskType = @taskType\r\n\t\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month AND Len(OrderNumber)<=6";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@taskType", taskType), new SqlParameter("@year", year), new SqlParameter("@month", month) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static cn.justwin.Domain.BudContractTask GetById(string id)
        {
            cn.justwin.Domain.BudContractTask task = new cn.justwin.Domain.BudContractTask();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_ContractTask
                    where m.TaskId == id
                    select new cn.justwin.Domain.BudContractTask { Id = m.TaskId, Code = m.TaskCode, Name = m.TaskName, StartDate = m.StartDate, EndDate = m.EndDate, Unit = m.Unit, UnitPrice = m.UnitPrice, Quantity = m.Quantity, Note = m.Note, OrderNumber = m.OrderNumber, ParentId = m.ParentId, PrjId = m.PrjId, Total = m.Total, InputUser = m.InputUser, InputDate = m.InputDate }).FirstOrDefault<cn.justwin.Domain.BudContractTask>();
            }
        }

        private List<string> GetChildrenTaskId(string taskId)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> list2 = (from m in entities.Bud_ContractTask
                    where m.ParentId == taskId
                    select m.TaskId).ToList<string>();
                if ((list2 != null) && (list2.Count > 0))
                {
                    foreach (string str in list2)
                    {
                        this.GetChildrenTaskId(str);
                    }
                    return list;
                }
                list.Add(taskId);
            }
            return list;
        }

        public static DataTable GetChildrenTaskInfo(string taskId)
        {
            string cmdText = "\r\n\t\t\t\tDECLARE @PrjId nvarchar(200)--, @ParentId nvarchar(200)\r\n\t\t\t\t--SET @ParentId = '44485d09-dc4b-4e3c-8820-0e2b659991fd';\r\n\t\t\t\tSELECT @PrjId = PrjId FROM Bud_ContractTask WHERE TaskId = @ParentId;\r\n                WITH cteContactTask AS\r\n                (\r\n\t                SELECT ct.TaskId,ct.ParentId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n\t\t                ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice,  ct.ConstructionPeriod,\r\n                        ct.FeatureDescription,ct.MainMaterial,ct.SubMaterial,ct.Labor,\r\n\t\t                dbo.fn_GetContractTaskCount(TaskId) AS SubCount\r\n\t                FROM Bud_ContractTask ct\r\n\t                WHERE PrjId = @PrjId\r\n                ),budConModifyInfo AS\r\n                (\r\n                    --针对原预算中的变更任务节点\r\n                    SELECT TaskId,ParentId,OrderNumber,ModifyTaskCode TaskCode,\r\n                    ModifyTaskContent TaskName,Unit,Quantity,StartDate,EndDate,conModifyTask.Note,\r\n                    Total,ISNULL(UnitPrice,0) UnitPrice,ConstructionPeriod,\r\n                    FeatureDescription,MainMaterial,SubMaterial,Labor,dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount\r\n                    FROM Bud_ConModifyTask conModifyTask\r\n                    JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                    where budConModify.PrjId=@PrjId \r\n                    and ModifyType=0 AND FlowState=1\r\n                ),budConModifyInfo2 AS\r\n                (\r\n                    --针对变更中的变更任务节点\r\n                    SELECT  TaskId,TaskId ParentId,OrderNumber,ModifyTaskCode TaskCode,\r\n                    ModifyTaskContent TaskName,Unit,Quantity,StartDate,EndDate,conModifyTask.Note,                  Total,ISNULL(UnitPrice,0) UnitPrice,ConstructionPeriod,FeatureDescription,MainMaterial,SubMaterial,Labor,dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount\r\n                    FROM Bud_ConModifyTask conModifyTask\r\n                    JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                    JOIN\r\n                    ( \r\n                        SELECT prjId from Bud_ConModifyTask \r\n                        JOIN Bud_ConModify ON Bud_ConModifyTask.ModifyId=Bud_ConModify.ModifyId\r\n                        WHERE ModifyTaskId=@ParentId \r\n                    ) prj\r\n                    on budConModify.PrjId=prj.prjId \r\n                    where ModifyType=0 AND FlowState=1\r\n                ),cteTask AS\r\n                (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,newTask.TaskId,ParentId,OrderNumber,TaskCode,\r\n                    TaskName,Unit,(newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,StartDate,EndDate,\r\n                    ConstructionPeriod,Note,UnitPrice,FeatureDescription,(newTask.MainMaterial+ISNULL(inBudConModifyTask.MainMaterial,0)) MainMaterial,\r\n\t\t(newTask.SubMaterial+ISNULL(inBudConModifyTask.SubMaterial,0)) SubMaterial,(newTask.Labor+ISNULL(inBudConModifyTask.Labor,0)) Labor,(newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount FROM\r\n                    (\r\n\t\t\t\t\t\tSELECT * FROM cteContactTask WHERE ParentId = @ParentId\r\n\t\t\t\t\t\tUNION\r\n\t\t\t\t\t\tSELECT * FROM budConModifyInfo WHERE ParentId = @ParentId\r\n\t\t\t\t\t\tUNION\r\n\t\t\t\t\t\tSELECT * FROM budConModifyInfo2 WHERE ParentId = @ParentId\r\n                    )newTask LEFT JOIN \r\n                    (\r\n                        --清单内的变更金额\r\n                        SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total,\r\nSUM(conModifyTask.MainMaterial) MainMaterial,SUM(conModifyTask.SubMaterial) SubMaterial,SUM(conModifyTask.Labor) Labor\r\nfrom Bud_ConModifyTask conModifyTask\r\n                        JOIN \r\n                        (\r\n                            SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId=@PrjId\r\n                        ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                        where budConModify.FlowState=1 AND modifyType=1 group by conModifyTask.TaskId\r\n                    ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                ), cteType AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                FROM XPM_Basic_CodeList AS cl\r\n\t                INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                )\r\n                SELECT * FROM\r\n                (\r\n\t                SELECT cteTask.*,\r\n\t\t                cteType.CodeNo, ISNULL(cteType.CodeName,'') AS TypeName\r\n\t                FROM cteTask\r\n\t                LEFT JOIN cteType ON LEN(cteTask.OrderNumber) / 3 = cteType.CodeNo\r\n                ) AS t\r\n                WHERE ParentId = @ParentId\r\n                ORDER BY No\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ParentId", taskId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetConByParent(string prjId, string contractID, string orderNumber, string taskType, string year, string month, int isValid)
        {
            string str = string.Empty;
            if (isValid == 0)
            {
                str = string.Empty;
            }
            else
            {
                str = str + " AND ContractId='" + contractID + "'";
            }
            if (!string.IsNullOrEmpty(orderNumber))
            {
                str = str + " AND OrderNumber like '" + orderNumber + "%' ";
            }
            string cmdText = "WITH cteType AS\r\n                        (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                                            FROM XPM_Basic_CodeList AS cl\r\n                                            INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                                            WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                        )\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber)AS No,dbo.fn_GetContractTaskCount(TaskId) AS SubCount,cteType.CodeName AS TypeName,\r\n                        ISNULL(Total/NULLIF(Quantity,0.0),0.0) AS UnitPrice ,\r\n                        TaskId,ParentId,OrderNumber,PrjId,TaskCode,TaskName,Unit,Quantity,StartDate,EndDate,Note,IsValid,\r\n                        ISNULL(MainMaterial,0)AS MainMaterial,ISNULL(SubMaterial,0) AS SubMaterial,ISNULL(Labor,0)AS Labor,\r\n                        ContractId,ModifyType,ModifyId,ConstructionPeriod,FeatureDescription,ISNULL(Total,0.0) AS Total\r\n                        FROM Bud_ContractTask Task\r\n                        LEFT JOIN cteType \r\n                        ON LEN(OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                        WHERE PrjId=@PrjId AND TaskType = @taskType\r\n\t\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month  " + str;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@taskType", taskType), new SqlParameter("@year", year), new SqlParameter("@month", month) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static Hashtable GetCurrentSum(string taskId, List<CostAccounting> costAccountingList)
        {
            cn.justwin.Domain.BudContractTask task = new cn.justwin.Domain.BudContractTask();
            List<string> childrenTaskId = task.GetChildrenTaskId(taskId);
            return task.GetSum(childrenTaskId, costAccountingList);
        }

        public static DataTable GetDtblByPrjidAndContractIDAndRptIds(string prjId, string contractId, string RptIDs)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("    WITH cteContactTask AS\r\n                            (\r\n                                SELECT ct.TaskId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n                                    ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice, ct.ConstructionPeriod,\r\n                                    dbo.fn_GetContractTaskCount(TaskId) AS SubCount,ContractID\r\n                                FROM Bud_ContractTask ct\r\n                                JOIN Bud_ContractTaskAudit ON ct.PrjId=Bud_ContractTaskAudit.prjId\r\n                                WHERE ct.PrjId = '{0}' AND TaskType='' AND FlowState=1\r\n                            ),outBudConModifyInfo AS\r\n                            (\r\n                                SELECT TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit,\r\n                                Quantity,StartDate,EndDate,conModifyTask.Note,Total,ISNULL(UnitPrice,0) UnitPrice,ConstructionPeriod,\r\n                                dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount,ContractId\r\n                                FROM Bud_ConModifyTask conModifyTask\r\n                                JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                                WHERE PrjId='{0}' AND ModifyType=0\r\n                                AND FlowState=1 \r\n                            ),cteTask AS\r\n                            (\r\n                                SELECT * FROM \r\n                                (\r\n                                    SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,\r\n                                    Unit,(newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note,UnitPrice,\r\n                                    (newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount,ContractId  FROM \r\n                                    (\r\n                                        SELECT * FROM cteContactTask\r\n                                        UNION\r\n                                        SELECT * FROM outBudConModifyInfo\r\n                                    )newTask LEFT JOIN \r\n                                    (\r\n                                        --清单内的变更金额\r\n                                        SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total from Bud_ConModifyTask conModifyTask\r\n                                        JOIN \r\n                                        (\r\n                                            SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId='{0}'\r\n                                        ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                                        WHERE budConModify.FlowState=1 AND modifyType=1\r\n                                        GROUP BY conModifyTask.TaskId\r\n                                    ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                                ) tab WHERE subcount='0' and contractId='{1}'\r\n                            ),ContractTask AS\r\n                        (\r\n                           SELECT TaskId,ISNULL(SUM(Amount),0) Amount,ISNULL(SUM(ApproveAmount),0)  ApproveAmount from Bud_ContractConsTask conTask\r\n                           JOIN Bud_ContractConsReport conRpt ON conTask.RptId=conRpt.RptId\r\n                           WHERE prjId='{0}'\r\n                           AND flowState=1  --流程状态\r\n                           AND conRpt.RptId in({2})\r\n                           GROUP BY TaskId\r\n                        )\r\n                        select ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,Amount,ApproveAmount,cteTask.* from ContractTask\r\n                        join  cteTask on ContractTask.taskId=cteTask.taskId\r\n                        ", prjId, contractId, RptIDs);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        private string GetListItemString(List<string> taskIds)
        {
            string str = string.Empty;
            foreach (string str2 in taskIds)
            {
                str = str + "'" + str2 + "',";
            }
            if (str.Length > 0)
            {
                return str.Substring(0, str.Length - 1);
            }
            return "''";
        }

        public static string GetOrderNumber(string prId, string parentId)
        {
            string str = "001";
            using (pm2Entities entities = new pm2Entities())
            {
                if (string.IsNullOrEmpty(parentId))
                {
                    int num;
                    int.TryParse((from m in entities.Bud_ContractTask
                        where (m.PrjId == prId) && (m.ParentId == null)
                        select m.OrderNumber).Max<string>(), out num);
                    string str2 = (num + 1).ToString();
                    if (str2.Length == 1)
                    {
                        str2 = "00" + str2;
                    }
                    if (str2.Length == 2)
                    {
                        str2 = "0" + str2;
                    }
                    return str2;
                }
                IQueryable<string> source = from m in entities.Bud_ContractTask
                    where m.ParentId == parentId
                    select m.OrderNumber;
                BudConModifyTaskService service = new BudConModifyTaskService();
                IQueryable<string> queryable3 = from m in service
                    where m.ParentId == parentId
                    select m.OrderNumber;
                string str3 = string.Empty;
                if (Convert.ToDecimal(source.Max<string>()) > Convert.ToDecimal(queryable3.Max<string>()))
                {
                    str3 = source.Max<string>();
                }
                else
                {
                    str3 = queryable3.Max<string>();
                }
                if ((source.Count<string>() > 0) || (queryable3.Count<string>() > 0))
                {
                    string str4 = str3;
                    string str5 = str4.Substring(0, str4.Length - 3);
                    string str7 = (Convert.ToInt32(str4.Substring(str4.Length - 3, 3)) + 1).ToString();
                    if (str7.Length == 1)
                    {
                        str7 = "00" + str7;
                    }
                    if (str7.Length == 2)
                    {
                        str7 = "0" + str7;
                    }
                    return (str5 + str7);
                }
                IQueryable<string> queryable4 = from m in entities.Bud_ContractTask
                    where m.TaskId == parentId
                    select m.OrderNumber;
                if (queryable4.Count<string>() > 0)
                {
                    str = queryable4.First<string>() + "001";
                }
            }
            return str;
        }

        public static List<string> GetOrderNumberById(List<string> lst)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<string>.Enumerator enumerator = lst.GetEnumerator())
                {
                    string id;
                    while (enumerator.MoveNext())
                    {
                        id = enumerator.Current;
                        string str = (from m in entities.Bud_ContractTask
                            where m.TaskId == id
                            select m.OrderNumber).FirstOrDefault<string>();
                        if (string.IsNullOrEmpty(str))
                        {
                            throw new Exception("要保存为模板的任务节点已不存在，请重新选择!!!");
                        }
                        list.Add(str);
                    }
                }
            }
            return list;
        }

        public static DataTable GetSingleBudContractTask(string prjID, string TaskID)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n                                 WITH cteContactTask AS\r\n                                    (\r\n                                        SELECT ct.TaskId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n                                            ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice, ct.ConstructionPeriod,\r\n                                            dbo.fn_GetContractTaskCount(TaskId) AS SubCount,ContractID\r\n                                        FROM Bud_ContractTask ct\r\n                                        JOIN Bud_ContractTaskAudit ON ct.PrjId=Bud_ContractTaskAudit.prjId\r\n                                        WHERE ct.PrjId = '{0}' AND TaskType='' AND FlowState=1\r\n                                            and taskid='{1}'\r\n                                    ),outBudConModifyInfo AS\r\n                                    (\r\n                                        SELECT TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit,\r\n                                        Quantity,StartDate,EndDate,conModifyTask.Note,Total,ISNULL(UnitPrice,0) UnitPrice,ConstructionPeriod,\r\n                                        dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount,ContractId\r\n                                        FROM Bud_ConModifyTask conModifyTask\r\n                                        JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                                        WHERE PrjId='{0}' AND ModifyType=0\r\n                                        AND FlowState=1 \r\n                                        and ModifyTaskId='{1}'\r\n                                    ),cteTask AS\r\n                                    (\r\n                                        SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n                                        (\r\n                                            SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,\r\n                                            Unit,(newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note,UnitPrice,\r\n                                            (newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount,ContractId  FROM \r\n                                            (\r\n                                                SELECT * FROM cteContactTask\r\n                                                UNION\r\n                                                SELECT * FROM outBudConModifyInfo\r\n                                            )newTask LEFT JOIN \r\n                                            (\r\n                                                --清单内的变更金额\r\n                                                SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total from Bud_ConModifyTask conModifyTask\r\n                                                JOIN \r\n                                                (\r\n                                                    SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId='{0}'\r\n                                                ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                                                where budConModify.FlowState=1 AND modifyType=1\r\n                                                group by conModifyTask.TaskId\r\n                                            ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                                        ) tab \r\n                                    )\r\n                                select * from cteTask", prjID, TaskID);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public decimal GetSingleSumTotal(string taskId)
        {
            using (new pm2Entities())
            {
            }
            return 0M;
        }

        public Hashtable GetSum(List<string> taskIds, List<CostAccounting> costAccountingList)
        {
            decimal sumTotal = this.GetSumTotal(taskIds);
            int num2 = costAccountingList.Count + 1;
            Hashtable hashtable = new Hashtable();
            hashtable.Add("Total", sumTotal);
            foreach (CostAccounting accounting in costAccountingList)
            {
                hashtable.Add(accounting.Code, 0M);
            }
            if (sumTotal != 0M)
            {
                foreach (DataRow row in this.GetSumTypeTotal(taskIds).Rows)
                {
                    ResType byId = ResType.GetById(Resource.GetFirstResourceTypeId(row["ResourceType"].ToString()));
                    if (!string.IsNullOrEmpty(byId.CBSCode) && (hashtable[byId.CBSCode] != null))
                    {
                        decimal num3 = Convert.ToDecimal(hashtable[byId.CBSCode]) + Convert.ToDecimal(row["Total"]);
                        hashtable.Remove(byId.CBSCode);
                        hashtable.Add(byId.CBSCode, num3);
                    }
                }
            }
            return hashtable;
        }

        private decimal GetSumTotal(List<string> taskIds)
        {
            string listItemString = this.GetListItemString(taskIds);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT ISNULL(SUM( ResourceQuantity*ResourcePrice),0) AS Total FROM Bud_ContractResource WHERE TaskId IN({0})", listItemString);
            return decimal.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()).ToString());
        }

        public DataTable GetSumTypeTotal(List<string> taskIds)
        {
            StringBuilder builder = new StringBuilder();
            string listItemString = this.GetListItemString(taskIds);
            builder.Append("SELECT res.ResourceType,Sum(ISNULL(taskRes.Total,0)) Total");
            builder.AppendLine();
            builder.Append("FROM Res_Resource AS res right join (SELECT SUM(ResourceQuantity*ResourcePrice) Total,ResourceId");
            builder.AppendLine();
            builder.AppendFormat("FROM Bud_ContractResource WHERE TaskId IN ({0})", listItemString);
            builder.AppendLine();
            builder.Append(" GROUP BY ResourceId) AS taskRes ON res.ResourceId=taskRes.ResourceId  GROUP BY res.ResourceType");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable GetTaskChild(string taskId)
        {
            string cmdText = "WITH cteType AS\r\n                        (\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n                                            FROM XPM_Basic_CodeList AS cl\r\n                                            INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n                                            WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                        )\r\n                        SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber)AS No,dbo.fn_GetContractTaskCount(TaskId) AS SubCount,cteType.CodeName AS TypeName,\r\n                        ISNULL(Total/NULLIF(Quantity,0.0),0.0) AS UnitPrice ,\r\n                        TaskId,ParentId,OrderNumber,PrjId,TaskCode,TaskName,Unit,Quantity,StartDate,EndDate,Note,IsValid,\r\n                        ISNULL(MainMaterial,0)AS MainMaterial,ISNULL(SubMaterial,0) AS SubMaterial,ISNULL(Labor,0)AS Labor,\r\n                        ContractId,ModifyType,ModifyId,ConstructionPeriod,FeatureDescription,ISNULL(Total,0.0) AS Total\r\n                        FROM Bud_ContractTask Task\r\n                        LEFT JOIN cteType \r\n                        ON LEN(OrderNumber) / 3 = CASE (SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                        WHERE ParentId=@taskId ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@taskId", taskId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static int GetTaskCount(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from bt in entities.Bud_ContractTask
                    orderby bt.OrderNumber
                    where bt.PrjId == prjId
                    select bt).Count<Bud_ContractTask>();
            }
        }

        private List<string> GetTaskId(string prjId)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> list2 = (from m in entities.Bud_ContractTask
                    where m.PrjId == prjId
                    select m.TaskId).ToList<string>();
                if ((list2 == null) || (list2.Count <= 0))
                {
                    return list;
                }
                foreach (string str in list2)
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public static DataTable GetTaskInfo(string prjId, string taskType, string year, string month)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(taskType))
            {
                str = " and 1<>1 ";
            }
            string cmdText = "\r\n\t\t\t\t--DECLARE @PrjId nvarchar(200)\r\n\t\t\t\t--DECLARE @taskType char(1)\r\n\t\t\t\t--DECLARE @year char(4)\r\n\t\t\t\t--DECLARE @month char(2)\r\n\t\t\t\t--SET @PrjId = '9359ab03-a70f-4067-9ab5-b4f378ee02a0';\r\n\t\t\t\t--SET @taskType = 'Y';\r\n\t\t\t\t--SET @year = '2011';\r\n\t\t\t\t--SET @month = '';\r\n                WITH cteContactTask AS\r\n                (\r\n\t                SELECT ct.TaskId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n\t\t                ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice, ct.ConstructionPeriod,\r\n                        ct.FeatureDescription,ISNULL(ct.MainMaterial,0) MainMaterial,ISNULL(ct.SubMaterial,0) AS   SubMaterial,ISNULL(ct.Labor,0) Labor,\r\n\t\t                dbo.fn_GetContractTaskCount(TaskId) AS SubCount\r\n\t                FROM Bud_ContractTask ct\r\n\t                WHERE PrjId = @PrjId AND TaskType = @taskType\r\n\t\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month\r\n                ),outBudConModifyInfo AS\r\n                (\r\n                    SELECT TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit,\r\n                    Quantity,StartDate,EndDate,conModifyTask.Note,Total,ISNULL(UnitPrice,0)      UnitPrice,ConstructionPeriod,FeatureDescription,ISNULL(MainMaterial,0) MainMaterial,ISNULL(SubMaterial,0)   SubMaterial,ISNULL(Labor,0) Labor,\r\n                    dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount\r\n                    FROM Bud_ConModifyTask conModifyTask\r\n                    JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                    WHERE PrjId=@PrjId AND ModifyType=0\r\n                    AND FlowState=1 ";
            cmdText = (cmdText + str + " ),cteTask AS\r\n                (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n                    (\r\n                        SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,\r\n                        Unit,(newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note,UnitPrice,ISNULL(FeatureDescription,'') FeatureDescription,(newTask.MainMaterial+ISNULL(inBudConModifyTask.MainMaterial,0)) MainMaterial,\r\n\t\t(newTask.SubMaterial+ISNULL(inBudConModifyTask.SubMaterial,0)) SubMaterial,(newTask.Labor+ISNULL(inBudConModifyTask.Labor,0)) Labor,\r\n                        (newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount  FROM \r\n                        (\r\n                            SELECT * FROM cteContactTask\r\n                            UNION\r\n                            SELECT * FROM outBudConModifyInfo\r\n                        )newTask LEFT JOIN \r\n                        (\r\n                            --清单内的变更金额\r\n                            SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total,\r\n                            SUM(conModifyTask.MainMaterial) MainMaterial,SUM(conModifyTask.SubMaterial) SubMaterial,\r\n\t\t\t                SUM(conModifyTask.Labor) Labor from Bud_ConModifyTask conModifyTask\r\n                            JOIN \r\n                            (\r\n                                SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId=@PrjId\r\n                            ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                            where budConModify.FlowState=1 AND modifyType=1 ") + str + "\r\n\t\t\t                group by conModifyTask.TaskId\r\n                        ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                    ) tab WHERE Len(OrderNumber)<=6\r\n                ),cteType AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                FROM XPM_Basic_CodeList AS cl\r\n\t                INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                )\r\n                SELECT * FROM\r\n                (\r\n\t                SELECT cteTask.*,\r\n\t\t                cteType.CodeNo, cteType.CodeName AS TypeName\r\n\t                FROM cteTask\r\n\t                LEFT JOIN cteType ON LEN(cteTask.OrderNumber) / 3 = \r\n\t\t                CASE(SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                ) AS t\r\n                WHERE LEN(OrderNumber)/3 <=2 --CodeNo <= 2\r\n                ORDER BY No ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@taskType", taskType), new SqlParameter("@year", year), new SqlParameter("@month", month) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetTaskInfoByPrjIdAndConId(string prjId, string contractID, string taskType, string year, string month)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(contractID))
            {
                str2 = str2 + " AND  ContractId='" + contractID + "' ";
            }
            if (!string.IsNullOrEmpty(taskType))
            {
                str = " and 1<>1 ";
            }
            string cmdText = "\r\n\t\t\t\t--DECLARE @PrjId nvarchar(200)\r\n\t\t\t\t--DECLARE @taskType char(1)\r\n\t\t\t\t--DECLARE @year char(4)\r\n\t\t\t\t--DECLARE @month char(2)\r\n\t\t\t\t--SET @PrjId = '9359ab03-a70f-4067-9ab5-b4f378ee02a0';\r\n\t\t\t\t--SET @taskType = 'Y';\r\n\t\t\t\t--SET @year = '2011';\r\n\t\t\t\t--SET @month = '';\r\n                WITH cteContactTask AS\r\n                (\r\n\t                SELECT ct.TaskId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n\t\t                ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice, ct.ConstructionPeriod,\r\n\t\t                dbo.fn_GetContractTaskCount(TaskId) AS SubCount,ContractId,'1' AS ModifyType\r\n\t                FROM Bud_ContractTask ct\r\n                    JOIN Bud_ContractTaskAudit ON ct.PrjId=Bud_ContractTaskAudit.prjId\r\n\t                WHERE ct.PrjId = @PrjId AND TaskType = @taskType AND FlowState=1\r\n\t\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month";
            cmdText = (((cmdText + str2) + "),outBudConModifyInfo AS\r\n                (\r\n                    SELECT TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit,\r\n                    Quantity,StartDate,EndDate,conModifyTask.Note,Total,ISNULL(UnitPrice,0) UnitPrice,ConstructionPeriod,\r\n                    dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount,ContractId,ModifyType\r\n                    FROM Bud_ConModifyTask conModifyTask\r\n                    JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                    WHERE PrjId=@PrjId AND ModifyType=0\r\n                    AND FlowState=1 " + str2) + str + " ),cteTask AS\r\n                (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n                    (\r\n                        SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,\r\n                        Unit,(newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note,UnitPrice,\r\n                        (newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount,ContractId,ModifyType FROM \r\n                        (\r\n                            SELECT * FROM cteContactTask\r\n                            UNION\r\n                            SELECT * FROM outBudConModifyInfo\r\n                        )newTask LEFT JOIN \r\n                        (\r\n                            --清单内的变更金额\r\n                            SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total from Bud_ConModifyTask conModifyTask\r\n                            JOIN \r\n                            (\r\n                                SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId=@PrjId\r\n                            ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                            where budConModify.FlowState=1 AND modifyType=1 ") + str + "\r\n\t\t\t                group by conModifyTask.TaskId\r\n                        ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                    ) tab-- WHERE Len(OrderNumber)<=6\r\n                ),cteType AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                FROM XPM_Basic_CodeList AS cl\r\n\t                INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                )\r\n                SELECT * FROM\r\n                (\r\n\t                SELECT cteTask.*,\r\n\t\t                cteType.CodeNo, cteType.CodeName AS TypeName\r\n\t                FROM cteTask\r\n\t                LEFT JOIN cteType ON LEN(cteTask.OrderNumber) / 3 = \r\n\t\t                CASE(SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                ) AS t\r\n               -- WHERE LEN(OrderNumber)/3 <=2 --CodeNo <= 2\r\n                ORDER BY No ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@taskType", taskType), new SqlParameter("@year", year), new SqlParameter("@month", month) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable GetTaskInfoByPrjIdAndConIdAndOrderNum(string prjId, string contractID, string orderNumber, string taskType, string year, string month)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(contractID))
            {
                str2 = str2 + " AND (ContractId IS NULL OR ContractId='" + contractID + "') ";
            }
            if (!string.IsNullOrEmpty(orderNumber))
            {
                str2 = str2 + " AND OrderNumber like '" + orderNumber + "%' ";
            }
            if (!string.IsNullOrEmpty(taskType))
            {
                str = " and 1<>1 ";
            }
            string cmdText = "\r\n\t\t\t\t--DECLARE @PrjId nvarchar(200)\r\n\t\t\t\t--DECLARE @taskType char(1)\r\n\t\t\t\t--DECLARE @year char(4)\r\n\t\t\t\t--DECLARE @month char(2)\r\n\t\t\t\t--SET @PrjId = '9359ab03-a70f-4067-9ab5-b4f378ee02a0';\r\n\t\t\t\t--SET @taskType = 'Y';\r\n\t\t\t\t--SET @year = '2011';\r\n\t\t\t\t--SET @month = '';\r\n                WITH cteContactTask AS\r\n                (\r\n\t                SELECT ct.TaskId, ct.OrderNumber, ct.TaskCode, ct.TaskName, ct.Unit, ct.Quantity, \r\n\t\t                ct.StartDate, ct.EndDate, ct.Note, ct.Total, ct.UnitPrice, ct.ConstructionPeriod,\r\n\t\t                dbo.fn_GetContractTaskCount(TaskId) AS SubCount,ContractId\r\n\t                FROM Bud_ContractTask ct\r\n                    JOIN Bud_ContractTaskAudit ON ct.PrjId=Bud_ContractTaskAudit.prjId\r\n\t                WHERE ct.PrjId = @PrjId AND TaskType = @taskType AND FlowState=1\r\n\t\t                AND SUBSTRING(TaskID, 2, LEN(@year + @month)) = @year + @month";
            cmdText = (((cmdText + str2) + "),outBudConModifyInfo AS\r\n                (\r\n                    SELECT TaskId,OrderNumber,ModifyTaskCode TaskCode,ModifyTaskContent TaskName,Unit,\r\n                    Quantity,StartDate,EndDate,conModifyTask.Note,Total,ISNULL(UnitPrice,0) UnitPrice,ConstructionPeriod,\r\n                    dbo.fn_GetContractTaskCount(ModifyTaskId) AS SubCount,ContractId\r\n                    FROM Bud_ConModifyTask conModifyTask\r\n                    JOIN Bud_ConModify budConModify ON conModifyTask.modifyId=budConModify.modifyId\r\n                    WHERE PrjId=@PrjId AND ModifyType=0\r\n                    AND FlowState=1 " + str2) + str + " ),cteTask AS\r\n                (\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No,* FROM \r\n                    (\r\n                        SELECT newTask.TaskId,OrderNumber,TaskCode,TaskName,\r\n                        Unit,(newTask.Quantity+ISNULL(inBudConModifyTask.Quantity,0)) Quantity,StartDate,EndDate,ConstructionPeriod,Note,UnitPrice,\r\n                        (newTask.Total+ISNULL(inBudConModifyTask.Total,0)) Total,SubCount,ContractId  FROM \r\n                        (\r\n                            SELECT * FROM cteContactTask\r\n                            UNION\r\n                            SELECT * FROM outBudConModifyInfo\r\n                        )newTask LEFT JOIN \r\n                        (\r\n                            --清单内的变更金额\r\n                            SELECT conModifyTask.TaskId,SUM(conModifyTask.Quantity) Quantity,SUM(conModifyTask.Total) Total from Bud_ConModifyTask conModifyTask\r\n                            JOIN \r\n                            (\r\n                                SELECT Bud_ConModify.* FROM Bud_ConModify WHERE PrjId=@PrjId\r\n                            ) budConModify ON conModifyTask.ModifyId=budConModify.ModifyId \r\n                            where budConModify.FlowState=1 AND modifyType=1 ") + str + "\r\n\t\t\t                group by conModifyTask.TaskId\r\n                        ) inBudConModifyTask ON newTask.TaskId=inBudConModifyTask.TaskId\r\n                    ) tab-- WHERE Len(OrderNumber)<=6\r\n                ),cteType AS\r\n                (\r\n\t                SELECT ROW_NUMBER() OVER(ORDER BY cl.CodeId) AS CodeNo, cl.CodeId, cl.CodeName \r\n\t                FROM XPM_Basic_CodeList AS cl\r\n\t                INNER JOIN XPM_Basic_CodeType AS ct ON cl.TypeId = ct.TypeId\r\n\t                WHERE ct.SignCode = 'taskType' AND cl.IsValid = '1'\r\n                )\r\n                SELECT * FROM\r\n                (\r\n\t                SELECT cteTask.*,\r\n\t\t                cteType.CodeNo, cteType.CodeName AS TypeName\r\n\t                FROM cteTask\r\n\t                LEFT JOIN cteType ON LEN(cteTask.OrderNumber) / 3 = \r\n\t\t                CASE(SELECT COUNT(*) FROM cteType) WHEN 0 THEN 1 ELSE cteType.CodeNo END\r\n                ) AS t\r\n               -- WHERE LEN(OrderNumber)/3 <=2 --CodeNo <= 2\r\n                ORDER BY No ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjId", prjId), new SqlParameter("@taskType", taskType), new SqlParameter("@year", year), new SqlParameter("@month", month) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static bool IsLeaf(string taskId)
        {
            bool flag = true;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Bud_ContractTask
                    where m.ParentId == taskId
                    select m).FirstOrDefault<Bud_ContractTask>() != null)
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static void Update(cn.justwin.Domain.BudContractTask budContractTask)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ContractTask task = (from m in entities.Bud_ContractTask
                    where m.TaskId == budContractTask.Id
                    select m).FirstOrDefault<Bud_ContractTask>();
                task.TaskCode = budContractTask.Code;
                task.TaskName = budContractTask.Name;
                task.StartDate = budContractTask.StartDate;
                task.EndDate = budContractTask.EndDate;
                task.Quantity = budContractTask.Quantity;
                task.Unit = budContractTask.Unit;
                task.UnitPrice = budContractTask.UnitPrice;
                task.Total = budContractTask.Total;
                task.Note = budContractTask.Note;
                entities.SaveChanges();
            }
        }

        public string Code { get; set; }

        public DateTime? EndDate { get; set; }

        public string Id { get; set; }

        public DateTime InputDate { get; set; }

        public string InputUser { get; set; }

        public int Layer
        {
            get
            {
                return (this.OrderNumber.Length / 3);
            }
        }

        public string Name { get; set; }

        public string Note { get; set; }

        public string OrderNumber { get; set; }

        public string ParentId { get; set; }

        public string PrjId { get; set; }

        public decimal Quantity { get; set; }

        public DateTime? StartDate { get; set; }

        public decimal? Total { get; set; }

        public string TypeName
        {
            get
            {
                if (string.IsNullOrEmpty(this.typeName))
                {
                    BudTaskType byLayer = BudTaskType.GetByLayer(this.Layer);
                    if (byLayer != null)
                    {
                        this.typeName = byLayer.Name;
                    }
                }
                return this.typeName;
            }
        }

        public string Unit { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}

