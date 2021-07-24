using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Project;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_PrequalificationManage : NBasePage, IRequiresSessionState
{
	private string SignUpWarnDay = ConfigurationManager.AppSettings["SignUpWarnDay"];
	private PTPrjInfoService prjInfoSer = new PTPrjInfoService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			TypeList.BindDrop(this.dropPrjState, false, "", null, new System.Collections.Generic.List<int>
			{
				int.Parse(ProjectParameter.Initiate),
				int.Parse(ProjectParameter.Prequalification)
			});
			this.hfldUserCode.Value = base.UserCode;
			this.bindGv();
		}
	}
	protected void bindGv()
	{
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>
		{
			int.Parse(ProjectParameter.Initiate),
			int.Parse(ProjectParameter.Prequalification)
		};
		if (this.dropPrjState.SelectedValue != "")
		{
			list.Clear();
			list.Add(int.Parse(this.dropPrjState.SelectedValue));
		}
		System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
		{
			1
		};
		string text = this.txtName.Text;
		int count = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, text, 4, null, "InitiateFlowState");
		this.AspNetPager1.RecordCount = count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, text, false, 4, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, null, "InitiateFlowState");
		this.gvDataInfo.DataSource = all;
		this.gvDataInfo.DataBind();
		string value = string.Empty;
		if (this.dropPrjState.SelectedValue != string.Empty)
		{
			value = this.dropPrjState.SelectedValue;
		}
		if (string.IsNullOrEmpty(value))
		{
			System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
			{
				3
			};
			int count2 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 4, null, "InitiateFlowState");
			prjState = new System.Collections.Generic.List<int>
			{
				14
			};
			int count3 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 4, null, "InitiateFlowState");
			string text2 = "<span style='margin-left:3px;margin-right:3px;'>";
			string text3 = "</span>";
			this.lblTotal.Text = string.Concat(new object[]
			{
				"汇总：报名通过",
				text2,
				count2,
				text3,
				"项，资格预审",
				text2,
				count3,
				text3,
				"项"
			});
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		System.Collections.Generic.List<int> prjState2 = new System.Collections.Generic.List<int>
		{
			System.Convert.ToInt32(value)
		};
		int count4 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState2, flowState, base.UserCode, text, 4, null, "InitiateFlowState");
		if (System.Convert.ToInt32(value) == 3)
		{
			num = count4;
		}
		else
		{
			if (System.Convert.ToInt32(value) == 14)
			{
				num2 = count4;
			}
			else
			{
				if (System.Convert.ToInt32(value) == 18)
				{
					num3 = count4;
				}
			}
		}
		string text4 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text5 = "</span>";
		this.lblTotal.Text = string.Concat(new object[]
		{
			"汇总：报名通过",
			text4,
			num,
			text5,
			"项，资格预审",
			text4,
			num2,
			text5,
			"项, 放弃",
			text4,
			num3,
			text5,
			"项"
		});
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			e.Row.Attributes["state"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjState"].ToString();
			e.Row.Attributes["OldState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["OldState"].ToString();
			e.Row.Attributes["PftFlowState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PftFlowState"].ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>
			{
				int.Parse(ProjectParameter.Initiate),
				int.Parse(ProjectParameter.Prequalification)
			};
			if (this.dropPrjState.SelectedValue != "")
			{
				list.Clear();
				list.Add(int.Parse(this.dropPrjState.SelectedValue));
			}
			System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
			{
				1
			};
			decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, this.txtName.Text, 4, null, "InitiateFlowState");
			e.Row.Cells[6].Text = sumTotal.ToString("#0.000");
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void btnSaveData_Click(object sender, System.EventArgs e)
	{
		try
		{
			string prjGuid = this.hfldPrjId.Value;
			TenderInfo byId = TenderInfo.GetById(prjGuid);
			System.DateTime? projApplyDate = null;
			if (!string.IsNullOrEmpty(this.txtApplyDate.Text))
			{
				projApplyDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApplyDate.Text));
			}
			byId.ProjApplyDate = projApplyDate;
			System.DateTime? projApprovalDate = null;
			if (!string.IsNullOrEmpty(this.txtApprovalDate.Text))
			{
				projApprovalDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApprovalDate.Text));
			}
			byId.ProjApprovalDate = projApprovalDate;
			System.DateTime? projTenderDate = null;
			if (!string.IsNullOrEmpty(this.txtTenderDate.Text))
			{
				projTenderDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderDate.Text));
			}
			byId.ProjTenderDate = projTenderDate;
			if (!string.IsNullOrEmpty(this.txtRegistDeadline.Text))
			{
				byId.ProjRegistDeadline = new int?(int.Parse(this.txtRegistDeadline.Text));
			}
			byId.ProgAgent = this.hfldAgent.Value;
			byId.PrequalificationRequire = this.txtPrequalificationRequire.Text;
			if (!string.IsNullOrWhiteSpace(this.txtQualificationMargin.Text))
			{
				byId.QualificationMargin = System.Convert.ToDecimal(this.txtQualificationMargin.Text);
			}
			byId.QualificationReadOne = this.hfldQualificationReadOne.Value;
			byId.UpdatePart(byId, ProjectParameter.Prequalification);
			System.Guid prjId = new System.Guid(prjGuid);
			bool flag = this.prjInfoSer.IsExist(prjId);
			if (flag)
			{
				PTPrjInfo byId2 = this.prjInfoSer.GetById(prjGuid);
				byId2.PrjState = new int?(System.Convert.ToInt32(ProjectParameter.Prequalification));
				this.prjInfoSer.Update(byId2);
			}
			if (!string.IsNullOrEmpty(byId.QualificationReadOne))
			{
				PTDBSJService pTDBSJService = new PTDBSJService();
				pTDBSJService.Add(new PTDBSJ
				{
					I_XGID = byId.PrjGuid.ToString(),
					V_LXBM = "024",
					V_YHDM = byId.QualificationReadOne,
					DTM_DBSJ = new System.DateTime?(System.DateTime.Now),
					C_OpenFlag = "0",
					V_Content = "项目：" + byId.PrjName + "已经开始资格预审",
					V_DBLJ = "TenderManage/InfoView.aspx?ic=" + byId.PrjGuid.ToString(),
					V_TPLJ = "new_Mail.gif"
				});
			}
			if (!string.IsNullOrEmpty(this.txtApplyDate.Text.Trim()))
			{
				PTDBSJTodayService pTDBSJTodayService = new PTDBSJTodayService();
				System.Collections.Generic.List<PTDBSJToday> list = (
					from dbsj in pTDBSJTodayService
					where dbsj.I_XGID == prjGuid
					select dbsj).ToList<PTDBSJToday>();
				foreach (PTDBSJToday current in list)
				{
					pTDBSJTodayService.Delete(current);
				}
				System.Collections.Generic.List<TenderUser> byId3 = TenderUser.GetById(byId.PrjGuid.ToString());
				System.DateTime value = System.Convert.ToDateTime(this.txtApplyDate.Text.Trim()).AddDays((double)(-(double)System.Convert.ToInt32(this.SignUpWarnDay)));
				foreach (TenderUser current2 in byId3)
				{
					pTDBSJTodayService.Add(new PTDBSJToday
					{
						I_XGID = prjGuid,
						V_LXBM = "027",
						V_YHDM = current2.UserCode,
						DTM_DBSJ = new System.DateTime?(value),
						V_TPLJ = "",
						V_DBLJ = "TenderManage/InfoView.aspx?ic=" + prjGuid,
						V_Content = "名称为：" + byId.PrjName + "的项目已经开始报名。",
						C_OpenFlag = "1"
					});
				}
			}
			this.bindGv();
			base.RegisterShow("系统提示", "保存成功");
		}
		catch (System.Exception)
		{
			base.RegisterShow("系统提示", "保存失败");
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldPrjId.Value;
		System.Guid id = new System.Guid(value);
		PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
		PTPrjInfoZTB byId = pTPrjInfoZTBService.GetById(id);
		byId.GiveUpTime = Common2.ConverToDateTime(this.txtGiveUpTime.Text.Trim());
		byId.OldState = new int?(System.Convert.ToInt32(ProjectParameter.Initiate));
		byId.IsGiveUp = true;
		byId.GiveUpReason = this.txtGiveUPReason.Text.Trim();
		byId.GiveUpNote = this.txtNote.Text.Trim();
		byId.PrjState = new int?(System.Convert.ToInt32(ProjectParameter.GiveUpState));
		byId.Operator = this.hfldOperator.Value;
		pTPrjInfoZTBService.Update(byId);
		this.bindGv();
		base.RegisterShow("系统提示", "保存成功");
	}
}


