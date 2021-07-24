﻿using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InBox : BasePage, IRequiresSessionState
	{
		private int _iSysID;
		private string _strSenderCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			if (!base.IsPostBack)
			{
				MailManage mailManage = new MailManage();
				this.tbSet.Text = mailManage.getPageSize(this.Session["yhdm"].ToString()).ToString();
				this.Session["mailPageSize"] = this.tbSet.Text;
				this.BindBox(this.DDLtBox, "d,c,l");
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
			DataTable inMailiss = mailManage.GetInMailiss(this._strSenderCode, -1);
			this.DGrdMail.DataSource = inMailiss.DefaultView;
			int count = inMailiss.Rows.Count;
			if (inMailiss.Rows.Count > 0)
			{
				this.DGrdMail.PageSize = System.Convert.ToInt32(this.Session["mailPageSize"].ToString());
				this.DGrdMail.DataSource = inMailiss.DefaultView;
				if (inMailiss.Rows.Count > 0 && iNewPage == (inMailiss.Rows.Count + this.DGrdMail.PageSize - 1) / this.DGrdMail.PageSize)
				{
					this.DGrdMail.CurrentPageIndex = iNewPage - 1;
				}
				decimal d = System.Convert.ToDecimal(mailManage.getAllAnnexSize(this.Session["yhdm"].ToString()));
				int userMailSpace = mailManage.getUserMailSpace(this.Session["yhdm"].ToString());
				this.LabMail.Text = string.Concat(new object[]
				{
					"收件箱邮件<FONT color=\"#ff0000\"><B>",
					count.ToString(),
					"</B></FONT>封 容量：",
					System.Convert.ToDecimal(d / 1024m / 1024m).ToString("0.00"),
					"M/",
					userMailSpace,
					"M"
				});
			}
			else
			{
				this.LabMail.Text = "收件箱邮件<FONT color=\"#ff0000\"><B>0</B></FONT>封";
			}
			this.DGrdMail.DataBind();
		}
		protected void DGrdMail_PreRender(object sender, System.EventArgs e)
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
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				string a = dataRowView["c_fj"].ToString();
				string a2 = dataRowView["c_xbs"].ToString();
				if (a == "n")
				{
					Image image = (Image)e.Item.Cells[6].Controls[1];
					image.Visible = false;
				}
				if (a2 == "n")
				{
					Image image2 = (Image)e.Item.Cells[1].Controls[1];
					image2.Visible = false;
					LinkButton linkButton = (LinkButton)e.Item.Cells[4].Controls[1];
					linkButton.Style.Add("color", "#333333");
					return;
				}
				e.Item.Attributes.Add("style", "font-weight:bolder;");
				LinkButton linkButton2 = (LinkButton)e.Item.Cells[4].Controls[1];
				linkButton2.Style.Add("color", "#333333");
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdMail.PageIndexChanged += new DataGridPageChangedEventHandler(this.DGrdMail_PageIndexChanged);
			this.DGrdMail.ItemDataBound += new DataGridItemEventHandler(this.DGrdMail_ItemDataBound);
		}
		protected void BtnMove_Click(object sender, System.EventArgs e)
		{
			MailManage mailManage = new MailManage();
			string a;
			if ((a = this.DDLtBox.SelectedValue.ToString()) != null)
			{
				if (a == "d")
				{
					foreach (DataGridItem dataGridItem in this.DGrdMail.Items)
					{
						CheckBox checkBox = (CheckBox)dataGridItem.FindControl("cbSelSingle");
						if (checkBox.Checked)
						{
							int iMailID = int.Parse(this.DGrdMail.DataKeys[dataGridItem.ItemIndex].ToString());
							if (!mailManage.MoveToOutBox(iMailID, this._iSysID, this._strSenderCode, "s"))
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
							if (!mailManage.MoveToDraft(iMailID, this._iSysID, this._strSenderCode, "s"))
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
		private void DGrdMail_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdMail.CurrentPageIndex = e.NewPageIndex;
			this.DGrdMail_Bind(this.DGrdMail.CurrentPageIndex);
		}
		protected void DGrdMail_SelectedIndexChanged(object sender, System.EventArgs e)
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
		protected void BtnDelN_Click(object sender, System.EventArgs e)
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
		protected void BtnDelY_Click(object sender, System.EventArgs e)
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
		protected void btnSet_Click(object sender, System.EventArgs e)
		{
			if (System.Convert.ToInt32(this.tbSet.Text.Trim()) <= 0)
			{
				this.js.Text = "alert('每页显示记录不能小于1');";
				return;
			}
			MailManage mailManage = new MailManage();
			mailManage.setPageSize(this.Session["yhdm"].ToString(), System.Convert.ToInt32(this.tbSet.Text.Trim()));
			this.Session["mailPageSize"] = this.tbSet.Text;
			this.DGrdMail_Bind(-1);
		}
	}


