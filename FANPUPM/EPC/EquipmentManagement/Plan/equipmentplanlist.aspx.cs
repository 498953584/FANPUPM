using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EquipmentPlanManage : NBasePage, IRequiresSessionState
	{
		private EquipmentAction equipmentAction = new EquipmentAction();
		private int iPageSize = 10;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["PrjCode"] != null)
			{
				this.Session["EquipmentPROJECTCODE"] = base.Request.QueryString["PrjCode"];
			}
			if (base.Request.QueryString["PrjName"] != null)
			{
				this.Session["EquipmentPROJECTNAME"] = base.Request.QueryString["PrjName"];
			}
			if (base.Request.QueryString["Type"] != null)
			{
				this.Session["EquipmentOpType"] = base.Request.QueryString["Type"];
			}
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["Type"] == "ShenHe")
				{
					this.Session["EquipmentPlanShenHe"] = "ShenHe";
				}
				if (base.Request.QueryString["Type"] == "Borrow")
				{
					this.editManue.Style.Add("display", "none");
					this.see.Style.Add("display", "");
				}
				if (base.Request.QueryString["Type"] == "Edit")
				{
					this.Session["EquipmentPlanShenHe"] = "Edit";
					this.editManue.Style.Add("display", "");
					this.see.Style.Add("display", "none");
				}
				if (base.Request.QueryString["PrjCode"] != null)
				{
					this.BindDataGrid();
					this.Lbbt.Text = base.Request.QueryString["PrjName"];
					this.Session["EquipmentPlanShenHe"] = "Edit";
					return;
				}
				this.BindDataGrid();
			}
		}
		private void BindDataGrid()
		{
			string text;
			if (base.Request.QueryString["PrjCode"] != null)
			{
				string str = base.Request.QueryString["PrjCode"];
				text = "PRJCODE='" + str + "'";
			}
			else
			{
				text = " (1=1)";
			}
			if (base.Request.QueryString["Type"].ToString() == "ShenHe")
			{
				text += " and  IsAuditing<>1";
			}
			this.grdList.Columns[6].Visible = false;
			this.grdList.DataSource = publicDbOpClass.GetPageData(text, "Ent_Ept_Plan", "PlanID desc ");
			this.grdList.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow gridViewRow in this.grdList.Rows)
			{
				CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
				if (checkBox != null && checkBox.Checked && this.equipmentAction.DelEquipmentPlan(checkBox.ToolTip) != 1)
				{
					this.js.Text = "alert('删除数据出错！');";
				}
			}
			this.BindDataGrid();
		}
		protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.grdList.PageIndex = e.NewPageIndex;
			this.BindDataGrid();
		}
		protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["id"] = this.grdList.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.grdList.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["prjGuid"] = this.grdList.DataKeys[e.Row.RowIndex].Values[2].ToString();
			}
		}
	}


