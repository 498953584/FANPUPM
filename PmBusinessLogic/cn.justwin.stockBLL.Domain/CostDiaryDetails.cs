namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class CostDiaryDetails
    {
        private CostDiaryDetails()
        {
        }

        public void Add(CostDiaryDetails costDDetails)
        {
            if (costDDetails != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_IndirectDiaryCost cost = (from m in entities.Bud_IndirectDiaryCost
                        where m.InDiaryId == costDDetails.InDiaryId
                        select m).FirstOrDefault<Bud_IndirectDiaryCost>();
                    Bud_IndirectDiaryDetails details = new Bud_IndirectDiaryDetails {
                        InDiaryDetailsId = costDDetails.Id,
                        IndetailsCode = costDDetails.Code,
                        Name = costDDetails.Name,
                        CBSCode = costDDetails.CBSCode,
                        Bud_IndirectDiaryCost = cost,
                        Amount = costDDetails.Amount,
                        Note = costDDetails.Note
                    };
                    entities.AddToBud_IndirectDiaryDetails(details);
                    entities.SaveChanges();
                }
            }
        }

        public static void ClearDetails(string inDiaryId)
        {
            if (!string.IsNullOrEmpty(inDiaryId))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    foreach (Bud_IndirectDiaryDetails details in (from m in entities.Bud_IndirectDiaryDetails
                        where m.Bud_IndirectDiaryCost.InDiaryId == inDiaryId
                        select m).ToList<Bud_IndirectDiaryDetails>())
                    {
                        entities.DeleteObject(details);
                    }
                    entities.SaveChanges();
                }
            }
        }

        public static CostDiaryDetails Create(string id, string InDiaryId, string name, decimal amount, string cbsCode, string note)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("间接成本日记明细", "Id不能为空!!!");
            }
            if (string.IsNullOrEmpty(InDiaryId))
            {
                throw new ArgumentNullException("间接成本日记明细", "InDiaryId不能为空!!!");
            }
            if (string.IsNullOrEmpty(cbsCode))
            {
                throw new ArgumentNullException("间接成本日记明细", "CBSCode不能为空!!!");
            }
            return new CostDiaryDetails { Id = id, InDiaryId = InDiaryId, CBSCode = cbsCode, Amount = amount, Name = name, Note = note };
        }

        public static void Del(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_IndirectDiaryDetails entity = (from m in entities.Bud_IndirectDiaryDetails
                        where m.InDiaryDetailsId == id
                        select m).FirstOrDefault<Bud_IndirectDiaryDetails>();
                    if (entity == null)
                    {
                        throw new Exception("找不到要删除的间接成本明细对象!!!");
                    }
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                }
            }
        }

        public static List<CostDiaryDetails> GetAllByInDiaryId(string diaryId)
        {
            List<CostDiaryDetails> list = new List<CostDiaryDetails>();
            if (string.IsNullOrEmpty(diaryId))
            {
                return list;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_IndirectDiaryDetails
                    where m.Bud_IndirectDiaryCost.InDiaryId == diaryId
                    select new CostDiaryDetails { Id = m.InDiaryDetailsId, Code = m.IndetailsCode, Amount = m.Amount, Name = m.Name, CBSCode = m.CBSCode, Note = m.Note, InDiaryId = m.Bud_IndirectDiaryCost.InDiaryId } into m
                    orderby m.Code, m.CBSCode
                    select m).ToList<CostDiaryDetails>();
            }
        }

        public static CostDiaryDetails GetById(string id)
        {
            CostDiaryDetails details = null;
            if (string.IsNullOrEmpty(id))
            {
                return details;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_IndirectDiaryDetails
                    where m.InDiaryDetailsId == id
                    select new CostDiaryDetails { Id = m.InDiaryDetailsId, Amount = m.Amount, Name = m.Name, CBSCode = m.CBSCode, Note = m.Note, InDiaryId = m.Bud_IndirectDiaryCost.InDiaryId }).FirstOrDefault<CostDiaryDetails>();
            }
        }

        public static decimal GetSumAmountByInDiaryId(string diaryId)
        {
            decimal num = 0M;
            if (!string.IsNullOrEmpty(diaryId))
            {
                pm2Entities entities = new pm2Entities();
                try
                {
                    num = ((IQueryable<decimal>) (from m in entities.Bud_IndirectDiaryDetails
                        where m.Bud_IndirectDiaryCost.InDiaryId == diaryId
                        select m.Amount)).Sum();
                }
                catch
                {
                    num = 0M;
                }
                finally
                {
                    if (entities != null)
                    {
                        entities.Dispose();
                    }
                }
            }
            return num;
        }

        public void Update(CostDiaryDetails costDDetails)
        {
            if (costDDetails != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_IndirectDiaryDetails details = (from m in entities.Bud_IndirectDiaryDetails
                        where m.InDiaryDetailsId == costDDetails.Id
                        select m).FirstOrDefault<Bud_IndirectDiaryDetails>();
                    if (details == null)
                    {
                        throw new Exception("找不到要修改的间接成本明细对象!!!");
                    }
                    details.Name = costDDetails.Name;
                    details.Note = costDDetails.Note;
                    details.Amount = costDDetails.Amount;
                    details.CBSCode = costDDetails.CBSCode;
                    entities.SaveChanges();
                }
            }
        }

        public decimal Amount { get; set; }

        public string CBSCode { get; set; }

        public string CBSName
        {
            get
            {
                CostAccounting byCode = CostAccounting.GetByCode(this.CBSCode);
                if (byCode != null)
                {
                    return byCode.Name;
                }
                return string.Empty;
            }
        }

        public string Code { get; set; }

        public string Id { get; set; }

        public string InDiaryId { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }
    }
}

