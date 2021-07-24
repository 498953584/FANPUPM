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

    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed), ServiceContract]
    public class PTBULLETINMAINService : Repository<PTBULLETINMAIN>
    {
        [WebGet(UriTemplate="/Bulletion/{id}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public PTBULLETINMAIN GetById(string id)
        {
            Guid guid = new Guid(id);
            return (from b in this
                where b.I_BULLETINID == guid
                select b).First<PTBULLETINMAIN>();
        }

        [WebGet(UriTemplate="/GetOutBulletion", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public IList<PTBULLETINMAIN> GetOutBulletion()
        {
            return (from b in this
                where b.I_RELEASEBOUND == 2
                select b).ToList<PTBULLETINMAIN>();
        }

        [OperationContract, WebGet(UriTemplate="/GetOutTitle", ResponseFormat=WebMessageFormat.Json)]
        public IList<string> GetOutTitle()
        {
            return (from b in this
                where b.I_RELEASEBOUND == 2
                select b.V_TITLE).ToList<string>();
        }
    }
}

