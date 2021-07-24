namespace cn.justwin.Domain
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.Project;
    using PmBusinessLogic;
    using System;

    public class ProjectBidSelf : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            Guid id = new Guid(key.ToString());
            PTPrjInfoZTBService service = new PTPrjInfoZTBService();
            PTPrjInfoService service2 = new PTPrjInfoService();
            PTPrjInfoZTB byId = service.GetById(id);
            if (byId.PrjState.ToString() == ProjectParameter.WinBid)
            {
                service2.ChangePrjInfo(byId, 5, 1);
            }
        }

        public void RefuseEvent(object key)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
        }
    }
}

