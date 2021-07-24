namespace cn.justwin.PrjManager
{
    using PmBusinessLogic;
    using System;

    public class PrjCompletedAudit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            ProjectInfo.UpdatePrjState(key.ToString(), "9");
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

