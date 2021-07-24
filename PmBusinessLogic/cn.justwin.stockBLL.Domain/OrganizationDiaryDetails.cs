namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class OrganizationDiaryDetails
    {
        private OrganizationDiaryDetails()
        {
        }

        public void Add(OrganizationDiaryDetails orgDetails)
        {
            if (orgDetails != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_OrgDiaryCost cost = (from m in entities.Bud_OrgDiaryCost
                        where m.OrgDiaryId == orgDetails.InDiaryId
                        select m).FirstOrDefault<Bud_OrgDiaryCost>();
                    Bud_OrgDiaryDetails details = new Bud_OrgDiaryDetails {
                        OrgDiaryDetailsId = orgDetails.Id,
                        OrgdetailsCode = orgDetails.Code,
                        Name = orgDetails.Name,
                        CBSCode = orgDetails.CBSCode,
                        Bud_OrgDiaryCost = cost,
                        Amount = orgDetails.Amount,
                        Note = orgDetails.Note
                    };
                    entities.AddToBud_OrgDiaryDetails(details);
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
                    foreach (Bud_OrgDiaryDetails details in (from m in entities.Bud_OrgDiaryDetails
                        where m.Bud_OrgDiaryCost.OrgDiaryId == inDiaryId
                        select m).ToList<Bud_OrgDiaryDetails>())
                    {
                        entities.DeleteObject(details);
                    }
                    entities.SaveChanges();
                }
            }
        }

        public static OrganizationDiaryDetails Create(string id, string InDiaryId, string name, decimal amount, string cbsCode, string note)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("组织机构日记明细", "Id不能为空!!!");
            }
            if (string.IsNullOrEmpty(InDiaryId))
            {
                throw new ArgumentNullException("组织机构日记明细", "InDiaryId不能为空!!!");
            }
            if (string.IsNullOrEmpty(cbsCode))
            {
                throw new ArgumentNullException("组织机构日记明细", "CBSCode不能为空!!!");
            }
            return new OrganizationDiaryDetails { Id = id, InDiaryId = InDiaryId, CBSCode = cbsCode, Amount = amount, Name = name, Note = note };
        }

        public static void Del(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_OrgDiaryDetails entity = (from m in entities.Bud_OrgDiaryDetails
                        where m.OrgDiaryDetailsId == id
                        select m).FirstOrDefault<Bud_OrgDiaryDetails>();
                    if (entity == null)
                    {
                        throw new Exception("找不到要删除的组织机构明细对象!!!");
                    }
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                }
            }
        }

        public static List<OrganizationDiaryDetails> GetAllByInDiaryId(string diaryId)
        {
            List<OrganizationDiaryDetails> list = new List<OrganizationDiaryDetails>();
            if (string.IsNullOrEmpty(diaryId))
            {
                return list;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_OrgDiaryDetails
                    where m.Bud_OrgDiaryCost.OrgDiaryId == diaryId
                    select new OrganizationDiaryDetails { Id = m.OrgDiaryDetailsId, Code = m.OrgdetailsCode, Amount = m.Amount, Name = m.Name, CBSCode = m.CBSCode, Note = m.Note, InDiaryId = m.Bud_OrgDiaryCost.OrgDiaryId } into m
                    orderby m.Code, m.CBSCode
                    select m).ToList<OrganizationDiaryDetails>();
            }
        }

        public static OrganizationDiaryDetails GetById(string id)
        {
            OrganizationDiaryDetails details = null;
            if (string.IsNullOrEmpty(id))
            {
                return details;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_OrgDiaryDetails
                    where m.OrgDiaryDetailsId == id
                    select new OrganizationDiaryDetails { Id = m.OrgDiaryDetailsId, Code = m.OrgdetailsCode, Amount = m.Amount, Name = m.Name, CBSCode = m.CBSCode, Note = m.Note, InDiaryId = m.Bud_OrgDiaryCost.OrgDiaryId }).FirstOrDefault<OrganizationDiaryDetails>();
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
                    num = ((IQueryable<decimal>) (from m in entities.Bud_OrgDiaryDetails
                        where m.Bud_OrgDiaryCost.OrgDiaryId == diaryId
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

        public void Update(CostDiaryDetails orgDetails)
        {
            if (orgDetails != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_OrgDiaryDetails details = (from m in entities.Bud_OrgDiaryDetails
                        where m.OrgDiaryDetailsId == orgDetails.Id
                        select m).FirstOrDefault<Bud_OrgDiaryDetails>();
                    if (details == null)
                    {
                        throw new Exception("找不到要修改的组织机构明细对象！");
                    }
                    details.Name = orgDetails.Name;
                    details.OrgdetailsCode = orgDetails.Code;
                    details.Note = orgDetails.Note;
                    details.Amount = orgDetails.Amount;
                    details.CBSCode = orgDetails.CBSCode;
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

