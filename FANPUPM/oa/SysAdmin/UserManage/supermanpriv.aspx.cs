using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SupermanPriv : BasePage, IRequiresSessionState
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
				userManageDb userManageDb = new userManageDb();
				DataTable dataTable = userManageDb.userQuaryDt(this.ViewState["YHDM"].ToString());
				string str = dataTable.Rows[0]["v_xm"].ToString();
				this.lblTitle.Text = "设置 " + str + " 的管理权限";
			}
		}
		public void ShowTaskList()
		{
			DataTable dt = new DataTable();
			dt = this.myUser.managePriv(this.ViewState["YHDM"].ToString());
			TableCell tableCell = new TableCell();
			TableRow tableRow = new TableRow();
			new CheckBox();
			tableCell.Text = "系统权限列表";
			tableRow.Cells.Add(tableCell);
			this.grd_ModuleList.Rows.Add(tableRow);
			this.CreateChild(dt, "", 0, "", false);
		}
		public void CreateChild(DataTable dt, string moduleCode, int level, string inherHead, bool isExpend)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			DataRow[] array = dt.Select(string.Concat(new string[]
			{
				"(c_mkdm like '",
				moduleCode.ToString(),
				"%')and(len(c_mkdm)= ",
				(moduleCode.Length + 2).ToString(),
				")"
			}));
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				tableRow = new TableRow();
				tableCell = new TableCell();
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
				tableCell.Height = Unit.Pixel(18);
				tableRow.Cells.Add(tableCell);
				tableRow.ID = moduleCode;
				tableRow.Attributes["id"] = moduleCode;
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
			if (this.myUser.managerUpdate(this.ViewState["YHDM"].ToString(), moduleCode))
			{
				base.RegisterScript("alert('保存成功! ');");
				base.CloseParent();
				return;
			}
			base.RegisterScript("alert('保存失败! ');");
			base.CloseParent();
		}
	}


