using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DocDispense : BasePage, IRequiresSessionState
	{
		public Guid FileID
		{
			get
			{
				object obj = this.ViewState["FileID"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["FileID"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["fid"] != "")
				{
					this.FileID = new Guid(base.Request["fid"]);
				}
				else
				{
					this.FileID = Guid.Empty;
				}
				this.dgCorpCode_bind();
			}
		}
		protected void dgCorpCode_bind()
		{
			DataTable dataSource = publicDbOpClass.DataTableQuary(" select * from pt_d_corpCode order by corpCode asc ");
			this.dgCorpCode.DataSource = dataSource;
			this.dgCorpCode.DataBind();
		}
		protected void dgCorpCode_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView arg_2D_0 = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				return;
			}
			default:
				return;
			}
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			int count = this.dgCorpCode.Items.Count;
			if (count > 0)
			{
				string[] array = new string[count];
				int num = 0;
				string text = " ";
				foreach (DataGridItem dataGridItem in this.dgCorpCode.Items)
				{
					CheckBox checkBox = (CheckBox)dataGridItem.FindControl("CheckBox1");
					if (checkBox.Checked)
					{
						array[num] = dataGridItem.Cells[1].Text.ToString();
						text += " Insert Into OA_Document_ReceiveFile ";
						text = text + " select newid(),'" + array[num] + "',userCode,RecordDate,'1',(select corpName from pt_d_corpCode where corpCode= (select corpCode from OA_Document_SendFile ";
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							" where FileID = '",
							this.FileID.ToString(),
							"'and CorpCode <> '",
							array[num],
							"')),Title,getdate(),Remark,OriginalName,FilePath,IsPigeonhole from OA_Document_SendFile "
						});
						string text3 = text;
						text = string.Concat(new string[]
						{
							text3,
							" where fileID = '",
							this.FileID.ToString(),
							"' and CorpCode <> '",
							array[num],
							"' "
						});
					}
					num++;
				}
				if (publicDbOpClass.NonQuerySqlString(text))
				{
					this.JS.Text = "alert('文件发送成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('文件发送失败！');";
			}
		}
		protected void dgCorpCode_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
	}


