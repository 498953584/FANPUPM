using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
 public partial class CodeList : BasePage, IRequiresSessionState
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.DG.DataSource = CodingAction.CodeInfoList(base.Request["typeID"].ToString());
            this.DG.DataBind();
        }
        protected override void OnInit(System.EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.DG.ItemDataBound += new DataGridItemEventHandler(this.DG_ItemDataBound);
        }
        private void DG_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        DataRowView dataRowView = (DataRowView)e.Item.DataItem;
                        e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick(this,'",
					this.DG.ClientID,
					"');ClickRow('",
					dataRowView["CodeName"],
					"','",
					dataRowView["CodeID"],
					"');"
				});
                        e.Item.Attributes["ondblclick"] = string.Concat(new object[]
				{
					"dbClickResRow('",
					dataRowView["CodeName"],
					"','",
					dataRowView["CodeID"],
					"');"
				});
                        e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
                        e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
                        return;
                    }
                default:
                    return;
            }
        }
    }