namespace Business.BaseClass
{
    using System;

    public interface SelfEvent
    {
        void CommitEvent(string primarykey);
        void RefuseEvent(string primarykey);
    }
}

