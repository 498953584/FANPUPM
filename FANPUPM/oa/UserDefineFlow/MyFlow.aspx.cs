using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class UserDefineFlow_MyFlow : NBasePage, IRequiresSessionState
{
	public OAWFApplyItemAction hrAction
	{
		get
		{
			return new OAWFApplyItemAction();
		}
	}
	public WFBusinessClassAction wfaction
	{
		get
		{
			return new WFBusinessClassAction();
		}
	}
	public AnnexAction _AnnexAction
	{
		get
		{
			return new AnnexAction();
		}
	}
	public int Templateid
	{
		get
		{
			object obj = this.ViewState["tid"];
			if (obj != null)
			{
				return (int)this.ViewState["tid"];
			}
			return 0;
		}
		set
		{
			this.ViewState["tid"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		string value = base.Request["tid"];
		if (!this.Page.IsPostBack)
		{
			if (base.Request["type"] != null)
			{
				this.hdfNodeValue.Value = base.Request.QueryString["type"];
				value = this.hdfNodeValue.Value;
			}
			this.Tree_Bind();
			BasicConfigService basicConfigService = new BasicConfigService();
			this.hfldPwd.Value = basicConfigService.GetValue("TheDeletePwd");
			this.KeepTreeViewState();
		}
		if (!string.IsNullOrEmpty(value))
		{
			this.HdnTemplateID.Value = value;
			DataTable dataTable = FlowTemplateAction.QueryOneTemplate(System.Convert.ToInt32(value));
			if (dataTable.Rows.Count > 0)
			{
				this.HdnBusinessClass.Value = dataTable.Rows[0]["Businessclass"].ToString();
			}
			this.BindView(base.UserCode, System.Convert.ToInt32(value));
		}
		if (this.ViewState["tid"] != null)
		{
			this.BindView(base.UserCode, System.Convert.ToInt32(this.ViewState["tid"].ToString()));
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
		this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF('999');";
		this.btnStartWFRecord.Attributes["onclick"] = "openAudit('999')";
		this.BtnView.Attributes["onclick"] = "OpenLock();";
	}
	private void KeepTreeViewState()
	{
		foreach (TreeNode treeNode in this.TVDept.Nodes)
		{
			if (this.hdfNodeValue.Value == "")
			{
				treeNode.Selected = true;
				break;
			}
			this.GetSelectedNode(treeNode);
		}
	}
	private void GetSelectedNode(TreeNode CNode)
	{
		foreach (TreeNode treeNode in CNode.ChildNodes)
		{
			if (treeNode.Value == this.hdfNodeValue.Value)
			{
				treeNode.Selected = true;
				break;
			}
			this.GetSelectedNode(treeNode);
		}
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"','",
				dataRowView["BusinessClass"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = System.Convert.ToString(e.Row.RowIndex + 1);
			if (e.Row.Cells[2].Text.Length > 40)
			{
				e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 40) + "...";
			}
		}
	}
	private void BindView(string usercode, int templateid)
	{
		string text = string.Format("\r\n\t\t\tSELECT OA_WF_ApplyItem.*, v_xm \r\n\t\t\tFROM OA_WF_ApplyItem \r\n\t\t\tLEFT JOIN PT_yhmc ON OA_WF_ApplyItem.UserCode = v_yhdm\r\n\t\t\tWHERE TemplateID = '{0}'\r\n\t\t", templateid);
		if (usercode != "00000000")
		{
			text += string.Format(" AND OA_WF_ApplyItem.UserCode = '{0}'", usercode);
		}
		text += " order by RecordDate desc";
		DataTable dataSource = publicDbOpClass.DataTableQuary(text);
		this.GVBook.DataSource = dataSource;
		this.GVBook.DataBind();
	}
	private string Display_File(string RecordID)
	{
		string text = "";
		System.Collections.ArrayList annexList = this._AnnexAction.GetAnnexList(RecordID, 0, 88);
		if (annexList.Count > 0)
		{
			for (int i = 0; i < annexList.Count; i++)
			{
				text = text + ((AnnexInfo)annexList[i]).OriginalName + ",";
			}
		}
		return text.Trim(new char[]
		{
			','
		});
	}
	protected void btnStartWF_Click(object sender, System.EventArgs e)
	{
		System.Guid recordID = new System.Guid(this.HdnRecordID.Value);
		string value = this.HdnBusinessCode.Value;
		int templateId = System.Convert.ToInt32(this.HdnTemplateID.Value);
		string text = FlowAuditAction.BeginFlow("999", value, recordID, "", base.UserCode, templateId);
		if (text == "工作流程已成功启动")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "err", "alert('" + text + "!');", true);
		}
		else
		{
			if (text == "请先设置当前模块的审核流程")
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			}
			else
			{
				if (text == "尚未定义流程，请与系统管理员联系")
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
				}
				else
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
				}
			}
		}
		this.GVBook.DataBind();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		if (this.hrAction.Delete(new System.Guid(this.HdnRecordID.Value)) > 0)
		{
			this.BindView(base.UserCode, System.Convert.ToInt32(this.HdnTemplateID.Value));
		}
	}
	private void Tree_Bind()
	{
		this.TVDept.Nodes.Clear();
		TreeNode treeNode = new TreeNode("电子办公", "");
		this.TVDept.Nodes.Add(treeNode);
		DataTable list;
		if (this.Session["yhdm"].ToString() == "00000000")
		{
			list = this.wfaction.GetList("BusinessCode=999");
		}
		else
		{
			list = this.wfaction.GetList("BusinessCode=999 and businessclass in (select businessclass from wf_privilege where userlist='" + this.Session["yhdm"] + "')");
		}
		if (list.Rows.Count > 0)
		{
			int num = 0;
			foreach (DataRow dataRow in list.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["BusinessClassName"].ToString();
				treeNode2.Value = "r" + num++;
				this.templateBind(treeNode2, dataRow["BusinessClass"].ToString());
				treeNode.ChildNodes.Add(treeNode2);
			}
		}
	}
	protected void TVDept_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		if (this.TVDept.SelectedValue != "" && this.TVDept.SelectedValue.Substring(0, 1) != "r")
		{
			this.btnAdd.Disabled = false;
			this.HdnTemplateID.Value = this.TVDept.SelectedValue;
			this.ViewState["tid"] = this.TVDept.SelectedValue;
			DataTable dataTable = FlowTemplateAction.QueryOneTemplate(System.Convert.ToInt32(this.TVDept.SelectedValue));
			if (dataTable.Rows.Count > 0)
			{
				this.HdnBusinessClass.Value = dataTable.Rows[0]["BusinessClass"].ToString();
			}
			this.BindView(base.UserCode, System.Convert.ToInt32(this.TVDept.SelectedValue));
			DataTable fistDt = FlowAuditAction.GetFistDt(System.Convert.ToInt32(this.HdnTemplateID.Value.ToString()));
			if (fistDt.Rows.Count > 0 && fistDt.Rows[0][3].ToString() != "1")
			{
				string text = fistDt.Rows[0][2].ToString();
				this.hdflisDuty.Value = "1";
				this.hdflisName.Value = "1";
				if (!string.IsNullOrEmpty(text))
				{
					if (fistDt.Rows[0][1].ToString() == "2" || fistDt.Rows[0][1].ToString() == "1")
					{
						if (!FlowAuditAction.GetUsersis(text))
						{
							this.hdflisName.Value = "0";
						}
						else
						{
							this.hdflisName.Value = "1";
						}
					}
					else
					{
						if (fistDt.Rows[0][1].ToString() == "3" || fistDt.Rows[0][1].ToString() == "4" || fistDt.Rows[0][1].ToString() == "5")
						{
							DataTable role = FlowAuditAction.GetRole(text);
							if (role.Rows.Count <= 0)
							{
								this.hdflisDuty.Value = "0";
							}
							else
							{
								this.hdflisDuty.Value = "1";
							}
						}
					}
				}
			}
		}
		else
		{
			this.btnAdd.Disabled = true;
			DataTable dataSource = new DataTable();
			this.GVBook.DataSource = dataSource;
			this.GVBook.DataBind();
		}
		this.hdfNodeValue.Value = this.TVDept.SelectedValue;
	}
	private void templateBind(TreeNode node, string BusinessClass)
	{
		DataTable templateList = this.wfaction.GetTemplateList(BusinessClass, this.Session["CorpCode"].ToString(), base.UserCode);
		if (templateList.Rows.Count > 0)
		{
			foreach (DataRow dataRow in templateList.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = string.Concat(new object[]
				{
					"<span title=",
					dataRow["Remark"],
					">",
					dataRow["TemplateName"].ToString(),
					"</span>"
				});
				treeNode.Value = dataRow["TemplateID"].ToString();
				if (!string.IsNullOrEmpty(base.Request["tid"]) && treeNode.Value == base.Request["tid"])
				{
					treeNode.Select();
				}
				node.ChildNodes.Add(treeNode);
			}
		}
	}
	protected void BtnWFDel_Click(object sender, System.EventArgs e)
	{
		if (this.Session["twopass"] != null && this.Session["twopass"].ToString().Trim() == "IsAllowDel")
		{
			try
			{
				System.Guid guid = new System.Guid(this.HdnRecordID.Value);
				string value = this.HdnBusinessCode.Value;
				string value2 = this.HdnBusinessClass.Value;
				string sqlString = "select LinkTable,KeyWord,StateField,BusinessName from WF_BusinessCode  where BusinessCode=" + value;
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				string text = dataTable.Rows[0]["LinkTable"].ToString();
				string text2 = dataTable.Rows[0]["KeyWord"].ToString();
				dataTable.Rows[0]["StateField"].ToString();
				string text3 = dataTable.Rows[0]["BusinessName"].ToString();
				string text4 = " Begin ";
				object obj = text4;
				text4 = string.Concat(new object[]
				{
					obj,
					" delete from WF_Instance where ID IN (SELECT ID FROM WF_Instance_Main WHERE BusinessCode=",
					value,
					" AND BusinessClass=",
					value2,
					" AND InstanceCode='",
					guid,
					"')"
				});
				object obj2 = text4;
				text4 = string.Concat(new object[]
				{
					obj2,
					" DELETE  FROM WF_Instance_Main WHERE BusinessCode=",
					value,
					" AND BusinessClass=",
					value2,
					" AND InstanceCode='",
					guid,
					"'"
				});
				string sqlString2 = "select FatherId, TableName, line, linkLine,linkTable from WF_supperDelete where BusinessCode=" + value + " and  BussinessClass=" + value2;
				DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
				if (dataTable2.Rows.Count > 0)
				{
					if (dataTable2.Rows.Count == 1)
					{
						string text5 = dataTable2.Rows[0]["TableName"].ToString();
						string text6 = dataTable2.Rows[0]["line"].ToString();
						string text7 = dataTable2.Rows[0]["linkLine"].ToString();
						string text8 = dataTable2.Rows[0]["linkTable"].ToString();
						object obj3 = text4;
						text4 = string.Concat(new object[]
						{
							obj3,
							" delete from ",
							text5,
							" where ",
							text6,
							" in (select ",
							text7,
							" from ",
							text8,
							" where ",
							text2,
							"= '",
							guid,
							"')"
						});
					}
					else
					{
						int arg_2F1_0 = dataTable2.Rows.Count;
						for (int i = 0; i < dataTable2.Rows.Count; i++)
						{
							string text9 = dataTable2.Rows[i]["TableName"].ToString();
							string text10 = dataTable2.Rows[i]["line"].ToString();
							string text11 = dataTable2.Rows[i]["linkLine"].ToString();
							string text12 = dataTable2.Rows[0]["linkTable"].ToString();
							string a = dataTable2.Rows[0]["Fatherid"].ToString();
							if (a == "1")
							{
								object obj4 = text4;
								text4 = string.Concat(new object[]
								{
									obj4,
									" delete from ",
									text9,
									" where ",
									text10,
									" in (select ",
									text11,
									" from ",
									text12,
									" where ",
									text2,
									"= '",
									guid,
									"')"
								});
							}
							text4 += " ";
						}
					}
				}
				object obj5 = text4;
				text4 = string.Concat(new object[]
				{
					obj5,
					" DELETE  FROM ",
					text,
					" where ",
					text2,
					"= '",
					guid,
					"' "
				});
				text4 += " ";
				text4 += "end";
				string str = "超级删除失败！";
				if (publicDbOpClass.ExecuteSQL(text4) >= 1)
				{
					str = "超级删除成功";
				}
				myxml.SetTwoPWDlog(this.Session["yhdm"].ToString(), this.Page.Request.UserHostAddress.ToString(), text3.ToString(), guid.ToString(), "");
				base.Response.Redirect(base.Request.Url.ToString());
				this.Page.RegisterStartupScript("", "<script>alert('" + str + "');</script>");
			}
			catch
			{
				this.Page.RegisterStartupScript("", "<script>alert('超级删除失败！');</script>");
			}
		}
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.BindView(base.UserCode, System.Convert.ToInt32(this.ViewState["tid"].ToString()));
	}
}


