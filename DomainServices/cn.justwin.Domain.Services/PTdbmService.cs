namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [NHibernateContext, ServiceContract, ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class PTdbmService : Repository<PTdbm>
    {
        [WebInvoke(UriTemplate="/Dep/{id}", ResponseFormat=WebMessageFormat.Json, Method="DELETE"), OperationContract]
        public string Delete(string id)
        {
            try
            {
                PTdbm byId = this.GetById(id);
                base.Delete(byId);
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public PTdbm GetById(object id)
        {
            int _id = Convert.ToInt32(id);
            return (from p in this
                where p.I_bmdm == _id
                select p).FirstOrDefault<PTdbm>();
        }

        public IList<PTdbm> GetChildren(object id)
        {
            return (from p in this
                where p.I_sjdm == ((int) id)
                select p).ToList<PTdbm>();
        }
    }
}

