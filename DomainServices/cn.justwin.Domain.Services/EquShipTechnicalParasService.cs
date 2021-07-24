namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EquShipTechnicalParasService : Repository<EquShipTechnicalParas>
    {
        public void DeleteAllEquShipTechInfos(string equId)
        {
            IList<EquShipTechnicalParas> list = (from t in this
                where t.EquId.Equals(equId)
                select t).ToList<EquShipTechnicalParas>();
            if ((list != null) && (list.Count > 0))
            {
                foreach (EquShipTechnicalParas paras in list)
                {
                    base.Delete(paras);
                }
            }
        }

        public EquShipTechnicalParas GetById(string techId)
        {
            return (from t in this
                where t.TechId.Equals(techId)
                select t).FirstOrDefault<EquShipTechnicalParas>();
        }

        public IList<EquShipTechnicalParas> GetEquShipTechParasByEquId(string equId)
        {
            return (from t in this
                where t.EquId.Equals(equId)
                select t).ToList<EquShipTechnicalParas>();
        }

        public void InsertEquShipTechParasTable(string equId, params string[] otherShipInfo)
        {
            if ((otherShipInfo != null) || (otherShipInfo.Length != 0))
            {
                EquShipTechnicalParas item = null;
                foreach (string str in otherShipInfo)
                {
                    item = new EquShipTechnicalParas {
                        TechId = Guid.NewGuid().ToString(),
                        EquId = equId,
                        OtherShipInfo = str
                    };
                    base.Add(item);
                }
            }
        }
    }
}

