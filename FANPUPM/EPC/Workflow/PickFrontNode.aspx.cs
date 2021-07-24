using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class PickFrontNode : BasePage, IRequiresSessionState
	{
		public int TemplateID
		{
			get
			{
				object obj = this.ViewState["TemplateID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TemplateID"] = value;
			}
		}
		public string FrontNode
		{
			get
			{
				object obj = this.ViewState["FrontNode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["FrontNode"] = value;
			}
		}
		public int xpos
		{
			get
			{
				object obj = this.ViewState["xpos"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["xpos"] = value;
			}
		}
		public int ypos
		{
			get
			{
				object obj = this.ViewState["ypos"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["ypos"] = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = System.Convert.ToInt32(base.Request["ti"]);
				this.FrontNode = base.Request["fn"];
				this.LbAll_Bound(this.FrontNode);
			}
		}
		private void LbAll_Bound(string frontNodeStr)
		{
			bool flag = FlowChartAction.IsPoolNode(this.TemplateID);
			if (flag)
			{
				int num = 0;
				int i = frontNodeStr.IndexOf(",");
				if (i > 0)
				{
					string value;
					object value2;
					while (i > 0)
					{
						value = frontNodeStr.Substring(0, i);
						value2 = FlowTemplateAction.QueryNodeName(System.Convert.ToInt32(value));
						ListItem item = new ListItem(System.Convert.ToString(value2), value);
						this.LbAll.Items.Insert(num, item);
						frontNodeStr = frontNodeStr.Substring(i + 1, frontNodeStr.Length - i - 1);
						i = frontNodeStr.IndexOf(",");
						num++;
					}
					value = frontNodeStr;
					value2 = FlowTemplateAction.QueryNodeName(System.Convert.ToInt32(value));
					ListItem item2 = new ListItem(System.Convert.ToString(value2), value);
					this.LbAll.Items.Insert(num, item2);
				}
				else
				{
					object value2 = FlowTemplateAction.QueryNodeName(System.Convert.ToInt32(frontNodeStr));
					ListItem item3 = new ListItem(System.Convert.ToString(value2), frontNodeStr);
					this.LbAll.Items.Insert(num, item3);
				}
				this.LoadNoPostPosition(num + 1);
				return;
			}
			if (this.LoadNoPostPosition(0) == 0)
			{
				object value2 = FlowTemplateAction.QueryNodeName(System.Convert.ToInt32(frontNodeStr));
				ListItem item4 = new ListItem(System.Convert.ToString(value2), frontNodeStr);
				this.LbAll.Items.Insert(0, item4);
			}
		}
		protected int LoadNoPostPosition(int h)
		{
			DataTable dataTable = FlowTemplateAction.QueryFlowChart(this.TemplateID);
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i += 2)
				{
					int j = dataTable.Columns.Count - 2;
					while (j > 2)
					{
						if (dataTable.Rows[i].ItemArray[j] != null && dataTable.Rows[i].ItemArray[j].ToString() != "")
						{
							int num = 0;
							string a = "";
							string text = "";
							string value = "";
							string text2 = dataTable.Rows[i].ItemArray[j].ToString();
							int k = text2.IndexOf(";");
							if (k <= 0)
							{
								break;
							}
							while (k > 0)
							{
								switch (num)
								{
								case 1:
									a = text2.Substring(0, k);
									value = string.Concat(new string[]
									{
										(j - 1).ToString(),
										";",
										(i + 1).ToString(),
										";",
										text2.Substring(0, k)
									});
									break;
								case 2:
									text = text2.Substring(0, k);
									break;
								}
								text2 = text2.Substring(k + 1, text2.Length - k - 1);
								k = text2.IndexOf(";");
								num++;
							}
							if (!(a == "00"))
							{
								ListItem item = new ListItem(text, value);
								this.LbAll.Items.Insert(h, item);
								h++;
								break;
							}
							break;
						}
						else
						{
							j--;
						}
					}
				}
			}
			return h;
		}
		protected void BtnAdd_Click(object sender, System.EventArgs e)
		{
			int count = this.LbAll.Items.Count;
			ListItem[] array = new ListItem[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new ListItem();
				array[i].Text = this.LbAll.Items[i].ToString();
				array[i].Value = this.LbAll.Items[i].Value.ToString();
			}
			this.LbCur.Items.AddRange(array);
			this.LbAll.Items.Clear();
		}
		protected void BtnDel_Click(object sender, System.EventArgs e)
		{
			int count = this.LbCur.Items.Count;
			ListItem[] array = new ListItem[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new ListItem();
				array[i].Text = this.LbCur.Items[i].ToString();
				array[i].Value = this.LbCur.Items[i].Value.ToString();
			}
			this.LbAll.Items.AddRange(array);
			this.LbCur.Items.Clear();
		}
	}


