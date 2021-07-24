namespace com.jwsoft.web.WebControls
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Items"), ToolboxData("<{0}:WinPop runat=server></{0}:WinPop>")]
    public class WinPop : WebControl
    {
        private WinPopItemCollection _items;

        public virtual void LoadFromTable(DataTable table)
        {
            try
            {
                this.Items.Clear();
                foreach (DataRow row in table.Rows)
                {
                    WinPopItem item = new WinPopItem();
                    item.LoadFromDataRow(row);
                    this.Items.Add(item);
                }
            }
            catch
            {
                throw new ArgumentException("Table or filed name error!", table.TableName);
            }
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
                    this.Items.LoadViewState(objArray[1]);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if ((this.Enabled && !this.Items.IsEmpty) && !this.Page.IsStartupScriptRegistered(this.ClientID))
            {
                builder.Append("<script language=\"JavaScript\" >");
                builder.Append("\n\r");
                for (int i = 0; i < this.Items.Count; i++)
                {
                    builder.Append(this.Items[i].GetPreRenderJScript());
                }
                builder.Append("</script>");
                this.Page.RegisterStartupScript(this.ClientID, builder.ToString());
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((base.Site != null) && base.Site.DesignMode)
            {
                writer.WriteLine(this.ID);
            }
        }

        protected override object SaveViewState()
        {
            object[] objArray = new object[] { base.SaveViewState(), this.Items.SaveViewState() };
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
            this.Items.TrackViewState();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), NotifyParentProperty(true)]
        public WinPopItemCollection Items
        {
            get
            {
                if (this._items == null)
                {
                    this._items = new WinPopItemCollection();
                    if (base.IsTrackingViewState)
                    {
                        this._items.TrackViewState();
                    }
                }
                return this._items;
            }
        }
    }
}

