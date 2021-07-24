using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class AffairEdit : NBasePage, IRequiresSessionState
	{
		public static string _showtitle = string.Empty;
		public static string _showAffairTitle = string.Empty;
		protected string i_id = "";
		public Guid PrjCode
		{
			get
			{
				object obj = this.ViewState["PrjCode"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PrjCode"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.hdnDacumClass.Value = base.Request.QueryString["DatumClass"];
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["PrjCode"] != null)
				{
					this.PrjCode = new Guid(base.Request.QueryString["PrjCode"].ToString());
				}
				if (base.Request.QueryString["Type"] == "Add")
				{
					this.FileLink1.Type = 1;
					this.FileLink1.MID = 1755;
					this.FileLink1.FID = Guid.NewGuid().ToString();
					this.CalDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
					return;
				}
				this.i_id = base.Request.QueryString["i_id"].ToString();
				string sqlString = "SELECT * FROM EPM_Datum_Affair eda WHERE eda.i_id='" + this.i_id + "';";
				DataRow dataRow = publicDbOpClass.QueryDataRow(sqlString);
				string text = dataRow["mark"].ToString();
				if (text != "2")
				{
					this.cbkmark.Checked = true;
				}
				else
				{
					this.cbkmark.Checked = false;
				}
				this.hdnmark.Value = text;
				AffairAction affairAction = new AffairAction();
				AffairModel singleAffair = affairAction.GetSingleAffair(this.i_id);
				this.TxtAffairTitle.Text = singleAffair.AffairTitle;
				this.TxtContext.Text = singleAffair.Context;
				this.CalDate.Text = singleAffair.Date.ToShortDateString();
				this.Txtremark.Text = singleAffair.Remark;
				this.hdnClass.Value = singleAffair.Flage;
				this.FileLink1.MID = 1755;
				this.FileLink1.FID = this.i_id;
				text = dataRow["filesType"].ToString();
				this.hidenClass.Value = text;
				if (base.Request.QueryString["Type"] == "EDIT")
				{
					this.FileLink1.Type = 1;
					if (base.Request.QueryString["Flag"] == "Q")
					{
						AffairEdit._showtitle = "修改质量事务";
						AffairEdit._showAffairTitle = "质量事务名称";
					}
					else
					{
						if (base.Request.QueryString["Flag"] == "S")
						{
						}
					}
				}
				else
				{
					if (base.Request.QueryString["Type"] == "SEE")
					{
						this.FileLink1.Type = 0;
						this.BtnSave.Visible = false;
						if (!(base.Request.QueryString["Flag"] == "Q"))
						{
						}
						this.showText.Visible = true;
						this.showText.InnerHtml = singleAffair.Context;
						this.TxtContext.Visible = false;
						this.Button1.Style.Add("display", "");
						this.Button1.Value = "关 闭";
						this.TxtAffairTitle.Enabled = (this.CalDate.Enabled = (this.TxtContext.Enabled = (this.Txtremark.Enabled = false)));
						this.DDTClass.Enabled = false;
						this.cbkmark.Enabled = false;
					}
				}
				if (dataRow["mark"].ToString() == "1")
				{
					this.FileLink1.Type = 0;
					this.BtnSave.Visible = false;
					this.showText.Visible = true;
					this.showText.InnerHtml = singleAffair.Context;
					this.TxtContext.Visible = false;
					this.Button1.Style.Add("display", "");
					this.Button1.Value = "关 闭";
					this.TxtAffairTitle.Enabled = (this.CalDate.Enabled = (this.TxtContext.Enabled = (this.Txtremark.Enabled = false)));
					this.DDTClass.Enabled = false;
					this.cbkmark.Enabled = false;
					this.DDTClass.Enabled = false;
					this.cbkmark.Enabled = false;
				}
			}
		}
		private AffairModel GetValueFromText()
		{
			return new AffairModel
			{
				AffairClass = "0000",
				AffairTitle = this.TxtAffairTitle.Text,
				Context = this.TxtContext.Text,
				Date = Convert.ToDateTime(this.CalDate.Text),
				Remark = this.Txtremark.Text,
				PrjCode = this.PrjCode,
				Flage = this.Session["DATUMCLASS"].ToString(),
				i_id = new Guid(this.FileLink1.FID)
			};
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (this.TxtAffairTitle.Text == "")
			{
				this.JS.Text = "alert('" + AffairEdit._showAffairTitle + "不能为空！');";
				return;
			}
			AffairModel affairModel = new AffairModel();
			affairModel = this.GetValueFromText();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" INSERT INTO EPM_Datum_Affair (");
			stringBuilder.Append("i_id, PrjCode,AffairClass,AffairTitle,AddDate,Remark,Context,DatumClass,CA,mark,filesType) ");
			stringBuilder.Append("VALUES(");
			stringBuilder.Append("'").Append(affairModel.i_id).Append("',");
			stringBuilder.Append("'").Append(affairModel.PrjCode).Append("',");
			stringBuilder.Append("'").Append(affairModel.AffairClass).Append("',");
			stringBuilder.Append("'").Append(affairModel.AffairTitle).Append("',");
			stringBuilder.Append("'").Append(affairModel.Date).Append("',");
			stringBuilder.Append("'").Append(affairModel.Remark).Append("',");
			stringBuilder.Append("'").Append(affairModel.Context).Append("',");
			stringBuilder.Append("").Append(affairModel.Flage).Append(",");
			stringBuilder.Append("").Append(0).Append(" ,");
			if (this.cbkmark.Checked)
			{
				stringBuilder.Append(" ").Append(3).Append(", ");
				if (this.DDTClass.SelectedValue == null || !(this.DDTClass.SelectedValue != ""))
				{
					this.JS.Text = "alert('请选择归档类型！');";
					this.JS.Text = "window.parent.lframe.location.href +='';";
					return;
				}
				stringBuilder.Append("").Append(this.DDTClass.SelectedValue).Append("");
			}
			else
			{
				stringBuilder.Append(" ").Append(2).Append(", ");
				stringBuilder.Append("").Append(0).Append("");
			}
			stringBuilder.Append(" )");
			int num = publicDbOpClass.ExecSqlString(stringBuilder.ToString());
			if (base.Request.QueryString["Type"] == "Add")
			{
				if (num != 1)
				{
					base.RegisterScript("top.ui.alert('新增失败');");
					return;
				}
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_quaitysafetyaffairlist' })");
				return;
			}
			else
			{
				if (AffairAction.Update(affairModel) != 1)
				{
					base.RegisterScript("top.ui.alert('修改失败');");
					return;
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("UPDATE EPM_Datum_Affair").Append(" ");
				stringBuilder2.Append("SET").Append(" ");
				if (this.cbkmark.Checked)
				{
					stringBuilder2.Append(" [mark] =").Append(3).Append(", ");
				}
				else
				{
					stringBuilder2.Append(" [mark] =").Append(2).Append(", ");
				}
				stringBuilder2.Append(" filesType =").Append(this.DDTClass.SelectedValue).Append(" ");
				stringBuilder2.Append(" ").Append(" WHERE i_id='" + affairModel.i_id + "'");
				publicDbOpClass.ExecSqlString(stringBuilder2.ToString());
				this.cen.Visible = false;
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_quaitysafetyaffairlist' })");
				return;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.showText.Visible = false;
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			if (base.Request.QueryString["Type"] == "Add")
			{
				if (base.Request.QueryString["Flag"] == "Q")
				{
					AffairEdit._showtitle = "新增质量事务";
					AffairEdit._showAffairTitle = "质量事务名称";
				}
				else
				{
					if (base.Request.QueryString["Flag"] == "S")
					{
						AffairEdit._showtitle = "新增安全事务";
						AffairEdit._showAffairTitle = "安全事务名称";
					}
				}
			}
			else
			{
				if (base.Request.QueryString["Type"] == "EDIT")
				{
					if (base.Request.QueryString["Flag"] == "Q")
					{
						AffairEdit._showtitle = "修改质量事务";
						AffairEdit._showAffairTitle = "质量事务名称";
					}
					else
					{
						if (base.Request.QueryString["Flag"] == "S")
						{
							AffairEdit._showtitle = "修改安全事务";
							AffairEdit._showAffairTitle = "安全事务名称";
						}
					}
				}
				else
				{
					if (base.Request.QueryString["Type"] == "SEE")
					{
						if (base.Request.QueryString["Flag"] == "Q")
						{
							AffairEdit._showtitle = "查看质量事务";
							AffairEdit._showAffairTitle = "质量事务名称";
						}
						else
						{
							if (base.Request.QueryString["Flag"] == "S")
							{
								AffairEdit._showtitle = "查看安全事务";
								AffairEdit._showAffairTitle = "安全事务名称";
							}
						}
					}
				}
			}
			base.OnInit(e);
		}
	}


