using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ViewSign : BasePage, IRequiresSessionState
	{
		public Guid FileID
		{
			get
			{
				object obj = this.ViewState["FileID"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["FileID"] = value;
			}
		}
		public string Types
		{
			get
			{
				object obj = this.ViewState["Types"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Types"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Types = base.Request["types"];
			if (base.Request.QueryString["fid"] != "")
			{
				this.FileID = new Guid(base.Request["fid"]);
			}
			else
			{
				this.FileID = Guid.Empty;
			}
			this.dgSign_Bind();
		}
		protected void dgSign_Bind()
		{
			this.dgSign.DataSource = DocumentAction.QuerySignRecord(this.FileID, this.Types);
			this.dgSign.DataBind();
		}
		protected void dgSign_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView arg_2D_0 = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				return;
			}
			default:
				return;
			}
		}
	}


