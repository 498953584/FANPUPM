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
	public partial class OutBox : BasePage, IRequiresSessionState
	{
		private string _strSenderCode = "";
		private int _iSysID;

		protected void Page_Load(object sender, EventArgs e)
		{
			this._strSenderCode = this.Session["yhdm"].ToString();
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			if (!base.IsPostBack)
			{
				MailManage mailManage = new MailManage();
				this.tbSet.Text = mailManage.getPageSize(this.Session["yhdm"].ToString()).ToString();
				this.Session["mailPageSize"] = this.tbSet.Text;
				this.BindBox(this.DDLtBox, "s,c,l");
				this.DGrdMail_Bind(-1);
				this.BtnDelY.Attributes["onclick"] = "return confirm('该操作不可恢复，你确认要删除?',1,0);";
			}
		}
		private void BindBox(DropDownList DropDownList1, string strBoxTag)
		{
			DropDownList1.Items.Clear();
			string[] array = strBoxTag.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string a;
				if ((a = array[i].ToLower()) != null)
				{
					if (!(a == "s"))
					{
						if (!(a == "d"))
						{
							if (!(a == "c"))
							{
								if (a == "l")
								{
									DropDownList1.Items.Add(new ListItem("垃圾箱", "l"));
								}
							}
							else
							{
								DropDownList1.Items.Add(new ListItem("草稿箱", "c"));
							}
						}
						else
						{
							DropDownList1.Items.Add(new ListItem("发件箱", "d"));
						}
					}
					else
					{
						DropDownList1.Items.Add(new ListItem("收件箱", "s"));
					}
				}
			}
		}
		private void DGrdMail_Bind(int iNewPage)
		{
			MailManage mailManage = new MailManage();
			DataTable outMail = mailManage.GetOutMail(this._strSenderCode);
			this.DGrdMail.PageSize = Convert.ToInt32(this.Session["mailPageSize"].ToString());
			this.DGrdMail.DataSource = outMail.DefaultView;
			if (outMail.Rows.Count > 0 && iNewPage == (outMail.Rows.Count + this.DGrdMail.PageSize - 1) / this.DGrdMail.PageSize)
			{
				this.DGrdMail.CurrentPageIndex = iNewPage - 1;
			}
			this.DGrdMail.DataBind();
			this.LabMail.Text = "发件箱邮件<FONT color=\"#ff0000\"><B>" + outMail.Rows.Count.ToString() + "</B></FONT>封";
		}
		protected void DGrdMail_PreRender(object sender, EventArgs e)
		{
			string text = "";
			text += "function selectAll(obj)\n";
			text += "{\n";
			text += "\t\tvar num = document.all.tags(\"input\").length;\n";
			text += "\t\tvar str_temp;\n";
			text += "\t\t//设置子模块复选框\n";
			text += "\t\tfor(var i=0; i<num; i++)\n";
			text += "\t\t{\n";
			text += "\t\t\tstr_temp = document.all.tags(\"input\")[i].id;\n";
			text += "\t\t\tif(str_temp.lastIndexOf('cbSelSingle') > 0 )\n";
			text += "\t\t\t{\n";
			text += "\t\t\t\tdocument.all.tags(\"input\")[i].checked = obj.checked;\n";
			text += "\t\t\t}\n";
			text += "\t\t}\n";
			text += "\t}";
			this.js.Text = text;
		}
		private void DGrdMail_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex == -1)
			{
				CheckBox checkBox = (CheckBox)e.Item.FindControl("cbSelAll");
				if (checkBox != null)
				{
					checkBox.Attributes.Add("onclick", "selectAll(this);");
					return;
				}
			}
			else
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Cells[2].Text = ((DataRowView)e.Item.DataItem)["v_jsrxm"].ToString().TrimEnd(new char[]
				{
					','
				});
				string a = dataRowView["c_fj"].ToString();
				dataRowView["c_xbs"].ToString();
				if (a == "n")
				{
					Image image = (Image)e.Item.Cells[5].Controls[1];
					image.Visible = false;
				}
				if (e.Item.Cells[3].Text.Length > 10)
				{
					e.Item.Cells[3].ToolTip = e.Item.Cells[3].Text;
					e.Item.Cells[3].Text = e.Item.Cells[3].Text.Substring(0, 9) + "...";
				}
				LinkButton linkButton = (LinkButton)e.Item.Cells[3].Controls[1];
				linkButton.Style.Add("color", "#333333");
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdMail.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGrdMail_PageIndexChanged);
			this.DGrdMail.ItemDataBound += new DataGridItemEventHandler(this.DGrdMail_ItemDataBound);
		}
		protected void DGrdMail_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.Response.Redirect(string.Concat(new string[]
			{
				"ViewMail.aspx?mailtype=s&hcode=",
				this._strSenderCode,
				"&mailID=",
				this.DGrdMail.DataKeys[this.DGrdMail.SelectedIndex].ToString(),
				"&mailBox=s&nowPage=",
				this.DGrdMail.CurrentPageIndex.ToString()
			}));
		}
		protected void BtnDelN_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			foreach (DataGridItem dataGridItem in this.DGrdMail.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
				if (checkBox.Checked)
				{
					int iMailID = int.Parse(this.DGrdMail.DataKeys[dataGridItem.ItemIndex].ToString());
					if (!mailManage.MoveToRecycle(iMailID, this._strSenderCode))
					{
						this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
					}
				}
			}
			this.DGrdMail_Bind(this.DGrdMail.CurrentPageIndex);
		}
		protected void BtnDelY_Click(object sender, EventArgs e)
		{
			int num = 0;
			MailManage mailManage = new MailManage();
			foreach (DataGridItem dataGridItem in this.DGrdMail.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
				if (checkBox.Checked)
				{
					num++;
					int iMailID = int.Parse(this.DGrdMail.DataKeys[dataGridItem.ItemIndex].ToString());
					if (!mailManage.DelMail(iMailID, this._strSenderCode))
					{
						this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
					}
				}
			}
			if (num > 0)
			{
				if (this.DGrdMail.Items.Count > 0)
				{
					this.RegisterClientScriptBlock("Success", "<SCRIPT language=\"JavaScript\">alert('操作成功！');window.location.href=window.location.href;</SCRIPT>");
				}
			}
			else
			{
				this.RegisterClientScriptBlock("Success", "<SCRIPT language=\"JavaScript\">alert('请选择要删除的项！');window.location.href=window.location.href;</SCRIPT>");
			}
			this.DGrdMail_Bind(this.DGrdMail.CurrentPageIndex);
		}
		private void DGrdMail_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdMail.CurrentPageIndex = e.NewPageIndex;
			this.DGrdMail_Bind(-1);
		}
		protected void BtnMove_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			string a;
			if ((a = this.DDLtBox.SelectedValue.ToString()) != null)
			{
				if (a == "s")
				{
					foreach (DataGridItem dataGridItem in this.DGrdMail.Items)
					{
						CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGrdMail.DataKeys[dataGridItem.ItemIndex].ToString());
							if (!mailManage.MoveToInBox(iMailID, this._iSysID, this._strSenderCode, "d"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGrdMail_Bind(this.DGrdMail.CurrentPageIndex);
					return;
				}
				if (a == "c")
				{
					foreach (DataGridItem dataGridItem2 in this.DGrdMail.Items)
					{
						CheckBox checkBox = (CheckBox)dataGridItem2.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGrdMail.DataKeys[dataGridItem2.ItemIndex].ToString());
							if (!mailManage.MoveToDraft(iMailID, this._iSysID, this._strSenderCode, "d"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGrdMail_Bind(this.DGrdMail.CurrentPageIndex);
					return;
				}
				if (!(a == "l"))
				{
					return;
				}
				foreach (DataGridItem dataGridItem3 in this.DGrdMail.Items)
				{
					CheckBox checkBox = (CheckBox)dataGridItem3.FindControl("cbSelSingle");
					if (checkBox.Checked)
					{
						int iMailID = int.Parse(this.DGrdMail.DataKeys[dataGridItem3.ItemIndex].ToString());
						if (!mailManage.MoveToRecycle(iMailID, this._strSenderCode))
						{
							this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
						}
					}
				}
				this.DGrdMail_Bind(this.DGrdMail.CurrentPageIndex);
			}
		}
		protected void btnSet_Click(object sender, EventArgs e)
		{
			if (Convert.ToInt32(this.tbSet.Text.Trim()) <= 0)
			{
				this.js.Text = "alert('每页显示记录不能小于1');";
				return;
			}
			MailManage mailManage = new MailManage();
			mailManage.setPageSize(this.Session["yhdm"].ToString(), Convert.ToInt32(this.tbSet.Text.Trim()));
			this.Session["mailPageSize"] = this.tbSet.Text;
			this.DGrdMail_Bind(-1);
		}
	}


