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
	public partial class CostSubjectsSetting : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindData();
			}
		}
		private void BindData()
		{
			CostSubjectCollection costSubjectInfos = CostSubjectAction.GetCostSubjectInfos();
			this.dgSubject.DataSource = costSubjectInfos;
			this.dgSubject.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgSubject.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgSubject_ItemDataBound);
		}
		public void CreateNewItem(object sender, EventArgs e)
		{
			CostSubjectCollection dataFromGrid = this.GetDataFromGrid();
			CostSubjectCollection costSubjectCollection = new CostSubjectCollection();
			for (int i = 0; i < dataFromGrid.Count; i++)
			{
				dataFromGrid[i].IsEdit = false;
				if (i != 0 && dataFromGrid[i].FirstNum != dataFromGrid[i - 1].FirstNum && dataFromGrid[i - 1].FirstNum == int.Parse(this.hidFirst.Value))
				{
					CostSubjectInfo newItem = CostSubjectAction.GetNewItem(dataFromGrid[i - 1].FirstNum);
					this.dgSubject.EditItemIndex = i;
					costSubjectCollection.Add(newItem);
				}
				costSubjectCollection.Add(dataFromGrid[i]);
				if (i == dataFromGrid.Count - 1 && dataFromGrid[i].FirstNum == int.Parse(this.hidFirst.Value))
				{
					CostSubjectInfo newItem2 = CostSubjectAction.GetNewItem(dataFromGrid[i].FirstNum);
					this.dgSubject.EditItemIndex = i + 1;
					costSubjectCollection.Add(newItem2);
				}
			}
			this.dgSubject.DataSource = costSubjectCollection;
			this.dgSubject.DataBind();
		}
		public void DelItem(object sender, EventArgs e)
		{
			CostSubjectCollection dataFromGrid = this.GetDataFromGrid();
			int i = 0;
			while (i < dataFromGrid.Count)
			{
				if (dataFromGrid[i].SubjectID == int.Parse(this.hidSubjectID.Value))
				{
					dataFromGrid[i].IsValid = false;
					if (CostSubjectAction.UpdateSubjectInfo(dataFromGrid[i]))
					{
						this.js.Text = "alert(\"操作成功！\");";
						this.dgSubject.EditItemIndex = -1;
						break;
					}
					this.js.Text = "alert(\"操作失败！\");";
					break;
				}
				else
				{
					i++;
				}
			}
			this.BindData();
		}
		public void EidtItem(object sender, EventArgs e)
		{
			CostSubjectCollection dataFromGrid = this.GetDataFromGrid();
			for (int i = 0; i < dataFromGrid.Count; i++)
			{
				if (dataFromGrid[i].SubjectID == int.Parse(this.hidSubjectID.Value))
				{
					dataFromGrid[i].IsEdit = true;
					this.dgSubject.EditItemIndex = i;
					break;
				}
				dataFromGrid[i].IsEdit = false;
			}
			this.dgSubject.DataSource = dataFromGrid;
			this.dgSubject.DataBind();
		}
		public void SaveItem(object sender, EventArgs e)
		{
			CostSubjectCollection dataFromGrid = this.GetDataFromGrid();
			int i = 0;
			while (i < dataFromGrid.Count)
			{
				if (dataFromGrid[i].SubjectID == int.Parse(this.hidSubjectID.Value))
				{
					if (CostSubjectAction.UpdateSubjectInfo(dataFromGrid[i]))
					{
						this.js.Text = "alert(\"操作成功！\");";
						this.dgSubject.EditItemIndex = -1;
						break;
					}
					this.js.Text = "alert(\"操作失败！\");";
					break;
				}
				else
				{
					i++;
				}
			}
			this.BindData();
		}
		private CostSubjectCollection GetDataFromGrid()
		{
			CostSubjectCollection costSubjectCollection = new CostSubjectCollection();
			foreach (System.Web.UI.WebControls.DataGridItem dataGridItem in this.dgSubject.Items)
			{
				CostSubjectInfo costSubjectInfo = new CostSubjectInfo();
				costSubjectInfo.SubjectID = int.Parse(this.dgSubject.DataKeys[dataGridItem.ItemIndex].ToString());
				costSubjectInfo.FirstNum = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dataGridItem.FindControl("hidFirstNum")).Value);
				costSubjectInfo.SecNum = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)dataGridItem.FindControl("hidSecNum")).Value);
				costSubjectInfo.IsHaveChild = (((System.Web.UI.HtmlControls.HtmlInputHidden)dataGridItem.FindControl("hidIsHaveChild")).Value == "True");
				if (((System.Web.UI.HtmlControls.HtmlInputHidden)dataGridItem.FindControl("hidIsEdit")).Value == "True")
				{
					costSubjectInfo.SubjectName = ((System.Web.UI.WebControls.TextBox)dataGridItem.FindControl("txtSubjectName")).Text;
				}
				else
				{
					costSubjectInfo.SubjectName = ((System.Web.UI.WebControls.Label)dataGridItem.FindControl("lbSubjectName")).Text;
				}
				costSubjectCollection.Add(costSubjectInfo);
			}
			return costSubjectCollection;
		}
		private void dgSubject_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn1")).Visible = false;
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn2")).Visible = false;
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn3")).Visible = false;
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn4")).Visible = false;
				int num = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidSecNum")).Value);
				int num2 = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidFirstNum")).Value);
				bool flag = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidIsEdit")).Value == "True";
				bool flag2 = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidIsHaveChild")).Value == "True";
				if (num != 0)
				{
					e.Item.Cells[0].Text = num2.ToString() + "." + num.ToString();
					((System.Web.UI.WebControls.Button)e.Item.FindControl("btn2")).Visible = true;
					if (flag)
					{
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn4")).Visible = true;
					}
					else
					{
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn3")).Visible = true;
					}
				}
				else
				{
					if (flag2 && !flag)
					{
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn1")).Visible = true;
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn3")).Visible = true;
					}
					if (flag2 && flag)
					{
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn4")).Visible = true;
					}
					if (!flag2 && !flag)
					{
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn1")).Visible = true;
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn2")).Visible = true;
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn3")).Visible = true;
					}
					if (!flag2 && flag)
					{
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn4")).Visible = true;
						((System.Web.UI.WebControls.Button)e.Item.FindControl("btn2")).Visible = true;
					}
					e.Item.Cells[0].Text = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidFirstNum")).Value;
					e.Item.Style.Add("font-weight", "bold");
				}
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgSubject.ClientID + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn1")).Attributes["onclick"] = string.Concat(new string[]
				{
					" ClickRow(this,'",
					this.dgSubject.ClientID,
					"','",
					this.dgSubject.DataKeys[e.Item.ItemIndex].ToString(),
					"',",
					num2.ToString(),
					");"
				});
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn2")).Attributes["onclick"] = string.Concat(new string[]
				{
					"return DelRow(this,'",
					this.dgSubject.ClientID,
					"','",
					this.dgSubject.DataKeys[e.Item.ItemIndex].ToString(),
					"',",
					num2.ToString(),
					");"
				});
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn3")).Attributes["onclick"] = string.Concat(new string[]
				{
					"return ClickRow(this,'",
					this.dgSubject.ClientID,
					"','",
					this.dgSubject.DataKeys[e.Item.ItemIndex].ToString(),
					"',",
					num2.ToString(),
					");"
				});
				((System.Web.UI.WebControls.Button)e.Item.FindControl("btn4")).Attributes["onclick"] = string.Concat(new string[]
				{
					"return ClickRow(this,'",
					this.dgSubject.ClientID,
					"','",
					this.dgSubject.DataKeys[e.Item.ItemIndex].ToString(),
					"',",
					num2.ToString(),
					");"
				});
			}
		}
		protected void btnNew_Click(object sender, EventArgs e)
		{
			CostSubjectCollection dataFromGrid = this.GetDataFromGrid();
			dataFromGrid.Add(CostSubjectAction.GetFistNewItem());
			this.dgSubject.EditItemIndex = dataFromGrid.Count - 1;
			this.dgSubject.DataSource = dataFromGrid;
			this.dgSubject.DataBind();
		}
	}


