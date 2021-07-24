using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SelectResource : PageBase, IRequiresSessionState
	{
		public string display = "";
		protected Guid ProjectCode
		{
			get
			{
				return (Guid)this.ViewState["PROJECTCODE"];
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		protected string TaskCode
		{
			get
			{
				return this.ViewState["TASKCODE"].ToString();
			}
			set
			{
				this.ViewState["TASKCODE"] = value;
			}
		}
		protected string TaskName
		{
			get
			{
				return this.ViewState["TASKNAME"].ToString();
			}
			set
			{
				this.ViewState["TASKNAME"] = value;
			}
		}
		protected PlanAction plan
		{
			get
			{
				return new PlanAction();
			}
		}
		protected TaskBookAction tba
		{
			get
			{
				return new TaskBookAction();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["tc"] == null || base.Request["bc"] == null)
			{
				this.JS.Text = "alert('参数错误！');window.close()";
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"]);
			this.TaskCode = base.Request["tc"].ToString();
			this.ViewState["TaskBookCode"] = base.Request["bc"].ToString();
			if (!this.Page.IsPostBack)
			{
				this.ViewState["UserCode"] = base.UserCode;
				TaskBookAction.DeleteTempResource(this.ViewState["UserCode"].ToString());
				DataTable dataTable = this.tba.TaskBookResourceList(this.ProjectCode.ToString(), this.TaskCode.ToString(), this.ViewState["TaskBookCode"].ToString());
				dataTable.Columns.Add("Amount", typeof(decimal));
				this.Session["TEMPRESOURCE"] = dataTable;
				this.GridBind(dataTable.DefaultView);
			}
			this.btn_Add.Attributes["onclick"] = "if( !pickResource()) { return false;}";
			if (base.Request.QueryString["optype"] != null)
			{
				this.btnSave.Visible = false;
				this.display = "display:none";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdResource.ItemDataBound += new DataGridItemEventHandler(this.DGrdResource_ItemDataBound);
		}
		private void DataToSession()
		{
			DataTable dataTable = (DataTable)this.Session["TEMPRESOURCE"];
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.DGrdResource.Items)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["ResourceCode"] = dataGridItem.Cells[1].Text;
				dataRow["ResourceName"] = dataGridItem.Cells[2].Text;
				dataRow["Specification"] = dataGridItem.Cells[3].Text;
				dataRow["UnitName"] = dataGridItem.Cells[4].Text;
				dataRow["Price"] = ((TextBox)dataGridItem.FindControl("TxtPrice")).Text;
				try
				{
					dataRow["Quantity"] = ((TextBox)dataGridItem.FindControl("TxtAmount")).Text;
				}
				catch
				{
					dataRow["Quantity"] = 0;
				}
				dataRow["ResourceStyle"] = dataGridItem.Cells[7].Text;
				dataTable.Rows.Add(dataRow);
			}
			this.Session["TEMPRESOURCE"] = dataTable;
		}
		private void GridBind(DataView dv)
		{
			this.DGrdResource.DataSource = dv;
			this.DGrdResource.DataBind();
		}
		private void TableUnit()
		{
			DataTable dataTable = (DataTable)this.Session["TEMPRESOURCE"];
			DataTable tempResource = TaskBookAction.GetTempResource(this.ViewState["UserCode"].ToString());
			tempResource.Columns.Add("Amount", typeof(decimal));
			tempResource.Columns.Add("TotalPrice", typeof(decimal));
			if (dataTable.Rows.Count == 0)
			{
				this.Session["TEMPRESOURCE"] = tempResource;
			}
			else
			{
				for (int i = 0; i < tempResource.Rows.Count; i++)
				{
					bool flag = true;
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						if (dataTable.Rows[j]["ResourceCode"].ToString().Trim() == tempResource.Rows[i]["ResourceCode"].ToString().Trim())
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						dataTable.ImportRow(tempResource.Rows[i]);
					}
				}
				this.Session["TEMPRESOURCE"] = dataTable;
			}
			this.GridBind(((DataTable)this.Session["TEMPRESOURCE"]).DefaultView);
		}
		private ArrayList TaskResource()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.DGrdResource.Items.Count; i++)
			{
				arrayList.Add(new TaskBookResource
				{
					ProjectCode = this.ProjectCode,
					TaskCode = this.TaskCode,
					TaskBookCode = this.ViewState["TaskBookCode"].ToString(),
					ResourceCode = this.DGrdResource.Items[i].Cells[1].Text,
					Quantity = Convert.ToDecimal(((TextBox)this.DGrdResource.Items[i].Cells[5].Controls[1]).Text),
					UnitPrice = Convert.ToDouble(((TextBox)this.DGrdResource.Items[i].Cells[6].Controls[1]).Text),
					FactQuantity = 0m,
					ResourceStyle = int.Parse(this.DGrdResource.Items[i].Cells[7].Text.Trim())
				});
			}
			return arrayList;
		}
		private void DGrdResource_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = "OnRecord(this);clickRow('" + e.Item.Cells[0].Text + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				if (((TextBox)e.Item.Cells[5].Controls[1]).Text.Trim() == "")
				{
					((TextBox)e.Item.Cells[5].Controls[1]).Text = "0";
				}
				if (((TextBox)e.Item.Cells[6].Controls[1]).Text.Trim() == "")
				{
					((TextBox)e.Item.Cells[6].Controls[1]).Text = "0";
				}
				((TextBox)e.Item.Cells[5].Controls[1]).Attributes["onblur"] = "checkDecimal(this);";
				((TextBox)e.Item.Cells[6].Controls[1]).Attributes["onblur"] = "checkDecimal(this);";
				e.Item.Cells[7].Text = TaskBookAction.GetResourceStyle(e.Item.Cells[1].Text.Trim());
				TextBox textBox = (TextBox)e.Item.FindControl("TxtAmount");
				TextBox textBox2 = (TextBox)e.Item.FindControl("TxtPrice");
				TextBox textBox3 = (TextBox)e.Item.FindControl("TxtTotal");
				textBox3.Text = Convert.ToString(decimal.Round(decimal.Parse(textBox.Text) * decimal.Parse(textBox2.Text), 2));
				textBox.Attributes.Add("onblur", string.Concat(new string[]
				{
					"checkDecimal(this);getMoney('",
					textBox.ClientID,
					"','",
					textBox2.ClientID,
					"','",
					textBox3.ClientID,
					"');test(this);"
				}));
				textBox2.Attributes.Add("onblur", string.Concat(new string[]
				{
					"checkDecimal(this);getMoney('",
					textBox.ClientID,
					"','",
					textBox2.ClientID,
					"','",
					textBox3.ClientID,
					"');test(this);"
				}));
			}
		}
		protected void btn_Add_Click(object sender, EventArgs e)
		{
			this.DataToSession();
			this.TableUnit();
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			DataTable dataTable = (DataTable)this.Session["TEMPRESOURCE"];
			int num = int.Parse(this.HdnId.Value);
			if (num > 0)
			{
				dataTable.DefaultView.Delete(num - 1);
			}
			this.Session["TEMPRESOURCE"] = dataTable;
			this.GridBind(dataTable.DefaultView);
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			string[] value = new string[]
			{
				ConfigurationSettings.AppSettings["Artificial consumption"],
				ConfigurationSettings.AppSettings["Material consumption"],
				ConfigurationSettings.AppSettings["Equipment consumption"]
			};
			this.Session.Add("AlertTypes", value);
			string str;
			switch (this.tba.AddTaskBookResource(this.TaskResource(), this.ProjectCode, this.TaskCode, this.ViewState["TaskBookCode"].ToString(), out str))
			{
			case 0:
				this.JS.Text = "alert('保存失败！');ReturnValue=false;";
				break;
			case 1:
				this.Session.Remove("TEMPRESOURCE");
				this.JS.Text = "alert('保存存功！');ReturnValue=true;window.close();";
				break;
			case 2:
				this.Session.Remove("TEMPRESOURCE");
				this.JS.Text = "alert('" + str + ",保存失败！');ReturnValue=false;";
				break;
			}
			this.Session.Remove("AlertTypes");
		}
		protected void HdnProjectCode_ServerChange(object sender, EventArgs e)
		{
		}
	}


