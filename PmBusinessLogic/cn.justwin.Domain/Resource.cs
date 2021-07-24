namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Resource
    {
        private Resource()
        {
        }

        public static void Add(Resource resource)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Resource resource2 = new Res_Resource {
                    ResourceId = resource.Id,
                    ResourceCode = resource.Code,
                    ResourceName = resource.Name,
                    Brand = resource.Brand,
                    TaxRate = resource.TaxRate,
                    Specification = resource.Specification,
                    ModelNumber = resource.ModelNumber,
                    TechnicalParameter = resource.TechnicalParameter,
                    Series = resource.Series,
                    InputUser = resource.InputUser,
                    InputDate = resource.InputDate,
                    SupplierId = resource.SupplierId,
                    Res_ResourceType = (from rt in entities.Res_ResourceType
                        where rt.ResourceTypeId == resource.ResourceType.Id
                        select rt).FirstOrDefault<Res_ResourceType>()
                };
                if (resource.TypeAttribute != null)
                {
                    resource2.Res_Attribute = (from a in entities.Res_Attribute
                        where a.AttributeId == resource.TypeAttribute.Id
                        select a).FirstOrDefault<Res_Attribute>();
                }
                if (resource.ResourceUnit != null)
                {
                    resource2.Res_Unit = (from u in entities.Res_Unit
                        where u.UnitId == resource.ResourceUnit.Id
                        select u).FirstOrDefault<Res_Unit>();
                }
                entities.AddToRes_Resource(resource2);
                entities.SaveChanges();
                if (resource.Prices != null)
                {
                    using (IEnumerator<ResPriceType> enumerator = resource.Prices.Keys.GetEnumerator())
                    {
                        ResPriceType resPriceType;
                        while (enumerator.MoveNext())
                        {
                            resPriceType = enumerator.Current;
                            Res_Price price = new Res_Price {
                                Res_Resource = (from r in entities.Res_Resource
                                    where r.ResourceId == resource.Id
                                    select r).FirstOrDefault<Res_Resource>(),
                                Res_PriceType = (from pt in entities.Res_PriceType
                                    where pt.PriceTypeId == resPriceType.Id
                                    select pt).FirstOrDefault<Res_PriceType>()
                            };
                            decimal? nullable = resource.Prices[resPriceType];
                            price.PriceValue = nullable.HasValue ? nullable.GetValueOrDefault() : 0M;
                            price.InputUser = resource.InputUser;
                            price.InputDate = resource.InputDate;
                            entities.AddToRes_Price(price);
                        }
                    }
                    entities.SaveChanges();
                }
            }
        }

        public static Resource Create(string id, ResType resType, string code, string name, string brand, decimal? taxRate, string specification, string modelNumber, string technicalParam, ResUnit resUnit, ResTypeAttribute typeAttr, string series, IDictionary<ResPriceType, decimal?> prices, string user, DateTime? date, int? supplierId)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("资源Id", "资源Id不能为空");
            }
            if (resType == null)
            {
                throw new ArgumentException("资源类型", "资源类型不能为空");
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("资源编号", "资源名称不能为空");
            }
            return new Resource { Id = id, ResourceType = resType, Code = code, Name = name, Brand = brand, TaxRate = taxRate, Specification = specification, ModelNumber = modelNumber, TechnicalParameter = technicalParam, ResourceUnit = resUnit, TypeAttribute = typeAttr, Series = series, Prices = prices, InputUser = user, InputDate = date, SupplierId = supplierId };
        }

        public static void Delete(string id)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_Price price in from p in entities.Res_Price
                    where p.Res_Resource.ResourceId == id
                    select p)
                {
                    entities.DeleteObject(price);
                }
                Res_Resource entity = (from r in entities.Res_Resource
                    where r.ResourceId == id
                    select r).FirstOrDefault<Res_Resource>();
                if (entity == null)
                {
                    throw new ArgumentException("找不到需要删除的资源");
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
            return (this.Id == ((Resource) obj).Id);
        }

        public static IList<Resource> GetAll()
        {
            List<Resource> list = new List<Resource>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (var typef in from r in entities.Res_Resource select new { Id = r.ResourceId, TypeId = r.Res_ResourceType.ResourceTypeId, Code = r.ResourceCode, Name = r.ResourceName, Brand = r.Brand, TaxRate = r.TaxRate, Specification = r.Specification, TechnicalParam = r.TechnicalParameter, UnitId = r.Res_Unit.UnitId, TypeAttributeId = r.Res_Attribute.AttributeId, Series = r.Series, User = r.InputUser, Date = r.InputDate })
                {
                    Resource item = new Resource {
                        Id = typef.Id,
                        ResourceType = ResType.GetById(typef.TypeId),
                        Code = typef.Code,
                        Name = typef.Name,
                        Brand = typef.Brand,
                        TaxRate = typef.TaxRate,
                        Specification = typef.Specification,
                        TechnicalParameter = typef.TechnicalParam,
                        ResourceUnit = ResUnit.GetById(typef.UnitId),
                        TypeAttribute = ResTypeAttribute.GetById(typef.TypeAttributeId),
                        Series = typef.Series,
                        Prices = GetPrices(typef.Id),
                        InputUser = typef.User,
                        InputDate = typef.Date
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public static Resource GetById(string id)
        {
            Resource resource = new Resource();
            using (pm2Entities entities = new pm2Entities())
            {
                var typee = (from m in entities.Res_Resource
                    where m.ResourceId == id
                    select new { Id = m.ResourceId, Name = m.ResourceName, Code = m.ResourceCode, Brand = (m.Brand == null) ? "" : m.Brand, InputDate = m.InputDate, InputUser = m.InputUser, Specification = (m.Specification == null) ? "" : m.Specification, Series = m.Series, SupplierId = m.SupplierId, TaxRate = m.TaxRate, TechnicalParameter = (m.TechnicalParameter == null) ? "" : m.TechnicalParameter, ModelNumber = (m.ModelNumber == null) ? "" : m.ModelNumber, unitId = m.Res_Unit.UnitId }).FirstOrDefault();
                if (typee != null)
                {
                    resource.Id = typee.Id;
                    resource.Name = typee.Name;
                    resource.Code = typee.Code;
                    resource.Brand = (typee.Brand == null) ? "" : typee.Brand;
                    resource.InputDate = typee.InputDate;
                    resource.InputUser = typee.InputUser;
                    resource.Specification = (typee.Specification == null) ? "" : typee.Specification;
                    resource.Series = typee.Series;
                    resource.SupplierId = typee.SupplierId;
                    resource.TaxRate = typee.TaxRate;
                    resource.TechnicalParameter = (typee.TechnicalParameter == null) ? "" : typee.TechnicalParameter;
                    resource.ModelNumber = (typee.ModelNumber == null) ? "" : typee.ModelNumber;
                    resource.ResourceUnit = ResUnit.GetById(typee.unitId);
                }
            }
            return resource;
        }

        public static string GetFirstResourceType(string resourceTypeId)
        {
            ResType byId = ResType.GetById(resourceTypeId);
            string parentId = byId.ParentId;
            if (string.IsNullOrEmpty(parentId))
            {
                return byId.Name;
            }
            return GetFirstResourceType(parentId);
        }

        public static string GetFirstResourceTypeId(string resourceTypeId)
        {
            ResType byId = ResType.GetById(resourceTypeId);
            string parentId = byId.ParentId;
            if (string.IsNullOrEmpty(parentId))
            {
                return byId.Id;
            }
            return GetFirstResourceTypeId(parentId);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        private static Dictionary<ResPriceType, decimal?> GetPrices(string id)
        {
            Dictionary<ResPriceType, decimal?> dictionary = new Dictionary<ResPriceType, decimal?>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (Res_Price price in from p in entities.Res_Price
                    where p.ResourceId == id
                    select p)
                {
                    ResPriceType byId = ResPriceType.GetById(price.PriceTypeId);
                    dictionary.Add(byId, new decimal?(price.PriceValue));
                }
            }
            return dictionary;
        }

        public static string GetResourceId(string resourceCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_Resource
                    where m.ResourceCode == resourceCode
                    select m.ResourceId).FirstOrDefault<string>();
            }
        }

        public static string GetResourceId(string name, string specification, string modelNumber)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_Resource
                    where ((m.ResourceName == name) && (m.Specification == specification)) && (m.ModelNumber == modelNumber)
                    select m.ResourceId).FirstOrDefault<string>();
            }
        }

        public static string GetResourceId(string name, string specification, string modelNumber, string brand)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Res_Resource
                    where (((m.ResourceName == name) && (m.Specification == specification)) && (m.ModelNumber == modelNumber)) && (m.Brand == brand)
                    select m.ResourceId).FirstOrDefault<string>();
            }
        }

        public static void GetResTypeTotal(string taskId, string priceType, DataTable dtrcj)
        {
            if (BudTask.GetById(taskId) != null)
            {
                foreach (TaskResource resource in BudTask.GetResourcesByTaskId(taskId))
                {
                    string resourceId = resource.Resource.Id;
                    decimal quantity = resource.Quantity;
                    decimal price = resource.Price;
                    string firstResourceType = GetFirstResourceType(GetById(resourceId).ResourceType.Id);
                    decimal num3 = 0M;
                    if (string.IsNullOrEmpty(priceType))
                    {
                        num3 = price * quantity;
                    }
                    else
                    {
                        using (pm2Entities entities = new pm2Entities())
                        {
                            using (List<ResPrice>.Enumerator enumerator2 = (from m in entities.Res_Price
                                where (m.ResourceId == resourceId) && (m.PriceTypeId == priceType)
                                select new ResPrice { PriceValue = m.PriceValue }).ToList<ResPrice>().GetEnumerator())
                            {
                                while (enumerator2.MoveNext())
                                {
                                    ResPrice current = enumerator2.Current;
                                    num3 = price * quantity;
                                }
                            }
                        }
                    }
                    object obj2 = dtrcj.Rows[0][firstResourceType];
                    obj2 = (obj2 == null) ? 0 : obj2;
                    dtrcj.Rows[0][firstResourceType] = Convert.ToInt32(obj2) + num3;
                }
            }
        }

        public static int? GetSupplierIdByName(string SupplierName)
        {
            int? nullable = null;
            using (pm2Entities entities = new pm2Entities())
            {
                XPM_Basic_ContactCorp corp = (from m in entities.XPM_Basic_ContactCorp
                    where m.CorpName == SupplierName
                    select m).FirstOrDefault<XPM_Basic_ContactCorp>();
                if (corp != null)
                {
                    nullable = new int?(corp.CorpID);
                }
            }
            return nullable;
        }

        public static decimal GetTypePrice(string taskId, string priceType, string resourceTypeId)
        {
            System.Func<ResPrice, bool> predicate = null;
            decimal num = 0M;
            if (BudTask.GetById(taskId) != null)
            {
                foreach (TaskResource resource in BudTask.GetResourcesByTaskId(taskId))
                {
                    System.Func<ResPrice, bool> func = null;
                    string resourceId = resource.Resource.Id;
                    decimal quantity = resource.Quantity;
                    decimal num3 = resource.Price;
                    if (GetFirstResourceType(GetById(resourceId).ResourceType.Id) == resourceTypeId)
                    {
                        if (func == null)
                        {
                            func = m => m.ResourceId == resourceId;
                        }
                        if (predicate == null)
                        {
                            predicate = m => m.PriceTypeId == priceType;
                        }
                        List<ResPrice> list2 = ResPrice.GetAll().Where<ResPrice>(func).Where<ResPrice>(predicate).ToList<ResPrice>();
                        if (list2.Count > 0)
                        {
                            foreach (ResPrice price in list2)
                            {
                                num += price.PriceValue * quantity;
                            }
                        }
                        else
                        {
                            num += num3 * quantity;
                        }
                    }
                }
            }
            return num;
        }

        public static void Update(Resource resource)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Resource resource2 = (from r in entities.Res_Resource
                    where r.ResourceId == resource.Id
                    select r).FirstOrDefault<Res_Resource>();
                if (resource2 == null)
                {
                    throw new ArgumentException("找不到需要需要的资源");
                }
                resource2.ResourceCode = resource.Code;
                resource2.ResourceCode = resource.Code;
                resource2.ResourceName = resource.Name;
                resource2.Brand = resource.Brand;
                resource2.TaxRate = resource.TaxRate;
                resource2.Specification = resource.Specification;
                resource2.TechnicalParameter = resource.TechnicalParameter;
                resource2.Series = resource.Series;
                resource2.InputUser = resource.InputUser;
                resource2.InputDate = resource.InputDate;
                resource2.Res_ResourceType = (from rt in entities.Res_ResourceType
                    where rt.ResourceTypeId == resource.ResourceType.Id
                    select rt).FirstOrDefault<Res_ResourceType>();
                resource2.Res_Attribute = (from a in entities.Res_Attribute
                    where a.AttributeId == resource.TypeAttribute.Id
                    select a).FirstOrDefault<Res_Attribute>();
                resource2.Res_Unit = (from u in entities.Res_Unit
                    where u.UnitId == resource.ResourceUnit.Id
                    select u).FirstOrDefault<Res_Unit>();
                foreach (Res_Price price in from p in entities.Res_Price
                    where p.Res_Resource.ResourceId == resource.Id
                    select p)
                {
                    entities.DeleteObject(price);
                }
                using (IEnumerator<ResPriceType> enumerator2 = resource.Prices.Keys.GetEnumerator())
                {
                    ResPriceType resPriceType;
                    while (enumerator2.MoveNext())
                    {
                        resPriceType = enumerator2.Current;
                        Res_Price price2 = new Res_Price {
                            Res_Resource = (from r in entities.Res_Resource
                                where r.ResourceId == resource.Id
                                select r).FirstOrDefault<Res_Resource>(),
                            Res_PriceType = (from pt in entities.Res_PriceType
                                where pt.PriceTypeId == resPriceType.Id
                                select pt).FirstOrDefault<Res_PriceType>()
                        };
                        decimal? nullable = resource.Prices[resPriceType];
                        price2.PriceValue = nullable.HasValue ? nullable.GetValueOrDefault() : 0M;
                        price2.InputUser = resource.InputUser;
                        price2.InputDate = resource.InputDate;
                        entities.AddToRes_Price(price2);
                    }
                }
                entities.SaveChanges();
            }
        }

        public string Brand { get; set; }

        public string Code { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string InputUser { get; set; }

        public string ModelNumber { get; set; }

        public string Name { get; set; }

        public IDictionary<ResPriceType, decimal?> Prices { get; set; }

        public ResType ResourceType { get; set; }

        public ResUnit ResourceUnit { get; set; }

        public string Series { get; set; }

        public string Specification { get; set; }

        public int? SupplierId { get; set; }

        public decimal? TaxRate { get; set; }

        public string TechnicalParameter { get; set; }

        public ResTypeAttribute TypeAttribute { get; set; }
    }
}

