using AjaxControlToolkit;
using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using FreeTextBoxControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WriteMail : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		private int _iSysID;
		private string _strSenderCode = "";
		private string _strSenderName = "";
		private int iMailType;


		protected void Page_Load(object sender, EventArgs e)
		{
			this.LinkButton1.Attributes["onclick"] = "showAnnex();return false;";
			this.TBoxConsignee.Attributes["onclick"] = "getWhereForc(1);return false;";
			this.txtCopy.Attributes["onclick"] = "getWhereForc(2);return false;";
			this.txtSecret.Attributes["onclick"] = "getWhereForc(3);return false;";
			this._iSysID = int.Parse(this.Session["SysID"].ToString());
			this._strSenderCode = this.Session["yhdm"].ToString();
			userManageDb userManageDb = new userManageDb();
			this._strSenderName = userManageDb.GetUserName(this._strSenderCode);
			if (!base.IsPostBack)
			{
				this.Session["System"] = "";
				this.Session["HumanCode"] = "";
				this.Session["HumanName"] = "";
				this.Session["HumanCode2"] = "";
				this.Session["HumanName2"] = "";
				this.Session["HumanCode3"] = "";
				this.Session["HumanName3"] = "";
				this.DelTempAnnex(this._strSenderCode);
				this.bindtree();
				this.BindTree2();
			}
			this.TBoxConsignee.Text = this.Session["HumanName"].ToString();
		}
		private void DelTempAnnex(string strSenderCode)
		{
			MailManage mailManage = new MailManage();
			DataTable tempAnnex = mailManage.GetTempAnnex(strSenderCode);
			for (int i = 0; i < tempAnnex.Rows.Count; i++)
			{
				if (!mailManage.DelTempAnnex(int.Parse(tempAnnex.Rows[i]["i_fj_id"].ToString()), true))
				{
					this.RegisterClientScriptBlock("warn", "<SCRIPT languange=\"JavaScript\">alert('" + mailManage.MessageString + "');</SCRIPT>");
				}
			}
		}
		protected void BtnToDraft_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (!mailManage.isAllowSize(this._strSenderCode))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法保存到草稿箱！');</script>");
				return;
			}
			int iSysID = int.Parse(this.Session["SysID"].ToString());
			string text = this.TBoxTitle.Text.ToString();
			if (text.Length == 0)
			{
				text = "---";
			}
			string strContent = this.TBoxContent.Text.ToString();
			string strUserCode = this.Session["HumanCode"].ToString();
			string strUserName = this.hf.Value.ToString();
			if (mailManage.SaveToDraft(iSysID, this._strSenderCode, this._strSenderName, text, strContent, strUserCode, strUserName, this.iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString()))
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('保存到草稿箱成功！')</SCRIPT>");
				return;
			}
			this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('保存到草稿箱出错！')</SCRIPT>");
		}
		protected void BtnSend_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (!mailManage.isAllowSize(this._strSenderCode))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法发送！');</script>");
				return;
			}
			string text = this.Session["HumanCode"].ToString() + this.Session["HumanCode2"].ToString() + this.Session["HumanCode3"].ToString();
			string text2 = this.hf.Value.ToString();
			string text3 = this.TBoxTitle.Text.ToString();
			if (text3.Trim() == "")
			{
				text3 = "---";
			}
			string strContent = this.TBoxContent.Text.ToString().Replace("\r\n", "").Replace("'", "''");
			int iSms;
			if (this.CBoxSMS.Checked)
			{
				iSms = 1;
			}
			else
			{
				iSms = 0;
			}
			int rtx;
			if (this.CBRTX.Checked)
			{
				rtx = 1;
			}
			else
			{
				rtx = 0;
			}
			string text4 = "";
			text2 = this.Session["HumanName"].ToString() + this.Session["HumanName2"].ToString() + this.Session["HumanName3"].ToString();
			string[] array = text.Split(new char[]
			{
				'!'
			});
			string[] array2 = text2.Split(new char[]
			{
				','
			});
			if (text.Length == 0)
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('收件人不能为空！')</SCRIPT>");
				return;
			}
			if (!mailManage.SendMail(this._iSysID, this._strSenderCode, this._strSenderName, DateTime.Now, text3, strContent, iSms, array, array2, this.iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString(), rtx))
			{
				text4 += "向本系统发送邮件失败\\n";
			}
			if (this.CBoxSend.Checked)
			{
				text2 = this.Session["HumanName"].ToString();
				text = this.Session["HumanCode"].ToString();
				if (!mailManage.SaveToOut(this._iSysID, this._strSenderCode, this._strSenderName, text3, strContent, text, text2, this.iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString()))
				{
					text4 += "将邮件保存到发件箱出错！";
				}
			}
			if (text4 != "")
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('" + text4 + "');</SCRIPT>");
				return;
			}
			mailManage.AddlatelyLinkman(this.Session["yhdm"].ToString(), array, array2);
			base.Server.Transfer("viewmail_2.aspx");
			this.TreeView1.Nodes.Clear();
			this.bindtree();
			mailManage.AddlatelyLinkman(this.Session["yhdm"].ToString(), array, array2);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		public void bindtree()
		{
			MailManage mailManage = new MailManage();
			DataTable dataTable = mailManage.getlatelyLinkman(this.Session["yhdm"].ToString());
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				System.Web.UI.WebControls.TreeNode treeNode = new System.Web.UI.WebControls.TreeNode();
				if (dataTable.Rows[i][3].ToString().Trim() != "")
				{
					treeNode.Text = dataTable.Rows[i][3].ToString() + "[" + dataTable.Rows[i][4].ToString() + "]";
					if (dataTable.Rows[i][2].ToString().Trim().Length > 8)
					{
						treeNode.Value = dataTable.Rows[i][2].ToString();
					}
					else
					{
						treeNode.Value = "0:" + dataTable.Rows[i][2].ToString();
					}
					treeNode.Expanded = new bool?(true);
					this.TreeView1.Nodes.Add(treeNode);
				}
			}
		}
		public void BindTree2()
		{
			string sqlString = "select groupID,groupName from EMS_MaileGroup where userID='" + this.Session["yhdm"].ToString() + "'";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				System.Web.UI.WebControls.TreeNode treeNode = new System.Web.UI.WebControls.TreeNode();
				if (dataTable.Rows[i][0].ToString().Trim() != "")
				{
					treeNode.Text = dataTable.Rows[i]["groupName"].ToString();
					treeNode.Value = dataTable.Rows[i]["groupID"].ToString();
					treeNode.Expanded = new bool?(true);
					this.TreeView2.Nodes.Add(treeNode);
				}
				string sqlString2 = string.Concat(new string[]
				{
					"select v_yhdm,v_xm from [v_dzyj_linkman] where UserCode ='",
					this.Session["yhdm"].ToString(),
					"' and groupid='",
					dataTable.Rows[i]["groupID"].ToString(),
					"'"
				});
				DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					System.Web.UI.WebControls.TreeNode treeNode2 = new System.Web.UI.WebControls.TreeNode();
					if (dataTable2.Rows[j]["v_yhdm"].ToString().Trim() != "")
					{
						treeNode2.Text = dataTable2.Rows[j]["v_xm"].ToString();
						treeNode2.Value = "0:" + dataTable2.Rows[j]["v_yhdm"].ToString();
						treeNode.ChildNodes.Add(treeNode2);
					}
				}
			}
		}
		protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
		{
			string text = this.TreeView1.SelectedNode.Text.ToString().Trim().Trim().Substring(0, this.TreeView1.SelectedNode.Text.ToString().LastIndexOf("[")).Trim();
			string text2 = this.TreeView1.SelectedNode.Value.ToString().Trim();
			if (this.fcnum.Value.ToString().Trim() == "1")
			{
				if (!this.isexit("HumanName", text))
				{
					System.Web.UI.WebControls.TextBox expr_A0 = this.TBoxConsignee;
					expr_A0.Text = expr_A0.Text + this.TreeView1.SelectedNode.Text.ToString().Trim().Substring(0, this.TreeView1.SelectedNode.Text.ToString().LastIndexOf("[")).Trim() + ",";
					System.Web.SessionState.HttpSessionState session;
					(session = this.Session)["HumanName"] = session["HumanName"] + text + ",";
					if (!this.isexit("HumanCode", text2))
					{
						System.Web.SessionState.HttpSessionState session2;
						(session2 = this.Session)["HumanCode"] = session2["HumanCode"] + text2 + "!";
						return;
					}
				}
			}
			else
			{
				if (this.fcnum.Value.ToString().Trim() == "2")
				{
					if (!this.isexit("HumanName2", text))
					{
						System.Web.UI.WebControls.TextBox expr_196 = this.txtCopy;
						expr_196.Text = expr_196.Text + this.TreeView1.SelectedNode.Text.ToString().Trim().Substring(0, this.TreeView1.SelectedNode.Text.ToString().LastIndexOf("[")).Trim() + ",";
						System.Web.SessionState.HttpSessionState session3;
						(session3 = this.Session)["HumanName2"] = session3["HumanName2"] + text + ",";
						if (!this.isexit("HumanCode2", text2))
						{
							System.Web.SessionState.HttpSessionState session4;
							(session4 = this.Session)["HumanCode2"] = session4["HumanCode2"] + text2 + "!";
							return;
						}
					}
				}
				else
				{
					if (this.fcnum.Value.ToString().Trim() == "3" && !this.isexit("HumanName3", text))
					{
						System.Web.UI.WebControls.TextBox expr_290 = this.txtSecret;
						expr_290.Text = expr_290.Text + this.TreeView1.SelectedNode.Text.ToString().Trim().Substring(0, this.TreeView1.SelectedNode.Text.ToString().LastIndexOf("[")).Trim() + ",";
						System.Web.SessionState.HttpSessionState session5;
						(session5 = this.Session)["HumanName3"] = session5["HumanName3"] + text + ",";
						if (!this.isexit("HumanCode3", text2))
						{
							System.Web.SessionState.HttpSessionState session6;
							(session6 = this.Session)["HumanCode3"] = session6["HumanCode3"] + text2 + "!";
						}
					}
				}
			}
		}
		public bool isexit(string sessName, string searStr)
		{
			string text = this.Session[sessName ?? ""].ToString().Trim();
			int num = text.IndexOf(searStr);
			return num >= 0;
		}
		protected void btnsend1_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (!mailManage.isAllowSize(this._strSenderCode))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法发送！');</script>");
				return;
			}
			string text = this.Session["HumanCode"].ToString() + this.Session["HumanCode2"].ToString() + this.Session["HumanCode3"].ToString();
			string text2 = this.hf.Value.ToString();
			string text3 = this.TBoxTitle.Text.ToString();
			if (text3.Trim() == "")
			{
				text3 = "---";
			}
			string strContent = this.TBoxContent.Text.ToString().Replace("\r\n", "").Replace("'", "''");
			int iSms;
			if (this.CBoxSMS.Checked)
			{
				iSms = 1;
			}
			else
			{
				iSms = 0;
			}
			int rtx;
			if (this.CBRTX.Checked)
			{
				rtx = 1;
			}
			else
			{
				rtx = 0;
			}
			string text4 = "";
			text2 = this.Session["HumanName"].ToString() + this.Session["HumanName2"].ToString() + this.Session["HumanName3"].ToString();
			string[] array = text.Split(new char[]
			{
				'!'
			});
			string[] array2 = text2.Split(new char[]
			{
				','
			});
			if (text.Length == 0)
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('收件人不能为空！')</SCRIPT>");
				return;
			}
			if (!mailManage.SendMail(this._iSysID, this._strSenderCode, this._strSenderName, DateTime.Now, text3, strContent, iSms, array, array2, this.iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString(), rtx))
			{
				text4 += "向本系统发送邮件失败\\n";
			}
			if (this.chfj.Checked)
			{
				text2 = this.Session["HumanName"].ToString();
				text = this.Session["HumanCode"].ToString();
				if (!mailManage.SaveToOut(this._iSysID, this._strSenderCode, this._strSenderName, text3, strContent, text, text2, this.iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString()))
				{
					text4 += "将邮件保存到发件箱出错！";
				}
			}
			if (text4 != "")
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('" + text4 + "');</SCRIPT>");
				return;
			}
			mailManage.AddlatelyLinkman(this.Session["yhdm"].ToString(), array, array2);
			base.Server.Transfer("viewmail_2.aspx");
			this.TreeView1.Nodes.Clear();
			this.bindtree();
		}
		protected void btnsave1_Click(object sender, EventArgs e)
		{
			MailManage mailManage = new MailManage();
			if (!mailManage.isAllowSize(this._strSenderCode))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('该邮件的附件总大小已经超过10M限制，请进行处理，否则该邮件无法保存到草稿箱！');</script>");
				return;
			}
			int iSysID = int.Parse(this.Session["SysID"].ToString());
			string text = this.TBoxTitle.Text.ToString();
			if (text.Length == 0)
			{
				text = "---";
			}
			string strContent = this.TBoxContent.Text.ToString();
			string strUserCode = this.Session["HumanCode"].ToString();
			string strUserName = this.hf.Value.ToString();
			if (mailManage.SaveToDraft(iSysID, this._strSenderCode, this._strSenderName, text, strContent, strUserCode, strUserName, this.iMailType, this.Session["HumanName"].ToString(), this.Session["HumanName2"].ToString()))
			{
				this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('保存到草稿箱成功！')</SCRIPT>");
				return;
			}
			this.RegisterClientScriptBlock("warn", "<SCRIPT language=\"JavaScript\">alert('保存到草稿箱出错！')</SCRIPT>");
		}
		protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
		{
			string text = this.TreeView2.SelectedNode.Text.ToString().Trim();
			string text2 = this.TreeView2.SelectedNode.Value.ToString().Trim();
			if (this.TreeView2.SelectedNode.Parent == null)
			{
				if (this.TreeView2.SelectedNode.ChildNodes.Count <= 0)
				{
					return;
				}
				string sqlString = string.Concat(new string[]
				{
					"select v_yhdm,v_xm from [v_dzyj_linkman] where UserCode ='",
					this.Session["yhdm"].ToString(),
					"' and groupid='",
					this.TreeView2.SelectedValue.ToString(),
					"'"
				});
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				IEnumerator enumerator = dataTable.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						text = dataRow["v_xm"].ToString();
						text2 = "0:" + dataRow["v_yhdm"].ToString();
						if (this.fcnum.Value.ToString().Trim() == "1")
						{
							if (!this.isexit("HumanName", text))
							{
								System.Web.UI.WebControls.TextBox expr_14E = this.TBoxConsignee;
								expr_14E.Text = expr_14E.Text + text + ",";
								System.Web.SessionState.HttpSessionState session;
								(session = this.Session)["HumanName"] = session["HumanName"] + text + ",";
								if (!this.isexit("HumanCode", text2))
								{
									System.Web.SessionState.HttpSessionState session2;
									(session2 = this.Session)["HumanCode"] = session2["HumanCode"] + text2 + "!";
								}
							}
						}
						else
						{
							if (this.fcnum.Value.ToString().Trim() == "2")
							{
								if (!this.isexit("HumanName2", text))
								{
									System.Web.UI.WebControls.TextBox expr_209 = this.txtCopy;
									expr_209.Text = expr_209.Text + text + ",";
									System.Web.SessionState.HttpSessionState session3;
									(session3 = this.Session)["HumanName2"] = session3["HumanName2"] + text + ",";
									if (!this.isexit("HumanCode2", text2))
									{
										System.Web.SessionState.HttpSessionState session4;
										(session4 = this.Session)["HumanCode2"] = session4["HumanCode2"] + text2 + "!";
									}
								}
							}
							else
							{
								if (this.fcnum.Value.ToString().Trim() == "3" && !this.isexit("HumanName3", text))
								{
									System.Web.UI.WebControls.TextBox expr_2C1 = this.txtSecret;
									expr_2C1.Text = expr_2C1.Text + text + ",";
									System.Web.SessionState.HttpSessionState session5;
									(session5 = this.Session)["HumanName3"] = session5["HumanName3"] + text + ",";
									if (!this.isexit("HumanCode3", text2))
									{
										System.Web.SessionState.HttpSessionState session6;
										(session6 = this.Session)["HumanCode3"] = session6["HumanCode3"] + text2 + "!";
									}
								}
							}
						}
					}
					return;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (this.fcnum.Value.ToString().Trim() == "1")
			{
				if (!this.isexit("HumanName", text))
				{
					System.Web.UI.WebControls.TextBox expr_39A = this.TBoxConsignee;
					expr_39A.Text = expr_39A.Text + this.TreeView2.SelectedNode.Text.ToString().Trim() + ",";
					System.Web.SessionState.HttpSessionState session7;
					(session7 = this.Session)["HumanName"] = session7["HumanName"] + text + ",";
					if (!this.isexit("HumanCode", text2))
					{
						System.Web.SessionState.HttpSessionState session8;
						(session8 = this.Session)["HumanCode"] = session8["HumanCode"] + text2 + "!";
						return;
					}
				}
			}
			else
			{
				if (this.fcnum.Value.ToString().Trim() == "2")
				{
					if (!this.isexit("HumanName2", text))
					{
						System.Web.UI.WebControls.TextBox expr_46A = this.txtCopy;
						expr_46A.Text = expr_46A.Text + this.TreeView2.SelectedNode.Text.ToString().Trim() + ",";
						System.Web.SessionState.HttpSessionState session9;
						(session9 = this.Session)["HumanName2"] = session9["HumanName2"] + text + ",";
						if (!this.isexit("HumanCode2", text2))
						{
							System.Web.SessionState.HttpSessionState session10;
							(session10 = this.Session)["HumanCode2"] = session10["HumanCode2"] + text2 + "!";
							return;
						}
					}
				}
				else
				{
					if (this.fcnum.Value.ToString().Trim() == "3" && !this.isexit("HumanName3", text))
					{
						System.Web.UI.WebControls.TextBox expr_53A = this.txtSecret;
						expr_53A.Text = expr_53A.Text + this.TreeView2.SelectedNode.Text.ToString().Trim() + ",";
						System.Web.SessionState.HttpSessionState session11;
						(session11 = this.Session)["HumanName3"] = session11["HumanName3"] + text + ",";
						if (!this.isexit("HumanCode3", text2))
						{
							System.Web.SessionState.HttpSessionState session12;
							(session12 = this.Session)["HumanCode3"] = session12["HumanCode3"] + text2 + "!";
						}
					}
				}
			}
		}
	}


