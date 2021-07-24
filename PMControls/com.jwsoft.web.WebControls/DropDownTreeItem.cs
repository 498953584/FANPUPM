namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Web.UI;

    public class DropDownTreeItem : ViewStatePartBase
    {
        protected com.jwsoft.web.WebControls.DropDownTree _dropDownTree;
        private DropDownTreeItemCollection _items;
        protected DropDownTreeItem _owner;
        protected DropDownTreeItemStyle _style;
        public const string TextKey = "A";
        public const string ValueKey = "B";

        protected virtual void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.RenderStyle.AddAttributesToRender(writer);
            string str = this.Value;
            if (this.DropDownTree.SelectLeafOnly && !this.Items.IsEmpty)
            {
                str = string.Empty;
            }
            if (str != string.Empty)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Value, str);
            }
            if (this.DropDownTree.SelectedValue == this.Value)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Selected, "true");
            }
        }

        protected virtual string GetPreText()
        {
            DropDownTreeItemCollection items;
            int level = this.Level;
            int index = this.Index;
            if (level == 0)
            {
                items = this.DropDownTree.Items;
            }
            else
            {
                items = this.Owner.Items;
            }
            if (((level != 0) || (items.Count != 1)) || ((items.Count - 1) == 0))
            {
                if (((level == 0) && (index == 0)) && (items.Count > 1))
                {
                    return "┌";
                }
                if (index == (items.Count - 1))
                {
                    return "└";
                }
            }
            return "├";
        }

        protected virtual string GetTabText()
        {
            DropDownTreeItemCollection items;
            if (this.Level == 0)
            {
                return string.Empty;
            }
            DropDownTreeItem owner = this.Owner;
            if (this.Level > 1)
            {
                items = owner.Owner.Items;
            }
            else
            {
                items = this.DropDownTree.Items;
            }
            if (owner.Index == (items.Count - 1))
            {
                return string.Format("{0}{1}", owner.GetTabText(), "&nbsp;&nbsp;" + this.DropDownTree.TabString);
            }
            return string.Format("{0}{1}", owner.GetTabText(), "│" + this.DropDownTree.TabString);
        }

        public virtual void Init()
        {
            this.Items.Owner = this;
            this.Items.DropDownTree = this.DropDownTree;
            this.Items.Init();
        }

        public virtual void LoadFromDataRow(DataRow row, string TextFiled, string ValueFiled)
        {
            DataTable table = row.Table;
            this.Text = row[TextFiled].ToString();
            this.Value = row[ValueFiled].ToString();
        }

        public override void LoadViewState(object state)
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
                    this.Style.LoadViewState(objArray[1]);
                }
                if (objArray[2] != null)
                {
                    this.Items.LoadViewState(objArray[2]);
                }
            }
        }

        public virtual void Render(HtmlTextWriter writer)
        {
            writer.WriteLine();
            this.AddAttributesToRender(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Option);
            this.RenderContent(writer);
            writer.RenderEndTag();
            writer.WriteLine();
            this.Items.Render(writer);
        }

        protected virtual void RenderContent(HtmlTextWriter writer)
        {
            writer.Write(string.Format("{0} {1}", this.GetTabText() + this.GetPreText(), this.Text));
        }

        public override object SaveViewState()
        {
            object[] objArray = new object[] { base.SaveViewState(), this.Style.SaveViewState(), this.Items.SaveViewState() };
            for (int i = 0; i < objArray.Length; i++)
            {
                if (objArray[i] != null)
                {
                    return objArray;
                }
            }
            return null;
        }

        public override void SetDirty()
        {
            base.SetDirty();
            this.Style.SetDirty();
            this.Items.SetDirty();
        }

        public override void TrackViewState()
        {
            base.TrackViewState();
            this.Style.TrackViewState();
            this.Items.TrackViewState();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public com.jwsoft.web.WebControls.DropDownTree DropDownTree
        {
            get
            {
                return this._dropDownTree;
            }
            set
            {
                this._dropDownTree = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int Index
        {
            get
            {
                if (this.Level != 0)
                {
                    return this.Owner.Items.IndexOf(this);
                }
                return this.DropDownTree.Items.IndexOf(this);
            }
        }

        [NotifyParentProperty(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public DropDownTreeItemCollection Items
        {
            get
            {
                if (this._items == null)
                {
                    this._items = new DropDownTreeItemCollection();
                    if (base._isTrackingViewState)
                    {
                        this._items.TrackViewState();
                    }
                }
                return this._items;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Level
        {
            get
            {
                int num = 0;
                DropDownTreeItem owner = this._owner;
                while (owner != null)
                {
                    owner = owner.Owner;
                    num++;
                }
                return num;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DropDownTreeItem Owner
        {
            get
            {
                return this._owner;
            }
            set
            {
                this._owner = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected virtual DropDownTreeItemStyle RenderStyle
        {
            get
            {
                if (!this.DropDownTree.ItemStyles.IsEmpty)
                {
                    DropDownTreeItemStyle style = new DropDownTreeItemStyle();
                    int level = this.Level;
                    if (level < this.DropDownTree.ItemStyles.Count)
                    {
                        style.CopyFrom(this.DropDownTree.ItemStyles[level]);
                    }
                    else
                    {
                        style.CopyFrom(this.DropDownTree.ItemStyles[this.DropDownTree.ItemStyles.Count - 1]);
                    }
                    if (!this.Style.IsEmpty)
                    {
                        style.CopyFrom(this.Style);
                    }
                    return style;
                }
                return this.Style;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), TypeConverter(typeof(ExpandableObjectConverter)), NotifyParentProperty(true), PersistenceMode(PersistenceMode.Attribute)]
        public virtual DropDownTreeItemStyle Style
        {
            get
            {
                if (this._style == null)
                {
                    this._style = new DropDownTreeItemStyle();
                    if (base._isTrackingViewState)
                    {
                        this._style.TrackViewState();
                    }
                }
                return this._style;
            }
        }

        [DefaultValue(""), NotifyParentProperty(true), Bindable(true), Category("Appearance")]
        public string Text
        {
            get
            {
                object obj2 = this.ViewState["A"];
                return ((obj2 == null) ? string.Empty : ((string) obj2));
            }
            set
            {
                this.ViewState["A"] = value;
            }
        }

        [DefaultValue(""), NotifyParentProperty(true), Bindable(true), Category("Appearance")]
        public string Value
        {
            get
            {
                object obj2 = this.ViewState["B"];
                return ((obj2 == null) ? string.Empty : ((string) obj2));
            }
            set
            {
                this.ViewState["B"] = value;
            }
        }
    }
}

