namespace cn.justwin.ProgressManagement
{
    using PmBusinessLogic;
    using System;

    public class ReportCommit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            Report.UpdateProgress(key.ToString());
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

