using System;
using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_QuaitySafety_ProjectSuperviseView : NBasePage, IRequiresSessionState
{
	public static string _showAffairTitle = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request.QueryString["i_id"] != null && base.Request.QueryString["i_id"].ToString() != "")
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			AffairAction affairAction = new AffairAction();
			AffairModel singleAffair = affairAction.GetSingleAffair(base.Request.QueryString["i_id"].ToString());
			EPC_QuaitySafety_ProjectSuperviseView._showAffairTitle = this.swTitle(singleAffair.CA.ToString(), base.Request.QueryString["Flag"].ToString());
			this.litContent.Text = singleAffair.Context;
			this.litRemark.Text = singleAffair.Remark;
			DateTime arg_DE_0 = singleAffair.Date;
			if (singleAffair.Date.ToString() != "")
			{
				this.litDate.Text = singleAffair.Date.ToShortDateString().Replace("/", "-");
			}
			this.litName.Text = singleAffair.AffairTitle;
			int arg_13F_0 = singleAffair.Mark;
			if (singleAffair.Mark.ToString() != "")
			{
				this.hdnmark.Value = singleAffair.Mark.ToString();
				if (singleAffair.Mark == 2)
				{
					this.litGD.Text = "否";
				}
				else
				{
					this.litGD.Text = "是";
				}
			}
			else
			{
				this.litGD.Text = "否";
			}
			this.Literal1.Text = FileView.FilesBind(1755, singleAffair.i_id.ToString());
			int arg_1E2_0 = singleAffair.FilesType;
			if (singleAffair.FilesType.ToString() != "" && singleAffair.FilesType > 0)
			{
				this.DDTClass.SelectedValue = singleAffair.FilesType.ToString();
				if (this.DDTClass.SelectedItem.Text != null && this.DDTClass.SelectedItem.Text.ToString() != "")
				{
					this.litType.Text = this.DDTClass.SelectedItem.Text.ToString();
				}
			}
		}
	}
	public string swTitle(string v, string QS)
	{
		string result = string.Empty;
		if (v != null)
		{
			if (!(v == "1"))
			{
				if (!(v == "2"))
				{
					if (!(v == "3"))
					{
						if (!(v == "4"))
						{
							if (!(v == "5"))
							{
								if (v == "6")
								{
									if (QS == "S")
									{
										result = "安全目标";
										EPC_QuaitySafety_ProjectSuperviseView._showAffairTitle = "安全目标名称";
									}
								}
							}
							else
							{
								if (QS == "S")
								{
									result = "事故报告";
									EPC_QuaitySafety_ProjectSuperviseView._showAffairTitle = "安全事故记录名称";
								}
							}
						}
						else
						{
							if (QS == "S")
							{
								result = "安全监督记录";
								EPC_QuaitySafety_ProjectSuperviseView._showAffairTitle = "安全检查记录名称";
							}
						}
					}
					else
					{
						if (QS == "Q")
						{
							result = "工程质量竣工验收资料";
							EPC_QuaitySafety_ProjectSuperviseView._showAffairTitle = "资料名称";
						}
					}
				}
				else
				{
					if (QS == "Q")
					{
						result = "事故记录";
						EPC_QuaitySafety_ProjectSuperviseView._showAffairTitle = "事故记录名称";
					}
				}
			}
			else
			{
				if (QS == "Q")
				{
					result = "质量检查记录";
					EPC_QuaitySafety_ProjectSuperviseView._showAffairTitle = "检查记录名称";
				}
			}
		}
		return result;
	}
	public string FilesBind(int moduleID, string recordCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int arg_53_0 = dataTable.Rows.Count;
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text = string.Empty;
			text = dataRow["OriginalName"].ToString();
			text = string.Concat(new object[]
			{
				"<span class=\"link\" onclick=\"javascript:return openannexpage('",
				dataRow["RecordCode"],
				"',",
				dataRow["ModuleID"],
				")\" title=\"",
				text,
				"\">",
				text,
				"</span>"
			});
			stringBuilder.Append(text);
			stringBuilder.Append(", ");
		}
		string result = string.Empty;
		if (stringBuilder.Length > 2)
		{
			result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
		}
		return result;
	}
}


