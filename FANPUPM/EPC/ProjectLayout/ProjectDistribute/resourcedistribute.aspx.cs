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
	public partial class ResourceDistribute : PageBase, IRequiresSessionState
	{
		protected TaskResourceAction tra
		{
			get
			{
				return new TaskResourceAction();
			}
		}
		protected ResourceItemAction ResItemAct
		{
			get
			{
				return new ResourceItemAction();
			}
		}
		public Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["PROJECTCODE"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		public string TaskCode
		{
			get
			{
				return (string)this.ViewState["TASKCODE"];
			}
			set
			{
				this.ViewState["TASKCODE"] = value;
			}
		}
		public int OpType
		{
			get
			{
				object obj = this.ViewState["OPTYPE"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["OPTYPE"] = value;
			}
		}
		public string ResourceCode
		{
			get
			{
				object obj = this.ViewState["RESOURCECODE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["RESOURCECODE"] = value;
			}
		}
		public int WbsType
		{
			get
			{
				object obj = this.ViewState["WBSTYPE"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				this.ViewState["WBSTYPE"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["tc"] == null || base.Request["wbs"] == null)
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			this.TaskCode = base.Request["tc"].ToString();
			this.WbsType = Convert.ToInt32(base.Request["wbs"].ToString());
			if (!this.Page.IsPostBack)
			{
				this.DataGrid_Bind("1,2,3", this.WbsType);
				this.BtnEdit.Attributes["onclick"] = "clickEdit();return false;";
				this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;} ";
				this.BtnAdd.Attributes["onclick"] = "if( !pickResource()) { return false;}";
				this.TxtResourceCode.Attributes.Add("ReadOnly", "true");
			}
			this.TxtPrice.Attributes["onkeydown"] = "event.returnValue=CheckInputIsFloat(event.keyCode,this)";
			this.TxtQuantity.Attributes["onkeydown"] = "event.returnValue=CheckInputIsFloat(event.keyCode,this)";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdRBS.ItemDataBound += new DataGridItemEventHandler(this.DGrdRBS_ItemDataBound);
		}
		public void DataGrid_Bind(string resourceStyle, int wbsType)
		{
			DataTable scheduleResource = this.tra.GetScheduleResource(this.ProjectCode, this.TaskCode, resourceStyle, wbsType);
			this.DGrdRBS.DataSource = scheduleResource;
			this.DGrdRBS.DataBind();
		}
		private void DGrdRBS_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DGrdRBS.ClientID,
					"');ClickRow(this,",
					dataRowView["NoteID"].ToString(),
					",'",
					dataRowView["ResourceCode"].ToString(),
					"');"
				});
				switch (Convert.ToInt32(dataRowView["ResourceStyle"].ToString()))
				{
				case 1:
					e.Item.Cells[7].Text = "人工";
					break;
				case 2:
					e.Item.Cells[7].Text = "材料";
					break;
				case 3:
					e.Item.Cells[7].Text = "机械";
					break;
				}
			}
			e.Item.Cells[9].Style["display"] = "none";
		}
		private SLTaskResource GetData()
		{
			SLTaskResource sLTaskResource = new SLTaskResource();
			sLTaskResource.NoteID = Convert.ToInt32(this.hdnNoteId.Value);
			sLTaskResource.ProjectCode = this.ProjectCode.ToString();
			sLTaskResource.TaskCode = this.TaskCode;
			sLTaskResource.WbsType = this.WbsType;
			sLTaskResource.ResourceCode = this.TxtResourceCode.Text.Trim();
			try
			{
				sLTaskResource.Quantity = Convert.ToDecimal(this.TxtQuantity.Text.Trim());
			}
			catch
			{
				sLTaskResource.Quantity = 0m;
			}
			try
			{
				sLTaskResource.UnitPrice = Convert.ToDecimal(this.TxtPrice.Text.Trim());
			}
			catch
			{
				sLTaskResource.UnitPrice = 0m;
			}
			try
			{
				sLTaskResource.Fee = Convert.ToDecimal(sLTaskResource.Quantity * sLTaskResource.UnitPrice);
			}
			catch
			{
				sLTaskResource.Fee = 0m;
			}
			try
			{
				sLTaskResource.Fee1 = Convert.ToDecimal(this.TxtFee1.Text.Trim());
			}
			catch
			{
				sLTaskResource.Fee1 = 0m;
			}
			return sLTaskResource;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			SLTaskResource data = this.GetData();
			if (TaskResourceAction.UpdResource(data))
			{
				this.JS.Text = "alert('更新成功！');";
				this.DataGrid_Bind("1,2,3", this.WbsType);
				return;
			}
			this.JS.Text = "alert('更新失败！');";
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			SLTaskResource data = this.GetData();
			if (TaskResourceAction.DelResource(data))
			{
				this.JS.Text = "alert('删除成功！');";
				this.DataGrid_Bind("1,2,3", this.WbsType);
				return;
			}
			this.JS.Text = "alert('删除失败！');";
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			if (TaskResourceAction.InsertResource(this.ProjectCode.ToString(), this.TaskCode, this.WbsType, "8D50B405-D418-4fe4-B204-0F654C79EF2B", base.UserCode))
			{
				this.DataGrid_Bind("1,2,3", this.WbsType);
				return;
			}
			this.JS.Text = "alert('添加失败！');";
		}
	}


