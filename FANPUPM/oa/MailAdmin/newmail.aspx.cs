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
	public partial class NewMail : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		private string _strSenderCode = "";
		private int _iSysID;
		protected static int i;


		protected void Page_Load(object sender, EventArgs e)
		{
			this._strSenderCode = this.Session["yhdm"].ToString();
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			if (!base.IsPostBack)
			{
				MailManage mailManage = new MailManage();
				this.tbSet.Text = mailManage.getPageSize(this.Session["yhdm"].ToString()).ToString();
				this.Session["mailPageSize"] = this.tbSet.Text;
				this.DGrdNew_Bind(-1);
				this.BindBox(this.DDLtBox, "d,c,l");
				this.BtnDelY.Attributes["onclick"] = "return confirm('该操作不可恢复，你确认要删除?',1,0);";
			}
		}
		private void BindBox(System.Web.UI.WebControls.DropDownList DropDownList1, string strBoxTag)
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
									DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem("垃圾箱", "l"));
								}
							}
							else
							{
								DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem("草稿箱", "c"));
							}
						}
						else
						{
							DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem("发件箱", "d"));
						}
					}
					else
					{
						DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem("收件箱", "s"));
					}
				}
			}
		}
		private void DGrdNew_Bind(int iNewPage)
		{
			MailManage mailManage = new MailManage();
			DataTable newMail = mailManage.GetNewMail(this._strSenderCode);
			int count = newMail.Rows.Count;
			this.LabMail.Text = "新邮件<FONT color=\"#ff0000\"><B>" + count.ToString() + "</B></FONT>封";
			this.DGrdNew.PageSize = Convert.ToInt32(this.Session["mailPageSize"].ToString());
			this.DGrdNew.DataSource = newMail.DefaultView;
			if (newMail.Rows.Count > 0 && iNewPage == (newMail.Rows.Count + this.DGrdNew.PageSize - 1) / this.DGrdNew.PageSize)
			{
				this.DGrdNew.CurrentPageIndex = iNewPage - 1;
			}
			this.DGrdNew.DataBind();
		}
		protected void DGrdDNew_PreRender(object sender, EventArgs e)
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
		private void DGrdNew_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex == -1)
			{
				System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)e.Item.FindControl("cbSelAll");
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
				string a = dataRowView["c_fj"].ToString();
				string a2 = dataRowView["c_xbs"].ToString();
				if (a == "n")
				{
					System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)e.Item.Cells[5].Controls[1];
					image.Visible = false;
				}
				if (a2 == "n")
				{
					System.Web.UI.WebControls.Image image2 = (System.Web.UI.WebControls.Image)e.Item.Cells[1].Controls[1];
					image2.Visible = false;
					return;
				}
				e.Item.Attributes.Add("style", "font-weight:bolder;");
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdNew.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DGrdNew_PageIndexChanged);
			this.DGrdNew.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DGrdNew_ItemDataBound);
		}
		protected void BtnDelN_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			foreach (System.Web.UI.WebControls.DataGridItem dataGridItem in this.DGrdNew.Items)
			{
				System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)dataGridItem.FindControl("cbSelSingle");
				if (checkBox.Checked)
				{
					int iMailID = int.Parse(this.DGrdNew.DataKeys[dataGridItem.ItemIndex].ToString());
					if (!mailManage.MoveToRecycle(iMailID, this._strSenderCode))
					{
						this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
					}
				}
			}
			this.DGrdNew_Bind(this.DGrdNew.CurrentPageIndex);
		}
		protected void BtnDelY_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			foreach (System.Web.UI.WebControls.DataGridItem dataGridItem in this.DGrdNew.Items)
			{
				System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)dataGridItem.FindControl("cbSelSingle");
				if (checkBox.Checked)
				{
					int iMailID = int.Parse(this.DGrdNew.DataKeys[dataGridItem.ItemIndex].ToString());
					if (!mailManage.DelMail(iMailID, this._strSenderCode))
					{
						this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
					}
				}
			}
			if (this.DGrdNew.Items.Count > 0)
			{
				this.RegisterClientScriptBlock("Success", "<SCRIPT language=\"JavaScript\">alert('操作成功！');window.location.href=window.location.href;</SCRIPT>");
			}
			this.DGrdNew_Bind(this.DGrdNew.CurrentPageIndex);
		}
		protected void BtnMove_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			string a;
			if ((a = this.DDLtBox.SelectedValue.ToString()) != null)
			{
				if (a == "d")
				{
					foreach (System.Web.UI.WebControls.DataGridItem dataGridItem in this.DGrdNew.Items)
					{
						System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)dataGridItem.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGrdNew.DataKeys[dataGridItem.ItemIndex].ToString());
							if (!mailManage.MoveToOutBox(iMailID, this._iSysID, this._strSenderCode, "s"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGrdNew_Bind(this.DGrdNew.CurrentPageIndex);
					return;
				}
				if (a == "c")
				{
					foreach (System.Web.UI.WebControls.DataGridItem dataGridItem2 in this.DGrdNew.Items)
					{
						System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)dataGridItem2.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGrdNew.DataKeys[dataGridItem2.ItemIndex].ToString());
							if (!mailManage.MoveToDraft(iMailID, this._iSysID, this._strSenderCode, "s"))
							{
								this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
							}
						}
					}
					this.DGrdNew_Bind(this.DGrdNew.CurrentPageIndex);
					return;
				}
				if (!(a == "l"))
				{
					return;
				}
				foreach (System.Web.UI.WebControls.DataGridItem dataGridItem3 in this.DGrdNew.Items)
				{
					System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)dataGridItem3.FindControl("cbSelSingle");
					if (checkBox.Checked)
					{
						int iMailID = int.Parse(this.DGrdNew.DataKeys[dataGridItem3.ItemIndex].ToString());
						if (!mailManage.MoveToRecycle(iMailID, this._strSenderCode))
						{
							this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('在移动部分邮件过程中出错！');</SCRIPT>");
						}
					}
				}
				this.DGrdNew_Bind(this.DGrdNew.CurrentPageIndex);
			}
		}
		protected void DGrdNew_SelectedIndexChanged(object sender, EventArgs e)
		{
			base.Response.Redirect("ViewMail.aspx?mailtype=n&mailID=" + this.DGrdNew.DataKeys[this.DGrdNew.SelectedIndex].ToString() + "&mailBox=s&nowPage=" + this.DGrdNew.CurrentPageIndex.ToString());
		}
		private void DGrdNew_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGrdNew.CurrentPageIndex = e.NewPageIndex;
			this.DGrdNew_Bind(this.DGrdNew.CurrentPageIndex);
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
			this.DGrdNew_Bind(-1);
		}
	}


