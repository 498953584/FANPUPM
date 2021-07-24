using ASP;
using cn.justwin.BLL;
using cn.justwin.epm.bll.datum;
using cn.justwin.stockBLL.Files;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CriterionList : NBasePage, IRequiresSessionState
	{
		private FilesLogic flc = new FilesLogic();
		protected string strName = "";
		protected string listTitleStr = string.Empty;
		private static string _typeVal = string.Empty;
		private static string _titleVal = string.Empty;
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static Regex RegNumber = new Regex("^[0-9]+$");
		public static string TitleVal
		{
			get
			{
				return CriterionList._titleVal;
			}
			set
			{
				CriterionList._titleVal = value;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["Flage"] == "Q")
			{
				this.QS.Value = "Q";
				if (base.Request.QueryString["Type"] == "Edit")
				{
					CriterionList.TitleVal = "质量事故记录";
				}
			}
			else
			{
				if (base.Request.QueryString["Flage"] == "S")
				{
					this.QS.Value = "S";
					if (base.Request.QueryString["Type"] == "Edit")
					{
						CriterionList.TitleVal = "安全事故记录";
					}
				}
				else
				{
					if (base.Request.QueryString["TypeId"] != null)
					{
						this.hdnClassID.Value = base.Request.QueryString["TypeId"].ToString();
					}
				}
			}
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[CriterionList.temTable] = list.Clone();
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["Type"] == "Edit")
			{
				this.Td_Btn.Visible = true;
				CriterionList._typeVal = "Edit";
			}
			else
			{
				this.BtnAdd.Visible = false;
				this.BtnDelete.Visible = false;
				this.BtnModify.Visible = false;
				CriterionList._typeVal = "List";
			}
			if (base.Request.QueryString["Flage"] == "Q")
			{
				if (base.Request.QueryString["Type"] == "Edit")
				{
					this.Session["CriterionFlageName"] = "质量规范维护";
				}
				else
				{
					this.Session["CriterionFlageName"] = "质量规范查看";
				}
				this.listTitleStr = "质量规范名称";
				this.Session["CriterionFlageCode"] = "1430";
				this.hdnClass.Value = "12";
			}
			else
			{
				this.QS.Value = "S";
				if (base.Request.QueryString["Type"] == "Edit")
				{
					this.Session["CriterionFlageName"] = "安全规范维护";
				}
				else
				{
					this.Session["CriterionFlageName"] = "安全规范查看";
				}
				this.listTitleStr = "安全规范名称";
				this.Session["CriterionFlageCode"] = "1320";
				this.hdnClass.Value = "11";
			}
			string arg_1AF_0 = base.Request.QueryString["TypeId"];
			this.Session["DATUMCLASS"] = base.Request.QueryString["TypeId"];
			this.strName = this.Session["CriterionFlageName"].ToString();
			if (!base.IsPostBack)
			{
				this.Data_Bind();
				this.ClientEvent();
			}
		}
		private void ClientEvent()
		{
			this.BtnSee.Attributes["onclick"] = "javascript:return OpType('See','" + this.Session["DATUMCLASS"] + "','');";
			this.BtnAdd.Attributes["onclick"] = "javascript:return OpType('ADD','" + this.Session["DATUMCLASS"] + "','');";
			this.BtnModify.Attributes["onclick"] = "javascript:return OpType('Edit','" + this.Session["DATUMCLASS"] + "','');";
			this.BtnDelete.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
		}
		protected void Data_Bind()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			text = this.Session["DATUMCLASS"].ToString();
			if (this.Session["DATUMCLASS"].ToString() == "2")
			{
				new datumClass();
				text2 = this.getlistTypeID("ParentId=2");
				if (!string.IsNullOrEmpty(text2))
				{
					text2 = text2.Substring(1);
					text = text2;
				}
				else
				{
					text = "null";
				}
				text += ",2";
			}
			else
			{
				if (this.Session["DATUMCLASS"].ToString() == "1")
				{
					this.BtnAdd.Enabled = false;
					new datumClass();
					if (this.QS.Value == "S")
					{
						text2 = this.getlistTypeID("ParentId=3");
					}
					else
					{
						text2 = this.getlistTypeID("ParentId=2");
					}
					if (!string.IsNullOrEmpty(text2))
					{
						text2 = text2.Substring(1);
						text = text2;
					}
					else
					{
						text = "null";
					}
					if (this.QS.Value == "S")
					{
						text += ",3";
					}
					else
					{
						text += ",2";
					}
				}
			}
			if (this.Session["DATUMCLASS"].ToString() == "3")
			{
				new datumClass();
				text2 = this.getlistTypeID("ParentId=3");
				if (!string.IsNullOrEmpty(text2))
				{
					text2 = text2.Substring(1);
					text = text2;
					text += ",3";
				}
				else
				{
					text = "null";
				}
			}
			DataTable pageData = CriterionAction.GetPageData(text, this.DDLLookup.SelectedValue, this.TxtLookup.Text);
			this.ViewState[CriterionList.resourceTable] = pageData;
			this.GridViewCriterion.DataSource = pageData.DefaultView;
			this.GridViewCriterion.DataBind();
		}
		public static bool IsNumber(string inputData)
		{
			Match match = CriterionList.RegNumber.Match(inputData);
			return match.Success;
		}
		protected void BtnDelete_Click(object sender, EventArgs e)
		{
			CriterionAction criterionAction = new CriterionAction();
			criterionAction.DelFiles(this.Hidden1.Value, this.Session["CriterionFlageCode"].ToString());
			List<string> list = new List<string>();
			if (this.Hidden1.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.Hidden1.Value);
			}
			else
			{
				list.Add(this.Hidden1.Value);
			}
			try
			{
				foreach (string current in list)
				{
					CriterionAction.Del(new Guid(current.ToString()), this.Session["CriterionFlageCode"].ToString());
				}
				this.JS.Text = "alert('操作成功!');";
				this.Data_Bind();
			}
			catch (Exception)
			{
				this.JS.Text = "alert('操作失败,网络连接故障，请稍候再试')";
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void BtnModify_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void btn_Search_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void GridViewCriterion_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.GridViewCriterion.PageIndex = e.NewPageIndex;
			this.Data_Bind();
		}
		protected void GridViewCriterion_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView arg_21_0 = (DataRowView)e.Row.DataItem;
				string text = this.GridViewCriterion.DataKeys[e.Row.RowIndex].Values[0].ToString();
				e.Row.Attributes["id"] = text;
				e.Row.Attributes["style"] = "cursor:hand";
				CheckBox checkBox = (CheckBox)e.Row.Cells[0].FindControl("chk");
				checkBox.Attributes["class"] = "chk";
				checkBox.Attributes["id"] = text;
				Label label = (Label)e.Row.Cells[0].FindControl("Label3");
				label.Attributes["style"] = "display:none";
				if (CriterionList.IsNumber(e.Row.Cells[4].Text))
				{
					e.Row.Cells[4].Text = com.jwsoft.pm.entpm.PageHelper.QueryDept(this, Convert.ToInt32(e.Row.Cells[4].Text.Trim()));
				}
				else
				{
					e.Row.Cells[4].Text = "";
				}
				e.Row.Attributes["onclick"] = "clickRow(this);";
				string text2 = CriterionList.StripHTML(e.Row.Cells[5].Text);
				e.Row.Cells[5].ToolTip = text2;
				if (text2.Length > 20)
				{
					text2 = text2.Substring(0, 19) + "…";
				}
				e.Row.Cells[5].Text = text2;
				Label label2 = (Label)e.Row.Cells[2].FindControl("Label1");
				this.Session["DATUMCLASS"].ToString();
				label2.Attributes["onclick"] = string.Concat(new object[]
				{
					"OpType('See','",
					this.Session["DATUMCLASS"],
					"','",
					text,
					"');"
				});
				if (CriterionList._typeVal == "List")
				{
					e.Row.Cells[0].Text = "";
					e.Row.Cells[0].Visible = false;
					return;
				}
			}
			else
			{
				if (e.Row.RowType == DataControlRowType.Header)
				{
					e.Row.Cells[2].Text = this.listTitleStr;
					if (CriterionList._typeVal == "List")
					{
						e.Row.Cells[0].Text = "";
						e.Row.Cells[0].Visible = false;
					}
				}
			}
		}
		public static string DelHTML(string Htmlstring)
		{
			Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<A>.*</A>", "");
			Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring.Replace("<", "");
			Htmlstring.Replace(">", "");
			Htmlstring.Replace("\r\n", "");
			return Htmlstring;
		}
		public static string StripHTML(string strHtml)
		{
			string[] array = new string[]
			{
				"<script[^>]*?>.*?</script>",
				"<(\\/\\s*)?!?((\\w+:)?\\w+)(\\w+(\\s*=?\\s*(([\"'])(file://[\"'tbnr]|[^/7])*?/7|/w+)|.{0})|/s)*?(///s*)?>",
				"([\\r\\n])[\\s]+",
				"&(quot|#34);",
				"&(amp|#38);",
				"&(lt|#60);",
				"&(gt|#62);",
				"&(nbsp|#160);",
				"&(iexcl|#161);",
				"&(cent|#162);",
				"&(pound|#163);",
				"&(copy|#169);",
				"&#(\\d+);",
				"-->",
				"<!--.*\\n"
			};
			string[] array2 = new string[]
			{
				"",
				"",
				"",
				"\"",
				"&",
				"<",
				">",
				" ",
				"¡",
				"¢",
				"£",
				"©",
				"",
				"\r\n",
				""
			};
			string arg_135_0 = array[0];
			string text = strHtml;
			for (int i = 0; i < array.Length; i++)
			{
				Regex regex = new Regex(array[i], RegexOptions.IgnoreCase);
				text = regex.Replace(text, array2[i]);
			}
			text.Replace("<", "");
			text.Replace(">", "");
			text.Replace("\r\n", "");
			return CriterionList.DelHTML(text);
		}
		private string getlistTypeID(string pid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string sqlString = "select TypeId from EPM_Datum_Class WHERE " + pid;
			DataTable dataTable = new DataTable();
			dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					if (dataRow["TypeId"] != null && dataRow["TypeId"].ToString() != "")
					{
						stringBuilder.Append(",").Append(dataRow["TypeId"]).Append("");
						stringBuilder.Append(this.GetType("ParentId=" + dataRow["TypeId"].ToString()));
					}
				}
			}
			return stringBuilder.ToString();
		}
		public string GetType(string strwhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string sqlString = "select TypeId from EPM_Datum_Class WHERE " + strwhere;
			DataTable dataTable = new DataTable();
			dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					if (dataRow["TypeId"] != null && dataRow["TypeId"].ToString() != "")
					{
						stringBuilder.Append(",").Append(dataRow["TypeId"]).Append("");
						stringBuilder.Append(this.GetType("ParentId=" + dataRow["TypeId"].ToString()));
					}
				}
			}
			return stringBuilder.ToString();
		}
	}


