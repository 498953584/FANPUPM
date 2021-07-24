namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:PaginationControl runat=server></{0}:PaginationControl>"), DefaultProperty("Name"), DefaultEvent("PageIndexChange")]
    public class PaginationControl : WebControl, INamingContainer
    {
        private LinkButton _endButton;
        private const string _endButtonId = "endPage";
        private LinkButton _firstButton;
        private const string _firstButtonId = "firstPage";
        private const string _goToPage = "ToPage";
        private LinkButton _nextButton;
        private const string _nextButtonId = "nextPage";
        private LinkButton _prevButton;
        private const string _prevButtonId = "prevPage";
        private static DropDownList ddl_GotoPage;
        private static readonly object EventPageIndexChange = new object();
        private static bool HiddenIden = false;
        private static Label l_CurrentPageIndex;
        private static Label l_PageCount;
        private Label l_Remark1;
        private Label l_Remark2;
        private Label l_Remark3;

        [Category("Action")]
        public event EventHandler PageIndexChange
        {
            add
            {
                base.Events.AddHandler(EventPageIndexChange, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPageIndexChange, value);
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            l_CurrentPageIndex = new Label();
            l_CurrentPageIndex.ID = "CurrentPage";
            l_CurrentPageIndex.Text = "1";
            l_CurrentPageIndex.CssClass = this.TextCssClass;
            l_PageCount = new Label();
            l_PageCount.ID = "PageSize";
            l_PageCount.Text = "1";
            l_PageCount.Text = this.TextCssClass;
            this.l_Remark1 = new Label();
            this.l_Remark1.Text = "当前第 ";
            this.l_Remark1.CssClass = this.TextCssClass;
            this.l_Remark2 = new Label();
            this.l_Remark2.Text = " 页 共 ";
            this.l_Remark2.CssClass = this.TextCssClass;
            this.l_Remark3 = new Label();
            this.l_Remark3.Text = " 页";
            this.l_Remark3.CssClass = this.TextCssClass;
            this._firstButton = new LinkButton();
            this._firstButton.ID = "firstPage";
            this._firstButton.Text = "第一页";
            this._firstButton.CommandName = "FirstPage";
            this._firstButton.CssClass = this.ButtonCssClass;
            this._endButton = new LinkButton();
            this._endButton.ID = "endPage";
            this._endButton.Text = "最后一页";
            this._endButton.CommandName = "EndPage";
            this._endButton.CssClass = this.ButtonCssClass;
            this._prevButton = new LinkButton();
            this._prevButton.ID = "prevPage";
            this._prevButton.Text = "上一页";
            this._prevButton.CommandName = "PrevPage";
            this._prevButton.CssClass = this.ButtonCssClass;
            this._nextButton = new LinkButton();
            this._nextButton.ID = "nextPage";
            this._nextButton.Text = "下一页";
            this._nextButton.CommandName = "NextPage";
            this._nextButton.CssClass = this.ButtonCssClass;
            ddl_GotoPage = new DropDownList();
            ddl_GotoPage.ID = "ToPage";
            ddl_GotoPage.AutoPostBack = true;
            ddl_GotoPage.SelectedIndexChanged += new EventHandler(this.OnGotoPage);
            this.Controls.Add(l_PageCount);
            this.Controls.Add(l_CurrentPageIndex);
            this.Controls.Add(this.l_Remark1);
            this.Controls.Add(this.l_Remark2);
            this.Controls.Add(this.l_Remark3);
            this.Controls.Add(this._firstButton);
            this.Controls.Add(this._endButton);
            this.Controls.Add(this._prevButton);
            this.Controls.Add(this._nextButton);
            this.Controls.Add(ddl_GotoPage);
        }

        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            bool flag = false;
            if (source is LinkButton)
            {
                string iD = ((LinkButton) source).ID;
                if (iD != null)
                {
                    iD = string.IsInterned(iD);
                    if (iD == "firstPage")
                    {
                        this.CurrentPageIndex = 1;
                    }
                    else if (iD == "endPage")
                    {
                        this.CurrentPageIndex = this.PageCount;
                    }
                    else if (iD == "prevPage")
                    {
                        if (this.CurrentPageIndex > 1)
                        {
                            this.CurrentPageIndex--;
                        }
                    }
                    else if ((iD == "nextPage") && (this.CurrentPageIndex < this.PageCount))
                    {
                        this.CurrentPageIndex++;
                    }
                }
                flag = true;
            }
            else if (source is DropDownList)
            {
                this.CurrentPageIndex = ddl_GotoPage.SelectedIndex + 1;
                flag = true;
            }
            this.OnChangeIndex(EventArgs.Empty);
            return flag;
        }

        protected virtual void OnChangeIndex(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EventPageIndexChange];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnGotoPage(object sender, EventArgs e)
        {
            if (e != null)
            {
                this.OnBubbleEvent(sender, e);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0", false);
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0", false);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "0", false);
            this.l_Remark1.RenderControl(writer);
            l_CurrentPageIndex.RenderControl(writer);
            this.l_Remark2.RenderControl(writer);
            l_PageCount.RenderControl(writer);
            this.l_Remark3.RenderControl(writer);
            writer.Write(" ");
            this._firstButton.RenderControl(writer);
            writer.Write(" | ");
            this._prevButton.RenderControl(writer);
            writer.Write(" | ");
            this._nextButton.RenderControl(writer);
            writer.Write(" | ");
            this._endButton.RenderControl(writer);
            writer.Write(" | ");
            writer.Write("转到第 ");
            ddl_GotoPage.RenderControl(writer);
            writer.Write("&nbsp;");
            writer.Write("页");
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        [Description("class of button"), Bindable(false), DefaultValue(""), Category("Appearance")]
        public string ButtonCssClass
        {
            get
            {
                this.EnsureChildControls();
                object obj2 = this.ViewState["ButtonCssClass"];
                return ((obj2 != null) ? obj2.ToString() : string.Empty);
            }
            set
            {
                this.EnsureChildControls();
                this.ViewState["ButtonCssClass"] = value;
            }
        }

        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        [Description("The current PageIndex "), DefaultValue("1"), Bindable(true), Category("Appearance")]
        public int CurrentPageIndex
        {
            get
            {
                this.EnsureChildControls();
                if (l_CurrentPageIndex.Text == "")
                {
                    return 0;
                }
                return int.Parse(l_CurrentPageIndex.Text.Trim());
            }
            set
            {
                this.EnsureChildControls();
                if (value > 0)
                {
                    l_CurrentPageIndex.Text = value.ToString();
                }
                else
                {
                    l_CurrentPageIndex.Text = "0";
                }
            }
        }

        [Description("The DropDrowList for the Goto page"), Bindable(true), Category("Appearance"), DefaultValue("")]
        public DropDownList Ddl_PageIndex
        {
            get
            {
                this.EnsureChildControls();
                return ddl_GotoPage;
            }
        }

        [Category("Appearance"), DefaultValue("true"), Bindable(true), Description("set remark message is display or hidden")]
        public bool DescriptVisible
        {
            get
            {
                this.EnsureChildControls();
                return HiddenIden;
            }
            set
            {
                this.EnsureChildControls();
                HiddenIden = value;
                this.l_Remark1.Visible = value;
                this.l_Remark2.Visible = value;
                this.l_Remark3.Visible = value;
                l_PageCount.Visible = value;
                l_CurrentPageIndex.Visible = value;
            }
        }

        [Category("Appearance"), Bindable(true), Description("The name of EndButton"), DefaultValue("")]
        public string EndButtonName
        {
            get
            {
                this.EnsureChildControls();
                return this._endButton.Text;
            }
            set
            {
                this.EnsureChildControls();
                this._endButton.Text = value;
            }
        }

        [Bindable(true), Description("The name of firstButton"), Category("Appearance"), DefaultValue("")]
        public string FirstButtonName
        {
            get
            {
                this.EnsureChildControls();
                return this._firstButton.Text;
            }
            set
            {
                this.EnsureChildControls();
                this._firstButton.Text = value;
            }
        }

        [Category("Appearance"), DefaultValue(""), Description("The name of NextButton"), Bindable(true)]
        public string NextButtonName
        {
            get
            {
                this.EnsureChildControls();
                return this._nextButton.Text;
            }
            set
            {
                this.EnsureChildControls();
                this._nextButton.Text = value;
            }
        }

        [Description("页的总数"), DefaultValue("1"), Bindable(true), Category("Appearance")]
        public int PageCount
        {
            get
            {
                this.EnsureChildControls();
                return ((l_PageCount.Text == "") ? 0 : int.Parse(l_PageCount.Text));
            }
            set
            {
                this.EnsureChildControls();
                int pageCount = this.PageCount;
                l_PageCount.Text = value.ToString();
                ddl_GotoPage.Items.Clear();
                if (l_PageCount.Text.Trim() != "")
                {
                    for (int i = 1; i <= this.PageCount; i++)
                    {
                        string text = i.ToString();
                        ddl_GotoPage.Items.Add(new ListItem(text, i.ToString()));
                    }
                }
                if (this.PageCount >= pageCount)
                {
                    ddl_GotoPage.SelectedValue = this.CurrentPageIndex.ToString();
                }
                else if ((this.PageCount < pageCount) && (this.CurrentPageIndex != 1))
                {
                    ddl_GotoPage.SelectedIndex = ddl_GotoPage.Items.Count - 1;
                    this.CurrentPageIndex = this.PageCount;
                }
            }
        }

        [Bindable(true), Description("The name of PrevButton"), Category("Appearance"), DefaultValue("")]
        public string PrevButtonName
        {
            get
            {
                this.EnsureChildControls();
                return this._prevButton.Text;
            }
            set
            {
                this.EnsureChildControls();
                this._prevButton.Text = value;
            }
        }

        [Description("class of text"), Category("Appearance"), Bindable(false), DefaultValue("")]
        public string TextCssClass
        {
            get
            {
                this.EnsureChildControls();
                object obj2 = this.ViewState["TextCssClass"];
                return ((obj2 != null) ? obj2.ToString() : string.Empty);
            }
            set
            {
                this.EnsureChildControls();
                this.ViewState["TextCssClass"] = value;
            }
        }
    }
}

