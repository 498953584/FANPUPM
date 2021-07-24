using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Basic_cityEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["action"]))
		{
			if (base.Request.QueryString["action"] == "Edit")
			{
				this.hlfdProvinceID.Value = base.Request.QueryString["provinceId"];
				this.hlfdCityID.Value = base.Request.QueryString["cityID"];
				this.showInfoCity(this.hlfdCityID.Value);
				return;
			}
			this.hlfdProvinceID.Value = base.Request.QueryString["provinceId"];
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request.QueryString["action"]))
		{
			this.action = base.Request.QueryString["action"];
		}
		base.OnInit(e);
	}
	protected void showInfoCity(string cityID)
	{
		BasicCity basicCity = new BasicCity();
		if (!string.IsNullOrEmpty(cityID))
		{
			basicCity = new BasicCityService().GetCityByCityID(cityID);
			this.txtCityName.Text = basicCity.Name;
			this.txtCityCode.Text = basicCity.Code;
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.action))
		{
			BasicCity basicCity = new BasicCity();
			BasicCityService basicCityService = new BasicCityService();
			DataTable dataTable = new DataTable();
			if (this.action == "Add")
			{
				string cmdText = "SELECT * FROM Basic_City WHERE Name='" + this.txtCityName.Text + "'";
				dataTable = basicCityService.ExecuteQuery(cmdText, new SqlParameter[0]);
				if (dataTable.Rows.Count > 0)
				{
					base.RegisterScript("top.ui.alert('系统提示：\\n\\n 该城市名称已经存在!');");
					return;
				}
				cmdText = "SELECT * FROM Basic_City WHERE Code='" + this.txtCityCode.Text + "'";
				dataTable = basicCityService.ExecuteQuery(cmdText, new SqlParameter[0]);
				if (dataTable.Rows.Count > 0)
				{
					base.RegisterScript("top.ui.alert('系统提示：\\n\\n 该城市编码已经存在!');");
					return;
				}
				cmdText = "SELECT MAX(OrderNo) FROM Basic_City WHERE ProvinceId='" + this.hlfdProvinceID.Value + "'";
				dataTable = basicCityService.ExecuteQuery(cmdText, new SqlParameter[0]);
				string value = dataTable.Rows[0][0].ToString();
				int num = 0;
				if (!string.IsNullOrEmpty(value))
				{
					num = Convert.ToInt32(value);
				}
				basicCity.Id = Guid.NewGuid();
				basicCity.Name = this.txtCityName.Text.Trim();
				basicCity.Code = this.txtCityCode.Text.Trim();
				basicCity.ProvinceId = new Guid(this.hlfdProvinceID.Value);
				basicCity.OrderNo = new int?(num + 1);
				basicCityService.Add(basicCity);
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_cityEdit' });");
				return;
			}
			else
			{
				if (this.action == "Edit")
				{
					string cmdText = string.Concat(new string[]
					{
						"SELECT * FROM Basic_City WHERE Name='",
						this.txtCityName.Text,
						"' AND Id NOT IN('",
						this.hlfdCityID.Value,
						"')"
					});
					dataTable = basicCityService.ExecuteQuery(cmdText, new SqlParameter[0]);
					if (dataTable.Rows.Count > 0)
					{
						base.RegisterScript("top.ui.alert('系统提示：\\n\\n 该城市名称已经存在!');");
						return;
					}
					cmdText = string.Concat(new string[]
					{
						"SELECT * FROM Basic_City WHERE Code='",
						this.txtCityCode.Text,
						"' AND Id NOT IN('",
						this.hlfdCityID.Value,
						"')"
					});
					dataTable = basicCityService.ExecuteQuery(cmdText, new SqlParameter[0]);
					if (dataTable.Rows.Count > 0)
					{
						base.RegisterScript("top.ui.alert('系统提示：\\n\\n 该城市编码已经存在!');");
						return;
					}
					basicCity = basicCityService.GetCityByCityID(this.hlfdCityID.Value);
					basicCity.Name = this.txtCityName.Text.Trim();
					basicCity.Code = this.txtCityCode.Text.Trim();
					basicCityService.Update(basicCity);
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_cityEdit' });");
				}
			}
		}
	}
}


