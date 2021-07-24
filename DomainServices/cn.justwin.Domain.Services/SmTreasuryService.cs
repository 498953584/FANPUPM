namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), NHibernateContext, AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed), ServiceContract]
    public class SmTreasuryService : Repository<SmTreasury>
    {
        public SmTreasury GetByCode(string code)
        {
            return (from t in this
                where t.tcode == code
                select t).FirstOrDefault<SmTreasury>();
        }

        [WebGet(UriTemplate="/Treasury/{id}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public SmTreasury GetById(string id)
        {
            return (from t in this
                where t.tid == id
                select t).FirstOrDefault<SmTreasury>();
        }
    }
}

