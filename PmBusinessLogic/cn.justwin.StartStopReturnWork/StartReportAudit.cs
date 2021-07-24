namespace cn.justwin.StartStopReturnWork
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using cn.justwin.PrjManager;
    using PmBusinessLogic;
    using System;

    public class StartReportAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            PTStartReport byId = new PTStartReport();
            byId = new PTStartReportService().GetById(key.ToString());
            if (byId != null)
            {
                string prjGuid = byId.PrjGuid;
                if (ProjectInfo.GetById(prjGuid) != null)
                {
                    ProjectInfo.UpdatePrjState(prjGuid, "7");
                    ProjectInfo.UpdatePrjGroup(prjGuid, byId.ImplDep);
                }
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

