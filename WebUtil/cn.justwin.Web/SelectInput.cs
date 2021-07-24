namespace cn.justwin.Web
{
    using System;
    using System.ComponentModel;
    using System.Security.Permissions;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:SelectInput runat=\"server\" />"), AspNetHostingPermission(SecurityAction.Demand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public class SelectInput : TextBox
    {
        protected override void OnInit(EventArgs e)
        {
            string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "cn.justwin.Web.CSS.style.css");
            HtmlLink child = new HtmlLink {
                Href = webResourceUrl
            };
            child.Attributes.Add("rel", "stylesheet");
            child.Attributes.Add("type", "text/css");
            this.Page.Header.Controls.Add(child);
            base.OnInit(e);
            this.Page.ClientScript.RegisterClientScriptResource(base.GetType(), "cn.justwin.Web.Js.SelectInput.js");
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("border: 1px solid #808080; ");
            builder.AppendFormat("width: {0}; ", this.Width);
            builder.AppendFormat("height: {0}", this.Height);
            writer.Write(string.Format("<div class='select_div' style='{0}'>", builder));
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("float: left; ");
            builder2.Append("border: 0px; ");
            builder2.AppendFormat("width: {0}; ", (this.Width.Value - 19.0) + "px");
            builder2.AppendFormat("height: {0}; ", (this.Height.Value - 2.0) + "px");
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("<input type=\"text\" ");
            builder3.AppendFormat("ID=\"{0}\" ", this.ID);
            builder3.AppendFormat("val=\"{0}\" ", this.Val);
            builder3.AppendFormat("value=\"{0}\" ", this.Text);
            builder3.AppendFormat("style=\"{0}\" ", builder2.ToString());
            builder3.Append(" ></input>");
            writer.Write(builder3.ToString());
            string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "cn.justwin.Web.Img.Popup_icon.bmp");
            StringBuilder builder4 = new StringBuilder();
            builder4.AppendFormat("background: url({0});", webResourceUrl);
            builder4.Append("overflow: hidden; ");
            builder4.AppendFormat("height: {0}; ", this.Height);
            builder4.Append("border: 0px; ");
            builder4.Append("margin: 0px; ");
            writer.Write(string.Format("<input type='button' class='select_btn' style='{0}' />", builder4));
            writer.Write("</div>");
        }

        [Description("选择的值."), DefaultValue(""), Bindable(true)]
        public virtual string Val
        {
            get
            {
                string str = (string) this.ViewState["Val"];
                return (str ?? string.Empty);
            }
            set
            {
                this.ViewState["Val"] = value;
            }
        }
    }
}

