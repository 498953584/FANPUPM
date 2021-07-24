using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class AuditStatus : NBasePage, IRequiresSessionState
	{
		public System.Guid InstanceCode
		{
			get
			{
				object obj = this.ViewState["InstanceCode"];
				if (obj != null)
				{
					return (System.Guid)obj;
				}
				return System.Guid.Empty;
			}
			set
			{
				this.ViewState["InstanceCode"] = value;
			}
		}
		protected string BusinessCode
		{
			get
			{
				return this.ViewState["BUSINESSCODE"].ToString();
			}
			set
			{
				this.ViewState["BUSINESSCODE"] = value;
			}
		}
		protected string BusinessClass
		{
			get
			{
				return this.ViewState["BUSINESSCLASS"].ToString();
			}
			set
			{
				this.ViewState["BUSINESSCLASS"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.InstanceCode = new System.Guid(base.Request["ic"]);
				this.BusinessCode = base.Request["bc"].ToString();
				this.BusinessClass = base.Request["bcl"].ToString();
				DataTable dataTable = FlowAuditAction.QueryAuditStatus(this.InstanceCode, this.BusinessCode, this.BusinessClass);
				this.dgFlow.DataSource = dataTable;
				this.dgFlow.DataBind();
				int num = 1;
				for (int i = 0; i < dataTable.Rows.Count - 1; i++)
				{
					if (dataTable.Rows[i]["id"].ToString() != dataTable.Rows[i + 1]["id"].ToString())
					{
						DataGridItem dataGridItem = new DataGridItem(-1, -1, ListItemType.Item);
						num++;
						for (int j = 0; j < this.dgFlow.Columns.Count; j++)
						{
							TableCell tableCell = new TableCell();
							tableCell.Text = "&nbsp;";
							dataGridItem.Cells.Add(tableCell);
						}
						dataGridItem.BackColor = Color.FromName("#E4ECF1");
						dataGridItem.BorderColor = Color.FromName("#E4ECF1");
						this.dgFlow.Controls[0].Controls.AddAt(i + num, dataGridItem);
					}
				}
			}
		}
		protected void dgFlow_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
		}
		protected void dgFlow_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgFlow.CurrentPageIndex = e.NewPageIndex;
			this.dgFlow.DataSource = FlowAuditAction.QueryAuditStatus(this.InstanceCode, this.BusinessCode, this.BusinessClass);
			this.dgFlow.DataBind();
		}
	}


