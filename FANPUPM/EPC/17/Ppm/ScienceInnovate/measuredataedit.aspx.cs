using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.epm.datum;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MeasureDataEdit : NBasePage, IRequiresSessionState
	{
		private string _Id;
		private string _PrjCode = "";
		private string _Type = "";
		private string _MaxId = "";
		private string _SmallSort = "";
		private string _BigSort = "";
		private string parentName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["pc"] != null)
				{
					this.ViewState["PRJCODE"] = base.Request.QueryString["pc"].ToString();
				}
				if (base.Request.QueryString["Type"] != null)
				{
					this.ViewState["TYPE"] = base.Request.QueryString["Type"].ToString();
				}
				if (this.ViewState["PRJCODE"] != null)
				{
					this._PrjCode = this.ViewState["PRJCODE"].ToString();
				}
				if (this.ViewState["TYPE"] != null)
				{
					this._Type = this.ViewState["TYPE"].ToString();
				}
				if (base.Request.QueryString["bs"] != null)
				{
					this.ViewState["BIGSORT"] = base.Request.QueryString["bs"].ToString();
				}
				if (this.ViewState["BIGSORT"] != null)
				{
					this._BigSort = this.ViewState["BIGSORT"].ToString();
				}
				if (base.Request.QueryString["sm"] != null)
				{
					this.ViewState["SMALLSORT"] = base.Request.QueryString["sm"].ToString();
					this._SmallSort = this.ViewState["SMALLSORT"].ToString();
				}
				if (this._BigSort == "1")
				{
					if (!string.IsNullOrEmpty(base.Request["sm"].ToString()))
					{
						string text = base.Request["sm"].ToString();
						if (text.Equals("1"))
						{
							this.LblTitle.Text = "业主控制网资料";
						}
						else
						{
							if (text.Equals("2"))
							{
								this.LblTitle.Text = "测量控制网资料";
							}
							else
							{
								this.LblTitle.Text = "测量放线资料";
							}
						}
					}
					this.LblName.Text = this.LblTitle.Text + "名称：";
				}
				else
				{
					if (this._BigSort == "2")
					{
						this.ViewState["parentName"] = "_measuredataquery";
						if (this._SmallSort == "1")
						{
							this.LblTitle.Text = "图纸自审";
							this.LblName.Text = "图纸自审名称：";
						}
						else
						{
							if (this._SmallSort == "10")
							{
								this.LblTitle.Text = "图纸审核";
								this.LblName.Text = "图纸审核名称：";
							}
							else
							{
								this.LblTitle.Text = "图纸会审";
								this.LblName.Text = "图纸会审名称：";
							}
						}
					}
					else
					{
						if (this._BigSort == "3")
						{
							this.ViewState["parentName"] = "__projectlinkquery";
							this.LblTitle.Text = "工程联系单";
							this.LblName.Text = "工程联系单名称：";
						}
						else
						{
							if (this._BigSort == "4")
							{
								this.ViewState["parentName"] = "__projectlinkquery";
								this.LblTitle.Text = "设计变更";
								this.LblName.Text = "设计变更名称：";
							}
							else
							{
								if (this._BigSort == "5")
								{
									this.ViewState["parentName"] = "__projectlinkquery";
									this.LblTitle.Text = "中间交接资料";
									this.LblName.Text = "中间交接名称：";
									this.TRName.Visible = true;
									this.TRReceive.Visible = true;
								}
								else
								{
									if (this._BigSort == "7")
									{
										this.ViewState["parentName"] = "_EngineerConfirmList";
										this.LblTitle.Text = "工程确认单";
										this.LblName.Text = "工程确认单名称：";
									}
									else
									{
										if (this._BigSort == "8")
										{
											this.ViewState["parentName"] = "_EngineerConfirmList";
											this.LblTitle.Text = "工程洽商单";
											this.LblName.Text = "工程洽商单名称：";
										}
										else
										{
											if (this._BigSort == "6")
											{
												this.ViewState["parentName"] = "_measuredataquery";
												if (this._SmallSort == "1")
												{
													this.LblTitle.Text = "技术竣工计划";
													this.LblName.Text = "技术竣工计划名称：";
												}
												else
												{
													this.LblTitle.Text = "试车主要计划";
													this.LblName.Text = "试车主要计划名称：";
												}
											}
										}
									}
								}
							}
						}
					}
				}
				if (this._Type == "Upd")
				{
					this.TxtStandCode.Enabled = false;
					this.ViewState["ID"] = base.Request.QueryString["Id"].ToString();
					this._Id = this.ViewState["ID"].ToString();
					this.GetMeasureInfoOfUpd(this._Id);
					this.FileLink1.MID = 1726;
					this.FileLink1.Type = 1;
					this.lb_change.Text = "编辑";
				}
				else
				{
					if (this._Type == "Add")
					{
						this.ViewState["MAXID"] = MeasureDataAction.GetMaxId();
						this._MaxId = this.ViewState["MAXID"].ToString();
						this.FileLink1.MID = 1726;
						this.hdnTechGuid.Value = Guid.NewGuid().ToString();
						this.FileLink1.FID = this.hdnTechGuid.Value.Trim();
						this.FileLink1.Type = 2;
						this.lb_change.Text = "新增";
					}
					else
					{
						if (this._Type == "View")
						{
							this.BtnSave.Visible = false;
							this.SetControl();
							this.TxtStandCode.Enabled = false;
							if (base.Request.QueryString["Id"] != null)
							{
								this.ViewState["ID"] = base.Request.QueryString["Id"].ToString();
							}
							if (base.Request.QueryString["ic"] != null)
							{
								DataTable modelByGuid = MeasureDataAction.GetModelByGuid(base.Request.QueryString["ic"].ToString());
								if (modelByGuid != null)
								{
									this.ViewState["ID"] = modelByGuid.Rows[0]["Id"].ToString();
								}
							}
							this._Id = this.ViewState["ID"].ToString();
							this.GetMeasureInfoOfUpd(this._Id);
							this.FileLink1.MID = 1726;
							this.FileLink1.Type = 0;
							this.lb_change.Text = "查看";
							this.BunClose.Value = "关 闭";
							this.FileLink1.Visible = false;
							this.Literal1.Text = FileView.FilesBind(1726, this.hdnTechGuid.Value.Trim());
						}
					}
				}
			}
			else
			{
				this._Type = this.ViewState["TYPE"].ToString();
				if (this._Type == "Add")
				{
					this._PrjCode = this.ViewState["PRJCODE"].ToString();
					this._MaxId = this.ViewState["MAXID"].ToString();
					this._SmallSort = this.ViewState["SMALLSORT"].ToString();
					this._BigSort = this.ViewState["BIGSORT"].ToString();
				}
				else
				{
					if (this._Type == "Upd")
					{
						this._Id = this.ViewState["ID"].ToString();
					}
					else
					{
						if (this._Type == "View")
						{
							this._Id = this.ViewState["ID"].ToString();
							if (this._Id != "" && this._Id != null)
							{
								this.GetMeasureInfoOfUpd(this._Id);
							}
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(base.Request["pn"]))
			{
				this.parentName = base.Request["pn"];
			}
		}
		private void SetControl()
		{
			this.TxtStandCode.Enabled = false;
			this.TxtStandName.Enabled = false;
			this.TxtRemark.Enabled = false;
		}
		protected override void OnInit(EventArgs e)
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private MeasureDataInfo GetMeasureInfo()
		{
			MeasureDataInfo measureDataInfo = new MeasureDataInfo();
			if (this._Type == "Add")
			{
				measureDataInfo.Id = int.Parse(this._MaxId);
				measureDataInfo.BigSort = int.Parse(this._BigSort);
				if (this._SmallSort.Trim() != null)
				{
					measureDataInfo.SmallSort = int.Parse(this._SmallSort);
				}
				else
				{
					measureDataInfo.SmallSort = 0;
				}
			}
			else
			{
				measureDataInfo.Id = int.Parse(this._Id);
			}
			measureDataInfo.PrjCode = this._PrjCode;
			measureDataInfo.SerialNumber = this.TxtStandCode.Text.Trim();
			measureDataInfo.ItemName = this.TxtStandName.Text.Trim();
			measureDataInfo.Remark = this.TxtRemark.Text;
			measureDataInfo.FlowState = -1;
			measureDataInfo.TechGuid = this.hdnTechGuid.Value.Trim();
			if (!string.IsNullOrEmpty(this.hfldPerson.Value.Trim()))
			{
				measureDataInfo.ReceivePerson = this.hfldPerson.Value.Trim();
			}
			else
			{
				measureDataInfo.ReceivePerson = this.txtReceive.Text;
			}
			if (!string.IsNullOrEmpty(this.hfldName.Value.Trim()))
			{
				measureDataInfo.JoinPerson = this.hfldName.Value.Trim();
			}
			else
			{
				measureDataInfo.JoinPerson = this.txtJoin.Text;
			}
			return measureDataInfo;
		}
		private void GetMeasureInfoOfUpd(string Id)
		{
			DataTable measureOfSingle = MeasureDataAction.GetMeasureOfSingle(Id);
			this.TxtStandCode.Text = measureOfSingle.Rows[0]["SerialNumber"].ToString();
			this.TxtStandName.Text = measureOfSingle.Rows[0]["ItemName"].ToString();
			this.TxtRemark.Text = measureOfSingle.Rows[0]["Remark"].ToString();
			string value = measureOfSingle.Rows[0]["mark"].ToString();
			this.hdnTechGuid.Value = measureOfSingle.Rows[0]["TechGuid"].ToString();
			this.FileLink1.FID = this.hdnTechGuid.Value.Trim();
			this.hdnmark.Value = value;
			if (measureOfSingle.Rows[0]["mark"] != null && measureOfSingle.Rows[0]["mark"].ToString() != "")
			{
				int num = int.Parse(measureOfSingle.Rows[0]["mark"].ToString());
				if (num != 2)
				{
					this.cbkmark.Checked = true;
					this.hidenClass.Value = measureOfSingle.Rows[0]["filesType"].ToString();
				}
				else
				{
					this.cbkmark.Checked = false;
				}
			}
			if (!string.IsNullOrEmpty(measureOfSingle.Rows[0]["JoinPerson"].ToString()))
			{
				if (!string.IsNullOrEmpty(WebUtil.GetUserNames(measureOfSingle.Rows[0]["JoinPerson"].ToString())))
				{
					this.txtJoin.Text = WebUtil.GetUserNames(measureOfSingle.Rows[0]["JoinPerson"].ToString());
					this.hfldName.Value = measureOfSingle.Rows[0]["JoinPerson"].ToString();
				}
				else
				{
					this.txtJoin.Text = measureOfSingle.Rows[0]["JoinPerson"].ToString();
				}
			}
			if (!string.IsNullOrEmpty(measureOfSingle.Rows[0]["ReceivePerson"].ToString()))
			{
				if (!string.IsNullOrEmpty(WebUtil.GetUserNames(measureOfSingle.Rows[0]["ReceivePerson"].ToString())))
				{
					this.txtReceive.Text = WebUtil.GetUserNames(measureOfSingle.Rows[0]["ReceivePerson"].ToString());
					this.hfldPerson.Value = measureOfSingle.Rows[0]["ReceivePerson"].ToString();
					return;
				}
				this.txtReceive.Text = measureOfSingle.Rows[0]["ReceivePerson"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			DatumLogic datumLogic = new DatumLogic();
			MeasureDataInfo measureInfo = this.GetMeasureInfo();
			if (this._Type == "Add")
			{
				int num = MeasureDataAction.MeasureAdd(measureInfo);
				if (num == 1)
				{
					int maxID = datumLogic.getMaxID("Prj_TechnologyManage", "ID");
					if (maxID > 0)
					{
						if (this.cbkmark.Checked)
						{
							datumLogic.UpdateByID("Prj_TechnologyManage", "[mark]=3 ,filesType=" + int.Parse(this.DDTClass.SelectedValue.ToString()) + " ", "ID", maxID.ToString(), null);
						}
						else
						{
							datumLogic.UpdateByID("Prj_TechnologyManage", "[mark]=2 ,filesType=" + int.Parse(this.DDTClass.SelectedValue.ToString()) + " ", "ID", maxID.ToString(), null);
						}
					}
					base.RegisterScript("top.ui.tabSuccess({ parentName: '" + this.parentName + "' });");
					this.BtnSave.Enabled = false;
					return;
				}
				base.RegisterScript("top.ui.show('保存失败')");
				return;
			}
			else
			{
				int num = MeasureDataAction.MeasureUpd(measureInfo);
				if (num == 1)
				{
					int id = measureInfo.Id;
					if (id > 0)
					{
						if (this.cbkmark.Checked)
						{
							datumLogic.UpdateByID("Prj_TechnologyManage", "[mark]=3 ,filesType=" + int.Parse(this.DDTClass.SelectedValue.ToString()) + " ", "ID", id.ToString(), null);
						}
						else
						{
							datumLogic.UpdateByID("Prj_TechnologyManage", "[mark]=2 ,filesType=" + int.Parse(this.DDTClass.SelectedValue.ToString()) + " ", "ID", id.ToString(), null);
						}
					}
					base.RegisterScript("top.ui.tabSuccess({ parentName: '" + this.parentName + "' });");
					return;
				}
				base.RegisterScript("top.ui.show('保存失败')");
				return;
			}
		}
	}


