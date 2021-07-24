using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Human : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
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
				if (num != 0 && num != 1)
				{
					userManageDb userManageDb = new userManageDb();
					DataSet dataSet = userManageDb.DepUserQuaryForMail(num.ToString());
					DataTable dataTable = dataSet.Tables[0];
					this.LBoxHuman.Items.Clear();
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						ListItem item = new ListItem(dataTable.Rows[i]["v_xm"].ToString(), dataTable.Rows[i]["v_yhdm"].ToString());
						this.LBoxHuman.Items.Add(item);
					}
				}
				this.SelectedList_Restore();
			}
		}
		private void SelectedList_Restore()
		{
			if (this.Session["HumanCode"] != null)
			{
				string text = this.Session["HumanCode"].ToString();
				string text2 = this.Session["HumanName"].ToString();
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
					this.LBoxSelected.Items.Add(new ListItem(array2[i], array[i]));
				}
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.LBoxHuman.Items.Count; i++)
			{
				if (this.LBoxHuman.Items[i].Selected)
				{
					string text = this.LBoxHuman.Items[i].Value.ToString();
					string text2 = this.LBoxHuman.Items[i].Text.ToString();
					bool flag = false;
					for (int j = 0; j < this.LBoxSelected.Items.Count; j++)
					{
						if (text == this.LBoxSelected.Items[j].Value.ToString())
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						HttpSessionState session;
						(session = this.Session)["HumanCode"] = session["HumanCode"] + text + "!";
						HttpSessionState session2;
						(session2 = this.Session)["HumanName"] = session2["HumanName"] + text2 + ",";
						this.LBoxSelected.Items.Add(new ListItem(text2, text));
					}
				}
			}
		}
		protected void BtnAddAll_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.LBoxHuman.Items.Count; i++)
			{
				string text = this.LBoxHuman.Items[i].Value.ToString();
				string text2 = this.LBoxHuman.Items[i].Text.ToString();
				bool flag = false;
				for (int j = 0; j < this.LBoxSelected.Items.Count; j++)
				{
					if (text == this.LBoxSelected.Items[j].Value.ToString())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					HttpSessionState session;
					(session = this.Session)["HumanCode"] = session["HumanCode"] + text + "!";
					HttpSessionState session2;
					(session2 = this.Session)["HumanName"] = session2["HumanName"] + text2 + ",";
					this.LBoxSelected.Items.Add(new ListItem(text2, text));
				}
			}
		}
		protected void BtnRemoveAll_Click(object sender, EventArgs e)
		{
			this.Session["HumanCode"] = "";
			this.Session["HumanName"] = "";
			this.LBoxSelected.Items.Clear();
		}
		protected void BtnRemove_Click(object sender, EventArgs e)
		{
			for (int i = this.LBoxSelected.Items.Count - 1; i >= 0; i--)
			{
				if (this.LBoxSelected.Items[i].Selected)
				{
					string a = this.LBoxSelected.Items[i].Value.ToString().Split(new char[]
					{
						':'
					})[0].ToString();
					this.LBoxSelected.Items.RemoveAt(i);
					int num = this.LBoxSelected.Items.Count - 1;
					while (num >= 0 && !(a == this.LBoxSelected.Items[num].Value.ToString().Split(new char[]
					{
						':'
					})[0].ToString()))
					{
						num--;
					}
				}
			}
			this.HumanList_Reset();
		}
		private void HumanList_Reset()
		{
			this.Session["HumanCode"] = "";
			this.Session["HumanName"] = "";
			for (int i = 0; i < this.LBoxSelected.Items.Count; i++)
			{
				HttpSessionState session;
				(session = this.Session)["HumanCode"] = session["HumanCode"] + this.LBoxSelected.Items[i].Value.ToString() + "!";
				HttpSessionState session2;
				(session2 = this.Session)["HumanName"] = session2["HumanName"] + this.LBoxSelected.Items[i].Text.ToString() + ",";
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
				ListItem item = new ListItem(searchResult.Rows[i]["v_xm"].ToString(), searchResult.Rows[i]["v_yhdm"].ToString());
				this.LBoxHuman.Items.Add(item);
			}
		}
	}


