namespace cn.justwin.opm
{
    using cn.justwin.opm.OPS;
    using PmBusinessLogic;
    using System;

    public class MaintainCommit : ISelfEvent
    {
        public void CommitEvent(object key)
        {
            new MaintainDataAction().AddmaintainDatas(new Guid(key.ToString()));
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

