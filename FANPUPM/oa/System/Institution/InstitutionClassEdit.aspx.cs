using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Enterprise_InstitutionClassEdit : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BtnSave.Attributes["onclick"] = "return ValidTXT();";
			if (base.Request.QueryString["op"] == null || base.Request.QueryString["lc"] == null)
			{
				this.RegisterStartupScript("alert1", "<script>alert('参数错误！');</script>");
				return;
			}
			if (base.Request.QueryString["op"] == "Edit")
			{
				this.Show_Data();
			}
			else
			{
				if (base.Request.QueryString["op"] == "Add")
				{
					this.RblPer.Items[2].Attributes["onclick"] = "return setPurviewPerson('','');";
				}
			}
			this.HdnLc.Value = base.Request.QueryString["lc"];
			this.RblPer.Items[1].Attributes["onclick"] = "return setPurview();";
		}
	}
	protected void Show_Data()
	{
		if (base.Request.QueryString["lc"].ToString() != "")
		{
			string sqlString = " select * from InstitutionClass where levecode='" + base.Request.QueryString["lc"] + "' ";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				this.txtClassName.Text = dataTable.Rows[0]["ClassName"].ToString();
				this.txtClassCode.Text = dataTable.Rows[0]["ClassCode"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["Remark"].ToString();
				this.HdnPer.Value = dataTable.Rows[0]["PermissionSet"].ToString();
				if (dataTable.Rows[0]["PermissionClass"].ToString() == "1")
				{
					this.RblPer.Items[2].Attributes["onclick"] = string.Concat(new string[]
					{
						"return setPurviewPerson('",
						base.Request.QueryString["ucode"],
						"','",
						base.Request.QueryString["un"],
						"');"
					});
				}
				else
				{
					this.RblPer.Items[2].Attributes["onclick"] = "return setPurviewPerson('','');";
				}
				try
				{
					if ((bool)dataTable.Rows[0]["ReadOnly"])
					{
						this.radlReadOnly.SelectedIndex = 0;
					}
					else
					{
						this.radlReadOnly.SelectedIndex = 1;
					}
				}
				catch
				{
				}
				this.RblPer.SelectedValue = dataTable.Rows[0]["PermissionClass"].ToString();
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		int num = -1;
		if (this.RblPer.SelectedValue == "0")
		{
			num = 0;
		}
		else
		{
			if (this.RblPer.SelectedValue == "1")
			{
				num = 1;
			}
		}
		if (num == -1)
		{
			this.HdnPer.Value = "";
		}
		if (!(base.Request.QueryString["op"] == "Add"))
		{
			if (base.Request.QueryString["op"] == "Edit")
			{
				string text = string.Concat(new object[]
				{
					" update InstitutionClass set ClassName='",
					this.txtClassName.Text,
					"',ClassCode='",
					this.txtClassCode.Text,
					"',Remark='",
					this.txtRemark.Text,
					"',PermissionClass=",
					num,
					",PermissionSet='",
					this.HdnPer.Value,
					"',ReadOnly='",
					this.radlReadOnly.SelectedValue,
					"' "
				});
				text = text + " where levecode='" + base.Request.QueryString["lc"] + "' ";
				if (publicDbOpClass.ExecuteSQL(text) > 0)
				{
					base.RegisterScript("successed();");
					return;
				}
				base.RegisterScript("top.ui.show( '保存失败')");
			}
			return;
		}
		string text2 = " insert into InstitutionClass(ClassName,ClassCode,LeveCode,Remark,PermissionClass,PermissionSet,writePerson,writetime, ReadOnly) ";
		object obj = text2;
		text2 = string.Concat(new object[]
		{
			obj,
			" values('",
			this.txtClassName.Text,
			"','",
			this.txtClassCode.Text,
			"','",
			this.GetNewLeveCode(base.Request.QueryString["lc"]),
			"','",
			this.txtRemark.Text,
			"',",
			num,
			",'",
			this.HdnPer.Value,
			"','",
			this.Session["yhdm"].ToString(),
			"','",
			DateTime.Now.ToShortDateString(),
			"','",
			this.radlReadOnly.SelectedValue,
			"')"
		});
		if (publicDbOpClass.ExecuteSQL(text2) > 0)
		{
			base.RegisterScript("successed();");
			return;
		}
		base.RegisterScript("top.ui.show( '保存失败')");
	}
	protected string GetNewLeveCode(string leveCode)
	{
		int length = leveCode.Trim().Length;
		string sqlString = string.Concat(new object[]
		{
			" select max(LeveCode) from InstitutionClass where levecode like '",
			leveCode,
			"%' and len(levecode)='",
			length + 3,
			"' "
		});
		object obj = publicDbOpClass.ExecuteScalar(sqlString);
		string text;
		if (obj != DBNull.Value)
		{
			text = obj.ToString().Substring(length, 3);
			text = (int.Parse(text) + 1).ToString();
			text = text.PadLeft(3, '0');
			text = leveCode.Trim() + text;
		}
		else
		{
			text = leveCode + "001";
		}
		return text;
	}
}


