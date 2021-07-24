using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.epm.bll.datum;
using cn.justwin.epm.datum;
using cn.justwin.stockBLL.epm.datum;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CriterionEdit : NBasePage, IRequiresSessionState
	{
		public static string _title = string.Empty;
		private static Regex RegNumber = new Regex("^[0-9]+$");

		protected void Page_Load(object sender, EventArgs e)
		{
			this.hdnDacumClass.Value = base.Request.QueryString["DatumClass"];
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["Type"] == "See")
				{
					this.FileLink1.Type = 0;
					this.Btn_Save.Visible = false;
					this.Button1.Style.Add("display", "");
					this.Button1.Value = "关 闭";
					if (base.Request.QueryString["Flage"] == "Q")
					{
						CriterionEdit._title = "查看质量规范";
					}
					else
					{
						if (base.Request.QueryString["Flage"] == "S")
						{
							CriterionEdit._title = "查看安全规范";
						}
					}
					this.TxtCriterionName.Enabled = false;
					this.TxtPublisher.Enabled = false;
					this.txtClass.Enabled = false;
					this.Session["CriterionCODE"] = base.Request.QueryString["Code"];
					this.SetValueFromTable();
				}
				else
				{
					if (base.Request.QueryString["Type"] == "Add")
					{
						this.FileLink1.Type = 2;
						if (base.Request.QueryString["Flage"] == "Q")
						{
							CriterionEdit._title = "新增质量规范";
						}
						else
						{
							if (base.Request.QueryString["Flage"] == "S")
							{
								CriterionEdit._title = "新增安全规范";
							}
						}
						this.Session["CriterionCODE"] = Guid.NewGuid();
						this.PublisherYhmc.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.Session["yhdm"].ToString());
					}
					else
					{
						if (base.Request.QueryString["Type"] == "Update")
						{
							this.FileLink1.Type = 1;
							if (base.Request.QueryString["Flage"] == "Q")
							{
								CriterionEdit._title = "修改质量规范";
							}
							else
							{
								if (base.Request.QueryString["Flage"] == "S")
								{
									CriterionEdit._title = "修改安全规范";
								}
							}
							this.Session["CriterionCODE"] = base.Request.QueryString["Code"];
							this.SetValueFromTable();
						}
					}
				}
				if (this.Session["CriterionFlageCode"] != null)
				{
					this.FileLink1.MID = int.Parse(this.Session["CriterionFlageCode"].ToString());
					this.FileLink1.FID = this.Session["CriterionCODE"].ToString();
				}
				if (base.Request.QueryString["ClassID"] != null)
				{
					this.hdnClass.Value = base.Request.QueryString["ClassID"].ToString();
					datumClassModel datumClassModel = new datumClassModel();
					datumClass datumClass = new datumClass();
					datumClassModel = datumClass.GetModel(int.Parse(base.Request.QueryString["ClassID"].ToString()));
					if (datumClassModel != null)
					{
						this.txtClass.Text = datumClassModel.TypeName.ToString();
					}
				}
			}
			this.LblTitle.Text = CriterionEdit._title;
		}
		private void SetValueFromTable()
		{
			CriterionModel criterionModel = new CriterionModel();
			criterionModel = CriterionAction.GetSingle(new Guid(this.Session["CriterionCODE"].ToString()));
			DataTable type = DatumClass.GetType(criterionModel.Flage.ToString());
			if (type != null && type.Rows.Count > 0)
			{
				foreach (DataRow dataRow in type.Rows)
				{
					object obj = dataRow["TypeName"];
					if (obj != null)
					{
						this.txtClass.Text = dataRow["TypeName"].ToString();
					}
				}
			}
			this.PublisherYhmc.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, criterionModel.PublishYhdm.Trim());
			this.TxtCriterionName.Text = criterionModel.CriterionName;
			if (CriterionEdit.IsNumber(criterionModel.Publisher))
			{
				this.TxtPublisher.Text = com.jwsoft.pm.entpm.PageHelper.QueryDept(this, Convert.ToInt32(criterionModel.Publisher));
			}
			this.Textbox1.Text = criterionModel.Remark;
			this.hdnClass.Value = KnowledgeTypeAction.GetSingleType(criterionModel.Flage).TypeName;
			this.hdnDept.Value = criterionModel.Publisher.ToString();
			CriterionLogic criterionLogic = new CriterionLogic();
			DataTable dtModelByID = criterionLogic.getDtModelByID(criterionModel.CriterionCode.ToString());
			foreach (DataRow dataRow2 in dtModelByID.Rows)
			{
				if (dataRow2["mark"].ToString() == "3")
				{
					this.HiddenField1.Value = "3";
				}
				string value = dataRow2["mark"].ToString();
				this.hdnmark.Value = value;
			}
		}
		private CriterionModel GetValueFromPage()
		{
			return new CriterionModel
			{
				CriterionCode = new Guid(this.Session["CriterionCODE"].ToString()),
				CriterionName = this.TxtCriterionName.Text,
				PublishDate = DateTime.Now,
				Publisher = this.hdnDept.Value,
				PublishYhdm = this.Session["yhdm"].ToString(),
				Remark = this.Textbox1.Text,
				Flage = this.hdnClass.Value
			};
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void Btn_Save_Click(object sender, EventArgs e)
		{
			if (this.TxtPublisher.Text == "")
			{
				base.RegisterScript("top.ui.alert('发布单位不能为空');");
				return;
			}
			if (this.txtClass.Text == "")
			{
				base.RegisterScript("top.ui.alert('记录类型不能为空');");
				return;
			}
			if (this.TxtCriterionName.Text == "")
			{
				base.RegisterScript("top.ui.alert('规范名称不能为空');");
				return;
			}
			CriterionModel cM = new CriterionModel();
			cM = this.GetValueFromPage();
			if (base.Request.QueryString["Type"] == "Add")
			{
				int num = this.Add(cM);
				if (num != 1)
				{
					base.RegisterScript("top.ui.alert('增加失败');");
					return;
				}
				base.RegisterScript("top.ui.tabSuccess({parentName: '_criterionlist'});");
				return;
			}
			else
			{
				if (CriterionAction.Update(cM) != 1)
				{
					base.RegisterScript("top.ui.alert('修改失败');");
					return;
				}
				base.RegisterScript("top.ui.tabSuccess({parentName: '_criterionlist'});");
				return;
			}
		}
		public static bool IsNumber(string inputData)
		{
			Match match = CriterionEdit.RegNumber.Match(inputData);
			return match.Success;
		}
		public int Add(CriterionModel CM)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into EPM_Datum_Criterion(");
			stringBuilder.Append("CriterionCode,CriterionName,PublishYhdm,PublishDate,Publisher,Remark,DatumClass)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@CriterionCode,@CriterionName,@PublishYhdm,@PublishDate,@Publisher,@Remark,@DatumClass)");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@CriterionCode", SqlDbType.UniqueIdentifier, 16),
				new SqlParameter("@CriterionName", SqlDbType.VarChar, 100),
				new SqlParameter("@PublishYhdm", SqlDbType.Char, 10),
				new SqlParameter("@PublishDate", SqlDbType.DateTime),
				new SqlParameter("@Publisher", SqlDbType.VarChar, 100),
				new SqlParameter("@Remark", SqlDbType.Text),
				new SqlParameter("@DatumClass", SqlDbType.Int, 4)
			};
			array[0].Value = CM.CriterionCode;
			array[1].Value = CM.CriterionName;
			array[2].Value = CM.PublishYhdm;
			array[3].Value = CM.PublishDate;
			array[4].Value = CM.Publisher;
			array[5].Value = CM.Remark;
			array[6].Value = CM.Flage;
			return SqlHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), array);
		}
		public void checkAdd()
		{
			if (this.TxtPublisher.Text == "")
			{
				this.js.Text = "   alert('发布单位不能为空！');";
				return;
			}
			if (this.txtClass.Text == "")
			{
				this.js.Text = "   alert('记录类型不能为空！');";
				return;
			}
			if (this.TxtCriterionName.Text == "")
			{
				this.js.Text = "   alert('规范名称不能为空！');";
			}
		}
	}


