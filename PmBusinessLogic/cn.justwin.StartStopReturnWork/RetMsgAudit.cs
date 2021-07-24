namespace cn.justwin.StartStopReturnWork
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.PrjManager;
    using PmBusinessLogic;
    using System;

    public class RetMsgAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            PTRetMsg byId = new PTRetMsgService().GetById(key.ToString());
            if (byId != null)
            {
                ProjectInfo.UpdatePrjState(byId.PrjGuid, "7");
            }
        }

        public void RefuseEvent(object primarykey)
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

