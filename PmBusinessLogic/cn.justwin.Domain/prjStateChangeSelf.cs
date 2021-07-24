namespace cn.justwin.Domain
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using PmBusinessLogic;
    using System;

    internal class prjStateChangeSelf : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            string prjId = key.ToString();
            PTPrjInfoStateChangeService service = new PTPrjInfoStateChangeService();
            PTPrjInfoZTBService service2 = new PTPrjInfoZTBService();
            PTPrjInfoStateChange byPrjIdByOrder = service.GetByPrjIdByOrder(prjId, -1);
            if (byPrjIdByOrder != null)
            {
                service.ChangeFlowStateInfo(prjId, -1, 1);
                service2.UpdatePrjStateAtStateChange(prjId, byPrjIdByOrder.OldState.Value, byPrjIdByOrder.ChangeState.Value);
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

