namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PriceType
    {
        private string priceTypeCode;
        private string priceTypeId;
        private string priceTypeName;

        public static IList<PriceType> GetPriceTypes()
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.Res_PriceType select new PriceType { PriceTypeId = p.PriceTypeId, PriceTypeCode = p.PriceTypeCode, PriceTypeName = p.PriceTypeName }).ToList<PriceType>();
            }
        }

        public static IList<PriceType> GetPriceTypes(string userCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.Res_PriceType
                    where p.UserCodes.Contains(userCode)
                    select new PriceType { PriceTypeId = p.PriceTypeId, PriceTypeCode = p.PriceTypeCode, PriceTypeName = p.PriceTypeName }).ToList<PriceType>();
            }
        }

        public string PriceTypeCode
        {
            get
            {
                return this.priceTypeCode;
            }
            set
            {
                this.priceTypeCode = value;
            }
        }

        public string PriceTypeId
        {
            get
            {
                return this.priceTypeId;
            }
            set
            {
                this.priceTypeId = value;
            }
        }

        public string PriceTypeName
        {
            get
            {
                return this.priceTypeName;
            }
            set
            {
                this.priceTypeName = value;
            }
        }
    }
}

