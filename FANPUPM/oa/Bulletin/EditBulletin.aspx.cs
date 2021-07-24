using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EditBulletin : BasePage, IRequiresSessionState
	{
		public System.Guid BulletinID
		{
			get
			{
				object obj = this.ViewState["BulletinID"];
				if (obj != null)
				{
					return (System.Guid)obj;
				}
				return System.Guid.Empty;
			}
			set
			{
				this.ViewState["BulletinID"] = value;
			}
		}
		public string CurrentDate
		{
			get
			{
				object obj = this.ViewState["CurrentDate"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["CurrentDate"] = value;
			}
		}
		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				System.DateTime now = System.DateTime.Now;
				this.dbExpriseDate.Text = now.AddDays(15.0).ToString("yyyy-MM-dd");
				base.Request.Url.ToString();
				this.UserCode = this.Session["yhdm"].ToString();
				if (base.Request.QueryString["rid"] != "")
				{
					this.BulletinID = new System.Guid(base.Request.QueryString["rid"]);
					this.hdnRecordId.Value = System.Convert.ToString(this.BulletinID);
					this.setData();
				}
				else
				{
					this.BulletinID = System.Guid.Empty;
					this.hdnRecordId.Value = System.Convert.ToString(System.Guid.NewGuid());
					System.DateTime dateTime = default(System.DateTime);
					dateTime = System.DateTime.Now;
					this.hdnDept.Value = "00";
					this.tbDept.Text = EditBulletin.getDeptName(this.hdnDept.Value);
					this.CurrentDate = dateTime.ToString("yyyy-MM-dd");
				}
				this.FileUpload1.RecordCode = this.hdnRecordId.Value;
				this.btnSelDept.Attributes["onclick"] = "javascript:selDept('" + this.UserCode + "');";
			}
		}
		protected void setData()
		{
			DataTable dataTable = com.jwsoft.pm.entpm.action.BulletinManage.QueryOneBulletin(this.BulletinID);
			if (dataTable.Rows.Count == 1)
			{
				this.hdnDept.Value = dataTable.Rows[0]["DeptRange"].ToString();
				this.TBoxTitle.Text = dataTable.Rows[0]["V_TITLE"].ToString();
				this.dbExpriseDate.Text = ((System.DateTime)dataTable.Rows[0]["DTM_EXPRIESDATE"]).ToString("yyyy-MM-dd");
				this.tbUrl.Text = dataTable.Rows[0]["URL"].ToString();
				this.txtContent.Value = dataTable.Rows[0]["V_CONTENT"].ToString();
				this.RBLtBound.SelectedValue = dataTable.Rows[0]["I_RELEASEBOUND"].ToString();
				string text = BulletionAction.ReturnCropName(this.hdnDept.Value);
				this.tbDept.Text = text;
				this.CurrentDate = dataTable.Rows[0]["DTM_RELEASETIME"].ToString();
			}
		}
		protected static string getDeptName(string deptCode)
		{
			string text = "";
			if (deptCode == "")
			{
				text = "";
			}
			else
			{
				int i = deptCode.IndexOf(",");
				if (i > 0)
				{
					while (i > 0)
					{
						string value = deptCode.Substring(0, i);
						text = text + EditBulletin.getDept(System.Convert.ToInt32(value)) + ",";
						deptCode = deptCode.Substring(i + 1, deptCode.Length - i - 1);
						i = deptCode.IndexOf(",");
					}
					text += EditBulletin.getDept(System.Convert.ToInt32(deptCode));
				}
				else
				{
					text += EditBulletin.getDept(System.Convert.ToInt32(deptCode));
				}
			}
			return text;
		}
		protected static string getDept(int deptCode)
		{
			string sqlString = "select V_BMMC from pt_d_bm where i_bmdm = " + deptCode.ToString() + " and c_sfyx='y'";
			object value = publicDbOpClass.ExecuteScalar(sqlString);
			return System.Convert.ToString(value);
		}
		protected void BtnAdd_Click(object sender, System.EventArgs e)
		{
			string sqlString = "select v_xm from pt_yhmc where c_sfyx='y' and v_yhdm = '" + this.UserCode + "'";
			object value = publicDbOpClass.ExecuteScalar(sqlString);
			string pStr = System.Convert.ToString(value);
			System.Guid guid = new System.Guid(this.hdnRecordId.Value);
			string value2 = this.hdnDept.Value;
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("I_BULLETINID", SqlStringConstructor.GetQuotedString(guid.ToString()));
			hashtable.Add("I_RELEASEBOUND", this.RBLtBound.SelectedValue.ToString());
			hashtable.Add("DeptRange", SqlStringConstructor.GetQuotedString(this.hdnDept.Value));
			hashtable.Add("V_TITLE", SqlStringConstructor.GetQuotedString(this.TBoxTitle.Text));
			hashtable.Add("DTM_EXPRIESDATE", SqlStringConstructor.GetQuotedString(System.Convert.ToDateTime(this.dbExpriseDate.Text).ToString()));
			hashtable.Add("DTM_RELEASETIME", SqlStringConstructor.GetQuotedString(this.CurrentDate.ToString()));
			hashtable.Add("V_RELUSERCODE", SqlStringConstructor.GetQuotedString(this.UserCode));
			hashtable.Add("V_CONTENT", SqlStringConstructor.GetQuotedString(this.txtContent.Value));
			hashtable.Add("URL", SqlStringConstructor.GetQuotedString(this.tbUrl.Text));
			hashtable.Add("CorpCode", SqlStringConstructor.GetQuotedString(value2));
			hashtable.Add("V_RELEASEUSER", SqlStringConstructor.GetQuotedString(pStr));
			if (this.TBoxTitle.Text.Trim() == "")
			{
				this.JS.Text = "top.ui.alert('公告标题不能为空！');";
				return;
			}
			if (this.BulletinID == System.Guid.Empty)
			{
				hashtable.Add("AuditState", "-1");
				if (com.jwsoft.pm.entpm.action.BulletinManage.AddBulletinInfo(hashtable))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_BulletinManage' });");
					return;
				}
				this.JS.Text = "top.ui.alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where I_BULLETINID = '" + this.BulletinID.ToString() + "'";
				if (com.jwsoft.pm.entpm.action.BulletinManage.UpdBulletinInfo(hashtable, where))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_BulletinManage' });");
					return;
				}
				this.JS.Text = "top.ui.alert('保存失败');";
				return;
			}
		}
	}


