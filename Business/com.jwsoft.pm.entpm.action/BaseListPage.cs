namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.common.baseclass;
    using System;
    using System.Web.UI.WebControls;

    public abstract class BaseListPage : PageBase
    {
        protected BaseListPage()
        {
        }

        protected abstract void Data_Bind();
        protected void Grd_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                e.Item.Style.Add("position", "relative");
                e.Item.Style.Add("top", "expression(this.offsetParent.scrollTop)");
            }
            if (e.Item.ItemIndex > -1)
            {
                e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
                e.Item.Cells[1].Text = Convert.ToString((int) (e.Item.ItemIndex + 1));
                e.Item.Attributes["style"] = "cursor:hand";
                e.Item.Attributes["onclick"] = "OnRecord(this);clickRow('0');";
            }
            e.Item.Cells[0].Attributes["style"] = "display:none";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if ((base.Request.QueryString["Type"] != null) && (base.Request.QueryString["Type"] != ""))
                {
                    this.PType = (PageType) Enum.Parse(typeof(PageType), base.Request.QueryString["Type"].ToString());
                }
                else
                {
                    this.PType = PageType.Borrow;
                }
                if ((base.Request.QueryString["NodeId"] != null) && (base.Request.QueryString["NodeId"].ToString() != ""))
                {
                    this.NodeId = base.Request.QueryString["NodeId"];
                }
                else
                {
                    this.NodeId = "1";
                }
                if (base.Request.QueryString["DatumId"] != null)
                {
                    this.DatumId = base.Request.QueryString["DatumId"].ToString();
                }
                else
                {
                    this.DatumId = "1";
                }
            }
            base.OnLoad(e);
        }

        protected void PageControl_PageIndexChange(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        public string BorrowId
        {
            get
            {
                return this.ViewState["BorrowId"].ToString();
            }
            set
            {
                this.ViewState["BorrowId"] = value;
            }
        }

        public Browse BorrowType
        {
            get
            {
                return (Browse) this.ViewState["BorrowType"];
            }
            set
            {
                this.ViewState["BorrowType"] = value;
            }
        }

        public Browse BrowseType
        {
            get
            {
                return (Browse) this.ViewState["BorrowType"];
            }
            set
            {
                this.ViewState["BorrowType"] = value;
            }
        }

        public string DatumId
        {
            get
            {
                return this.ViewState["DatumId"].ToString();
            }
            set
            {
                this.ViewState["DatumId"] = value;
            }
        }

        public string NodeId
        {
            get
            {
                return this.ViewState["NodeId"].ToString();
            }
            set
            {
                this.ViewState["NodeId"] = value;
            }
        }

        public PageType PType
        {
            get
            {
                return (PageType) this.ViewState["PType"];
            }
            set
            {
                this.ViewState["PType"] = value;
            }
        }

        public string strWhere
        {
            get
            {
                return this.ViewState["strWhere"].ToString();
            }
            set
            {
                this.ViewState["strWhere"] = value;
            }
        }

        public string UserId
        {
            get
            {
                if ((this.Session["yhdm"] != null) && (this.Session["yhdm"].ToString() != ""))
                {
                    return this.Session["yhdm"].ToString();
                }
                return "0";
            }
        }
    }
}

