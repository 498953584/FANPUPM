using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_MailAdmin_DBMailView : BasePage, IRequiresSessionState
{

	private int _iMailID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["IMAILID"]);
		}
		set
		{
			this.ViewState["IMAILID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request["rid"] != null)
		{
			this._iMailID = Convert.ToInt32(base.Request["rid"]);
			this.RestoreMail(this._iMailID, this.Session["yhdm"].ToString());
			PTDBSJAction pTDBSJAction = new PTDBSJAction();
			pTDBSJAction.Delete(string.Concat(new object[]
			{
				" v_lxbm = '008' and v_YHDM = '",
				this.Session["yhdm"].ToString(),
				"' and i_XGID = '",
				this._iMailID,
				"'"
			}), 1);
			try
			{
				MailManage mailManage = new MailManage();
				mailManage.ReadMail(this._iMailID, this.Session["yhdm"].ToString());
			}
			catch
			{
			}
		}
	}
	private void RestoreMail(int iMailID, string strSenderCode)
	{
		MailManage mailManage = new MailManage();
		DataTable oneMail = mailManage.GetOneMail(iMailID, strSenderCode);
		if (oneMail.Rows.Count > 0)
		{
			int.Parse(oneMail.Rows[0]["i_SysID"].ToString());
			this.LabSender.Text = oneMail.Rows[0]["v_fxrxm"].ToString();
			this.LabConsignee.Text = this.sub(oneMail.Rows[0]["v_SJR"].ToString());
			this.LbCSR.Text = this.sub(oneMail.Rows[0]["V_CSR"].ToString());
			this.LabTitle.Text = oneMail.Rows[0]["v_zt"].ToString();
			this.LabDateTime.Text = oneMail.Rows[0]["dtm_sjsj"].ToString();
			this.LblCon.Text = oneMail.Rows[0]["txt_zw"].ToString();
			DataTable mailAnnex = mailManage.GetMailAnnex(iMailID);
			int num = 20 * mailAnnex.Rows.Count;
			this.tr_fj.Attributes["height"] = num.ToString() + "px";
			if (mailAnnex.Rows.Count > 0)
			{
				for (int i = 0; i < mailAnnex.Rows.Count; i++)
				{
					string[] array = mailAnnex.Rows[i]["v_Lmc"].ToString().Split(new char[]
					{
						'-'
					});
					HtmlGenericControl expr_1C0 = this.annexBlock;
					string innerHtml = expr_1C0.InnerHtml;
					expr_1C0.InnerHtml = string.Concat(new string[]
					{
						innerHtml,
						"<LI><A href=\"#\" onclick=\"javascript:download('",
						mailAnnex.Rows[i]["v_fjlj"].ToString(),
						mailAnnex.Rows[i]["v_Lmc"].ToString(),
						"','",
						array[1].ToString(),
						"');\">",
						array[1].ToString(),
						"</A>"
					});
				}
			}
		}
	}
	protected string sub(string strSub)
	{
		if (strSub != "")
		{
			strSub = strSub.Substring(0, strSub.Length - 1);
		}
		return strSub;
	}
}


