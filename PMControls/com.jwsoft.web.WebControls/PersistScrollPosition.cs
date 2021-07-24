namespace com.jwsoft.web.WebControls
{
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ToolboxData("<{0}:PersistScrollPosition runat=server></{0}:PersistScrollPosition>")]
    public class PersistScrollPosition : Control, INamingContainer
    {
        private string _controlToPersist = "";
        private HtmlInputHidden _scrollLeft;
        private bool _scrollPersistence = true;
        private HtmlInputHidden _scrollTop;

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            this._scrollTop = new HtmlInputHidden();
            this._scrollLeft = new HtmlInputHidden();
            this._scrollTop.ID = "scrollTop";
            this._scrollLeft.ID = "scrollLeft";
            this.Controls.Add(this._scrollTop);
            this.Controls.Add(this._scrollLeft);
        }

        protected override void OnPreRender(EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("<script language='javascript'>");
            if (this._controlToPersist.Trim() == "")
            {
                builder.Append("function " + this.ID + "_getScrollPosition() {");
                builder.Append("\tdocument.forms[0]." + this.ID + "_scrollTop.value = document.body.scrollTop;");
                builder.Append("\tdocument.forms[0]." + this.ID + "_scrollLeft.value = document.body.scrollLeft;");
                builder.Append("}");
                builder.Append("\twindow.attachEvent(\"onscroll\"," + this.ID + "_getScrollPosition);");
            }
            else
            {
                builder.Append("function " + this.ID + "_getScrollPosition() {");
                builder.Append("\tdocument.forms[0]." + this.ID + "_scrollTop.value = document.getElementById(\"" + this._controlToPersist + "\").scrollTop;");
                builder.Append("\tdocument.forms[0]." + this.ID + "_scrollLeft.value = document.getElementById(\"" + this._controlToPersist + "\").scrollLeft;");
                builder.Append("}");
                builder.Append("\tdocument.getElementById(\"" + this._controlToPersist + "\").attachEvent(\"onscroll\"," + this.ID + "_getScrollPosition);");
            }
            builder.Append("</script>");
            if (!this.Page.IsClientScriptBlockRegistered(this.ID + "_getScroll"))
            {
                this.Page.RegisterStartupScript(this.ID + "_getScroll", builder.ToString());
            }
            if (this.Page.IsPostBack && this._scrollPersistence)
            {
                builder2.Append("<script language='javascript'>");
                if (this._controlToPersist.Trim() == "")
                {
                    builder2.Append("function " + this.ID + "_setScrollPosition() {");
                    builder2.Append("\tdocument.body.scrollTop = " + this._scrollTop.Value + ";");
                    builder2.Append("\tdocument.body.scrollLeft= " + this._scrollLeft.Value + ";");
                    builder2.Append("}");
                    builder2.Append("\twindow.attachEvent(\"onload\"," + this.ID + "_setScrollPosition);");
                }
                else
                {
                    builder2.Append("function " + this.ID + "_setScrollPosition() {");
                    builder2.Append("\tdocument.getElementById(\"" + this._controlToPersist + "\").scrollTop = " + this._scrollTop.Value + ";");
                    builder2.Append("\tdocument.getElementById(\"" + this._controlToPersist + "\").scrollLeft= " + this._scrollLeft.Value + ";");
                    builder2.Append("}");
                    builder2.Append("\twindow.attachEvent(\"onload\"," + this.ID + "_setScrollPosition);");
                }
                builder2.Append("</script>");
                this.Page.RegisterStartupScript(this.ID + "_setScroll", builder2.ToString());
            }
            if (!this.Page.IsPostBack)
            {
                builder2.Append("<script language='javascript'>");
                builder2.Append("function " + this.ID + "_setScrollPosition() {");
                builder2.Append("\tdocument.forms[0]." + this.ID + "_scrollTop.value = 0;");
                builder2.Append("\tdocument.forms[0]." + this.ID + "_scrollLeft.value = 0;");
                builder2.Append("}");
                builder2.Append("\twindow.attachEvent(\"onload\"," + this.ID + "_setScrollPosition);");
                builder2.Append("</script>");
                this.Page.RegisterStartupScript(this.ID + "_setScroll", builder2.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this._scrollTop.RenderControl(writer);
            this._scrollLeft.RenderControl(writer);
        }

        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        public string ControlToPersist
        {
            get
            {
                return this._controlToPersist;
            }
            set
            {
                this._controlToPersist = value;
            }
        }

        public bool ScrollPersistence
        {
            get
            {
                return this._scrollPersistence;
            }
            set
            {
                this._scrollPersistence = value;
            }
        }
    }
}

