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
	public partial class DraftBox : BasePage, IRequiresSessionState
	{
		private int _iSysID;
		private string _strSenderCode = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			if (!base.IsPostBack)
			{
				MailManage mailManage = new MailManage();
				this.tbSet.Text = mailManage.getPageSize(this.Session["yhdm"].ToString()).ToString();
				this.Session["mailPageSize"] = this.tbSet.Text;
				this.DGridDraft_Bind(-1);
				this.BindBox(this.DDLtBox, "s,d,l");
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
		private void DGridDraft_Bind(int iNewPage)
		{
			MailManage mailManage = new MailManage();
			DataTable draftMail = mailManage.GetDraftMail(this._strSenderCode);
			this.DGridDraft.PageSize = Convert.ToInt32(this.Session["mailPageSize"].ToString());
			this.DGridDraft.DataSource = draftMail.DefaultView;
			if (draftMail.Rows.Count > 0 && iNewPage == (draftMail.Rows.Count + this.DGridDraft.PageSize - 1) / this.DGridDraft.PageSize)
			{
				this.DGridDraft.CurrentPageIndex = iNewPage - 1;
			}
			this.DGridDraft.DataBind();
			this.LabMail.Text = "草稿箱邮件<FONT color=\"#ff0000\"><B>" + draftMail.Rows.Count + "</B></FONT>封";
		}
		protected void DGridDraft_PreRender(object sender, EventArgs e)
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
		private void DGridDraft_ItemDataBound(object sender, DataGridItemEventArgs e)
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
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGridDraft.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGridDraft_PageIndexChanged);
			this.DGridDraft.ItemDataBound += new DataGridItemEventHandler(this.DGridDraft_ItemDataBound);
		}
		protected void BtnDelN_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			foreach (DataGridItem dataGridItem in this.DGridDraft.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
				if (checkBox.Checked)
				{
					int iMailID = int.Parse(this.DGridDraft.DataKeys[dataGridItem.ItemIndex].ToString());
					if (!mailManage.MoveToRecycle(iMailID, this._strSenderCode))
					{
						this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
					}
				}
			}
			this.DGridDraft_Bind(this.DGridDraft.CurrentPageIndex);
		}
		protected void BtnDelY_Click(object sender, EventArgs e)
		{
			int num = 0;
			MailManage mailManage = new MailManage();
			foreach (DataGridItem dataGridItem in this.DGridDraft.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
				if (checkBox.Checked)
				{
					int iMailID = int.Parse(this.DGridDraft.DataKeys[dataGridItem.ItemIndex].ToString());
					if (!mailManage.DelMail(iMailID, this._strSenderCode))
					{
						this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
					}
				}
			}
			if (num > 0)
			{
				if (this.DGridDraft.Items.Count > 0)
				{
					this.RegisterClientScriptBlock("Success", "<SCRIPT language=\"JavaScript\">alert('操作成功！');window.location.href=window.location.href;</SCRIPT>");
				}
			}
			else
			{
				this.RegisterClientScriptBlock("Success", "<SCRIPT language=\"JavaScript\">alert('请选择要删除的项！');window.location.href=window.location.href;</SCRIPT>");
			}
			this.DGridDraft_Bind(this.DGridDraft.CurrentPageIndex);
		}
		protected void BtnMove_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			string a;
			if ((a = this.DDLtBox.SelectedValue.ToString()) != null)
			{
				if (a == "s")
				{
					foreach (DataGridItem dataGridItem in this.DGridDraft.Items)
					{
						CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGridDraft.DataKeys[dataGridItem.ItemIndex].ToString());
							if (!mailManage.MoveToInBox(iMailID, this._iSysID, this._strSenderCode, "c"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGridDraft_Bind(this.DGridDraft.CurrentPageIndex);
					return;
				}
				if (a == "d")
				{
					foreach (DataGridItem dataGridItem2 in this.DGridDraft.Items)
					{
						CheckBox checkBox = (CheckBox)dataGridItem2.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGridDraft.DataKeys[dataGridItem2.ItemIndex].ToString());
							if (!mailManage.MoveToOutBox(iMailID, this._iSysID, this._strSenderCode, "c"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGridDraft_Bind(this.DGridDraft.CurrentPageIndex);
					return;
				}
				if (!(a == "l"))
				{
					return;
				}
				foreach (DataGridItem dataGridItem3 in this.DGridDraft.Items)
				{
					CheckBox checkBox = (CheckBox)dataGridItem3.FindControl("cbSelSingle");
					if (checkBox.Checked)
					{
						int iMailID = int.Parse(this.DGridDraft.DataKeys[dataGridItem3.ItemIndex].ToString());
						if (!mailManage.MoveToRecycle(iMailID, this._strSenderCode))
						{
							this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
						}
					}
				}
				this.DGridDraft_Bind(this.DGridDraft.CurrentPageIndex);
			}
		}
		private void DGridDraft_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGridDraft.CurrentPageIndex = e.NewPageIndex;
			this.DGridDraft_Bind(-1);
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
			this.DGridDraft_Bind(-1);
		}
	}


