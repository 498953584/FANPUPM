using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Human2 : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		private int _iSysID;


		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			SysManageDb sysManageDb = new SysManageDb();
			if (base.Request.QueryString.Count > 0)
			{
				try
				{
					this._iSysID = int.Parse(base.Request.QueryString["sysid"].ToString());
					goto IL_5F;
				}
				catch
				{
					goto IL_5F;
				}
			}
			this._iSysID = sysManageDb.GetDefault();
			IL_5F:
			this.HdnSysID.Value = this._iSysID.ToString();
			if (!base.IsPostBack)
			{
				int num = 0;
				try
				{
					num = int.Parse(base.Request.QueryString["deptid"].ToString());
				}
				catch
				{
				}
				if (num > 1)
				{
					userManageDb userManageDb = new userManageDb();
					DataSet dataSet = userManageDb.DepUserQuaryForMail(num.ToString());
					DataTable dataTable = dataSet.Tables[0];
					this.LBoxHuman.Items.Clear();
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(dataTable.Rows[i]["v_xm"].ToString(), dataTable.Rows[i]["v_yhdm"].ToString());
						this.LBoxHuman.Items.Add(item);
					}
				}
				else
				{
					if (num == -1)
					{
						MailManage mailManage = new MailManage();
						DataTable dataTable2 = mailManage.SelLinkman(this.Session["yhdm"].ToString());
						this.LBoxHuman.Items.Clear();
						for (int j = 0; j < dataTable2.Rows.Count; j++)
						{
							System.Web.UI.WebControls.ListItem item2 = new System.Web.UI.WebControls.ListItem(dataTable2.Rows[j]["v_xm"].ToString(), dataTable2.Rows[j]["v_yhdm"].ToString());
							this.LBoxHuman.Items.Add(item2);
						}
					}
				}
				this.SelectedList_Restore();
			}
		}
		private void SelectedList_Restore()
		{
			string text = this.Session["HumanCode2"].ToString();
			string text2 = this.Session["HumanName2"].ToString();
			string[] array = text.Split(new char[]
			{
				'!'
			});
			string[] array2 = text2.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length - 1; i++)
			{
				this.LBoxSelected.Items.Add(new System.Web.UI.WebControls.ListItem(array2[i], array[i]));
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			bool flag = false;
			for (int i = 0; i < this.LBoxHuman.Items.Count; i++)
			{
				if (this.LBoxHuman.Items[i].Selected)
				{
					string text = this.HdnSysID.Value.ToString() + ":" + this.LBoxHuman.Items[i].Value.ToString();
					string text2 = this.LBoxHuman.Items[i].Text.ToString();
					bool flag2 = false;
					for (int j = 0; j < this.LBoxSelected.Items.Count; j++)
					{
						if (text == this.LBoxSelected.Items[j].Value.ToString())
						{
							flag2 = true;
							break;
						}
					}
					for (int k = 0; k < this.LBoxSelected.Items.Count; k++)
					{
						if (this.HdnSysID.Value.ToString() == this.LBoxSelected.Items[k].Value.ToString().Split(new char[]
						{
							':'
						})[0].ToString())
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						System.Web.SessionState.HttpSessionState session;
						(session = this.Session)["System"] = session["System"] + this.HdnSysID.Value.ToString() + ",";
					}
					if (!flag2)
					{
						System.Web.SessionState.HttpSessionState session2;
						(session2 = this.Session)["HumanCode2"] = session2["HumanCode2"] + text + "!";
						System.Web.SessionState.HttpSessionState session3;
						(session3 = this.Session)["HumanName2"] = session3["HumanName2"] + text2 + ",";
						this.LBoxSelected.Items.Add(new System.Web.UI.WebControls.ListItem(text2, text));
					}
				}
			}
		}
		protected void BtnAddAll_Click(object sender, EventArgs e)
		{
			bool flag = false;
			for (int i = 0; i < this.LBoxSelected.Items.Count; i++)
			{
				if (this.HdnSysID.Value.ToString() == this.LBoxSelected.Items[i].Value.ToString().Split(new char[]
				{
					':'
				})[0].ToString())
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				System.Web.SessionState.HttpSessionState session;
				(session = this.Session)["System"] = session["System"] + this.HdnSysID.Value.ToString() + ",";
			}
			for (int j = 0; j < this.LBoxHuman.Items.Count; j++)
			{
				string text = this.HdnSysID.Value.ToString().Trim() + ":" + this.LBoxHuman.Items[j].Value.ToString();
				string text2 = this.LBoxHuman.Items[j].Text.ToString();
				bool flag2 = false;
				for (int k = 0; k < this.LBoxSelected.Items.Count; k++)
				{
					if (text == this.LBoxSelected.Items[k].Value.ToString())
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					System.Web.SessionState.HttpSessionState session2;
					(session2 = this.Session)["HumanCode2"] = session2["HumanCode2"] + text + "!";
					System.Web.SessionState.HttpSessionState session3;
					(session3 = this.Session)["HumanName2"] = session3["HumanName2"] + text2 + ",";
					this.LBoxSelected.Items.Add(new System.Web.UI.WebControls.ListItem(text2, text));
				}
			}
		}
		protected void BtnRemoveAll_Click(object sender, EventArgs e)
		{
			this.Session["System"] = "";
			this.Session["HumanCode2"] = "";
			this.Session["HumanName2"] = "";
			this.LBoxSelected.Items.Clear();
		}
		protected void BtnRemove_Click(object sender, EventArgs e)
		{
			for (int i = this.LBoxSelected.Items.Count - 1; i >= 0; i--)
			{
				if (this.LBoxSelected.Items[i].Selected)
				{
					bool flag = false;
					string text = this.LBoxSelected.Items[i].Value.ToString().Split(new char[]
					{
						':'
					})[0].ToString();
					this.LBoxSelected.Items.RemoveAt(i);
					for (int j = this.LBoxSelected.Items.Count - 1; j >= 0; j--)
					{
						if (text == this.LBoxSelected.Items[j].Value.ToString().Split(new char[]
						{
							':'
						})[0].ToString())
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						string[] array = this.Session["System"].ToString().Split(new char[]
						{
							','
						});
						this.Session["System"] = "";
						for (int k = 0; k < array.Length - 1; k++)
						{
							if (array[k].ToString() != text)
							{
								System.Web.SessionState.HttpSessionState session;
								(session = this.Session)["System"] = session["System"] + array[k].ToString() + ",";
							}
						}
					}
				}
			}
			this.HumanList_Reset();
		}
		private void HumanList_Reset()
		{
			this.Session["HumanCode2"] = "";
			this.Session["HumanName2"] = "";
			for (int i = 0; i < this.LBoxSelected.Items.Count; i++)
			{
				System.Web.SessionState.HttpSessionState session;
				(session = this.Session)["HumanCode2"] = session["HumanCode2"] + this.LBoxSelected.Items[i].Value.ToString() + "!";
				System.Web.SessionState.HttpSessionState session2;
				(session2 = this.Session)["HumanName2"] = session2["HumanName2"] + this.LBoxSelected.Items[i].Text.ToString() + ",";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			userManageDb userManageDb = new userManageDb();
			DataTable searchResult = userManageDb.getSearchResult(this.txtName.Text.Trim());
			this.LBoxHuman.Items.Clear();
			for (int i = 0; i < searchResult.Rows.Count; i++)
			{
				System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(searchResult.Rows[i]["v_xm"].ToString(), searchResult.Rows[i]["v_yhdm"].ToString());
				this.LBoxHuman.Items.Add(item);
			}
		}
	}


