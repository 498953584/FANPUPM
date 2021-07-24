namespace UserControl
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:Dropdown runat=server></{0}:Dropdown>"), DefaultProperty("Items")]
    public class EditDropDownList : WebControl
    {
        private static readonly object EventSelectedIndexChanged = new object();
        private DropDownList m_oDDList;
        private TextBox m_oTxtList;

        public event EventHandler SelectedIndexChanged
        {
            add
            {
                base.Events.AddHandler(EventSelectedIndexChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventSelectedIndexChanged, value);
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            this.m_oDDList = new DropDownList();
            this.m_oDDList.EnableViewState = true;
            this.m_oDDList.Style.Add("width", "127");
            this.m_oDDList.Style.Add("CLIP", "rect(3px 130px 20px 110px)");
            this.m_oDDList.Style.Add("POSITION", "absolute");
            this.m_oDDList.SelectedIndexChanged += new EventHandler(this.m_oDDList_SelectedIndexChanged);
            this.Controls.Add(this.m_oDDList);
            this.m_oTxtList = new TextBox();
            this.m_oTxtList.AutoPostBack = false;
            this.m_oTxtList.EnableViewState = true;
            this.m_oTxtList.Wrap = true;
            this.m_oTxtList.Style.Add("width", "127");
            this.Controls.Add(this.m_oTxtList);
        }

        private void m_oDDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = this.m_oDDList.SelectedValue.ToString();
            this.OnSelectedIndexChanged(e);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            this.EnsureChildControls();
            this.m_oDDList.DataBind();
            this.m_oTxtList.DataBind();
            base.OnDataBinding(e);
        }

        protected virtual void OnSelectedIndexChanged(EventArgs args)
        {
            EventHandler handler = (EventHandler) base.Events[EventSelectedIndexChanged];
            if (handler != null)
            {
                handler(this.m_oDDList, args);
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.WriteLine("<script language=\"JavaScript\">");
            output.WriteLine("function SelectDroup(txtName,DropuName){");
            output.WriteLine("txtName.value=DropuName.options(DropuName.selectedIndex).text");
            output.WriteLine("}");
            output.WriteLine("</script>");
            this.m_oDDList.Attributes.Add("onchange", "javascript:SelectDroup(" + this.m_oTxtList.ClientID + ",this);");
            this.m_oDDList.RenderControl(output);
            this.m_oTxtList.RenderControl(output);
        }

        [Category("Behavior")]
        public bool AutoPostBack
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oDDList.AutoPostBack;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oDDList.AutoPostBack = value;
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

        [Category("Data")]
        public string DataMember
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oDDList.DataMember;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oDDList.DataMember = value;
            }
        }

        [Category("Data"), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oDDList.DataSource;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oDDList.DataSource = value;
            }
        }

        [Category("Data")]
        public string DataTextField
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oDDList.DataTextField;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oDDList.DataTextField = value;
            }
        }

        [Category("Data")]
        public string DataTextFormatString
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oDDList.DataTextFormatString;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oDDList.DataTextFormatString = value;
            }
        }

        [Category("Data")]
        public string DataValueField
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oDDList.DataValueField;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oDDList.DataValueField = value;
            }
        }

        [Browsable(false)]
        public ListItemCollection Items
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oDDList.Items;
            }
        }

        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public string Text
        {
            get
            {
                this.EnsureChildControls();
                if (this.m_oTxtList != null)
                {
                    return this.m_oTxtList.Text;
                }
                return null;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oTxtList.Text = value;
            }
        }

        [Bindable(true), Browsable(false)]
        public string Value
        {
            get
            {
                this.EnsureChildControls();
                return this.m_oTxtList.Text;
            }
            set
            {
                this.EnsureChildControls();
                this.m_oTxtList.Text = value;
            }
        }
    }
}

