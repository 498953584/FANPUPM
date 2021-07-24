using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.datas;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TypeList : PageBase, IRequiresSessionState
	{

		protected string QProjectID
		{
			get
			{
				if (base.Request.QueryString["PrjCode"] != null)
				{
					return base.Request.QueryString["PrjCode"];
				}
				return "";
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.QProjectID == "-1")
			{
				this.BtnAdd.Enabled = false;
			}
			this.hdnPmId.Value = this.QProjectID;
			this.BtnAdd.Attributes["onclick"] = "openClassEdit(1)";
			this.BtnEdit.Attributes["onclick"] = "openClassEdit(2)";
			this.BtnLook.Attributes["onclick"] = "openClassEdit(3)";
			this.BtnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
			this.myDataBind();
		}
		private void myDataBind()
		{
			this.dgClass.DataSource = WokLog.GetList(string.Format(" ProjectId='{0}'", this.QProjectID));
			this.dgClass.DataBind();
		}
		private void myDataQuery()
		{
			this.dgClass.DataSource = WokLog.GetQuery(this.txtCode.Text, this.txtPart.Text, this.txtOperations.Text, this.QProjectID, this.workdate.Text);
			this.dgClass.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}
		protected void dgClass_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);doClickRow('" + dataRowView["LogId"].ToString() + "');";
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				return;
			}
			default:
				return;
			}
		}
		protected void dgClass_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgClass.CurrentPageIndex = e.NewPageIndex;
			this.myDataQuery();
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			string logID = Convert.ToString(this.hdnLogID.Value);
			if (WokLog.Delete(logID) == 1)
			{
				this.js.Text = "alert('删除成功！');";
				this.myDataBind();
				return;
			}
			this.js.Text = "alert('删除错误！');";
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.myDataQuery();
		}
	}


