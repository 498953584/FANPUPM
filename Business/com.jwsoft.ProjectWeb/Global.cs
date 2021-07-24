namespace com.jwsoft.ProjectWeb
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Web;

    public class Global : HttpApplication
    {
        private IContainer components;

        public Global()
        {
            this.InitializeComponent();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            base.Application["num"] = 0;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            DataTable table = (DataTable) base.Application["UserCodeCollection"];
            string str = base.Session["yhdm"].ToString();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["userCode"].ToString() == str)
                {
                    table.Rows.Remove(table.Rows[i]);
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
    }
}

