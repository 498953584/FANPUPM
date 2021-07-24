using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class UTaskRelation : System.Web.UI.UserControl
	{
		private Page _Owner;

		protected SchedulePlanAction ScheduleAct
		{
			get
			{
				return new SchedulePlanAction();
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
				return Guid.NewGuid();
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
				object obj = this.ViewState["TASKCODE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["TASKCODE"] = value;
			}
		}
		public Page Owner
		{
			get
			{
				return this._Owner;
			}
			set
			{
				this._Owner = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdMarket.ItemDataBound += new DataGridItemEventHandler(this.DGrdMarket_ItemDataBound);
		}
		public void Bind(string flag)
		{
			if (flag == "View")
			{
				this.BtnAdd.Visible = false;
				this.BtnEdit.Visible = false;
				this.BtnDel.Visible = false;
			}
			TaskRelationCollection taskRelation = this.ScheduleAct.GetTaskRelation(this.ProjectCode, this.TaskCode);
			this.DGrdTender_Bind(taskRelation);
		}
		private void DGrdTender_Bind(TaskRelationCollection tc)
		{
			this.DGrdMarket.DataSource = tc;
			base.Session["SESTENDER"] = tc;
			this.DGrdMarket.DataBind();
		}
		private void DGrdMarket_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				TaskRelation taskRelation = (TaskRelation)e.Item.DataItem;
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick(this,'",
					this.DGrdMarket.ClientID,
					"');clickTenderRow('",
					e.Item.ItemIndex,
					"');"
				});
				try
				{
					((Label)e.Item.Cells[2].FindControl("LblTaskName")).Text = this.ScheduleAct.GetTaskName(this.ProjectCode, taskRelation.BeginTaskCode);
					switch (taskRelation.Relationship)
					{
					case 0:
						((Label)e.Item.Cells[0].FindControl("LblRelation")).Text = "完成完成[FF]";
						break;
					case 1:
						((Label)e.Item.Cells[0].FindControl("LblRelation")).Text = "完成开始[FS]";
						break;
					case 2:
						((Label)e.Item.Cells[0].FindControl("LblRelation")).Text = "开始完成[SF]";
						break;
					case 3:
						((Label)e.Item.Cells[0].FindControl("LblRelation")).Text = "开始开始[SS]";
						break;
					}
					return;
				}
				catch
				{
					return;
				}
				break;
			}
			case ListItemType.SelectedItem:
				return;
			case ListItemType.EditItem:
				break;
			default:
				return;
			}
			TaskRelation taskRelation2 = (TaskRelation)e.Item.DataItem;
			e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
			e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			e.Item.Attributes["onclick"] = string.Concat(new object[]
			{
				"doClick(this,'",
				this.DGrdMarket.ClientID,
				"');clickTenderRow('",
				e.Item.ItemIndex,
				"');"
			});
			DropDownList dropDownList = (DropDownList)e.Item.Cells[0].FindControl("DDLRelation");
			try
			{
				if (dropDownList.Items.Count > 0)
				{
					dropDownList.SelectedValue = taskRelation2.Relationship.ToString();
				}
				((TextBox)e.Item.Cells[2].FindControl("TxtTaskName")).Text = this.ScheduleAct.GetTaskName(this.ProjectCode, taskRelation2.BeginTaskCode);
			}
			catch
			{
			}
		}
		private TaskRelationCollection DGrdMarket_GetData()
		{
			TaskRelationCollection taskRelationCollection = (TaskRelationCollection)base.Session["SESTENDER"];
			for (int i = this.DGrdMarket.Items.Count - 1; i >= 0; i--)
			{
				DataGridItem dataGridItem = this.DGrdMarket.Items[i];
				switch (dataGridItem.ItemType)
				{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					if (((Label)dataGridItem.Cells[1].FindControl("LblBeginTaskCode")).Text.ToString() != "")
					{
						taskRelationCollection[i].ProjectCode = this.ProjectCode;
						taskRelationCollection[i].EndTaskCode = this.TaskCode;
						taskRelationCollection[i].BeginTaskCode = ((Label)dataGridItem.Cells[1].FindControl("LblBeginTaskCode")).Text;
						string text = ((Label)dataGridItem.Cells[0].FindControl("LblRelation")).Text;
						int relationship = 0;
						string a;
						if ((a = text) != null)
						{
							if (!(a == "完成开始[FS]"))
							{
								if (!(a == "完成完成[FF]"))
								{
									if (!(a == "开始完成[SF]"))
									{
										if (a == "开始开始[SS]")
										{
											relationship = 3;
										}
									}
									else
									{
										relationship = 2;
									}
								}
								else
								{
									relationship = 0;
								}
							}
							else
							{
								relationship = 1;
							}
						}
						taskRelationCollection[i].Relationship = relationship;
						taskRelationCollection[i].WaitDay = Convert.ToInt32((((Label)dataGridItem.Cells[3].FindControl("LblWaitDay")).Text == "") ? "0" : ((Label)dataGridItem.Cells[3].FindControl("LblWaitDay")).Text);
					}
					break;
				case ListItemType.EditItem:
					if (((TextBox)dataGridItem.Cells[1].FindControl("TxtBeginTaskCode")).Text.ToString() != "")
					{
						taskRelationCollection[i].ProjectCode = this.ProjectCode;
						taskRelationCollection[i].EndTaskCode = this.TaskCode;
						taskRelationCollection[i].BeginTaskCode = ((TextBox)dataGridItem.Cells[1].FindControl("TxtBeginTaskCode")).Text;
						taskRelationCollection[i].Relationship = Convert.ToInt32(((DropDownList)dataGridItem.Cells[0].FindControl("DDLRelation")).SelectedValue);
						taskRelationCollection[i].WaitDay = Convert.ToInt32((((TextBox)dataGridItem.Cells[3].FindControl("TxtWaitDay")).Text == "") ? "0" : ((TextBox)dataGridItem.Cells[3].FindControl("TxtWaitDay")).Text);
					}
					break;
				}
			}
			return taskRelationCollection;
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			TaskRelationCollection taskRelationCollection = this.DGrdMarket_GetData();
			taskRelationCollection.Insert(0, new TaskRelation
			{
				ProjectCode = this.ProjectCode,
				EndTaskCode = this.TaskCode,
				BeginTaskCode = string.Empty,
				WaitDay = 0,
				Relationship = 1
			});
			this.DGrdMarket.EditItemIndex = 0;
			this.DGrdTender_Bind(taskRelationCollection);
		}
		protected void BtnEdit_Click(object sender, EventArgs e)
		{
			this.DGrdMarket.EditItemIndex = int.Parse(this.HdnSelectedIndex.Value);
			TaskRelationCollection tc = this.DGrdMarket_GetData();
			this.DGrdTender_Bind(tc);
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			this.DGrdMarket.EditItemIndex = -1;
			TaskRelationCollection taskRelationCollection = this.DGrdMarket_GetData();
			taskRelationCollection.RemoveAt(int.Parse(this.HdnSelectedIndex.Value));
			this.DGrdTender_Bind(taskRelationCollection);
		}
		public int Update()
		{
			TaskRelationCollection tc = this.DGrdMarket_GetData();
			if (this.ScheduleAct.UpdateBeforeTask(tc, this.ProjectCode, this.TaskCode) == 1)
			{
				return 1;
			}
			return 0;
		}
	}


