namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ResPrice
    {
        public static IList<ResPrice> GetAll()
        {
            List<ResPrice> list = new List<ResPrice>();
            using (pm2Entities entities = new pm2Entities())
            {
                var list2 = (from m in entities.Res_Price select new { ResourceId = m.ResourceId, PriceValue = m.PriceValue, PriceTypeId = m.Res_PriceType.PriceTypeId }).ToList();
                if (list2.Count > 0)
                {
                    foreach (var typed in list2)
                    {
                        ResPrice item = new ResPrice {
                            ResourceId = typed.ResourceId,
                            PriceValue = typed.PriceValue,
                            PriceTypeId = typed.PriceTypeId
                        };
                        list.Add(item);
                    }
                    return list;
                }
                return null;
            }
        }

        public static decimal GetPriceValue(string resourceId, string priceTypeId)
        {
            decimal priceValue = 0M;
            using (pm2Entities entities = new pm2Entities())
            {
                Res_Price price = (from rp in entities.Res_Price
                    where (rp.Res_Resource.ResourceId == resourceId) && (rp.Res_PriceType.PriceTypeId == priceTypeId)
                    select rp).FirstOrDefault<Res_Price>();
                if (price != null)
                {
                    priceValue = price.PriceValue;
                }
            }
            return priceValue;
        }

        public string PriceTypeId { get; set; }

        public decimal PriceValue { get; set; }

        public string ResourceId { get; set; }
    }
}

