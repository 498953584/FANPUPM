namespace cn.justwin.Tender
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ProjectRank
    {
        public static IList<ProjectRank> GetRanks(Guid prjGuid)
        {
            System.Collections.Generic.IList<ProjectRank> ranks = new System.Collections.Generic.List<ProjectRank>();
            using (pm2Entities pm2 = new pm2Entities())
            {
                IQueryable<PT_PrjInfo_Rank> query =
                    from r in pm2.PT_PrjInfo_Rank
                    where r.PrjGuid == prjGuid
                    select r;
                foreach (PT_PrjInfo_Rank item in query)
                {
                    ranks.Add(new ProjectRank
                    {
                        Rank = item.PrjRank,
                        RankLevel = item.RankLevel
                    });
                }
            }
            return ranks;
        }

        public static void Update(string prjGuid, IList<ProjectRank> rankList)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                string commandText = string.Format("DELETE FROM PT_PrjInfo_Rank WHERE PrjGuid = '{0}'", prjGuid);
                entities.ExecuteStoreCommand(commandText, new object[0]);
                if (rankList != null)
                {
                    foreach (ProjectRank rank in rankList)
                    {
                        PT_PrjInfo_Rank rank2 = new PT_PrjInfo_Rank {
                            RankId = Guid.NewGuid().ToString(),
                            PrjGuid = new Guid(prjGuid),
                            PrjRank = rank.Rank,
                            RankLevel = rank.RankLevel
                        };
                        entities.AddToPT_PrjInfo_Rank(rank2);
                    }
                }
            }
        }

        public static void Update(string prjGuid, IList<ProjectRank> rankList, pm2Entities pm2)
        {
            string commandText = string.Format("DELETE FROM PT_PrjInfo_Rank WHERE PrjGuid = '{0}'", prjGuid);
            pm2.ExecuteStoreCommand(commandText, new object[0]);
            if (rankList != null)
            {
                foreach (ProjectRank rank in rankList)
                {
                    PT_PrjInfo_Rank rank2 = new PT_PrjInfo_Rank {
                        RankId = Guid.NewGuid().ToString(),
                        PrjGuid = new Guid(prjGuid),
                        PrjRank = rank.Rank,
                        RankLevel = rank.RankLevel
                    };
                    pm2.AddToPT_PrjInfo_Rank(rank2);
                }
            }
        }

        public string Rank { get; set; }

        public string RankLevel { get; set; }
    }
}

