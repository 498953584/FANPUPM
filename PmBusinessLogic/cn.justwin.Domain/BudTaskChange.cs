namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BudTaskChange
    {
        private BudTaskChange()
        {
        }

        public static void Add(BudTaskChange budTaskChange)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TaskChange change = new Bud_TaskChange {
                    TaskChangeId = budTaskChange.Id,
                    PrjId = budTaskChange.PrjId,
                    Version = budTaskChange.Version,
                    VersionCode = budTaskChange.VersionCode,
                    FlowState = budTaskChange.Flowstate,
                    Note = budTaskChange.Note,
                    InputUser = budTaskChange.InputUser,
                    InputDate = new DateTime?(DateTime.Now)
                };
                entities.AddToBud_TaskChange(change);
                entities.SaveChanges();
            }
        }

        public static BudTaskChange Create(string id, string prjId, int version, string versionCode, int flowState, string note, string inputUser)
        {
            return new BudTaskChange { Id = id, PrjId = prjId, Version = new int?(version), VersionCode = versionCode, Flowstate = new int?(flowState), Note = note, InputUser = inputUser };
        }

        public static int GetCount(string versionCode, string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from t in entities.Bud_TaskChange
                    where (t.VersionCode == versionCode) && (t.PrjId == prjId)
                    select t.TaskChangeId).Count<string>();
            }
        }

        public static int? GetCurBudVersion(string prjId)
        {
            int? curVersion = null;
            using (pm2Entities entities = new pm2Entities())
            {
                vGetCurBudVersion version = (from v in entities.vGetCurBudVersion
                    where v.PrjId == prjId
                    select v).FirstOrDefault<vGetCurBudVersion>();
                if (version != null)
                {
                    curVersion = version.CurVersion;
                }
            }
            return curVersion;
        }

        public static int? GetFlowState(string prjId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TaskChange change = (from t in entities.Bud_TaskChange
                    where t.PrjId == prjId
                    orderby t.Version descending
                    select t).FirstOrDefault<Bud_TaskChange>();
                if (change != null)
                {
                    return change.FlowState;
                }
                return null;
            }
        }

        public static BudTaskChange GetModel(string taskChangeId)
        {
            using (pm2Entities context = new pm2Entities())
            {
                IQueryable<string> source = from t in context.Bud_TaskChange
                    where t.TaskChangeId == taskChangeId
                    select t.PrjId;
                Guid? guid = new Guid(source.FirstOrDefault<string>().ToString());
                return (from t in context.Bud_TaskChange
                    where t.TaskChangeId == taskChangeId
                    from p in context.PT_PrjInfo
                    where p.PrjGuid == guid
                    select new BudTaskChange { Id = t.TaskChangeId, PrjId = t.PrjId, PrjName = p.PrjName, Flowstate = t.FlowState, Version = t.Version, VersionCode = t.VersionCode, Note = t.Note, InputUser = t.InputUser, InputDate = t.InputDate }).FirstOrDefault<BudTaskChange>();
            }
        }

        public static BudTaskChange GetTaskChange(string prjId)
        {
            BudTaskChange change = new BudTaskChange();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from t in entities.Bud_TaskChange
                    where t.PrjId == prjId
                    select new BudTaskChange { Id = t.TaskChangeId, PrjId = t.PrjId, Version = t.Version, VersionCode = t.VersionCode, Flowstate = t.FlowState, Note = t.Note, InputUser = t.InputUser, InputDate = t.InputDate }).FirstOrDefault<BudTaskChange>();
            }
        }

        public static IList<BudTaskChange> GetVersions(string prjId)
        {
            IList<BudTaskChange> list = new List<BudTaskChange>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from t in entities.Bud_TaskChange
                    where t.PrjId == prjId
                    orderby t.Version descending
                    select new BudTaskChange { Id = t.TaskChangeId, PrjId = t.PrjId, Version = t.Version, VersionCode = t.VersionCode, Flowstate = t.FlowState, Note = t.Note, InputUser = t.InputUser, InputDate = t.InputDate }).ToList<BudTaskChange>();
            }
        }

        public static bool IsAllowBudget(string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TaskChange change = (from t in entities.Bud_TaskChange
                    where (t.PrjId == prjId) && (t.Version == 1)
                    select t).FirstOrDefault<Bud_TaskChange>();
                if (change == null)
                {
                    return flag;
                }
                if ((change.FlowState != -1) && (change.FlowState != -3))
                {
                    return flag;
                }
                return true;
            }
        }

        public static bool IsAllowBudgetChange(string prjId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TaskChange change = (from t in entities.Bud_TaskChange
                    where (t.PrjId == prjId) && (t.Version == 1)
                    select t).FirstOrDefault<Bud_TaskChange>();
                if ((change != null) && (change.FlowState == 1))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static void Update(BudTaskChange budTaskChange)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_TaskChange change = (from t in entities.Bud_TaskChange
                    where t.TaskChangeId == budTaskChange.Id
                    select t).FirstOrDefault<Bud_TaskChange>();
                if (change != null)
                {
                    change.Version = budTaskChange.Version;
                    change.VersionCode = budTaskChange.VersionCode;
                    change.FlowState = budTaskChange.Flowstate;
                    change.InputUser = budTaskChange.InputUser;
                    change.Note = budTaskChange.Note;
                }
                entities.SaveChanges();
            }
        }

        public int? Flowstate { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string Note { get; set; }

        public string PrjId { get; set; }

        public string PrjName { get; set; }

        public int? Version { get; set; }

        public string VersionCode { get; set; }
    }
}

