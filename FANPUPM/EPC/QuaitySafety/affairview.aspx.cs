using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Web;
public partial class FileView
{
    public static string FilesBind(int moduleID, string recordCode)
    {
        StringBuilder builder = new StringBuilder();
        DataTable table = com.jwsoft.pm.data.publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexList where (RecordCode = '" + recordCode + "' and ModuleID = " + moduleID.ToString() + "  and state<>-1)");
        int count = table.Rows.Count;
        string str2 = string.Empty;
        builder.Append("<div   Style=\" word-break:break-all; width:90%;\">");
        for (int i = 0; i < table.Rows.Count; i++)
        {
            DataRow row = table.Rows[i];
            str2 = "<a href='/Common/DownLoad2.aspx?path=" + HttpUtility.UrlEncode(row["FilePath"].ToString() + row["AnnexName"].ToString()) + "&Name=" + HttpUtility.UrlEncode(row["OriginalName"].ToString()) + "'>" + row["OriginalName"].ToString() + "</a>";
            builder.Append(str2);
            if (((i + 1) % 3) == 0)
            {
                builder.Append("</br></br>");
            }
            else
            {
                builder.Append(",");
            }
        }
        string str3 = string.Empty;
        if (builder.Length > 2)
        {
            str3 = builder.ToString().Substring(0, builder.Length - 1);
        }
        return (str3 + "</div>");
    }
}


public partial class EPC_QuaitySafety_affairview : NBasePage, IRequiresSessionState
{
	public static string _showAffairTitle = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			if (base.Request.QueryString["Flag"] == "Q")
			{
				EPC_QuaitySafety_affairview._showAffairTitle = "质量事务名称";
			}
			else
			{
				if (base.Request.QueryString["Flag"] == "S")
				{
					EPC_QuaitySafety_affairview._showAffairTitle = "安全事务名称";
				}
			}
			AffairAction affairAction = new AffairAction();
			AffairModel singleAffair = affairAction.GetSingleAffair(base.Request.QueryString["i_id"].ToString());
			this.litContent.Text = singleAffair.Context;
			this.litRemark.Text = singleAffair.Remark;
			DateTime arg_C0_0 = singleAffair.Date;
			if (singleAffair.Date.ToString() != "")
			{
				this.litDate.Text = singleAffair.Date.ToShortDateString().Replace("/", "-");
			}
			this.litName.Text = singleAffair.AffairTitle;
			int arg_120_0 = singleAffair.Mark;
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
			int arg_1C3_0 = singleAffair.FilesType;
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
}


