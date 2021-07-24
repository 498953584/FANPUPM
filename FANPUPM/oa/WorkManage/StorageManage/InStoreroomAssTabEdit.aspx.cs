using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_InStoreroomAssTabEdit : BasePage, IRequiresSessionState
{

	public ptOfficeResInStorageDetailAction amAction
	{
		get
		{
			return new ptOfficeResInStorageDetailAction();
		}
	}
	public int DepotID
	{
		get
		{
			object obj = this.ViewState["DEPOTID"];
			if (obj != null)
			{
				return (int)this.ViewState["DEPOTID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DEPOTID"] = value;
		}
	}
	public int RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (int)this.ViewState["RECORDID"];
			}
			return 0;
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public int InStorageID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (int)this.ViewState["INSTORAGEID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public string BillCode
	{
		get
		{
			object obj = this.ViewState["BILLCODE"];
			if (obj != null)
			{
				return (string)this.ViewState["BILLCODE"];
			}
			return "";
		}
		set
		{
			this.ViewState["BILLCODE"] = value;
		}
	}
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["rid"] == null || base.Request["id"] == null || base.Request["dd"] == null || base.Request["bc"] == null || base.Request["op"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["rid"].ToString() != "")
			{
				this.RecordID = int.Parse(base.Request["rid"].ToString());
			}
			if (base.Request["id"].ToString() != "")
			{
				this.InStorageID = int.Parse(base.Request["id"].ToString());
			}
			if (base.Request["dd"].ToString() != "")
			{
				this.DepotID = int.Parse(base.Request["dd"].ToString());
				this.HdnDepotID.Value = this.DepotID.ToString();
			}
			this.BillCode = base.Request["bc"].ToString();
			this.txtInBillCode.Text = this.BillCode;
			if (this.OperateType == "upd")
			{
				this.imgsel.Disabled = true;
				this.EditDisplay();
			}
		}
		this.txtNumber.Attributes["onblur"] = "javascript:checkDecimal(this);";
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("b.RecordID=" + this.RecordID);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtResName.Text = dataRow["ResName"].ToString();
			this.txtUnit.Text = dataRow["Unit"].ToString();
			this.DDLGetType.SelectedValue = dataRow["GetType"].ToString();
			this.DDLUseType.SelectedValue = dataRow["UseType"].ToString();
			this.HdnMatterialID.Value = dataRow["MaterialID"].ToString();
			this.txtNumber.Text = dataRow["Amount"].ToString();
			this.txtInStoragePrice.Text = dataRow["InStoragePrice"].ToString();
			this.txtHandleMan.Text = dataRow["HandleMan"].ToString();
		}
	}
	private ptOfficeResInStorageDetail GetData()
	{
		return new ptOfficeResInStorageDetail
		{
			Amount = (this.txtNumber.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtNumber.Text.Trim()),
			HandleMan = this.txtHandleMan.Text,
			InStorageID = this.InStorageID,
			InStoragePrice = (this.txtInStoragePrice.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtInStoragePrice.Text.Trim()),
			MaterialID = int.Parse(this.HdnMatterialID.Value),
			RecordID = this.RecordID
		};
	}
	private ptOfficeResStock GetStockData()
	{
		return new ptOfficeResStock
		{
			DepotID = this.DepotID,
			RecordID = this.RecordID,
			MaterialID = int.Parse(this.HdnMatterialID.Value),
			Number = (this.txtNumber.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtNumber.Text.Trim()),
			AvgPrice = (this.txtInStoragePrice.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtInStoragePrice.Text.Trim())
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ptOfficeResInStorageDetail data = this.GetData();
		ptOfficeResStock stockData = this.GetStockData();
		if (this.OperateType == "add")
		{
			this.amAction.AddStock(stockData);
			int num = this.amAction.Add(data);
			if (num > 0)
			{
				this.Page.RegisterStartupScript("", "<script>alert('添加成功!');returnValue=true;window.close();</script>");
			}
			else
			{
				this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可添加!');</script>");
			}
		}
		if (this.OperateType == "upd")
		{
			this.amAction.UpdateStock(stockData, this.RecordID);
			int num = this.amAction.Update(data);
			if (num > 0)
			{
				this.Page.RegisterStartupScript("", "<script>alert('修改成功!');returnValue=true;window.close();</script>");
				return;
			}
			this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可更新!');</script>");
		}
	}
}


