namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Web.UI;

    public class DropDownTreeItemCollection : ViewStatePartCollectionBase
    {
        protected com.jwsoft.web.WebControls.DropDownTree _dropDownTree;
        protected DropDownTreeItem _owner;

        public int Add(DropDownTreeItem item)
        {
            return this.AddItem(item);
        }

        protected override int AddItem(ViewStatePartBase item)
        {
            DropDownTreeItem item2 = (DropDownTreeItem) item;
            item2.DropDownTree = this.DropDownTree;
            item2.Owner = this.Owner;
            item2.Init();
            return base.AddItem(item2);
        }

        public DropDownTreeItem GetItemByValue(string _value)
        {
            if (!this.IsEmpty)
            {
                foreach (DropDownTreeItem item in base.InnerList)
                {
                    if (item.Value == _value)
                    {
                        return item;
                    }
                    DropDownTreeItem itemByValue = item.Items.GetItemByValue(_value);
                    if (itemByValue != null)
                    {
                        return itemByValue;
                    }
                }
            }
            return null;
        }

        public int IndexOf(DropDownTreeItem item)
        {
            return base.InnerList.IndexOf(item);
        }

        public virtual void Init()
        {
            for (int i = 0; i < base.Count; i++)
            {
                this[i].DropDownTree = this.DropDownTree;
                this[i].Owner = this.Owner;
                this[i].Init();
            }
        }

        protected override ViewStatePartBase NewItem()
        {
            return new DropDownTreeItem();
        }

        public virtual void Render(HtmlTextWriter writer)
        {
            if (!this.IsEmpty)
            {
                for (int i = 0; i < base.Count; i++)
                {
                    this[i].Render(writer);
                }
            }
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

        public DropDownTreeItem this[int index]
        {
            get
            {
                if ((index >= base.Count) || (index < 0))
                {
                    return null;
                }
                return (DropDownTreeItem) base.InnerList[index];
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
    }
}

