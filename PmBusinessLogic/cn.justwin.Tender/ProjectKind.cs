using cn.justwin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
namespace cn.justwin.Tender
{
    public class ProjectKind
    {
        public string PrjKind
        {
            get;
            set;
        }
        public decimal ProfessionalCost
        {
            get;
            set;
        }
        public static void Update(string prjGuid, System.Collections.Generic.IList<ProjectKind> kindList, pm2Entities pm2)
        {
            string delSql = string.Format("DELETE FROM PT_PrjInfo_Kind WHERE PrjGuid = '{0}'", prjGuid);
            pm2.ExecuteStoreCommand(delSql, new object[0]);
            if (kindList != null)
            {
                foreach (ProjectKind kind in kindList)
                {
                    pm2.AddToPT_PrjInfo_Kind(new PT_PrjInfo_Kind
                    {
                        KindId = System.Guid.NewGuid().ToString(),
                        PrjGuid = new System.Guid(prjGuid),
                        PrjKind = kind.PrjKind,
                        ProfessionalCost = kind.ProfessionalCost
                    });
                }
            }
        }
        public static void Update(string prjGuid, System.Collections.Generic.IList<ProjectKind> kindList)
        {
            using (pm2Entities pm2 = new pm2Entities())
            {
                string delSql = string.Format("DELETE FROM PT_PrjInfo_Kind WHERE PrjGuid = '{0}'", prjGuid);
                pm2.ExecuteStoreCommand(delSql, new object[0]);
                if (kindList != null)
                {
                    foreach (ProjectKind kind in kindList)
                    {
                        pm2.AddToPT_PrjInfo_Kind(new PT_PrjInfo_Kind
                        {
                            KindId = System.Guid.NewGuid().ToString(),
                            PrjGuid = new System.Guid(prjGuid),
                            PrjKind = kind.PrjKind,
                            ProfessionalCost = kind.ProfessionalCost
                        });
                    }
                }
            }
        }
        public static System.Collections.Generic.IList<ProjectKind> GetProjectKinds(System.Guid prjGuid)
        {
            System.Collections.Generic.IList<ProjectKind> kindList = new System.Collections.Generic.List<ProjectKind>();
            using (pm2Entities pm2 = new pm2Entities())
            {
                IQueryable<PT_PrjInfo_Kind> query =
                    from k in pm2.PT_PrjInfo_Kind
                    where k.PrjGuid == prjGuid
                    select k;
                foreach (PT_PrjInfo_Kind item in query)
                {
                    kindList.Add(new ProjectKind
                    {
                        PrjKind = item.PrjKind,
                        ProfessionalCost = item.ProfessionalCost
                    });
                }
            }
            return kindList;
        }
    }
}
