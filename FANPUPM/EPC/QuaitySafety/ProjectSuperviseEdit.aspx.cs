using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProjectSuperviseEdit : NBasePage, IRequiresSessionState
	{
		public static string _showtitle = string.Empty;
		public static string _showAffairTitle = string.Empty;
		protected string i_id = "";
		private int _ca;
		public static string _QS = string.Empty;
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
				}
				else
				{
					this.i_id = base.Request.QueryString["i_id"].ToString();
					AffairAction affairAction = new AffairAction();
					AffairModel singleAffair = affairAction.GetSingleAffair(this.i_id);
					this.TxtAffairTitle.Text = singleAffair.AffairTitle;
					this.TxtContext.Text = singleAffair.Context;
					this.CalDate.Text = singleAffair.Date.ToShortDateString();
					this.Txtremark.Text = singleAffair.Remark;
					this.hdnClass.Value = singleAffair.Flage;
					this.FileLink1.MID = 1755;
					this.FileLink1.FID = this.i_id;
					string text = singleAffair.Mark.ToString();
					if (text != "2")
					{
						this.cbkmark.Checked = true;
					}
					else
					{
						this.cbkmark.Checked = false;
					}
					this.hdnmark.Value = text;
					this.hidenClass.Value = singleAffair.FilesType.ToString();
					if (base.Request.QueryString["Type"] == "EDIT")
					{
						this.FileLink1.Type = 1;
					}
					else
					{
						if (base.Request.QueryString["Type"] == "SEE")
						{
							this.FileLink1.Type = 0;
							this.BtnSave.Visible = false;
							this.showText.Visible = true;
							this.TextBox1.Text = ProjectSuperviseEdit.StripHTML(singleAffair.Context);
							this.TextBox1.Enabled = false;
							this.TxtContext.Visible = false;
							this.Button1.Style.Add("display", "");
							this.Button1.Value = "关 闭";
							this.TxtAffairTitle.Enabled = (this.CalDate.Enabled = (this.TxtContext.Enabled = (this.Txtremark.Enabled = false)));
							this.DDTClass.Enabled = false;
							this.cbkmark.Enabled = false;
						}
					}
					if (singleAffair.Mark.ToString() == "1")
					{
						this.FileLink1.Type = 0;
						this.BtnSave.Visible = false;
						this.showText.Visible = true;
						this.TextBox1.Text = ProjectSuperviseEdit.StripHTML(singleAffair.Context);
						this.TextBox1.Enabled = false;
						this.TxtContext.Visible = false;
						this.TxtContext.Text = singleAffair.Context;
						this.Button1.Style.Add("display", "");
						this.Button1.Value = "关 闭";
						this.TxtAffairTitle.Enabled = (this.CalDate.Enabled = (this.TxtContext.Enabled = (this.Txtremark.Enabled = false)));
						this.DDTClass.Enabled = false;
						this.cbkmark.Enabled = false;
					}
				}
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			}
		}
		private AffairModel GetValueFromText()
		{
			AffairModel affairModel = new AffairModel();
			affairModel.CA = this._ca;
			affairModel.AffairClass = "0000";
			affairModel.AffairTitle = this.TxtAffairTitle.Text;
			affairModel.Context = this.TxtContext.Text;
			affairModel.Date = Convert.ToDateTime(this.CalDate.Text);
			affairModel.Remark = this.Txtremark.Text;
			affairModel.PrjCode = this.PrjCode;
			affairModel.Flage = this.Session["DATUMCLASS"].ToString();
			affairModel.i_id = new Guid(this.FileLink1.FID);
			if (this.cbkmark.Checked)
			{
				affairModel.Mark = 3;
				if (this.DDTClass.SelectedValue != null && this.DDTClass.SelectedValue != "")
				{
					affairModel.FilesType = int.Parse(this.DDTClass.SelectedValue.ToString());
				}
			}
			else
			{
				affairModel.Mark = 2;
				affairModel.FilesType = 0;
			}
			return affairModel;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			this.DDTClass.Visible = this.cbkmark.Checked;
			this.c1.Visible = this.cbkmark.Checked;
			AffairModel aM = new AffairModel();
			aM = this.GetValueFromText();
			if (base.Request.QueryString["Type"] == "Add")
			{
				int num = AffairAction.ADD(aM);
				if (num != 1)
				{
					this.JS.Text = "alert('保存失败！');";
					return;
				}
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_ProjectSupervise' })");
				return;
			}
			else
			{
				if (AffairAction.Update(aM) != 1)
				{
					this.JS.Text = "alert('修改失败！');";
					return;
				}
				this.cen.Visible = false;
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_ProjectSupervise' })");
				return;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["CA"] != null && base.Request.QueryString["CA"].ToString() != "")
			{
				this._ca = int.Parse(base.Request.QueryString["CA"].ToString());
			}
			if (base.Request.QueryString["Flag"] != null && base.Request.QueryString["Flag"] == "Q")
			{
				ProjectSuperviseEdit._QS = "Q";
			}
			else
			{
				if (base.Request.QueryString["Flag"] != null && base.Request.QueryString["Flag"] == "S")
				{
					ProjectSuperviseEdit._QS = "S";
				}
			}
			if (base.Request.QueryString["Type"] != null)
			{
				ProjectSuperviseEdit._showtitle = this.swTitle(this._ca.ToString(), base.Request.QueryString["Type"].ToString(), ProjectSuperviseEdit._QS);
			}
			else
			{
				if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"] == "List")
				{
					ProjectSuperviseEdit._showtitle = this.swTitle(this._ca.ToString(), "List", ProjectSuperviseEdit._QS);
				}
			}
			this.TxtContext.CssClass = "wysiwyg";
			this.showText.Visible = false;
			base.OnInit(e);
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
			return text;
		}
		public string swTitle(string v, string edit, string QS)
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
											if (edit.ToUpper() == "EDIT")
											{
												result = "安全措施";
											}
											else
											{
												if (edit.ToUpper() == "SEE")
												{
													result = "安全措施";
												}
												else
												{
													if (edit.ToUpper() == "ADD")
													{
														result = "安全措施";
													}
												}
											}
											ProjectSuperviseEdit._showAffairTitle = "安全措施名称";
										}
									}
								}
								else
								{
									if (QS == "S")
									{
										if (edit.ToUpper() == "EDIT")
										{
											result = "质量事故报告";
										}
										else
										{
											if (edit.ToUpper() == "SEE")
											{
												result = "事故报告";
											}
											else
											{
												if (edit.ToUpper() == "ADD")
												{
													result = "事故报告";
												}
											}
										}
										ProjectSuperviseEdit._showAffairTitle = "安全事故记录名称";
									}
								}
							}
							else
							{
								if (QS == "S")
								{
									if (edit.ToUpper() == "EDIT")
									{
										result = "安全监督";
									}
									else
									{
										if (edit.ToUpper() == "SEE")
										{
											result = "安全监督记录";
										}
										else
										{
											if (edit.ToUpper() == "ADD")
											{
												result = "安全监督记录";
											}
										}
									}
									ProjectSuperviseEdit._showAffairTitle = "安全检查记录名称";
								}
							}
						}
						else
						{
							if (QS == "Q")
							{
								if (edit.ToUpper() == "EDIT")
								{
									result = "工程质量竣工验收资料";
								}
								else
								{
									if (edit.ToUpper() == "SEE")
									{
										result = "工程质量竣工验收记录";
									}
									else
									{
										if (edit.ToUpper() == "ADD")
										{
											result = "工程质量竣工验收资料";
										}
									}
								}
								ProjectSuperviseEdit._showAffairTitle = "资料名称";
							}
						}
					}
					else
					{
						if (QS == "Q")
						{
							if (edit.ToUpper() == "EDIT")
							{
								result = "事故记录";
							}
							else
							{
								if (edit.ToUpper() == "SEE")
								{
									result = "事故记录";
								}
								else
								{
									if (edit.ToUpper() == "ADD")
									{
										result = "事故记录";
									}
								}
							}
							ProjectSuperviseEdit._showAffairTitle = "事故记录名称";
						}
					}
				}
				else
				{
					if (QS == "Q")
					{
						if (edit.ToUpper() == "Edit")
						{
							result = "质量检查记录";
						}
						else
						{
							if (edit.ToUpper() == "ADD")
							{
								result = "质量检查记录";
							}
							else
							{
								if (edit.ToUpper() == "SEE")
								{
									result = "质量检查记录";
								}
							}
						}
						ProjectSuperviseEdit._showAffairTitle = "检查记录名称";
					}
				}
			}
			return result;
		}
		protected void cbkmark_CheckedChanged(object sender, EventArgs e)
		{
		}
	}


