using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using FredCK.FCKeditorV2;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Help_helpupd : PageBase, System.Web.SessionState.IRequiresSessionState
{
	protected string helpId
	{
		get
		{
			return this.ViewState["helpID"].ToString();
		}
		set
		{
			this.ViewState["helpID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["id"] == null || base.Request["mc"] == null)
			{
				base.Response.Write("参数错误!");
				base.Response.End();
				return;
			}
			this.Txt_mc.Text = base.Request["mc"].ToString();
			this.helpId = base.Request["id"];
			if (this.helpId != "")
			{
				this.Data_Bind();
			}
		}
	}
	private void Data_Bind()
	{
		DataTable dataTable = publicDbOpClass.DataTableQuary("select * from [PT_HELP] where C_MKDM='" + this.helpId + "'");
		if (dataTable.Rows.Count > 0)
		{
			this.FCKeditor1.Value = dataTable.Rows[0]["txt_help"].ToString();
		}
	}
	public void Btn_save_Click(object sender, EventArgs e)
	{
		string text = this.FCKeditor1.Value.Replace("http://helpsel.aspx", "helpSel.aspx");
		string text2 = "begin  ";
		text2 = text2 + " if exists(select * from [PT_HELP] where C_MKDM='" + this.helpId + "')";
		string text3 = text2;
		text2 = string.Concat(new string[]
		{
			text3,
			" update [PT_HELP] set txt_help='",
			text,
			"',v_MKMC='",
			this.Txt_mc.Text,
			"' where C_MKDM='",
			this.helpId,
			"'"
		});
		string text4 = text2;
		text2 = string.Concat(new string[]
		{
			text4,
			" else INSERT INTO  [PT_HELP]([C_MKDM] ,[txt_help] ,[v_MKMC])VALUES('",
			this.helpId,
			"','",
			text,
			"','",
			this.Txt_mc.Text,
			"')"
		});
		text2 += " end ";
		int num = publicDbOpClass.ExecuteSQL(text2);
		if (num == 1)
		{
			this.JS.Text = "alert('保存成功!');window.returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
}


