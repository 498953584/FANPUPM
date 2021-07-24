namespace cn.justwin.stockBLL.Domain
{
    using PmBusinessLogic;
    using System;

    public class IndirectCommit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            IndirectBudget.SwapData(key.ToString(), "1");
        }

        public void RefuseEvent(object key)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
            IndirectBudget.SwapData(key.ToString(), "2");
        }
    }
}

