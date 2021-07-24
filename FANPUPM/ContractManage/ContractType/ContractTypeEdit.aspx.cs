using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractType_ContractTypeEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string typeId = string.Empty;
	private ContractType contractType = new ContractType();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["TypeID"]))
		{
			if (base.Request["TypeID"].Contains("["))
			{
				this.typeId = JsonHelper.GetListFromJson(base.Request["TypeID"])[0];
			}
			else
			{
				this.typeId = base.Request["TypeID"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			System.Collections.Generic.List<CostAccounting> byD = CostAccounting.GetByD();
			this.ddlCBS.DataSource = byD;
			this.ddlCBS.DataTextField = "Name";
			this.ddlCBS.DataValueField = "Code";
			this.ddlCBS.DataBind();
			this.ddlCBS.Items.Insert(0, new ListItem("--请选择直接成本--", ""));
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InitAdd();
				return;
			}
			if (string.Compare(this.action, "Update", true) == 0)
			{
				this.InitUpdate();
				return;
			}
			if (string.Compare(this.action, "Query", true) == 0)
			{
				this.InitUpdate();
			}
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		ConContractTypeService conContractTypeService = new ConContractTypeService();
		ConContractType conContractType = new ConContractType();
		if (string.Compare(this.action, "Add", true) == 0)
		{
			conContractType = conContractTypeService.GetContractTypeByTypeName(this.txtTypeName.Text.Trim());
			if (conContractType != null)
			{
				base.RegisterScript("top.ui.alert('合同类型名称已经存在！')");
				return;
			}
			conContractType = conContractTypeService.GetContractTypeByTypeShort(this.txtTypeShort.Text.Trim());
			if (conContractType != null)
			{
				base.RegisterScript("top.ui.alert('合同类型简写已经存在！')");
				return;
			}
			ContractTypeModel contractTypeModel = new ContractTypeModel();
			contractTypeModel.TypeID = System.Guid.NewGuid().ToString();
			contractTypeModel.TypeCode = this.txtTypeCode.Text.Trim();
			contractTypeModel.TypeName = this.txtTypeName.Text.Trim();
			if (base.UserCode == "00000000")
			{
				contractTypeModel.UserCodes = JsonHelper.Serialize(new string[]
				{
					"00000000"
				});
			}
			else
			{
				contractTypeModel.UserCodes = JsonHelper.Serialize(new string[]
				{
					"00000000",
					base.UserCode
				});
			}
			contractTypeModel.Notes = this.txtNotes.Text.Trim();
			contractTypeModel.InputPerson = this.txtInputPerson.Text;
			contractTypeModel.InputDate = new System.DateTime?(System.Convert.ToDateTime(this.txtInputDate.Text));
			contractTypeModel.TypeShort = this.txtTypeShort.Text.Trim();
			if (!string.IsNullOrEmpty(this.ddlCBS.SelectedValue.Trim()))
			{
				contractTypeModel.CBSCode = this.ddlCBS.SelectedValue.Trim();
			}
			else
			{
				contractTypeModel.CBSCode = null;
			}
			try
			{
				this.contractType.Add(contractTypeModel);
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_ContractTypeEdit' });");
				return;
			}
			catch (System.Exception)
			{
				base.RegisterScript("top.ui.alert('添加失败')");
				return;
			}
		}
		string text = base.Request["TypeID"].ToString();
		string cmdText = string.Concat(new string[]
		{
			"SELECT * FROM Con_ContractType WHERE TypeID not in('",
			text,
			"') and TypeName='",
			this.txtTypeName.Text.Trim(),
			"'"
		});
		DataTable dataTable = conContractTypeService.ExecuteQuery(cmdText, null);
		if (dataTable.Rows.Count > 0)
		{
			base.RegisterScript("top.ui.alert('合同类型名称已经存在！')");
			return;
		}
		cmdText = string.Concat(new string[]
		{
			"SELECT * FROM Con_ContractType WHERE TypeID not in('",
			text,
			"') and TypeShort='",
			this.txtTypeShort.Text.Trim(),
			"'"
		});
		dataTable = conContractTypeService.ExecuteQuery(cmdText, null);
		if (dataTable.Rows.Count > 0)
		{
			base.RegisterScript("top.ui.alert('合同类型简写已经存在！')");
			return;
		}
		ContractTypeModel model = this.contractType.GetModel(text);
		model.TypeCode = this.txtTypeCode.Text.Trim();
		model.TypeName = this.txtTypeName.Text.Trim();
		model.Notes = this.txtNotes.Text.Trim();
		model.TypeShort = this.txtTypeShort.Text.Trim();
		if (!string.IsNullOrEmpty(this.ddlCBS.SelectedValue.Trim()))
		{
			model.CBSCode = this.ddlCBS.SelectedValue.Trim();
		}
		else
		{
			model.CBSCode = null;
		}
		try
		{
			this.contractType.Update(model);
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_ContractTypeEdit' });");
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.alert('添加失败')");
		}
	}
	private void InitAdd()
	{
		System.DateTime now = System.DateTime.Now;
		this.txtTypeCode.Text = now.ToString("yyyyMMdd") + now.Millisecond.ToString();
		this.txtInputDate.Text = System.DateTime.Now.ToString();
		new PTPrjInfoBll();
		this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
	}
	private void InitUpdate()
	{
		if (!string.IsNullOrEmpty(this.typeId))
		{
			ContractTypeModel model = this.contractType.GetModel(this.typeId);
			this.txtTypeCode.Text = model.TypeCode;
			this.txtTypeName.Text = model.TypeName;
			this.txtInputPerson.Text = model.InputPerson;
			this.txtInputDate.Text = model.InputDate.ToString();
			this.txtNotes.Text = model.Notes;
			this.ddlCBS.SelectedValue = model.CBSCode;
			this.txtTypeShort.Text = model.TypeShort;
		}
	}
}


