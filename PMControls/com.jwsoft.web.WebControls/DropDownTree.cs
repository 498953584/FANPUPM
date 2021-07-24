namespace com.jwsoft.web.WebControls
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ValidationProperty("SelectedValue"), ToolboxData("<{0}:DropDownTree runat=server></{0}:DropDownTree>"), ParseChildren(true), PersistChildren(false), DefaultProperty("Items")]
    public class DropDownTree : WebControl, IPostBackDataHandler
    {
        private DropDownTreeItemCollection _items;
        private DropDownTreeItemStyleCollection _itemStyles;
        protected static readonly object EventSelectedIndexChanged = new object();

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

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            if (this.AutoPostBack)
            {
                writer.AddAttribute("language", "javascript");
                writer.AddAttribute("onchange", this.Page.GetPostBackClientEvent(this, string.Empty));
            }
        }

        public bool HasChild(DataTable table, DataRow row, string idfield, string superidfiled, bool isStr)
        {
            DataRow[] rowArray;
            if (isStr)
            {
                rowArray = table.Select(string.Format("{0}='{1}'", superidfiled, row[idfield].ToString()));
            }
            else
            {
                rowArray = table.Select(string.Format("{0}={1}", superidfiled, row[idfield].ToString()));
            }
            if (rowArray.Length == 0)
            {
                return false;
            }
            return true;
        }

        public virtual void LoadFromTable(DataTable table, bool isStr, string idField, string parentIDField, string txtfiled, string valuefiled)
        {
            try
            {
                this.Items.Clear();
                foreach (DataRow row in table.Select(string.Format("{0} =0", parentIDField)))
                {
                    DropDownTreeItem item = new DropDownTreeItem();
                    item.LoadFromDataRow(row, txtfiled, valuefiled);
                    this.Items.Add(item);
                    this.LoadItemsFromTable(item, row[idField], table, isStr, idField, parentIDField, txtfiled, valuefiled);
                }
            }
            catch
            {
                throw new ArgumentException("Table or filed name error!", table.TableName);
            }
        }

        protected virtual void LoadItemsFromTable(DropDownTreeItem item, object idValue, DataTable table, bool isStr, string idField, string parentIDField, string txtfiled, string valuefiled)
        {
            string str;
            if (isStr)
            {
                str = "{0} = '{1}'";
            }
            else
            {
                str = "{0} = {1}";
            }
            try
            {
                foreach (DataRow row in table.Select(string.Format(str, parentIDField, idValue)))
                {
                    DropDownTreeItem item2 = new DropDownTreeItem();
                    item2.LoadFromDataRow(row, txtfiled, valuefiled);
                    item.Items.Add(item2);
                    this.LoadItemsFromTable(item2, row[idField], table, isStr, idField, parentIDField, txtfiled, valuefiled);
                }
            }
            catch
            {
                throw new ArgumentException("Table or filed name error!", table.TableName);
            }
        }

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string selectedValue = this.SelectedValue;
            string str2 = postCollection[this.UniqueID];
            if ((selectedValue == null) || !selectedValue.Equals(str2))
            {
                this.SelectedValue = str2;
                return true;
            }
            return false;
        }

        protected override void LoadViewState(object state)
        {
            if (state != null)
            {
                object[] objArray = (object[]) state;
                if (objArray[0] != null)
                {
                    base.LoadViewState(objArray[0]);
                }
                if (objArray[1] != null)
                {
                    this.ItemStyles.LoadViewState(objArray[1]);
                }
                if (objArray[2] != null)
                {
                    this.Items.LoadViewState(objArray[2]);
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Items.Owner = null;
            this.Items.DropDownTree = this;
            this.Items.Init();
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[EventSelectedIndexChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void RaisePostDataChangedEvent()
        {
            this.OnSelectedIndexChanged(EventArgs.Empty);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page != null)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }
            if (!this.Items.IsEmpty)
            {
                base.Render(writer);
            }
            else if ((base.Site != null) && base.Site.DesignMode)
            {
                writer.WriteLine(this.ID);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            foreach (DropDownTreeItem item in this.Items)
            {
                item.Render(writer);
            }
        }

        protected override object SaveViewState()
        {
            object[] objArray = new object[] { base.SaveViewState(), this.ItemStyles.SaveViewState(), this.Items.SaveViewState() };
            for (int i = 0; i < objArray.Length; i++)
            {
                if (objArray[i] != null)
                {
                    return objArray;
                }
            }
            return null;
        }

        protected override void TrackViewState()
        {
            base.TrackViewState();
            this.ItemStyles.TrackViewState();
            this.Items.TrackViewState();
        }

        [NotifyParentProperty(true), DefaultValue(false)]
        public bool AutoPostBack
        {
            get
            {
                object obj2 = this.ViewState["autoPostBack"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["autoPostBack"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true)]
        public DropDownTreeItemCollection Items
        {
            get
            {
                if (this._items == null)
                {
                    this._items = new DropDownTreeItemCollection();
                    if (base.IsTrackingViewState)
                    {
                        this._items.TrackViewState();
                    }
                }
                return this._items;
            }
        }

        [NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DropDownTreeItemStyleCollection ItemStyles
        {
            get
            {
                if (this._itemStyles == null)
                {
                    this._itemStyles = new DropDownTreeItemStyleCollection();
                    if (base.IsTrackingViewState)
                    {
                        this._itemStyles.TrackViewState();
                    }
                }
                return this._itemStyles;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DropDownTreeItem SelectedItem
        {
            get
            {
                return this.Items.GetItemByValue(this.SelectedValue);
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string SelectedValue
        {
            get
            {
                object obj2 = this.ViewState["selectedValue"];
                return ((obj2 == null) ? "" : ((string) obj2));
            }
            set
            {
                this.ViewState["selectedValue"] = value;
            }
        }

        [DefaultValue(false), NotifyParentProperty(true)]
        public bool SelectLeafOnly
        {
            get
            {
                object obj2 = this.ViewState["selectLeafOnly"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["selectLeafOnly"] = value;
            }
        }

        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public string TabString
        {
            get
            {
                object obj2 = this.ViewState["tabString"];
                return ((obj2 == null) ? "" : ((string) obj2));
            }
            set
            {
                this.ViewState["tabString"] = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Select;
            }
        }
    }
}

