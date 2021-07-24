namespace cn.justwin.Domain
{
    using cn.justwin.contractBLL;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Project
    {
        private Project()
        {
        }

        public static bool CheckProIsLoceke(string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                bool? nullable = (from m in entities.Bud_PrjTaskInfo
                    where m.PrjId == prjId
                    select m.IsLocked).FirstOrDefault<bool?>();
                if (nullable.HasValue)
                {
                    flag = nullable.Value;
                }
            }
            return flag;
        }

        public static void CoverVersion(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> ids = (from m in entities.Bud_Task
                    where m.PrjId == prjId
                    select m.TaskId).ToList<string>();
                new PayoutTarget().DelByTaskId(ids);
                foreach (string str in ids)
                {
                    BudTask.DeleteReourceByTaskId(str);
                    BudTask.Delete(str);
                }
            }
        }

        public static void CoverVersionNew(string prjId, int version)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> ids = (from m in entities.Bud_Task
                    where (m.PrjId == prjId) && (m.Version == version)
                    select m.TaskId).ToList<string>();
                new PayoutTarget().DelByTaskId(ids);
                foreach (string str in ids)
                {
                    TaskResource.DelRes(prjId, version);
                    ResourceTemp.DelByPrjId(prjId);
                    BudTask.Delete(str);
                }
            }
        }

        public static Project Create(string Id, string Code, string Name, DateTime StartDate, DateTime EndDate, bool isLocked)
        {
            if (string.IsNullOrEmpty(Id))
            {
                throw new ArgumentNullException("项目Id", "项目Id不能为空");
            }
            return new Project { Id = Id, Name = Name, Code = Code, StartDate = StartDate, EndDate = EndDate, IsLocked = isLocked };
        }

        public static IList<BudTask> GetTask(string prjId, string priceType, string isWBSRelevance, string taskType, string year, string month)
        {
            List<BudTask> list = new List<BudTask>();
            return BudTask.GetAll(priceType, prjId, isWBSRelevance, taskType, year, month).ToList<BudTask>();
        }

        public static int GetWBSFlowState(string prjId)
        {
            int num = -1;
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TaskChange change = (from c in entities.Bud_TaskChange
                    where (c.PrjId == prjId) && (c.Version == 1)
                    select c).FirstOrDefault<Bud_TaskChange>();
                if (change != null)
                {
                    num = change.FlowState.Value;
                }
            }
            return num;
        }

        public static bool LockProject(string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_PrjTaskInfo info = new Bud_PrjTaskInfo {
                    PrjId = prjId,
                    IsLocked = true,
                    PrjTaskInfoId = Guid.NewGuid().ToString()
                };
                entities.AddToBud_PrjTaskInfo(info);
                if (entities.SaveChanges() != 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public string Code { get; set; }

        public DateTime EndDate { get; set; }

        public string Id { get; set; }

        public bool IsLocked { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    }
}

