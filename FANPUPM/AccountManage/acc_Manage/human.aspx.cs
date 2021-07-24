using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SalaryManage_SetSalary_human : BasePage, IRequiresSessionState
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
			userManageDb userManageDb = new userManageDb();
			DataSet dataSet = userManageDb.DepUserQuaryForMail(num.ToString());
			DataTable dataTable = dataSet.Tables[0];
			this.LBoxHuman.Items.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				ListItem item = new ListItem(dataTable.Rows[i]["v_xm"].ToString(), dataTable.Rows[i]["v_yhdm"].ToString());
				this.LBoxHuman.Items.Add(item);
			}
			this.SelectedList_Restore();
		}
	}
	private void SelectedList_Restore()
	{
		string text = this.Session["HumanCode"].ToString();
		string text2 = this.Session["HumanName"].ToString();
		string[] array = text.Split(new char[]
		{
			','
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
	protected void BtnAdd_Click(object sender, EventArgs e)
	{
		bool flag = false;
		for (int i = 0; i < this.LBoxHuman.Items.Count; i++)
		{
			if (this.LBoxHuman.Items[i].Selected)
			{
				string text = this.LBoxHuman.Items[i].Value.ToString();
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
					if (this.HdnSysID.Value.ToString() == this.LBoxSelected.Items[k].Value.ToString())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					HttpSessionState session;
					(session = this.Session)["System"] = session["System"] + this.HdnSysID.Value.ToString() + ",";
				}
				if (!flag2)
				{
					HttpSessionState session2;
					(session2 = this.Session)["HumanCode"] = session2["HumanCode"] + text + ",";
					HttpSessionState session3;
					(session3 = this.Session)["HumanName"] = session3["HumanName"] + text2 + ",";
					this.LBoxSelected.Items.Add(new ListItem(text2, text));
				}
			}
		}
	}
	protected void BtnAddAll_Click(object sender, EventArgs e)
	{
		bool flag = false;
		for (int i = 0; i < this.LBoxSelected.Items.Count; i++)
		{
			if (this.HdnSysID.Value.ToString() == this.LBoxSelected.Items[i].Value.ToString())
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			HttpSessionState session;
			(session = this.Session)["System"] = session["System"] + this.HdnSysID.Value.ToString() + ",";
		}
		for (int j = 0; j < this.LBoxHuman.Items.Count; j++)
		{
			string text = this.LBoxHuman.Items[j].Value.ToString();
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
				HttpSessionState session2;
				(session2 = this.Session)["HumanCode"] = session2["HumanCode"] + text + ",";
				HttpSessionState session3;
				(session3 = this.Session)["HumanName"] = session3["HumanName"] + text2 + ",";
				this.LBoxSelected.Items.Add(new ListItem(text2, text));
			}
		}
	}
	protected void BtnRemoveAll_Click(object sender, EventArgs e)
	{
		this.Session["System"] = "";
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
				bool flag = false;
				string text = this.LBoxSelected.Items[i].Value.ToString();
				this.LBoxSelected.Items.RemoveAt(i);
				for (int j = this.LBoxSelected.Items.Count - 1; j >= 0; j--)
				{
					if (text == this.LBoxSelected.Items[j].Value.ToString())
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
							HttpSessionState session;
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
		this.Session["HumanCode"] = "";
		this.Session["HumanName"] = "";
		for (int i = 0; i < this.LBoxSelected.Items.Count; i++)
		{
			HttpSessionState session;
			(session = this.Session)["HumanCode"] = session["HumanCode"] + this.LBoxSelected.Items[i].Value.ToString() + ",";
			HttpSessionState session2;
			(session2 = this.Session)["HumanName"] = session2["HumanName"] + this.LBoxSelected.Items[i].Text.ToString() + ",";
		}
	}
}


