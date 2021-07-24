namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:JavaScriptControl runat=server></{0}:JavaScriptControl>"), DefaultProperty("Text")]
    public class JavaScriptControl : WebControl
    {
        private string lang = "javascript";
        private string text;

        protected override void Render(HtmlTextWriter output)
        {
            output.Write("<script language=\"" + this.lang + "\">\n");
            output.Write(this.Text);
            output.Write("</script>\n");
        }

        public string Lang
        {
            get
            {
                return this.lang;
            }
            set
            {
                this.lang = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

