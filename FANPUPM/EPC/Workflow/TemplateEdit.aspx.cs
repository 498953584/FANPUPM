using ASP;
using cn.justwin.BLL;
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
	public partial class TemplateEdit : NBasePage, IRequiresSessionState
	{
		public string BusinessCode
		{
			get
			{
				object obj = this.ViewState["BusinessCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["BusinessCode"] = value;
			}
		}
		public string BusinessClass
		{
			get
			{
				object obj = this.ViewState["BusinessClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["BusinessClass"] = value;
			}
		}
		public int TemplateID
		{
			get
			{
				object obj = this.ViewState["TemplateID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TemplateID"] = value;
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
		public string IsGeneral
		{
			get
			{
				object obj = this.ViewState["IsGeneral"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["IsGeneral"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string arg_20_0 = base.Request["ti"];
				this.TemplateID = System.Convert.ToInt32(base.Request["ti"]);
				this.BusinessCode = System.Convert.ToString(base.Request["code"]);
				this.BusinessClass = System.Convert.ToString(base.Request["class"]);
				this.IsGeneral = base.Request["flag"];
				this.UserCode = this.Session["yhdm"].ToString();
				this.hdnBusinessCode.Value = this.BusinessCode.ToString();
				this.hdnBusinessClass.Value = this.BusinessClass.ToString();
				this.hdnIsComplete.Value = "0";
				this.CreateDdltRecordID();
				if (this.TemplateID != 0)
				{
					this.TemplateRestore(this.TemplateID);
				}
				string isGeneral;
				if ((isGeneral = this.IsGeneral) != null)
				{
					if (isGeneral == "0")
					{
						this.trCorpCode.Visible = false;
						return;
					}
					if (!(isGeneral == "1"))
					{
						if (!(isGeneral == "2"))
						{
							return;
						}
					}
					else
					{
						this.trCorpCode.Visible = false;
					}
				}
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void TemplateRestore(int templateId)
		{
			DataTable dataTable = FlowTemplateAction.QueryOneTemplate(templateId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtTemplateName.Text = dataTable.Rows[0]["TemplateName"].ToString();
				this.hdnBusinessCode.Value = dataTable.Rows[0]["BusinessCode"].ToString();
				this.hdnBusinessClass.Value = dataTable.Rows[0]["BusinessClass"].ToString();
				this.hdnIsComplete.Value = dataTable.Rows[0]["IsComplete"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["remark"].ToString();
				if (dataTable.Rows[0]["IsAbnormality"].ToString() == "1")
				{
					this.ckbIsAbnormality.Checked = true;
				}
				else
				{
					this.ckbIsAbnormality.Checked = false;
				}
				if (dataTable.Rows[0]["IsGeneral"].ToString() == "1")
				{
					this.ckbIsGeneral.Checked = true;
				}
				else
				{
					this.ckbIsGeneral.Checked = false;
				}
				this.IsGeneral = dataTable.Rows[0]["IsGeneral"].ToString();
				string text = dataTable.Rows[0]["RecordID"].ToString();
				if (text != "" && text != null)
				{
					this.ddltRecordID.ClearSelection();
					this.ddltRecordID.Items.FindByValue(text).Selected = true;
				}
				if (this.trCorpCode.Visible)
				{
					this.DDLCorpCode.SelectedValue = dataTable.Rows[0]["CorpCode"].ToString();
				}
			}
		}
		public bool IsSameTemplateName(string businessclass, string businesscode, string templatename)
		{
			DataTable dataTable = publicDbOpClass.DataTableQuary(string.Concat(new string[]
			{
				"select * from wf_template where IsGeneral = '1' and BusinessClass ='",
				businessclass,
				"' and BusinessCode ='",
				businesscode,
				"' and TemplateName='",
				templatename,
				"' "
			}));
			return dataTable.Rows.Count > 0;
		}
		private void BtnAdd_Click(object sender, System.EventArgs e)
		{
			string arg_0B_0 = this.txtTemplateName.Text;
			string arg_17_0 = this.txtRemark.Text;
			this.ddltCorpCode.SelectedValue.ToString();
			string text = this.ddltRecordID.SelectedValue.ToString();
			string pStr = "";
			string pStr2 = this.Session["CorpCode"].ToString();
			string isGeneral;
			if ((isGeneral = this.IsGeneral) != null)
			{
				if (!(isGeneral == "0"))
				{
					if (!(isGeneral == "1"))
					{
						if (isGeneral == "2")
						{
							pStr2 = this.DDLCorpCode.SelectedValue.ToString();
							pStr = "0";
						}
					}
					else
					{
						pStr2 = this.Session["CorpCode"].ToString();
						pStr = "1";
					}
				}
				else
				{
					pStr2 = this.Session["CorpCode"].ToString();
					pStr = "0";
				}
			}
			string pStr3;
			if (this.ckbIsAbnormality.Checked)
			{
				pStr3 = "1";
			}
			else
			{
				pStr3 = "0";
			}
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
			hashtable.Add("BusinessCode", SqlStringConstructor.GetQuotedString(this.hdnBusinessCode.Value));
			hashtable.Add("BusinessClass", SqlStringConstructor.GetQuotedString(this.hdnBusinessClass.Value));
			if ((this.IsGeneral == "2" || this.IsGeneral == "0") && this.TemplateID == 0)
			{
				hashtable.Add("templateName", SqlStringConstructor.GetQuotedString(this.txtTemplateName.Text + "[分]"));
			}
			else
			{
				hashtable.Add("templateName", SqlStringConstructor.GetQuotedString(this.txtTemplateName.Text));
			}
			hashtable.Add("Remark", SqlStringConstructor.GetQuotedString(this.txtRemark.Text));
			hashtable.Add("IsAbnormality", SqlStringConstructor.GetQuotedString(pStr3));
			hashtable.Add("IsGeneral", SqlStringConstructor.GetQuotedString(pStr));
			hashtable.Add("CorpCode", SqlStringConstructor.GetQuotedString(pStr2));
			hashtable.Add("IsComplete", SqlStringConstructor.GetQuotedString(this.hdnIsComplete.Value));
			if (text != "0")
			{
				hashtable.Add("RecordID", text.ToString());
			}
			if (this.TemplateID != 0)
			{
				DataTable dataTable = FlowTemplateAction.QueryOneTemplate(this.TemplateID);
				if (dataTable.Rows.Count == 1)
				{
					if (dataTable.Rows[0]["TemplateName"].ToString() == this.txtTemplateName.Text)
					{
						string where = " where templateId = '" + this.TemplateID + " '";
						if (FlowTemplateAction.UpdTemplate(hashtable, where))
						{
							base.RegisterScript("top.ui.tabSuccess({ parentName: '_wfTemplateList' });");
							return;
						}
						base.RegisterScript("top.ui.alert('保存失败');");
						return;
					}
					else
					{
						if (!this.IsSameTemplateName(this.BusinessClass, this.BusinessCode, this.txtTemplateName.Text))
						{
							string where2 = " where templateId = '" + this.TemplateID + " '";
							if (FlowTemplateAction.UpdTemplate(hashtable, where2))
							{
								base.RegisterScript("top.ui.tabSuccess({ parentName: '_wfTemplateList' });");
								return;
							}
							base.RegisterScript("top.ui.alert('保存失败');");
							return;
						}
						else
						{
							base.RegisterScript("top.ui.alert('存在名称相同的模板名称');");
						}
					}
				}
				return;
			}
			if (this.IsSameTemplateName(this.BusinessClass, this.BusinessCode, this.txtTemplateName.Text))
			{
				base.RegisterScript("top.ui.alert('存在名称相同的模板名称');");
				return;
			}
			if (FlowTemplateAction.AddTemplate(hashtable, this.UserCode))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_wfTemplateList' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败');");
		}
		private void CreateDdltRecordID()
		{
			string sqlString = "select * from WF_JoinBusiness where BusinessCode = " + this.BusinessCode.ToString();
			DataSet dataSource = publicDbOpClass.DataSetQuary(sqlString);
			this.ddltRecordID.DataSource = dataSource;
			this.ddltRecordID.DataTextField = "DisplayName";
			this.ddltRecordID.DataValueField = "RecordID";
			this.ddltRecordID.DataBind();
			ListItem listItem = new ListItem("---请选择关联业务---", "0");
			listItem.Selected = true;
			this.ddltRecordID.Items.Insert(0, listItem);
		}
	}


