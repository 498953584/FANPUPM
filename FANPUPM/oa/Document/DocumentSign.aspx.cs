using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DocumentSign : BasePage, IRequiresSessionState
	{

		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = Convert.ToString(this.Session["yhdm"]);
				this.dgSign_Bind();
				this.btnSign.Attributes["onclick"] = " return confirm('确定签收所选记录数据吗？');";
				this.btnView.Attributes["onclick"] = "openClassEdit('','" + this.UserCode + "')";
			}
		}
		protected void dgSign_Bind()
		{
			this.dgSign.DataSource = DocumentAction.QuerySignRecordInfo(this.UserCode);
			this.dgSign.DataBind();
		}
		protected void dgSign_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);doClickRow('",
					dataRowView["DocumentType"].ToString(),
					"','",
					dataRowView["FileID"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				return;
			}
			default:
				return;
			}
		}
		protected void btnSign_Click(object sender, EventArgs e)
		{
			int count = this.dgSign.Items.Count;
			if (count > 0)
			{
				int[] array = new int[count];
				int num = 0;
				string text = " ";
				foreach (DataGridItem dataGridItem in this.dgSign.Items)
				{
					CheckBox checkBox = (CheckBox)dataGridItem.FindControl("CheckBox1");
					if (checkBox.Checked)
					{
						array[num] = Convert.ToInt32(dataGridItem.Cells[2].Text.ToString());
						text = text + " Update OA_Document_SignIn Set SignDate = getDate() where RecordID = " + array[num].ToString();
					}
					num++;
				}
				if (publicDbOpClass.NonQuerySqlString(text))
				{
					this.JS.Text = "alert('文件签收成功！');";
				}
				else
				{
					this.JS.Text = "alert('文件签收失败！');";
				}
				this.dgSign_Bind();
			}
		}
	}


