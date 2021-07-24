using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProgressImplementEvaluateAll : NBasePage, IRequiresSessionState
	{
		protected int pageSize = 10;
		protected string MainId;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["mainId"] == null)
				{
					return;
				}
				this.MainId = base.Request.Params["mainId"].ToString();
				this.ViewState["MAINID"] = this.MainId;
				this.BindData();
			}
			this.MainId = this.ViewState["MAINID"].ToString();
		}
		private ProgressEvaluateCollection GetPageData(ProgressEvaluateCollection objShouSchs)
		{
			ProgressEvaluateCollection progressEvaluateCollection = new ProgressEvaluateCollection();
			if (objShouSchs.Count > this.pageSize * (progressEvaluateCollection.Count / this.pageSize))
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
					progressEvaluateCollection.Add(objShouSchs[i]);
				}
			}
			else
			{
				progressEvaluateCollection = objShouSchs;
			}
			return progressEvaluateCollection;
		}
		private void BindData()
		{
			ProgressImplementAction progressImplementAction = new ProgressImplementAction();
			this.dglist.DataSource = this.GetPageData(progressImplementAction.GetEvaluateInfos(this.MainId));
			this.dglist.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dglist.CancelCommand += new DataGridCommandEventHandler(this.dglist_CancelCommand);
			this.dglist.EditCommand += new DataGridCommandEventHandler(this.dglist_EditCommand);
			this.dglist.UpdateCommand += new DataGridCommandEventHandler(this.dglist_UpdateCommand);
			this.dglist.DeleteCommand += new DataGridCommandEventHandler(this.dglist_DeleteCommand);
			this.dglist.ItemDataBound += new DataGridItemEventHandler(this.dglist_ItemDataBound);
		}
		protected void pc_PageIndexChange(object sender, EventArgs e)
		{
			this.BindData();
		}
		private void dglist_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.dglist.ClientID + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				if (e.Item.Cells[2].Text.Length > 10)
				{
					string text = e.Item.Cells[2].Text;
					e.Item.Cells[2].Attributes["Title"] = text;
					e.Item.Cells[2].Text = text.Substring(0, 10) + "...";
				}
			}
		}
		private void dglist_EditCommand(object source, DataGridCommandEventArgs e)
		{
			UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
			if (currentUserInfo == null)
			{
				this.js.Text = "alert('用户身份过期，请重新登录！');window.close();";
				return;
			}
			if (e.Item.Cells[0].Text.Trim() != currentUserInfo.UserName)
			{
				this.js.Text = "alert(\"没有操作权限！\");";
				return;
			}
			this.dglist.EditItemIndex = e.Item.ItemIndex;
			this.BindData();
		}
		private void dglist_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
			if (currentUserInfo == null)
			{
				this.js.Text = "alert('用户身份过期，请重新登录！');window.close();";
				return;
			}
			if (e.Item.Cells[0].Text.Trim() != currentUserInfo.UserName)
			{
				this.js.Text = "alert(\"没有操作权限！\");";
				return;
			}
			if (ProgressImplementAction.DelEvaluation(this.dglist.DataKeys[e.Item.ItemIndex].ToString()))
			{
				this.js.Text = "alert(\"操作成功！\");";
				this.dglist.EditItemIndex = -1;
				this.BindData();
				return;
			}
			this.js.Text = "alert(\"操作失败！\");";
		}
		private void dglist_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.dglist.EditItemIndex = -1;
			this.BindData();
		}
		private void dglist_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			ProgressEvaluateInfo progressEvaluateInfo = new ProgressEvaluateInfo();
			progressEvaluateInfo.Appraise = ((TextBox)e.Item.Cells[2].FindControl("Appraise")).Text.Trim();
			progressEvaluateInfo.AppraisePeople = e.Item.Cells[0].Text.Trim();
			progressEvaluateInfo.AppraiseTime = DateTime.Parse(((TextBox)e.Item.Cells[1].FindControl("AppriaseTime")).Text.Trim());
			progressEvaluateInfo.MainID = this.dglist.DataKeys[e.Item.ItemIndex].ToString();
			UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
			if (currentUserInfo == null)
			{
				this.js.Text = "alert('用户身份过期，请重新登录！');window.close();";
				return;
			}
			if (e.Item.Cells[0].Text.Trim() != currentUserInfo.UserName)
			{
				this.js.Text = "alert(\"没有操作权限！\");";
				return;
			}
			if (ProgressImplementAction.EditEvaluation(progressEvaluateInfo))
			{
				this.js.Text = "alert(\"操作成功！\");";
				this.dglist.EditItemIndex = -1;
				this.BindData();
				return;
			}
			this.js.Text = "alert(\"操作失败！\");";
		}
	}


