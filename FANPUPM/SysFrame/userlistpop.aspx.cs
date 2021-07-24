using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class UserListPop : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.CheckUserCode();
			if (base.Application["UserCodeCollection"] == null)
			{
				this.grdUserList.DataSource = new DataTable();
				this.grdUserList.DataBind();
				return;
			}
			this.grdUserList.DataSource = (DataTable)base.Application["UserCodeCollection"];
			this.grdUserList.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.grdUserList.ItemDataBound += new DataGridItemEventHandler(this.grdUserList_ItemDataBound);
		}
		private void grdUserList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.grdUserList.ClientID + "');";
			}
		}
		private void CheckUserCode()
		{
			string userHostAddress = HttpContext.Current.Request.UserHostAddress;
			if (base.Application["TempUserCodeCollection"] != null)
			{
				base.Application.Lock();
				DataTable dataTable = (DataTable)base.Application["TempUserCodeCollection"];
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (dataTable.Rows[i]["userCode"].ToString().Trim() == this.Session["yhdm"].ToString().Trim() && dataTable.Rows[i]["ip"].ToString().Trim() != userHostAddress.Trim())
					{
						DataTable dataTable2 = (DataTable)base.Application["UserCodeCollection"];
						foreach (DataRow dataRow in dataTable2.Rows)
						{
							if (dataRow["userCode"].ToString() == dataTable.Rows[i]["userCode"].ToString())
							{
								dataRow["ip"] = dataTable.Rows[i]["ip"];
								dataRow["loginTime"] = dataTable.Rows[i]["loginTime"];
								dataRow["endTime"] = dataTable.Rows[i]["endTime"];
								break;
							}
						}
						base.Application["UserCodeCollection"] = dataTable2;
						dataTable.Rows[i].Delete();
						base.Application["TempUserCodeCollection"] = dataTable;
						this.js.Text = "returnOut('" + dataTable.Rows[i]["ip"].ToString() + "');";
					}
				}
			}
		}
	}


