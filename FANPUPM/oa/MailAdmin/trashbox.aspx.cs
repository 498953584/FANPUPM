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
	public partial class TrashBox : BasePage, IRequiresSessionState
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
				this.DGrdTrash_Bind(-1);
				this.BindBox(this.DDLtBox, "s,d,c");
				this.BtnDel.Attributes["onclick"] = "return confirm('该操作不可恢复，你确认要删除?',1,0);";
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
		private void DGrdTrash_Bind(int iNewPage)
		{
			MailManage mailManage = new MailManage();
			DataTable trashMail = mailManage.GetTrashMail(this._strSenderCode);
			this.DGrdTrash.PageSize = Convert.ToInt32(this.Session["mailPageSize"].ToString());
			this.DGrdTrash.DataSource = trashMail.DefaultView;
			if (trashMail.Rows.Count > 0 && iNewPage == (trashMail.Rows.Count + this.DGrdTrash.PageSize - 1) / this.DGrdTrash.PageSize)
			{
				this.DGrdTrash.CurrentPageIndex = iNewPage - 1;
			}
			this.DGrdTrash.DataBind();
			this.LabMail.Text = "垃圾箱共有邮件<FONT color=\"#ff0000\"><B>" + trashMail.Rows.Count.ToString() + "</B></FONT>封";
		}
		protected void DGrdTrash_PreRender(object sender, EventArgs e)
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
		private void DGrdTrash_ItemDataBound(object sender, DataGridItemEventArgs e)
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
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			int num = 0;
			MailManage mailManage = new MailManage();
			foreach (DataGridItem dataGridItem in this.DGrdTrash.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
				if (checkBox.Checked)
				{
					int iMailID = int.Parse(this.DGrdTrash.DataKeys[dataGridItem.ItemIndex].ToString());
					if (!mailManage.DelMail(iMailID, this._strSenderCode))
					{
						this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
					}
				}
			}
			if (num > 0)
			{
				if (this.DGrdTrash.Items.Count > 0)
				{
					this.RegisterClientScriptBlock("Success", "<SCRIPT language=\"JavaScript\">alert('操作成功！');window.location.href=window.location.href;</SCRIPT>");
				}
			}
			else
			{
				this.RegisterClientScriptBlock("Success", "<SCRIPT language=\"JavaScript\">alert('请选择要删除的项！');window.location.href=window.location.href;</SCRIPT>");
			}
			this.DGrdTrash_Bind(this.DGrdTrash.CurrentPageIndex);
		}
		protected void BtnMove_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			string a;
			if ((a = this.DDLtBox.SelectedValue.ToString()) != null)
			{
				if (a == "s")
				{
					foreach (DataGridItem dataGridItem in this.DGrdTrash.Items)
					{
						CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGrdTrash.DataKeys[dataGridItem.ItemIndex].ToString());
							if (!mailManage.MoveToInBox(iMailID, this._iSysID, this._strSenderCode, "l"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGrdTrash_Bind(this.DGrdTrash.CurrentPageIndex);
					return;
				}
				if (a == "d")
				{
					foreach (DataGridItem dataGridItem2 in this.DGrdTrash.Items)
					{
						CheckBox checkBox = (CheckBox)dataGridItem2.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGrdTrash.DataKeys[dataGridItem2.ItemIndex].ToString());
							if (!mailManage.MoveToOutBox(iMailID, this._iSysID, this._strSenderCode, "l"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGrdTrash_Bind(this.DGrdTrash.CurrentPageIndex);
					return;
				}
				if (!(a == "c"))
				{
					return;
				}
				foreach (DataGridItem dataGridItem3 in this.DGrdTrash.Items)
				{
					CheckBox checkBox = (CheckBox)dataGridItem3.FindControl("cbSelSingle");
					if (checkBox.Checked)
					{
						int iMailID = int.Parse(this.DGrdTrash.DataKeys[dataGridItem3.ItemIndex].ToString());
						if (!mailManage.MoveToDraft(iMailID, this._iSysID, this._strSenderCode, "l"))
						{
							this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
						}
					}
				}
				this.DGrdTrash_Bind(this.DGrdTrash.CurrentPageIndex);
			}
		}
		private void DGrdTrash_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdTrash.CurrentPageIndex = e.NewPageIndex;
			this.DGrdTrash_Bind(-1);
		}
		protected void DGrdTrash_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.Response.Redirect("ViewMail.aspx?mailtype=l&mailID=" + this.DGrdTrash.DataKeys[this.DGrdTrash.SelectedIndex].ToString() + "&mailBox=l&nowPage=" + this.DGrdTrash.CurrentPageIndex.ToString());
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
			this.DGrdTrash_Bind(-1);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdTrash.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGrdTrash_PageIndexChanged);
			this.DGrdTrash.ItemDataBound += new DataGridItemEventHandler(this.DGrdTrash_ItemDataBound);
		}
	}


