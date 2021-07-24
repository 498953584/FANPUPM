namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConContractTypeService : Repository<ConContractType>
    {
        public ConContractType GetByID(string typeID)
        {
            new Guid(typeID);
            return (from p in this
                where p.TypeID == typeID
                select p).FirstOrDefault<ConContractType>();
        }

        public ConContractType GetContractTypeByTypeName(string typeName)
        {
            return (from p in this
                where p.TypeName == typeName
                select p).FirstOrDefault<ConContractType>();
        }

        public ConContractType GetContractTypeByTypeShort(string typeShort)
        {
            return (from p in this
                where p.TypeShort == typeShort
                select p).FirstOrDefault<ConContractType>();
        }

        public List<ConContractType> GetLstByNameAndShort(string typeName, string typeShort)
        {
            return (from p in this
                where (p.TypeName == typeName) && (p.TypeShort == typeShort)
                select p).ToList<ConContractType>();
        }
    }
}

