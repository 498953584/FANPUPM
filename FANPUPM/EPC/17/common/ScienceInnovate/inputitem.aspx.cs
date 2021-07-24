using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InputItem : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["MainId"] != null)
				{
					this.hidMainId.Value = base.Request.Params["MainId"].ToString();
					if (base.Request.Params["Type"] != null)
					{
						this.hidType.Value = base.Request.Params["Type"].ToString();
					}
					this.SetControlByType();
					this.BindData();
					return;
				}
				this.SetControlDisabled();
			}
		}
		private void SetControlDisabled()
		{
			this.btnDel.Enabled = false;
			this.btnSave.Enabled = false;
			this.js.Text = "disPlayTable();";
		}
		private void SetControlByType()
		{
			this.btnDel.Attributes["onclick"] = "return doCheck();";
			if (this.hidType.Value != "Edit")
			{
				this.btnSave.Visible = false;
				this.btnDel.Visible = false;
			}
		}
		private void BindData()
		{
			this.hidChildID.Value = "";
			DevelopmentInputAction developmentInputAction = new DevelopmentInputAction();
			this.dgList.DataSource = developmentInputAction.GetItemInfos(int.Parse(this.hidMainId.Value));
			this.dgList.DataBind();
			this.hidgridRowCount.Value = this.dgList.Items.Count.ToString();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgList.ItemDataBound += new DataGridItemEventHandler(this.dgList_ItemDataBound);
		}
		private InputItemCollectin GetDataFromGrid()
		{
			InputItemCollectin inputItemCollectin = new InputItemCollectin();
			foreach (DataGridItem dataGridItem in this.dgList.Items)
			{
				inputItemCollectin.Add(new InputItemInfo
				{
					ChildMainId = int.Parse(this.dgList.DataKeys[dataGridItem.ItemIndex].ToString()),
					MainID = int.Parse(this.hidMainId.Value),
					Remark = ((TextBox)dataGridItem.FindControl("txtRemark") == null) ? "" : ((TextBox)dataGridItem.FindControl("txtRemark")).Text,
					DevotionMoney = decimal.Parse(((TextBox)dataGridItem.FindControl("txtDevotionMoney") == null) ? "0" : ((TextBox)dataGridItem.FindControl("txtDevotionMoney")).Text),
					SubjectID = int.Parse(((HtmlInputHidden)dataGridItem.FindControl("SubjectId")).Value)
				});
			}
			return inputItemCollectin;
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (DevelopmentInputAction.SaveItems(this.GetDataFromGrid()))
			{
				this.js.Text = "alert(\"操作成功！\");";
				this.BindData();
				return;
			}
			this.js.Text = "alert(\"操作失败！\");";
		}
		private void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				((TextBox)e.Item.FindControl("txtDevotionMoney")).Attributes["onblur"] = "SumAll(this);";
				if (this.hidType.Value != "Edit")
				{
					((TextBox)e.Item.FindControl("txtDevotionMoney")).Enabled = false;
					((TextBox)e.Item.FindControl("txtRemark")).Enabled = false;
				}
				int num = int.Parse(((HtmlInputHidden)e.Item.FindControl("secNum")).Value);
				int num2 = int.Parse(((HtmlInputHidden)e.Item.FindControl("firstNum")).Value);
				if (num == 0)
				{
					e.Item.Cells[0].Text = num2.ToString();
					e.Item.Style.Add("font-weight", "bold");
					((TextBox)e.Item.FindControl("txtDevotionMoney")).Visible = false;
					((TextBox)e.Item.FindControl("txtRemark")).Visible = false;
				}
				else
				{
					e.Item.Cells[0].Text = num2.ToString() + "." + num.ToString();
				}
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow(this,'",
					this.dgList.ClientID,
					"','",
					this.dgList.DataKeys[e.Item.ItemIndex].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			if (DevelopmentInputAction.DelInputItem(int.Parse(this.hidChildID.Value)))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
	}


