using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InputReceiptsItem : BasePage, IRequiresSessionState
	{
		protected int MainID;
		protected int pageSize = 10;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["MainId"] == null)
				{
					return;
				}
				this.ViewState["MAINID"] = base.Request.Params["MainId"].ToString();
				this.MainID = int.Parse(this.ViewState["MAINID"].ToString());
				if (base.Request.Params["Type"] != null && base.Request.Params["Type"].ToString() == "List")
				{
					this.dgItemList.Columns[3].Visible = false;
					this.dgItemList.Columns[4].Visible = false;
					this.btnNew.Visible = false;
				}
				this.BindData();
			}
			this.MainID = int.Parse(this.ViewState["MAINID"].ToString());
		}
		private void BindData()
		{
			inputReceiptAction inputReceiptAction = new inputReceiptAction();
			this.dgItemList.DataSource = this.GetPageData(inputReceiptAction.GetPlanItemInfos(this.MainID));
			this.dgItemList.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgItemList.CancelCommand += new DataGridCommandEventHandler(this.dgItemList_CancelCommand);
			this.dgItemList.EditCommand += new DataGridCommandEventHandler(this.dgItemList_EditCommand);
			this.dgItemList.UpdateCommand += new DataGridCommandEventHandler(this.dgItemList_UpdateCommand);
			this.dgItemList.DeleteCommand += new DataGridCommandEventHandler(this.dgItemList_DeleteCommand);
			this.dgItemList.ItemDataBound += new DataGridItemEventHandler(this.dgItemList_ItemDataBound);
		}
		private void dgItemList_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.dgItemList.EditItemIndex = -1;
			this.btnNew.Enabled = true;
			this.BindData();
		}
		private void dgItemList_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			int itemId = int.Parse(this.dgItemList.DataKeys[e.Item.ItemIndex].ToString());
			if (inputReceiptAction.DelItemInfo(itemId))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.btnNew.Enabled = true;
			this.BindData();
		}
		private void dgItemList_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.btnNew.Enabled = false;
			this.dgItemList.EditItemIndex = e.Item.ItemIndex;
			this.BindData();
		}
		private inputReceiptItemCollectin GetPageData(inputReceiptItemCollectin objShouSchs)
		{
			inputReceiptItemCollectin inputReceiptItemCollectin = new inputReceiptItemCollectin();
			if (objShouSchs.Count > this.pageSize * (objShouSchs.Count / this.pageSize))
			{
				this.pc.PageCount = objShouSchs.Count / this.pageSize + 1;
			}
			else
			{
				this.pc.PageCount = objShouSchs.Count / this.pageSize;
			}
			if (objShouSchs.Count > this.pageSize * (this.pc.CurrentPageIndex - 1))
			{
				int num = this.pageSize * this.pc.CurrentPageIndex;
				if (objShouSchs.Count < this.pageSize * this.pc.CurrentPageIndex)
				{
					num = objShouSchs.Count;
				}
				for (int i = this.pageSize * (this.pc.CurrentPageIndex - 1); i < num; i++)
				{
					inputReceiptItemCollectin.Add(objShouSchs[i]);
				}
			}
			else
			{
				inputReceiptItemCollectin = objShouSchs;
			}
			return inputReceiptItemCollectin;
		}
		private void dgItemList_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			if (inputReceiptAction.UpdateItemInfo(new inputReceiptItem
			{
				ChildMainID = int.Parse(this.dgItemList.DataKeys[e.Item.ItemIndex].ToString()),
				DepartmentName = ((TextBox)e.Item.FindControl("txtDepartmentName")).Text,
				DepartCode = ((HtmlInputHidden)e.Item.FindControl("hidDepartCode")).Value,
				DevotionMoney = decimal.Parse(((TextBox)e.Item.FindControl("txtDevotionMoney")).Text),
				IncomeMoney = decimal.Parse(((TextBox)e.Item.FindControl("txtIncomeMoney")).Text)
			}))
			{
				this.dgItemList.EditItemIndex = -1;
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.btnNew.Enabled = true;
			this.BindData();
		}
		protected void btnNew_Click(object sender, EventArgs e)
		{
			inputReceiptAction inputReceiptAction = new inputReceiptAction();
			if (inputReceiptAction.CreateNewItem(this.MainID) == null)
			{
				this.js.Text = "alert(\"操作失败,请检查数据库操作语句！\");";
			}
			this.BindData();
		}
		private void dgItemList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgItemList.ClientID + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				if (e.Item.ItemIndex == this.dgItemList.EditItemIndex)
				{
					((TextBox)e.Item.FindControl("txtDevotionMoney")).Attributes["onblur"] = "checkNum(this);";
					((TextBox)e.Item.FindControl("txtIncomeMoney")).Attributes["onblur"] = "checkNum(this);";
					((TextBox)e.Item.FindControl("txtDepartmentName")).Attributes["onclick"] = "SelDepartment(this,'" + ((HtmlInputHidden)e.Item.FindControl("hidDepartCode")).ClientID + "');";
				}
			}
		}
		protected void pc_PageIndexChange(object sender, EventArgs e)
		{
			this.BindData();
		}
	}


