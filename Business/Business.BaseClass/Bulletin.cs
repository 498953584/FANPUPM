namespace Business.BaseClass
{
    using System;
    using System.Web;

    public class Bulletin : SelfEvent
    {
        public void CommitEvent(string primarykey)
        {
        }

        public void RefuseEvent(string primarykey)
        {
            HttpContext.Current.Response.Write("hello good");
        }
    }
}

