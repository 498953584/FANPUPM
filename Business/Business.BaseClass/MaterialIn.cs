namespace Business.BaseClass
{
    using System;
    using System.Web;

    public class MaterialIn : SelfEvent
    {
        public void CommitEvent(string primarykey)
        {
            HttpContext.Current.Response.Write("入库单通过事件");
        }

        public void RefuseEvent(string primarykey)
        {
            HttpContext.Current.Response.Write("入库单驳回事件");
        }
    }
}

