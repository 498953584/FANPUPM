using ASP;
using cn.justwin.BLL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_TypeAndState_TypeAndStateEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string typeId = string.Empty;
	private string priGuid = string.Empty;
	private TypeAndStateBll TASBLL = new TypeAndStateBll();
	private TypeAndState model;

	protected override void OnInit(EventArgs e)
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
		if (!string.IsNullOrEmpty(base.Request["PrjGuid"]))
		{
			this.priGuid = base.Request["PrjGuid"];
		}
		this.trr.Visible = false;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.priGuid != "")
			{
				this.hidenPTID.Value = this.priGuid;
			}
			else
			{
				this.hidenPTID.Value = Guid.NewGuid().ToString();
			}
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
	private void InitUpdate()
	{
		if (!string.IsNullOrEmpty(this.typeId))
		{
			this.model = new TypeAndState();
			this.model = this.TASBLL.GetModel(new Guid(this.typeId));
			this.txtName.Text = this.model.Name.ToString();
			this.txtCode.Text = this.model.code;
			this.txtCode.Enabled = false;
			this.HidenTypeNumber.Value = this.model.Type.ToString();
		}
	}
	private void InitAdd()
	{
		DateTime now = DateTime.Now;
		this.txtCode.Text = now.ToString("yyyyMMdd") + now.Millisecond.ToString();
		this.txtCode.Enabled = false;
		if (this.priGuid != "")
		{
			TypeAndState typeAndState = this.TASBLL.GetModel(new Guid(this.priGuid));
			string text = string.Empty;
			if (typeAndState != null)
			{
				text = typeAndState.Type.ToString();
				this.HidenTypeNumber.Value = text.ToString();
				this.hidenPTID.Value = this.priGuid;
				return;
			}
			if (this.priGuid == "00000000-0000-0000-0000-000000000000")
			{
				List<TypeAndState> modelList = this.TASBLL.GetModelList("ParentGuid='00000000-0000-0000-0000-000000000000'");
				if (modelList.Count >= 2)
				{
					this.txtName.Enabled = true;
					this.trr.Visible = false;
					this.btnSave.Enabled = true;
				}
				else
				{
					this.txtName.Enabled = true;
					this.trr.Visible = true;
				}
				this.RadioButtonList1.Items[0].Selected = true;
				for (int i = 0; i < modelList.Count; i++)
				{
					if (modelList[i].Type == 1)
					{
						this.RadioButtonList1.Items[0].Selected = true;
					}
					else
					{
						this.RadioButtonList1.Items[1].Selected = true;
					}
				}
			}
		}
	}
	protected void addBtn_Click(object sender, EventArgs e)
	{
		if (string.Compare(this.action, "Add", true) == 0)
		{
			this.model = new TypeAndState();
			this.model.guid = default(Guid);
			this.model.Name = this.txtName.Text.ToString().Trim();
			if (this.HidenTypeNumber.Value.ToString() != "")
			{
				this.model.Type = new int?(int.Parse(this.HidenTypeNumber.Value.ToString()));
			}
			else
			{
				this.model.Type = new int?(int.Parse(this.RadioButtonList1.SelectedValue.ToString()));
			}
			this.model.code = this.txtCode.Text.ToString().Trim();
			this.model.ParentGuid = new Guid(this.hidenPTID.Value.ToString());
			try
			{
				if (this.txtName.Text.ToString().Trim() != "")
				{
					DataTable list = this.TASBLL.GetList("Name= '" + this.txtName.Text.ToString().Trim() + "'");
					if (list != null && list.Rows.Count > 0)
					{
						base.RegisterScript("top.ui.alert('类型名称已经存在')");
					}
					else
					{
						this.TASBLL.Add(this.model);
						base.RegisterScript("successed();");
					}
				}
				return;
			}
			catch (Exception)
			{
				base.RegisterScript("top.ui.alert('添加失败')");
				return;
			}
		}
		this.model = new TypeAndState();
		this.model = this.TASBLL.GetModel(new Guid(this.typeId));
		this.model.Name = this.txtName.Text.ToString().Trim();
		this.model.code = this.txtCode.Text.ToString().Trim();
		this.model.Type = new int?(int.Parse(this.HidenTypeNumber.Value.ToString()));
		if (this.TASBLL.Update(this.model))
		{
			base.RegisterScript("successed();");
			return;
		}
		base.RegisterScript("top.ui.alert('更新失败')");
	}
	protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.HidenTypeNumber.Value = this.RadioButtonList1.SelectedValue.ToString();
	}
}


