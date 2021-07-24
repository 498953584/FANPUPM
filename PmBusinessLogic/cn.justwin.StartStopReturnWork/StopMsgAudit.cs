namespace cn.justwin.StartStopReturnWork
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.PrjManager;
    using PmBusinessLogic;
    using System;

    public class StopMsgAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            PTStopMsg byId = new PTStopMsgService().GetById(key.ToString());
            if (byId != null)
            {
                ProjectInfo.UpdatePrjState(byId.PrjGuid, "8");
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

