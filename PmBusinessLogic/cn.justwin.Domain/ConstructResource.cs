namespace cn.justwin.Domain
{
    using cn.justwin.contractBLL;
    using cn.justwin.DAL;
    using cn.justwin.stockBLL.Domain;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    [Serializable]
    public class ConstructResource
    {
        private string cbsName;
        [NonSerialized]
        private cn.justwin.Domain.ConstructTask constructTask;
        private static string priceTypeId = ConfigHelper.Get("BudgetPriceType");

        private ConstructResource()
        {
        }

        public void Add(ConstructResource consRes)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Resource resource = (from m in entities.Res_Resource
                    where m.ResourceId == consRes.ResourceId
                    select m).FirstOrDefault<Res_Resource>();
                Bud_ConsTask task = (from m in entities.Bud_ConsTask
                    where m.ConsTaskId == consRes.ConsTaskId
                    select m).FirstOrDefault<Bud_ConsTask>();
                if ((resource != null) && (task != null))
                {
                    Bud_ConsTaskRes res = new Bud_ConsTaskRes {
                        ConsTaskResId = consRes.Id,
                        Quantity = consRes.Quantity,
                        Bud_ConsTask = task,
                        Res_Resource = resource,
                        UnitPrice = consRes.UnitPrice
                    };
                    entities.AddToBud_ConsTaskRes(res);
                    entities.SaveChanges();
                }
            }
        }

        public static List<ConstructResource> BackUpResource(string consTaskId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_ConsTaskRes
                    where m.Bud_ConsTask.ConsTaskId == consTaskId
                    select new ConstructResource { Id = m.ConsTaskResId, ResourceId = m.Res_Resource.ResourceId, ConsTaskId = m.Bud_ConsTask.ConsTaskId, Quantity = m.Quantity, UnitPrice = m.UnitPrice }).ToList<ConstructResource>();
            }
        }

        public static ConstructResource Create(string id, string consTaskId, string resourceId, decimal quantity, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("Id");
            }
            if (string.IsNullOrEmpty(consTaskId))
            {
                throw new ArgumentNullException("TaskId");
            }
            if (string.IsNullOrEmpty(resourceId))
            {
                throw new ArgumentNullException("ResourceId");
            }
            return new ConstructResource { Id = id, ConsTaskId = consTaskId, ResourceId = resourceId, Quantity = quantity, UnitPrice = unitPrice };
        }

        public static void Del(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ConsTaskRes entity = (from m in entities.Bud_ConsTaskRes
                    where m.ConsTaskResId == id
                    select m).FirstOrDefault<Bud_ConsTaskRes>();
                if (entity == null)
                {
                    throw new Exception("已删除不存在!");
                }
                entities.DeleteObject(entity);
                entities.SaveChanges();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (base.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.ResourceId == ((ConstructResource) obj).ResourceId);
        }

        public static List<ConstructResource> GetAll(string consTaskId, List<string> resourceIds)
        {
            List<ConstructResource> list = new List<ConstructResource>();
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<string>.Enumerator enumerator = resourceIds.GetEnumerator())
                {
                    string resourceId;
                    while (enumerator.MoveNext())
                    {
                        resourceId = enumerator.Current;
                        Bud_ConsTaskRes res = (from m in entities.Bud_ConsTaskRes
                            where (m.Bud_ConsTask.ConsTaskId == consTaskId) && (m.Res_Resource.ResourceId == resourceId)
                            select m).FirstOrDefault<Bud_ConsTaskRes>();
                        ConstructResource item = null;
                        if (res != null)
                        {
                            item = new ConstructResource {
                                ResourceId = resourceId,
                                Quantity = res.Quantity,
                                UnitPrice = res.UnitPrice,
                                ConsTaskId = consTaskId
                            };
                        }
                        else
                        {
                            item = new ConstructResource {
                                ResourceId = resourceId,
                                Quantity = 0M,
                                UnitPrice = 0M,
                                ConsTaskId = consTaskId
                            };
                        }
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static DataTable GetAllByConsTaskId(string consTaskId, string ResourceIds)
        {
            new List<ConstructResource>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT res.ResourceId,ResourceCode,ResourceName,Specification,unit.Name as unit,conRes.ConsTaskId,");
            builder.Append("ISNULL(conRes.Quantity,0) AS Quantity,ISNULL(conRes.UnitPrice,0) AS UnitPrice FROM Res_Resource AS res");
            builder.AppendLine();
            builder.Append("RIGHT JOIN Bud_ConsTaskRes AS conRes ON res.ResourceId = conRes.ResourceId");
            builder.AppendLine();
            builder.AppendFormat("LEFT JOIN Res_Unit AS unit ON Unit=unit.UnitId WHERE ConsTaskId='{0}'", consTaskId);
            builder.AppendLine();
            builder.Append("UNION");
            builder.AppendLine();
            builder.Append("SELECT * FROM(");
            builder.Append("SELECT res.ResourceId,ResourceCode,ResourceName,Specification,unit.Name as unit,conRes.ConsTaskId,");
            builder.Append("ISNULL(conRes.Quantity,0) AS Quantity,ISNULL(conRes.UnitPrice,0) AS UnitPrice FROM Res_Resource AS res");
            builder.AppendLine();
            builder.Append("LEFT JOIN Bud_ConsTaskRes AS conRes ON res.ResourceId = conRes.ResourceId");
            builder.AppendLine();
            builder.Append("LEFT JOIN Res_Unit AS unit ON Unit=unit.UnitId");
            builder.AppendLine();
            builder.AppendFormat("WHERE res.ResourceId IN ({0})", ResourceIds);
            builder.AppendLine();
            builder.AppendFormat(")AS RESULT WHERE ResourceId NOT IN(SELECT ResourceId FROM Bud_ConsTaskRes WHERE ConsTaskId='{0}')", consTaskId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public static List<ConstructResource> GetByConsTask(string consTaskId, string TaskId)
        {
            List<ConstructResource> list = new List<ConstructResource>();
            using (pm2Entities entities = new pm2Entities())
            {
                using (var enumerator = (from m in entities.Bud_ConsTaskRes
                    where m.Bud_ConsTask.ConsTaskId == consTaskId
                    select new { ConsTaskResId = m.ConsTaskResId, ResourceId = m.Res_Resource.ResourceId, Quantity = m.Quantity, UnitPrice = m.UnitPrice, AccountingQuantity = m.AccountingQuantity, ResourceTypeId = m.Res_Resource.Res_ResourceType.ResourceTypeId, CBSCode = m.CBSCode }).ToList().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var bud_consRes = enumerator.Current;
                        ConstructResource item = null;
                        if (bud_consRes != null)
                        {
                            item = new ConstructResource {
                                Id = bud_consRes.ConsTaskResId,
                                ResourceId = bud_consRes.ResourceId,
                                Quantity = bud_consRes.Quantity,
                                UnitPrice = bud_consRes.UnitPrice,
                                ConsTaskId = consTaskId,
                                AccountingQuantity = bud_consRes.AccountingQuantity
                            };
                        }
                        Bud_TaskResource resource3 = (from btr in entities.Bud_TaskResource
                            where (btr.Bud_Task.TaskId == TaskId) && (btr.Res_Resource.ResourceId == bud_consRes.ResourceId)
                            select btr).FirstOrDefault<Bud_TaskResource>();
                        if (resource3 != null)
                        {
                            item.BudQuantity = Convert.ToDecimal(resource3.ResourceQuantity);
                        }
                        else
                        {
                            item.BudQuantity = 0M;
                        }
                        if (bud_consRes.CBSCode != null)
                        {
                            item.CBScode = bud_consRes.CBSCode;
                        }
                        else
                        {
                            ResType byId = ResType.GetById(cn.justwin.Domain.Resource.GetFirstResourceTypeId(bud_consRes.ResourceTypeId));
                            if (byId != null)
                            {
                                if (!string.IsNullOrEmpty(byId.CBSCode))
                                {
                                    item.CBScode = byId.CBSCode;
                                }
                                else
                                {
                                    item.CBScode = string.Empty;
                                }
                            }
                            else
                            {
                                item.CBScode = string.Empty;
                            }
                        }
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public static ConstructResource GetById(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from ctr in entities.Bud_ConsTaskRes
                    where ctr.ConsTaskResId == id
                    select new ConstructResource { Id = ctr.ConsTaskResId, ConsTaskId = ctr.Bud_ConsTask.ConsTaskId, ResourceId = ctr.Res_Resource.ResourceId, Quantity = ctr.Quantity, UnitPrice = ctr.UnitPrice }).FirstOrDefault<ConstructResource>();
            }
        }

        public override int GetHashCode()
        {
            return this.ResourceId.GetHashCode();
        }

        public static DataTable GetInfoByConsReportIds(string consReportIds)
        {
            new List<ConstructResource>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ResourceId,ResourceName, Unit,Specification,Brand,ModelNumber,TechnicalParameter,UnitPrice,Quantity,cast((UnitPrice*Quantity) as decimal(20,3)) Total FROM ( ");
            builder.Append("select Bud_ConsTaskRes.ResourceId,ResourceName,Res_Unit.[Name] as Unit,");
            builder.Append("Res_Resource.Brand,ModelNumber,TechnicalParameter,");
            builder.Append("Res_Resource.Specification,UnitPrice,sum(Quantity) Quantity from Bud_ConsTaskRes LEFT JOIN ");
            builder.Append("Res_Resource ON Bud_ConsTaskRes.ResourceId=Res_Resource.ResourceId LEFT JOIN Res_Unit ON Unit=UnitId where ConsTaskId in (");
            builder.Append("select ConsTaskId from bud_ConsTask where consReportId=@consReportIds");
            builder.Append(" )group by Bud_ConsTaskRes.ResourceId,ResourceName,Res_Unit.Name,Specification ,UnitPrice,Res_Resource.Brand,ModelNumber,TechnicalParameter) tabRes ");
            SqlParameter parameter = new SqlParameter("@consReportIds", SqlDbType.NVarChar, 500) {
                Value = consReportIds
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static IList<ConstructResource> GetListByConsTaskId(string consTaskId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from ctr in entities.Bud_ConsTaskRes
                    where ctr.Bud_ConsTask.ConsTaskId == consTaskId
                    select new ConstructResource { Id = ctr.ConsTaskResId, ConsTaskId = ctr.Bud_ConsTask.ConsTaskId, ResourceId = ctr.Res_Resource.ResourceId, Quantity = ctr.Quantity, UnitPrice = ctr.UnitPrice }).ToList<ConstructResource>();
            }
        }

        private decimal GetPriceByTypeId(string typeId, string resId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_Price
                    where (m.Res_PriceType.PriceTypeId == typeId) && (m.Res_Resource.ResourceId == resId)
                    select m.PriceValue).FirstOrDefault<decimal>();
            }
        }

        public static DataTable GetResInfo(string ConsTaskId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT ConsTaskRes.ConsTaskResId AS Id,ConsTaskRes.UnitPrice,ConsTaskRes.Quantity,ISNULL(ConsTaskRes.AccountingQuantity,ConsTaskRes.Quantity) AS AccountingQuantity\r\n,CASE ISNULL(ConsTaskRes.CBSCode,'')\r\n\tWHEN '' THEN (SELECT CBSCode FROM Bud_CostAccounting WHERE Type='D' AND ResourceType=dbo.ufn_GetRootResTypeId(ConsTaskRes.ResourceId))\r\n\tELSE ConsTaskRes.CBSCode\r\nEND AS CBSCode\r\n,Res.ResourceCode,Res.ResourceName,Res.Specification,ResUnit.Name AS UnitName\r\n,ISNULL((SELECT ResourceQuantity FROM Bud_TaskResource WHERE TaskId=(SELECT TaskId FROM Bud_ConsTask\r\n\tWHERE ConsTaskId ='{0}') \r\n\tAND ResourceId=ConsTaskRes.ResourceId),0)AS BudQuantity \r\nFROM Bud_ConsTaskRes AS ConsTaskRes\r\nJOIN Res_Resource AS Res ON ConsTaskRes.ResourceId=Res.ResourceId\r\nLEFT JOIN Res_Unit AS ResUnit ON Res.Unit=ResUnit.UnitId\r\n WHERE ConsTaskId='{1}'", ConsTaskId, ConsTaskId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static List<string> GetResourceIds(string consTaskId)
        {
            List<string> list = new List<string>();
            using (pm2Entities entities = new pm2Entities())
            {
                List<string> list2 = (from m in entities.Bud_ConsTaskRes
                    where m.Bud_ConsTask.ConsTaskId == consTaskId
                    select m.Res_Resource.ResourceId).ToList<string>();
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

        private string GetResourceUnitName()
        {
            string str = string.Empty;
            using (pm2Entities entities = new pm2Entities())
            {
                if (this.Resource.ResourceUnit != null)
                {
                    string str2 = (from m in entities.Res_Unit
                        where m.UnitId == this.Resource.ResourceUnit.Id
                        select m.Name).FirstOrDefault<string>();
                    if (str2 != null)
                    {
                        str = str2;
                    }
                }
            }
            return str;
        }

        public static bool IsExistCBSCode(string code)
        {
            bool flag = false;
            using (pm2Entities entities = new pm2Entities())
            {
                if ((from m in entities.Res_ResourceType
                    where m.CBSCode == code
                    select m).ToList<Res_ResourceType>().Count > 0)
                {
                    flag = true;
                }
                if (!flag)
                {
                    ContractType type = new ContractType();
                    if (type.GetListByCBSCode(code).Count > 0)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public static void Update(List<ConstructResource> consResList)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                using (List<ConstructResource>.Enumerator enumerator = consResList.GetEnumerator())
                {
                    ConstructResource cr;
                    while (enumerator.MoveNext())
                    {
                        cr = enumerator.Current;
                        Bud_ConsTaskRes res = (from m in entities.Bud_ConsTaskRes
                            where m.ConsTaskResId == cr.Id
                            select m).FirstOrDefault<Bud_ConsTaskRes>();
                        if (res == null)
                        {
                            throw new Exception("要修改的记录不存在!");
                        }
                        res.AccountingQuantity = cr.AccountingQuantity;
                        res.CBSCode = cr.CBScode;
                    }
                }
                entities.SaveChanges();
            }
        }

        public static void UpdateAccountingQuantityCBSCode(List<string> consTaskId)
        {
            if ((consTaskId != null) && (consTaskId.Count > 0))
            {
                string inParameterSql = DBHelper.GetInParameterSql(consTaskId.ToArray());
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("UPDATE Bud_ConsTaskRes SET AccountingQuantity=Quantity\r\n,CBSCode=(SELECT CBSCode FROM Bud_CostAccounting WHERE Type='D' AND ResourceType=dbo.ufn_GetRootResTypeId(ResourceId))\r\nWHERE ConsTaskId IN({0})", inParameterSql);
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString());
            }
        }

        public static void UpdateAccountingQuantityCBSCode(string consTaskResId, decimal accountingQuantity, string CBSCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Bud_ConsTaskRes res = (from m in entities.Bud_ConsTaskRes
                    where m.ConsTaskResId == consTaskResId
                    select m).FirstOrDefault<Bud_ConsTaskRes>();
                if (res != null)
                {
                    res.AccountingQuantity = new decimal?(accountingQuantity);
                    res.CBSCode = CBSCode;
                    entities.SaveChanges();
                }
            }
        }

        public decimal? AccountingQuantity { get; set; }

        public decimal BudQuantity { get; set; }

        public string CBScode { get; set; }

        public string CbsName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.CBScode))
                {
                    CostAccounting byCode = CostAccounting.GetByCode(this.CBScode.Trim());
                    if (byCode != null)
                    {
                        this.cbsName = byCode.Name;
                    }
                    else
                    {
                        this.cbsName = "";
                    }
                }
                else
                {
                    this.cbsName = "";
                }
                return this.cbsName;
            }
            set
            {
                this.cbsName = value;
            }
        }

        public string ConsTaskId { get; set; }

        public cn.justwin.Domain.ConstructTask ConstructTask
        {
            get
            {
                if (this.constructTask == null)
                {
                    this.constructTask = cn.justwin.Domain.ConstructTask.GetById(this.ConsTaskId);
                }
                return this.constructTask;
            }
            set
            {
                this.constructTask = value;
            }
        }

        public string Id { get; set; }

        public decimal Quantity { get; set; }

        public cn.justwin.Domain.Resource Resource
        {
            get
            {
                return cn.justwin.Domain.Resource.GetById(this.ResourceId);
            }
        }

        public string ResourceId { get; set; }

        public string UnitName
        {
            get
            {
                return this.GetResourceUnitName();
            }
        }

        public decimal UnitPrice { get; set; }
    }
}

