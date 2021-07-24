namespace com.jwsoft.pm.ppm.web
{
    using com.jwsoft.pm.web;
    using System;

    public class PPageBase : PageBase
    {
        public Guid ProjectCode
        {
            get
            {
                return (Guid) this.Session["PROJECTCODE"];
            }
        }

        public Guid ProjectName
        {
            get
            {
                return (Guid) this.Session["PROJECTNAME"];
            }
        }
    }
}

