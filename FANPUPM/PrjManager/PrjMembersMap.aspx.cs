using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class PrjManager_PrjMembersMap : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.GetData();
		}
	}
	protected void GetData()
	{
		DataTable projectInfo = ProjectInfo.GetProjectInfo(base.UserCode);
		DataColumn column = new DataColumn("MemberName", typeof(string));
		projectInfo.Columns.Add(column);
		foreach (DataRow dataRow in projectInfo.Rows)
		{
			int membersCount = PrjMember.GetMembersCount(dataRow["PrjGuid"].ToString(), "", "", "", "", "");
			if (membersCount > 0)
			{
				DataTable members = PrjMember.GetMembers(dataRow["PrjGuid"].ToString(), "", "", "", "", "", 1, membersCount);
				string text = string.Empty;
				if (members.Rows.Count > 0)
				{
					for (int i = 0; i < members.Rows.Count; i++)
					{
						if (i > 0)
						{
							if ((i + 1) % 8 == 0)
							{
								text = text + members.Rows[i]["MemberName"].ToString() + ",<br>";
							}
							else
							{
								text = text + members.Rows[i]["MemberName"].ToString() + ",";
							}
						}
						else
						{
							text = text + members.Rows[i]["MemberName"].ToString() + ",";
						}
					}
					text = text.Substring(0, text.Length - 1);
					dataRow["MemberName"] = text;
				}
			}
		}
		string text2 = "";
		if (projectInfo.Rows.Count > 0)
		{
			text2 = "[";
			foreach (DataRow dataRow2 in projectInfo.Rows)
			{
				string text3 = dataRow2["City"].ToString();
				string text4 = dataRow2["PrjPlace"].ToString();
				string text5 = dataRow2["PrjCode"].ToString();
				string text6 = dataRow2["PrjName"].ToString();
				string text7 = dataRow2["PrjState"].ToString();
				string text8 = dataRow2["MemberName"].ToString();
				if (string.IsNullOrEmpty(text4))
				{
					text4 = text3;
				}
				if (!string.IsNullOrEmpty(text3))
				{
					if (!PrjMember.IsPrimit(dataRow2[0].ToString(), base.UserCode) && PrjMember.GetFlowState(dataRow2[0].ToString()) != "1")
					{
						text8 = string.Empty;
					}
					string text9 = text2;
					text2 = string.Concat(new string[]
					{
						text9,
						"{'prjCode':'",
						text5,
						"','prjName':'",
						text6,
						"','city':'",
						text3,
						"','prjPrice':'",
						text4,
						"','prjState':'",
						text7,
						"','members':'",
						text8,
						"'},"
					});
				}
			}
			text2 = text2.Remove(text2.Length - 1);
			text2 += "]";
		}
		this.hfldPrjInfo.Value = text2;
	}
}


