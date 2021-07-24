using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class repContAccount : PageBase, IRequiresSessionState
	{
		public QueryItemCollection ItemList
		{
			get
			{
				object obj = this.ViewState["ItemList"];
				if (obj != null)
				{
					return (QueryItemCollection)obj;
				}
				return new QueryItemCollection();
			}
			set
			{
				this.ViewState["ItemList"] = value;
			}
		}
		public string reportid
		{
			get
			{
				return (string)this.ViewState["reportid"];
			}
			set
			{
				this.ViewState["reportid"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.reportid = base.Request.QueryString["reportid"];
				DataRow reportRow = QueryItemAction.GetReportRow(this.reportid);
				string text = base.Request["type"].ToString();
				string text2 = "";
				string a;
				if ((a = text) != null)
				{
					if (!(a == "1"))
					{
						if (!(a == "2"))
						{
							if (!(a == "3"))
							{
								if (!(a == "4"))
								{
									if (a == "5")
									{
										text2 = "其他";
									}
								}
								else
								{
									text2 = "租赁";
								}
							}
							else
							{
								text2 = "采购";
							}
						}
						else
						{
							text2 = "分包";
						}
					}
					else
					{
						text2 = "承包";
					}
				}
				this.Lb_Title.Text = text2 + reportRow["Title"].ToString();
				int n = Convert.ToInt32(reportRow["headwidth"].ToString());
				string text3 = reportRow["Header"].ToString().Replace("[getdate()]", DateTime.Now.ToLongDateString());
				text3 = text3.Replace("[yhmc]", com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.Session["yhdm"].ToString()));
				string[] array = text3.Split(new char[]
				{
					';'
				});
				try
				{
					this.Lb_H1.Text = array[0];
					this.Lb_H2.Text = array[1];
					this.Lb_H3.Text = array[2];
				}
				catch
				{
				}
				string text4 = reportRow["Footer"].ToString().Replace("[getdate()]", DateTime.Now.ToLongDateString());
				text4 = text4.Replace("[yhmc]", com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.Session["yhdm"].ToString()));
				string[] array2 = text4.Split(new char[]
				{
					';'
				});
				try
				{
					this.Lb_F1.Text = array2[0];
					this.Lb_F2.Text = array2[1];
					this.Lb_F3.Text = array2[2];
				}
				catch
				{
				}
				if (this.Lb_Title.Text.Trim() == "")
				{
					this.TRTitle.Visible = false;
				}
				this.DG1.Width = n;
				if (this.Lb_F1.Text.Trim() == "" && this.Lb_F2.Text.Trim() == "" && this.Lb_F3.Text.Trim() == "")
				{
					this.TrFooter.Visible = false;
				}
				if (this.Lb_H1.Text.Trim() == "" && this.Lb_H2.Text.Trim() == "" && this.Lb_H3.Text.Trim() == "")
				{
					this.TrHeader.Visible = false;
				}
				this.btnSet.Attributes["onclick"] = "if (!openQuery(" + this.reportid + ")){return false;}";
				string text5 = " where (1=1) ";
				if (text2.Trim() != "")
				{
					text5 = text5 + "  and 合同类型 like '" + text2 + "合同' ";
				}
				this.DG1_BIND(text5);
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			string text = "   where (1=1) ";
			QueryItemCollection queryItemCollection = (QueryItemCollection)this.Session["QUERY"];
			for (int i = 0; i < queryItemCollection.Count; i++)
			{
				if (queryItemCollection[i].ItemValue.Trim() != "")
				{
					text = text + "   and " + queryItemCollection[i].StrSql.Replace("[]", queryItemCollection[i].ItemValue.Trim()).Trim();
				}
			}
			string text2 = base.Request["type"].ToString();
			string text3 = "";
			string a;
			if ((a = text2) != null)
			{
				if (!(a == "1"))
				{
					if (!(a == "2"))
					{
						if (!(a == "3"))
						{
							if (!(a == "4"))
							{
								if (a == "5")
								{
									text3 = "其他";
								}
							}
							else
							{
								text3 = "租赁";
							}
						}
						else
						{
							text3 = "采购";
						}
					}
					else
					{
						text3 = "分包";
					}
				}
				else
				{
					text3 = "承包";
				}
			}
			if (text3.Trim() != "")
			{
				text = text + "   and 合同类型 like '" + text3 + "合同' ";
			}
			this.DG1_BIND(text);
		}
		public void DG1_BIND(string strWhere)
		{
			if (strWhere == "")
			{
				strWhere = "   where (1=0)";
			}
			DataTable reportList = QueryItemAction.GetReportList(this.reportid, strWhere);
			if (reportList.Rows.Count != 0)
			{
				if (reportList.Columns.Contains("合同金额"))
				{
					if (!string.IsNullOrEmpty(reportList.Compute("sum(合同金额)", "").ToString()))
					{
						this.Lb_ContractAccount.Text = "合同金额总计:" + Convert.ToDouble(reportList.Compute("sum(合同金额)", "").ToString());
					}
					else
					{
						this.Lb_ContractAccount.Text = "合同金额总计:0.000";
					}
				}
				if (reportList.Columns.Contains("结算金额"))
				{
					if (!string.IsNullOrEmpty(reportList.Compute("sum(结算金额)", "").ToString()))
					{
						this.Lb_jiesuan.Text = "结算金额总计:" + Convert.ToDouble(reportList.Compute("sum(结算金额)", "").ToString());
					}
					else
					{
						this.Lb_jiesuan.Text = "结算金额总计:0.000";
					}
				}
				if (reportList.Columns.Contains("累计支付金额"))
				{
					if (!string.IsNullOrEmpty(reportList.Compute("sum(累计支付金额)", "").ToString()))
					{
						this.Lb_Leiji.Text = "累计支付金额总计:" + Convert.ToDouble(reportList.Compute("sum(累计支付金额)", ""));
					}
					else
					{
						this.Lb_Leiji.Text = "累计支付金额总计:0.000";
					}
				}
			}
			else
			{
				this.Lb_ContractAccount.Text = "合同金额总计:0.000";
				this.Lb_jiesuan.Text = "结算金额总计:0.000";
				this.Lb_Leiji.Text = "累计支付金额总计:0.000";
			}
			this.DG1.DataSource = reportList;
			this.DG1.DataBind();
		}
		protected void DG1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex != -1)
			{
				e.Row.Cells[0].Text = Convert.ToString(Convert.ToInt32(e.Row.RowIndex) + 1);
				DataRowView arg_4B_0 = (DataRowView)e.Row.DataItem;
				e.Row.Attributes["onclick"] = "OnRecord(this);";
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
			}
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", this.Lb_Title.Text + ".xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", this.Lb_Title.Text + ".doc");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.UTF8;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.Table1.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
		protected void DG1_Sorting(object sender, GridViewSortEventArgs e)
		{
			string sortExpression = e.SortExpression;
			if (this.ViewState["SortOrder"].ToString() != sortExpression)
			{
				this.ViewState["SortOrder"] = e.SortExpression;
			}
		}
	}


