namespace PmBusinessLogic
{
    using System;

    public interface ISelfEvent
    {
        void CommitEvent(object key);
        void RefuseEvent(object key);
        void RestatedEvent(object key);
        void SuperDelete(object key);
    }
}

