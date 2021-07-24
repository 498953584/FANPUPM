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
	public partial class UserPriv : BasePage, IRequiresSessionState
	{
		private userManageDb myUser = new userManageDb();
		private string[] moduleCodeList;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.moduleCodeList = this.hdn_ModuleCodeList.Value.Split(new char[]
			{
				','
			});
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["yhdm"] != null)
				{
					this.ViewState["YHDM"] = base.Request.QueryString["yhdm"].ToString();
				}
				this.ShowTaskList();
				this.grd_ModuleList.Style["table-layout"] = "fixed";
				this.btn_Save.Attributes["onclick"] = "collectModules();";
			}
		}
		public void ShowTaskList()
		{
			DataTable dt = new DataTable();
			if (this.Session["yhdm"].ToString() == "00000000")
			{
				dt = this.myUser.GetUserPurv(this.ViewState["YHDM"].ToString());
			}
			else
			{
				dt = this.myUser.GetUserPurv(this.ViewState["YHDM"].ToString(), this.Session["yhdm"].ToString());
			}
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
				tableRow.Attributes["pid"] = moduleCode;
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
				string arg = dataRow["c_mkdm"].ToString();
				string text = string.Format("<input type=\"checkbox\" id=\"{0}\" name=\"cbn{1}\" value=\"{0}\" ", arg, moduleCode);
				if (dataRow["IsHave"].ToString() != "0")
				{
					text += " checked";
				}
				if (dataRow["IsManagerFixed"].ToString() == "0")
				{
					text += " disabled";
				}
				text += string.Format(" onclick=\"cbClick(this);\" />{0}", dataRow["v_mkmc"].ToString());
				tableCell.Text = inherHead + this.getHeadImg(i, num, dataRow) + text;
				tableCell.Attributes["nowrap"] = "nowrap";
				tableCell.Height = Unit.Pixel(18);
				tableRow.Cells.Add(tableCell);
				tableRow.ID = moduleCode;
				if (level == 0)
				{
					tableRow.Style["display"] = "";
					isExpend = true;
				}
				else
				{
					if (isExpend)
					{
						tableRow.Style["display"] = "";
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
					if (dataRow["IsManagerFixed"].ToString() == "0")
					{
						text2 += " disabled ";
					}
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
						result = "<img src=\"images/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','t');\">";
					}
					else
					{
						result = "<img src=\"images/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','t');\">";
					}
				}
				else
				{
					result = "<img src=\"images/t.gif\" align=\"absmiddle\">";
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
							result = "<img src=\"images/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','l');\">";
						}
						else
						{
							result = "<img src=\"images/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','l');\">";
						}
					}
					else
					{
						result = "<img src=\"images/l.gif\" align=\"absmiddle\">";
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
				str = "<img src=\"images/white.gif\" align=\"absmiddle\">";
			}
			else
			{
				str = "<img src=\"images/i.gif\" align=\"absmiddle\">";
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
			if (this.myUser.updateRolePriv(scopeCode, moduleCode, this.ViewState["YHDM"].ToString()))
			{
				myxml.Setmenu(this.ViewState["YHDM"].ToString());
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_userlist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败');");
		}
	}


