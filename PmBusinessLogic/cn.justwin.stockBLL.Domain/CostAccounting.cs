namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    [Serializable]
    public class CostAccounting
    {
        private CostAccounting()
        {
        }

        public void Add(CostAccounting budCost, string inputUser)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_CostAccounting accounting = new Bud_CostAccounting {
                    Id = budCost.Id,
                    CBSCode = budCost.Code,
                    Name = budCost.Name,
                    Type = budCost.Type,
                    Note = budCost.Note
                };
                entities.AddToBud_CostAccounting(accounting);
                entities.SaveChanges();
            }
        }

        private void AddToIndirectBudget(string cbsCode, string inputUser)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (System.Linq.IGrouping<string, string> grouping in (from m in entities.Bud_IndirectBudget
                    select m.ProjectId into prjId
                    group prjId by prjId).ToList<System.Linq.IGrouping<string, string>>())
                {
                    string key = grouping.Key;
                    IndirectBudget indirectBudget = IndirectBudget.Create(Guid.NewGuid().ToString(), key, cbsCode, 0M, 0M, inputUser, DateTime.Now, string.Empty);
                    indirectBudget.Add(indirectBudget);
                }
            }
        }

        private void AddToOrganBudget(string cbsCode, string inputUser)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (System.Linq.IGrouping<string, string> grouping in (from m in entities.Bud_OrganizationBudget
                    select m.OrganizationBudgetId into id
                    group id by id).ToList<System.Linq.IGrouping<string, string>>())
                {
                    string key = grouping.Key;
                    OrganizationBudget organBudget = OrganizationBudget.Create(Guid.NewGuid().ToString(), key, cbsCode, 0M, 0M, inputUser, DateTime.Now, string.Empty);
                    organBudget.Add(organBudget);
                }
            }
        }

        public static CostAccounting Create(string id, string name, string code, string type, string note)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("核算成本", "Id不能为空!");
            }
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException("核算成本", "类型不能为空!");
            }
            return new CostAccounting { Id = id, Name = name, Code = code, Type = type, Note = note };
        }

        public static bool Del(List<string> ids)
        {
            bool flag = false;
            CostAccounting accounting = new CostAccounting();
            if (accounting.IsDelAll(ids))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    using (List<string>.Enumerator enumerator = ids.GetEnumerator())
                    {
                        string id;
                        while (enumerator.MoveNext())
                        {
                            id = enumerator.Current;
                            Bud_CostAccounting entity = (from m in entities.Bud_CostAccounting
                                where (m.Id == id) && (m.CBSCode.Length >= 6)
                                select m).FirstOrDefault<Bud_CostAccounting>();
                            if (entity != null)
                            {
                                bool flag3 = true;
                                string cBSCode = entity.CBSCode;
                                if (entity.Type == "D")
                                {
                                    flag3 = ConstructResource.IsExistCBSCode(cBSCode);
                                }
                                else
                                {
                                    flag3 = IndirectBudget.IsExistCBSCode(cBSCode) || OrganizationBudget.IsExistCBSCode(cBSCode);
                                }
                                if (!flag3)
                                {
                                    entities.DeleteObject(entity);
                                    entities.SaveChanges();
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }
            return flag;
        }

        public static List<CostAccounting> GetAll()
        {
            List<CostAccounting> list = new List<CostAccounting>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_CostAccounting
                    orderby m.CBSCode
                    select new CostAccounting { Id = m.Id, Name = m.Name, Code = m.CBSCode, Note = m.Note, Type = m.Type }).ToList<CostAccounting>();
            }
        }

        public static CostAccounting GetByCode(string code)
        {
            CostAccounting accounting = new CostAccounting();
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_CostAccounting accounting2 = (from m in entities.Bud_CostAccounting
                    where m.CBSCode == code
                    select m).FirstOrDefault<Bud_CostAccounting>();
                if (accounting2 != null)
                {
                    return new CostAccounting { Id = accounting2.Id, Name = accounting2.Name, Code = accounting2.CBSCode, Note = accounting2.Note, Type = accounting2.Type };
                }
                return null;
            }
        }

        public static List<CostAccounting> GetByD()
        {
            List<CostAccounting> list = new List<CostAccounting>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_CostAccounting
                    where (m.Type == "D") && (m.CBSCode.Length == 9)
                    orderby m.CBSCode
                    select new CostAccounting { Name = m.Name, Code = m.CBSCode }).ToList<CostAccounting>();
            }
        }

        public static CostAccounting GetById(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_CostAccounting accounting = (from m in entities.Bud_CostAccounting
                    where m.Id == id
                    select m).FirstOrDefault<Bud_CostAccounting>();
                if (accounting == null)
                {
                    throw new Exception("核算成本,不能找到!");
                }
                return new CostAccounting { Id = accounting.Id, Name = accounting.Name, Code = accounting.CBSCode, Note = accounting.Note, Type = accounting.Type };
            }
        }

        public static string GetByResourceType(string ResourceType)
        {
            string cBSCode = " ";
            using (pm2Entities entities = new pm2Entities())
            {
                var typea = (from m in entities.Bud_CostAccounting
                    where m.ResourceType == ResourceType
                    select new { CBSCode = m.CBSCode }).FirstOrDefault();
                if (typea != null)
                {
                    cBSCode = typea.CBSCode;
                }
                else
                {
                    cBSCode = " ";
                }
                return cBSCode;
            }
        }

        public static string GetCode(string parentId)
        {
            string parentCode;
            string str = "001";
            using (pm2Entities entities = new pm2Entities())
            {
                parentCode = (from m in entities.Bud_CostAccounting
                    where m.Id == parentId
                    select m.CBSCode).FirstOrDefault<string>();
                if (string.IsNullOrEmpty(parentCode))
                {
                    string str2 = (from m in entities.Bud_CostAccounting
                        where m.CBSCode.Length == 3
                        orderby m.CBSCode descending
                        select m.CBSCode).FirstOrDefault<string>();
                    if (string.IsNullOrEmpty(str2))
                    {
                        return str;
                    }
                    int num = Convert.ToInt32(str2) + 1;
                    if (num.ToString().Length == 1)
                    {
                        return ("00" + num.ToString());
                    }
                    if (num.ToString().Length == 2)
                    {
                        return ("0" + num.ToString());
                    }
                    return num.ToString();
                }
                string str3 = (from m in entities.Bud_CostAccounting
                    where (m.CBSCode.StartsWith(parentCode) && ((m.CBSCode.Length - 3) == parentCode.Length)) && (m.CBSCode.Substring(6, 3) != "999")
                    orderby m.CBSCode descending
                    select m.CBSCode).FirstOrDefault<string>();
                if (string.IsNullOrEmpty(str3))
                {
                    return (parentCode + str);
                }
                int num2 = Convert.ToInt32(str3.Substring(str3.Length - 3)) + 1;
                if (num2.ToString().Length == 1)
                {
                    return (parentCode + "00" + num2.ToString());
                }
                if (num2.ToString().Length == 2)
                {
                    return (parentCode + "0" + num2.ToString());
                }
                return (parentCode + num2.ToString());
            }
        }

        public static List<CostAccounting> GetIndirectCost()
        {
            List<CostAccounting> list = new List<CostAccounting>();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_CostAccounting
                    where (m.CBSCode.Length >= 6) & (m.Type == "I")
                    select new CostAccounting { Code = m.CBSCode, Name = m.Name } into m
                    orderby m.Code
                    select m).ToList<CostAccounting>();
            }
        }

        public static DataTable GetResourceType()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ResourceTypeId,ResourceTypeName FROM Res_ResourceType WHERE ParentId IS NULL");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        protected bool IsDelAll(List<string> ids)
        {
            bool flag = true;
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<string>.Enumerator enumerator = ids.GetEnumerator())
                {
                    string id;
                    while (enumerator.MoveNext())
                    {
                        id = enumerator.Current;
                        Bud_CostAccounting accounting = (from m in entities.Bud_CostAccounting
                            where (m.Id == id) && (m.CBSCode.Length >= 6)
                            select m).FirstOrDefault<Bud_CostAccounting>();
                        if (accounting != null)
                        {
                            bool flag2 = true;
                            string cBSCode = accounting.CBSCode;
                            if (accounting.Type == "D")
                            {
                                flag2 = ConstructResource.IsExistCBSCode(cBSCode);
                            }
                            else
                            {
                                flag2 = IndirectBudget.IsExistCBSCode(cBSCode) || OrganizationBudget.IsExistCBSCode(cBSCode);
                            }
                            if (flag2)
                            {
                                return false;
                            }
                        }
                    }
                    return flag;
                }
            }
        }

        public static bool IsExistResType(string resTypeId, string exceptId)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if (!string.IsNullOrEmpty((from m in entities.Bud_CostAccounting
                    where (m.ResourceType == resTypeId) && (m.Id != exceptId)
                    select m.Id).FirstOrDefault<string>()))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public void Update(CostAccounting budCost)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_CostAccounting accounting = (from m in entities.Bud_CostAccounting
                    where m.Id == budCost.Id
                    orderby m.CBSCode
                    select m).FirstOrDefault<Bud_CostAccounting>();
                if (accounting == null)
                {
                    throw new Exception("核算成本，不能找到要修改的项!");
                }
                accounting.Name = budCost.Name;
                accounting.Note = budCost.Note;
                accounting.Type = budCost.Type;
                entities.SaveChanges();
            }
        }

        public string Code { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public string Type { get; set; }
    }
}

