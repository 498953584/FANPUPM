namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SmWantplanService : Repository<SmWantplan>
    {
        public SmWantplan GetById(string Id)
        {
            return (from p in this
                where p.swid == Id
                select p).FirstOrDefault<SmWantplan>();
        }

        public List<string> SmWlanPlanIds(List<string> prjIds)
        {
            List<string> list = new List<string>();
            List<SmWantplan> list2 = (from p in this
                where prjIds.Contains(p.procode) && (p.flowstate == 1)
                select p).ToList<SmWantplan>();
            if (list2.Count > 0)
            {
                foreach (SmWantplan wantplan in list2)
                {
                    if (!list.Contains(wantplan.procode))
                    {
                        list.Add(wantplan.procode);
                    }
                }
            }
            return list;
        }
    }
}

