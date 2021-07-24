using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ResourceSelect : PageBase, IRequiresSessionState
	{
		protected new string UserCode
		{
			get
			{
				return this.Session["yhdm"].ToString();
			}
		}
		protected Guid OpSession
		{
			get
			{
				object obj = this.ViewState["OPSESSION"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["OPSESSION"] = value;
			}
		}
		protected string CategoryCode
		{
			get
			{
				object obj = this.ViewState["CATEGORYCODE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["CATEGORYCODE"] = value;
			}
		}
		protected static ResourceItemAction ResItemAct
		{
			get
			{
				return new ResourceItemAction();
			}
		}
		public ArrayList ResourceList
		{
			get
			{
				object obj = this.ViewState["RESOURCELIST"];
				if (obj != null)
				{
					return (ArrayList)obj;
				}
				return new ArrayList();
			}
			set
			{
				this.ViewState["RESOURCELIST"] = value;
			}
		}
		public ArrayList TempSourceList
		{
			get
			{
				object obj = this.ViewState["TEMPSOURCELIST"];
				if (obj != null)
				{
					return (ArrayList)obj;
				}
				return new ArrayList();
			}
			set
			{
				this.ViewState["TEMPSOURCELIST"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["ses"] == null && base.Request["cate"] == null)
				{
					this.js.Text = "alert('参数错误！');window.returnValue = true;window.Close();";
					return;
				}
				string arg_55_0 = base.Request["ses"];
				this.OpSession = new Guid("6A1A7050-1F92-4291-B932-43E1DFCE6E92");
				this.CategoryCode = base.Request["cate"];
				this.TempSourceList = new ArrayList();
				this.ResourceList = new ArrayList();
				this.DgdResourceTemp_Bind();
				this.DgdResourceList_Bind(this.CategoryCode, this.TxtCate.Text, this.TxtCode.Text, this.TxtName.Text);
				this.BtnAddResource.Attributes["onclick"] = "if (!checkData()){return false;}";
				this.BtnDelResource.Attributes["onclick"] = "if (!checkData1()){return false;}";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DgdResourceList.ItemDataBound += new DataGridItemEventHandler(this.DgdResourceList_ItemDataBound);
			this.DgdResourceTemp.ItemDataBound += new DataGridItemEventHandler(this.DgdResourceTemp_ItemDataBound);
		}
		private void DgdResourceList_Bind()
		{
			ResourceBasicCollection resourceBasicCollection = ResourceSelect.ResItemAct.QueryResourceBasicList(this.CategoryCode, base.Request["t"].ToString());
			int count = resourceBasicCollection.Count;
			this.ResourceList.Clear();
			for (int i = 0; i < count; i++)
			{
				this.ResourceList.Add(resourceBasicCollection[i].ResourceCode);
			}
			this.DgdResourceList.DataSource = resourceBasicCollection;
			this.DgdResourceList.DataBind();
		}
		private void DgdResourceList_Bind(string cate, string cateName, string code, string name)
		{
			ResourceBasicCollection resourceBasicCollection = ResourceSelect.ResItemAct.QueryResourceBasicList(cate, cateName, code, name, base.Request["t"].ToString());
			int count = resourceBasicCollection.Count;
			this.ResourceList.Clear();
			for (int i = 0; i < count; i++)
			{
				this.ResourceList.Add(resourceBasicCollection[i].ResourceCode);
			}
			this.DgdResourceList.DataSource = resourceBasicCollection;
			this.DgdResourceList.DataBind();
		}
		private void DgdResourceTemp_Bind()
		{
			ResourceBasicCollection resourceBasicCollection = ResourceSelect.ResItemAct.QueryValidTempResourceList(this.OpSession, this.UserCode);
			int count = resourceBasicCollection.Count;
			this.TempSourceList.Clear();
			for (int i = 0; i < count; i++)
			{
				this.TempSourceList.Add(resourceBasicCollection[i].ResourceCode);
			}
			this.DgdResourceTemp.DataSource = resourceBasicCollection;
			this.DgdResourceTemp.DataBind();
		}
		private void DgdResourceList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				ResourceBasicInfo resourceBasicInfo = (ResourceBasicInfo)e.Item.DataItem;
				int count = this.TempSourceList.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.TempSourceList[i].ToString() == resourceBasicInfo.ResourceCode)
					{
						CheckBox checkBox = new CheckBox();
						checkBox = (CheckBox)e.Item.Cells[0].Controls[1];
						checkBox.Enabled = false;
						checkBox.ForeColor = Color.Red;
					}
				}
				return;
			}
			default:
				return;
			}
		}
		protected void BtnAddResource_Click(object sender, EventArgs e)
		{
			ArrayList arrayList = new ArrayList();
			int count = this.ResourceList.Count;
			int count2 = this.TempSourceList.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				CheckBox checkBox = new CheckBox();
				checkBox = (CheckBox)this.DgdResourceList.Items[i].Cells[0].Controls[1];
				if (checkBox.Enabled && checkBox.Checked)
				{
					for (int j = 0; j < count2; j++)
					{
						if (this.TempSourceList[j] == this.ResourceList[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						arrayList.Add(this.ResourceList[i].ToString());
					}
				}
			}
			if (arrayList.Count > 0)
			{
				ResourceSelect.ResItemAct.AddNewResource(this.OpSession, this.UserCode, arrayList);
				this.js.Text = "parent.document.getElementById('HdnOperate').value = 1 ";
				this.DgdResourceTemp_Bind();
				this.DgdResourceList_Bind(this.CategoryCode, this.TxtCate.Text, this.TxtCode.Text, this.TxtName.Text);
			}
		}
		protected void BtnDelResource_Click(object sender, EventArgs e)
		{
			ArrayList arrayList = new ArrayList();
			int count = this.DgdResourceTemp.Items.Count;
			for (int i = 0; i < count; i++)
			{
				CheckBox checkBox = new CheckBox();
				checkBox = (CheckBox)this.DgdResourceTemp.Items[i].Cells[0].Controls[1];
				if (checkBox.Checked)
				{
					arrayList.Add(this.TempSourceList[i]);
				}
			}
			if (arrayList.Count > 0)
			{
				ResourceSelect.ResItemAct.DelSelectedResource(this.OpSession, this.UserCode, arrayList);
				this.js.Text = "parent.document.getElementById('HdnOperate').value = 1 ";
				this.DgdResourceTemp_Bind();
				this.DgdResourceList_Bind(this.CategoryCode, this.TxtCate.Text, this.TxtCode.Text, this.TxtName.Text);
			}
		}
		private void DgdResourceTemp_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				return;
			default:
				return;
			}
		}
		protected void BtnSearch_Click(object sender, EventArgs e)
		{
			this.DgdResourceTemp_Bind();
			this.DgdResourceList_Bind(this.CategoryCode, this.TxtCate.Text, this.TxtCode.Text, this.TxtName.Text);
		}
	}


