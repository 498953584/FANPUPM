namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class XPMBasicCodeTypeService : Repository<XPMBasicCodeType>
    {
        public XPMBasicCodeType GetById(int id)
        {
            return (from t in this
                where t.TypeID == id
                select t).FirstOrDefault<XPMBasicCodeType>();
        }

        public XPMBasicCodeType GetBySignCode(string signCode)
        {
            return (from t in this
                where t.SignCode == signCode
                select t).FirstOrDefault<XPMBasicCodeType>();
        }
    }
}

