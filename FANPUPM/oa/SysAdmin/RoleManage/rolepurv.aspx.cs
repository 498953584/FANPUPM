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
	public partial class RolePurv : BasePage, IRequiresSessionState
	{
		private string[] moduleCodeList;
		private roleManageDb myRole = new roleManageDb();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.moduleCodeList = this.hdn_ModuleCodeList.Value.Split(new char[]
			{
				','
			});
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["id"] != null)
				{
					this.ViewState["ID"] = base.Request.QueryString["id"].ToString();
				}
				this.ShowTaskList();
				this.grd_ModuleList.Style["table-layout"] = "fixed";
				this.btn_Save.Attributes["onclick"] = "collectModules();";
			}
		}
		public void ShowTaskList()
		{
			DataTable dt = new DataTable();
			dt = this.myRole.GetRoleModules(this.ViewState["ID"].ToString());
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			TableRow tableRow = new TableRow();
			new CheckBox();
			tableCell.Text = "系统权限列表";
			tableCell2.Text = "&nbsp;";
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(tableCell2);
			this.grd_ModuleList.Rows.Add(tableRow);
			this.CreateChild(dt, "", 0, "", false);
		}
		public void CreateChild(DataTable dt, string moduleCode, int level, string inherHead, bool isExpend)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			DataRow[] array = dt.Select(string.Concat(new string[]
			{
				"(c_mkdm like '",
				moduleCode.ToString(),
				"%')and(len(c_mkdm)= ",
				(moduleCode.Length + 2).ToString(),
				")"
			}), "i_xh,c_mkdm");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell2 = new TableCell();
				bool flag = false;
				DataRow dataRow = array[i];
				for (int j = 0; j < this.moduleCodeList.Length; j++)
				{
					if (this.moduleCodeList[j] == dataRow["c_mkdm"].ToString())
					{
						flag = true;
						break;
					}
				}
				string text;
				if (dataRow["IsBasic"].ToString() == "1")
				{
					text = string.Concat(new string[]
					{
						"<input type=\"checkbox\" id=\"cb",
						dataRow["c_mkdm"].ToString(),
						"\" name=\"cbn",
						moduleCode,
						"\" value=\"",
						dataRow["c_mkdm"].ToString(),
						"\""
					});
					text += " checked disabled ";
					text = text + " onclick=\"cbClick(this);\">" + dataRow["v_mkmc"].ToString();
				}
				else
				{
					text = string.Concat(new string[]
					{
						"<input type=\"checkbox\" id=\"cb",
						dataRow["c_mkdm"].ToString(),
						"\" name=\"cbn",
						moduleCode,
						"\" value=\"",
						dataRow["c_mkdm"].ToString(),
						"\""
					});
					text += ((dataRow["IsHave"].ToString() == "0") ? "" : "checked");
					text = text + " onclick=\"cbClick(this);\">" + dataRow["v_mkmc"].ToString();
				}
				tableCell.Text = inherHead + this.getHeadImg(i, num, dataRow) + text;
				tableCell.Attributes["nowrap"] = "nowrap";
				tableCell.Height = Unit.Pixel(20);
				tableRow.Cells.Add(tableCell);
				tableRow.ID = moduleCode;
				if (level == 0)
				{
					tableRow.Style["display"] = "block";
					isExpend = true;
				}
				else
				{
					if (isExpend)
					{
						tableRow.Style["display"] = "block";
					}
					else
					{
						tableRow.Style["display"] = "none";
					}
				}
				string text2;
				if (dataRow["IsMaintainable"].ToString() == "1")
				{
					text2 = string.Concat(new string[]
					{
						"<input type=\"checkbox\" id=\"cbMaintainable",
						dataRow["c_mkdm"].ToString(),
						"\" name=\"cbMaintainablen",
						moduleCode,
						"\" value=\"",
						dataRow["c_mkdm"].ToString(),
						"\""
					});
					text2 += " onclick=\"cbClick(this);\" ";
					text2 += ((dataRow["IsHaveOp"].ToString() == "0") ? "" : "checked");
					text2 += ">维护权限";
				}
				else
				{
					text2 = "&nbsp;";
				}
				tableCell2.Text = text2;
				tableCell2.Attributes["nowrap"] = "nowrap";
				tableCell2.Width = Unit.Pixel(70);
				tableRow.Cells.Add(tableCell2);
				this.grd_ModuleList.Rows.Add(tableRow);
				this.CreateChild(dt, dataRow["c_mkdm"].ToString(), level + 1, this.getInherImg(i, num, inherHead), isExpend && flag);
			}
		}
		public string getHeadImg(int currentIndex, int length, DataRow dr)
		{
			string result = "";
			bool flag = false;
			if (currentIndex != length - 1)
			{
				if ((int)dr["i_childnum"] > 0)
				{
					for (int i = 0; i < this.moduleCodeList.Length; i++)
					{
						if (this.moduleCodeList[i] == dr["c_mkdm"].ToString())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','t');\">";
					}
					else
					{
						result = "<img src=\"../../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','t');\">";
					}
				}
				else
				{
					result = "<img src=\"../../../images/tree/t.gif\" align=\"absmiddle\">";
				}
			}
			else
			{
				if (currentIndex == length - 1)
				{
					if ((int)dr["i_childnum"] > 0)
					{
						for (int j = 0; j < this.moduleCodeList.Length; j++)
						{
							if (this.moduleCodeList[j] == dr["c_mkdm"].ToString())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','l');\">";
						}
						else
						{
							result = "<img src=\"../../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','l');\">";
						}
					}
					else
					{
						result = "<img src=\"../../../images/tree/l.gif\" align=\"absmiddle\">";
					}
				}
			}
			return result;
		}
		public string getInherImg(int currentIndex, int length, string inherHead)
		{
			string str;
			if (currentIndex == length - 1)
			{
				str = "<img src=\"../../../images/tree/white.gif\" align=\"absmiddle\">";
			}
			else
			{
				str = "<img src=\"../../../images/tree/i.gif\" align=\"absmiddle\">";
			}
			return inherHead + str;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btn_Save_Click(object sender, EventArgs e)
		{
			string moduleCode = this.hdn_Modules.Value.ToString().Trim(new char[]
			{
				','
			});
			string scopeCode = this.hdn_Scope.Value.ToString().Trim(new char[]
			{
				','
			});
			if (this.myRole.UpdateRolePrivilage(this.ViewState["ID"].ToString(), scopeCode, moduleCode))
			{
				string script = "\r\n\t\t\t\t\t<script>\r\n\t\t\t\t\t\twindow.alert('保存成功！');\r\n\t\t\t\t\t\twindow.returnValue=true;\r\n\t\t\t\t\t\twindow.parent.close();\r\n\t\t\t\t\t</script>\r\n\t\t\t\t";
				base.ClientScript.RegisterStartupScript(base.GetType(), "success", script);
				return;
			}
			string script2 = "\r\n\t\t\t\t\t<script>\r\n\t\t\t\t\t\twindow.alert('保存失败！');\r\n\t\t\t\t\t</script>\r\n\t\t\t\t";
			base.ClientScript.RegisterStartupScript(base.GetType(), "error", script2);
		}
	}


