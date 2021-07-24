namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class ClearHelper
    {
        public static void ClearBudTask(object o)
        {
            try
            {
                new PTPrjInfoZTBDetailService();
                BudTaskService service = new BudTaskService();
                if ((from t in service select t.Version.Value).Max<int>() != 1)
                {
                    using (List<string>.Enumerator enumerator = (from t in service
                        where t.Version >= 1
                        select t.PrjId).Distinct<string>().ToList<string>().GetEnumerator())
                    {
                        string prjId;
                        while (enumerator.MoveNext())
                        {
                            prjId = enumerator.Current;
                            int num2 = (from t in service
                                where t.PrjId == prjId
                                select t.Version.Value).Max<int>();
                            if (num2 > 1)
                            {
                                string cmdText = string.Format("DELETE FROM Bud_Task WHERE PrjId = '{0}' AND Version < {1}", prjId, num2);
                                SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText);
                                cmdText = string.Format("UPDATE Bud_Task SET Version = 1 WHERE PrjId = '{0}' AND Version = {1}", prjId, num2);
                                SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ClearPTPrjInfo(object o)
        {
            try
            {
                Log4netHelper.Error(new Exception("ClearPTPrjInfo 开始执行"), "ClearPTPrjInfo", "bery");
                BasicConfigService service = new BasicConfigService();
                BasicConfig byName = service.GetByName("IsClearPTPrjInfo");
                if (byName == null)
                {
                    byName = new BasicConfig {
                        Id = Guid.NewGuid().ToString(),
                        ParaName = "IsClearPTPrjInfo",
                        ParaValue = "0",
                        Note = "是否已经把PT_PrjInfo_ZTB表的数据导入PT_PrjInfo表中"
                    };
                    service.Add(byName);
                }
                if (byName.ParaValue != "1")
                {
                    string cmdText = "SELECT PrjGuid FROM PT_PrjInfo_ZTB WHERE PrjGuid Not IN (SELECT PrjGuid FROM PT_PrjInfo)";
                    foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, cmdText).Rows)
                    {
                        Guid id = new Guid(row[0].ToString());
                        PTPrjInfoZTBService service2 = new PTPrjInfoZTBService();
                        PTPrjInfoService service3 = new PTPrjInfoService();
                        PTPrjInfoZTB byId = service2.GetById(id);
                        service3.ChangePrjInfo(byId, 2, 1);
                    }
                    byName.ParaValue = "1";
                    service.Update(byName);
                }
            }
            catch (Exception exception)
            {
                Log4netHelper.Error(exception, "ClearPTPrjInfo", "bery");
            }
        }

        public static void ClearTotal2(object o)
        {
            Log4netHelper.Error(new Exception("clearTotal2 开始执行"), "clearTotal2", "bery");
            BasicConfigService service = new BasicConfigService();
            BasicConfig byName = service.GetByName("IsClearTaskTotal2");
            if (byName.ParaValue != "1")
            {
                try
                {
                    BudTaskService service2 = new BudTaskService();
                    BudModifyTaskService source = new BudModifyTaskService();
                    new PTPrjInfoService();
                    foreach (BudModifyTask task in source.ToList<BudModifyTask>())
                    {
                        source.UpdateTotal2(task.ModifyTaskId);
                    }
                    string cmdText = string.Format("\r\n\t\t\t\t\tSELECT TaskId FROM Bud_Task\r\n\t\t\t\t\tWHERE TaskId IN ( SELECT TaskId FROM Bud_TaskResource )\r\n\t\t\t\t", new object[0]);
                    DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, cmdText);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string taskId = table.Rows[i][0].ToString();
                        service2.UpdateTotal2(taskId);
                    }
                    byName.ParaValue = "1";
                    service.Update(byName);
                    Log4netHelper.Error(new Exception("clearTotal2 执行成功"), "clearTotal2", "bery");
                }
                catch (Exception exception)
                {
                    Log4netHelper.Error(exception, "clearTotal2", "bery");
                }
            }
        }
    }
}

